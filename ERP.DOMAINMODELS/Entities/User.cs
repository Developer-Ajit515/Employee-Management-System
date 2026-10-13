using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        //public string Role { get; set; } = "Employee";

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();
    }
}
