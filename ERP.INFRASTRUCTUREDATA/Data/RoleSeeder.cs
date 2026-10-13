
using ERP.DOMAINMODELS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace ERP.INFRASTRUCTUREDATA.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // =====================================================
            // 1. CREATE ROLES
            // =====================================================

            var adminRole = await GetOrCreateRole(
                context,
                "Admin",
                "System Administrator"
            );

            var managerRole = await GetOrCreateRole(
                context,
                "Manager",
                "ERP Manager"
            );

            var employeeRole = await GetOrCreateRole(
                context,
                "Employee",
                "Normal ERP Employee"
            );

            await context.SaveChangesAsync();


            // =====================================================
            // 2. CREATE PERMISSIONS
            // =====================================================

            var permissionNames = new[]
            {
                // Dashboard
                "Dashboard.View",

                // Users
                "Users.View",
                "Users.Create",
                "Users.Edit",
                "Users.Delete",

                // Employees
                "Employees.View",
                "Employees.Create",
                "Employees.Edit",
                "Employees.Delete",

                // Projects
                "Projects.View",
                "Projects.Create",
                "Projects.Edit",
                "Projects.Delete",

                // Tasks
                "Tasks.View",
                "Tasks.Create",
                "Tasks.Edit",
                "Tasks.Delete",

                // Products
                "Products.View",
                "Products.Create",
                "Products.Edit",
                "Products.Delete",

                // Roles
                "Roles.View",
                "Roles.Create",
                "Roles.Edit",
                "Roles.Delete",

                // Settings
                "Settings.View"
            };

            foreach (var permissionName in permissionNames)
            {
                var permissionExists =
                    await context.Permissions
                        .AnyAsync(x => x.Name == permissionName);

                if (!permissionExists)
                {
                    context.Permissions.Add(
                        new Permission
                        {
                            Name = permissionName,
                            Description = permissionName
                        }
                    );
                }
            }

            await context.SaveChangesAsync();


            // =====================================================
            // 3. GET PERMISSIONS
            // =====================================================

            var permissions =
                await context.Permissions.ToListAsync();


            // =====================================================
            // 4. ADMIN → ALL PERMISSIONS
            // =====================================================

            foreach (var permission in permissions)
            {
                await AddRolePermissionIfNotExists(
                    context,
                    adminRole.Id,
                    permission.Id
                );
            }


            // =====================================================
            // 5. MANAGER → MANAGEMENT PERMISSIONS
            // =====================================================

            var managerPermissions = new[]
            {
                "Dashboard.View",

                // Users
                "Users.View",
                "Users.Create",
                "Users.Edit",

                // Employees
                "Employees.View",
                "Employees.Create",
                "Employees.Edit",

                // Projects
                "Projects.View",
                "Projects.Create",
                "Projects.Edit",
                "Projects.Delete",

                // Tasks
                "Tasks.View",
                "Tasks.Create",
                "Tasks.Edit",
                "Tasks.Delete",

                // Products
                "Products.View",
                "Products.Create",
                "Products.Edit",

                "Settings.View"
            };

            foreach (var permissionName in managerPermissions)
            {
                var permission =
                    permissions.FirstOrDefault(
                        x => x.Name == permissionName
                    );

                if (permission == null)
                    continue;

                await AddRolePermissionIfNotExists(
                    context,
                    managerRole.Id,
                    permission.Id
                );
            }


            // =====================================================
            // 6. EMPLOYEE → BASIC PERMISSIONS
            // =====================================================

            var employeePermissions = new[]
            {
                "Dashboard.View",

                "Employees.View",

                "Projects.View",

                "Tasks.View",
                "Tasks.Create",
                "Tasks.Edit",

                "Products.View",

                "Settings.View"
            };

            foreach (var permissionName in employeePermissions)
            {
                var permission =
                    permissions.FirstOrDefault(
                        x => x.Name == permissionName
                    );

                if (permission == null)
                    continue;

                await AddRolePermissionIfNotExists(
                    context,
                    employeeRole.Id,
                    permission.Id
                );
            }


            // =====================================================
            // 7. SAVE EVERYTHING
            // =====================================================

            await context.SaveChangesAsync();
        }


        // =========================================================
        // CREATE ROLE IF NOT EXISTS
        // =========================================================

        private static async Task<Role> GetOrCreateRole(
            AppDbContext context,
            string roleName,
            string description)
        {
            var role = await context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == roleName
                );

            if (role == null)
            {
                role = new Role
                {
                    Name = roleName,
                    Description = description,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                context.Roles.Add(role);
            }
            else
            {
                // Make sure existing role remains active
                role.IsActive = true;

                if (string.IsNullOrWhiteSpace(role.Description))
                {
                    role.Description = description;
                }
            }

            return role;
        }


        // =========================================================
        // ADD ROLE PERMISSION IF NOT EXISTS
        // =========================================================

        private static async Task AddRolePermissionIfNotExists(
            AppDbContext context,
            int roleId,
            int permissionId)
        {
            var exists =
                await context.RolePermissions.AnyAsync(
                    x =>
                        x.RoleId == roleId &&
                        x.PermissionId == permissionId
                );

            if (!exists)
            {
                context.RolePermissions.Add(
                    new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId,
                        CreatedDate = DateTime.Now
                    }
                );
            }
        }
    }
}

