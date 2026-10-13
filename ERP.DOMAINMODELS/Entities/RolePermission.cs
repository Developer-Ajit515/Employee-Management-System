using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class RolePermission
    {
        // COMPOSITE KEYS

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        // NAVIGATION PROPERTIES

        public Role Role { get; set; } = null!;

        public Permission Permission { get; set; } = null!;

        // AUDIT FIELDS

        public DateTime CreatedDate { get; set; }
            = DateTime.Now;
    }
}