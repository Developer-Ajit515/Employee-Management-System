using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public bool IsActive { get; set; }
    }
}
