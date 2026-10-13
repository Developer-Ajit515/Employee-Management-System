using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class Permission
    {
        public int Id { get; set; }

        public string Name { get; set; }
            = string.Empty;

        public string? Description { get; set; }

        // NAVIGATION

        public ICollection<RolePermission>
            RolePermissions
        { get; set; }
            = new List<RolePermission>();
    }
}