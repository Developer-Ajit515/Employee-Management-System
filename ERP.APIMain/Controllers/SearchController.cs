
using ERP.DOMAINMODELS.Dtos;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace ERP.APIMain.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Search(
            [FromQuery] string? query,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Ok(new List<SearchResultDto>());
            }

            query = query.Trim();

            var results = new List<SearchResultDto>();

            // Discover all mapped entities that have a primary key.
            var entityTypes = _context.Model.GetEntityTypes()
     .Where(e => !e.IsOwned())
     .Where(e => !e.ClrType.IsAbstract)
     .Where(e => e.FindPrimaryKey() != null)
     .OrderBy(e => e.ClrType.Name)
     .ToList();

            // Find the generic search method once.
            var searchMethod = GetType().GetMethod(
                nameof(SearchEntityAsync),
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            if (searchMethod == null)
            {
                return StatusCode(
                    500,
                    new { message = "Search method could not be found." }
                );
            }

            foreach (var entityType in entityTypes)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (results.Count >= 50)
                    break;

                // Search only string properties belonging to this entity.
                var stringProperties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string))
                    .ToList();

                if (stringProperties.Count == 0)
                    continue;

                try
                {
                    var genericMethod = searchMethod.MakeGenericMethod(
                        entityType.ClrType
                    );

                    // IMPORTANT:
                    // SearchEntityAsync has exactly two parameters.
                    // Pass exactly two arguments here.
                    var taskObject = genericMethod.Invoke(
                        this,
                        new object[]
                        {
                            query,
                            entityType,
                            cancellationToken
                        }
                    );

                    if (taskObject is not Task<List<SearchResultDto>> task)
                    {
                        continue;
                    }

                    var entityResults = await task;

                    results.AddRange(
                        entityResults.Take(50 - results.Count)
                    );
                }
                catch (TargetInvocationException ex)
                {
                    // Unwrap reflection exceptions for useful diagnostics.
                    var actualException = ex.InnerException ?? ex;

                    // Log the error without exposing database details to clients.
                    Console.Error.WriteLine(
                        $"Search failed for {entityType.ClrType.Name}: " +
                        actualException.Message
                    );

                    // Continue searching other entity types.
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine(
                        $"Search failed for {entityType.ClrType.Name}: " +
                        ex.Message
                    );
                }
            }

            results = results
    .OrderBy(r =>
        string.Equals(
            r.Title,
            query,
            StringComparison.OrdinalIgnoreCase
        ) ? 0 :
        r.Title.StartsWith(
            query,
            StringComparison.OrdinalIgnoreCase
        ) ? 1 :
        r.Description.Contains(
            query,
            StringComparison.OrdinalIgnoreCase
        ) ? 2 : 3)
    .Take(50)
    .ToList();

            return Ok(results);

            
        }

        private async Task<List<SearchResultDto>> SearchEntityAsync<TEntity>(
            string query,
            IEntityType entityType,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            var results = new List<SearchResultDto>();

            var stringProperties = entityType.GetProperties()
                .Where(p => p.ClrType == typeof(string))
                .ToList();

            if (stringProperties.Count == 0)
                return results;

            var parameter = Expression.Parameter(typeof(TEntity), "x");

            Expression? predicate = null;

            var containsMethod = typeof(string).GetMethod(
                nameof(string.Contains),
                new[] { typeof(string) }
            )!;

            foreach (var property in stringProperties)
            {
                // EF.Property supports mapped and shadow properties.
                var member = Expression.Call(
                    typeof(EF),
                    nameof(EF.Property),
                    new[] { typeof(string) },
                    parameter,
                    Expression.Constant(property.Name)
                );

                var notNull = Expression.NotEqual(
                    member,
                    Expression.Constant(null, typeof(string))
                );

                var contains = Expression.Call(
                    member,
                    containsMethod,
                    Expression.Constant(query)
                );

                var condition = Expression.AndAlso(notNull, contains);

                predicate = predicate == null
                    ? condition
                    : Expression.OrElse(predicate, condition);
            }

            if (predicate == null)
                return results;

            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                predicate,
                parameter
            );

            // Query the corresponding mapped entity set.
            var entities = await _context.Set<TEntity>()
                .Where(lambda)
                .Take(10)
                .ToListAsync(cancellationToken);

            var primaryKey = entityType.FindPrimaryKey();

            if (primaryKey == null)
                return results;

            // Fields that should be preferred as search-result titles.
            var preferredNames = new[]
            {
                "Name",
                "Title",
                "CompanyName",
                "ProductName",
                "PONumber",
                "ReceiptNumber",
                "InvoiceNumber",
                "OrderNumber",
                "TaskCode",
                "PaymentNumber",
                "EmployeeCode",
                "SupplierCode",
                "Email",
                "Code",
                "Number"
            };

            foreach (var entity in entities)
            {
                var entry = _context.Entry(entity);

                string? GetStringValue(string propertyName)
                {
                    var property = entityType.FindProperty(propertyName);

                    if (property == null ||
                        property.ClrType != typeof(string))
                    {
                        return null;
                    }

                    return entry.Property(propertyName).CurrentValue
                        as string;
                }

                var title = preferredNames
                    .Select(GetStringValue)
                    .FirstOrDefault(value =>
                        !string.IsNullOrWhiteSpace(value));

                title ??= stringProperties
                    .Select(p => GetStringValue(p.Name))
                    .FirstOrDefault(value =>
                        !string.IsNullOrWhiteSpace(value));

                title ??= entityType.ClrType.Name;

                var description = stringProperties
                    .Select(p => GetStringValue(p.Name))
                    .FirstOrDefault(value =>
                        !string.IsNullOrWhiteSpace(value) &&
                        !string.Equals(
                            value,
                            title,
                            StringComparison.OrdinalIgnoreCase
                        ) &&
                        value.Contains(
                            query,
                            StringComparison.OrdinalIgnoreCase
                        ));

                // Existing SearchResultDto.Id is assumed to be int.
                // Use the first primary-key property.
                var keyProperty = primaryKey.Properties[0];

                var keyValue = entry.Property(keyProperty.Name).CurrentValue;

                var id = 0;

                if (keyValue != null)
                {
                    int.TryParse(
                        Convert.ToString(
                            keyValue,
                            System.Globalization.CultureInfo.InvariantCulture
                        ),
                        out id
                    );
                }

                results.Add(new SearchResultDto
                {
                    Type = entityType.ClrType.Name,
                    Id = id,
                    Title = title,
                    Description = description ?? entityType.ClrType.Name
                });
            }

            return results;
        }
    }
}
