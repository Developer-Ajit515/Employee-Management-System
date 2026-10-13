using System.Net;
using System.Text.Json;
using ERP.APPLICATIONBUSINESS.Exceptions;

namespace ERP.APIMain.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = ex.InnerException?.Message ?? ex.Message
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
    }
}
