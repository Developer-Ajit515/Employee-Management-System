using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new();

        public List<string> Permissions { get; set; } = new();
    }
}
