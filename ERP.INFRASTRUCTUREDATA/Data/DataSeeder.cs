using ERP.DOMAINMODELS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.INFRASTRUCTUREDATA.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await context.Database.MigrateAsync();

            // Roles
            if (!await context.Roles.AnyAsync())
            {
                var roles = new List<Role>
                {
                    new Role { Name = "Admin" },
                    new Role { Name = "Manager" },
                    new Role { Name = "Employee" }
                };

                await context.Roles.AddRangeAsync(roles);
                await context.SaveChangesAsync();
            }

            // Permissions
            var permissions = new List<Permission>
{
    // Users
    new() { Name = "User.View" },
    new() { Name = "User.Create" },
    new() { Name = "User.Edit" },
    new() { Name = "User.Delete" },

    // Roles
    new() { Name = "Role.View" },
    new() { Name = "Role.Create" },
    new() { Name = "Role.Edit" },
    new() { Name = "Role.Delete" },

    // Employees
    new() { Name = "Employee.View" },
    new() { Name = "Employee.Create" },
    new() { Name = "Employee.Edit" },
    new() { Name = "Employee.Delete" },

    // Products
    new() { Name = "Product.View" },
    new() { Name = "Product.Create" },
    new() { Name = "Product.Edit" },
    new() { Name = "Product.Delete" },

    // Categories
    new() { Name = "Category.View" },
    new() { Name = "Category.Create" },
    new() { Name = "Category.Edit" },
    new() { Name = "Category.Delete" },

    // Projects
    new() { Name = "Project.View" },
    new() { Name = "Project.Create" },
    new() { Name = "Project.Edit" },
    new() { Name = "Project.Delete" },

    // Tasks
    new() { Name = "Task.View" },
    new() { Name = "Task.Create" },
    new() { Name = "Task.Edit" },
    new() { Name = "Task.Delete" },

    // Reports
    new() { Name = "Report.View" },
    new() { Name = "Report.Export" }
};
        }
    }
}
