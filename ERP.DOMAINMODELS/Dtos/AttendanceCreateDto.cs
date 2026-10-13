
using System;

namespace ERP.DOMAINMODELS.Dtos
{
    public class AttendanceCreateDto
    {
        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string? CheckInTime { get; set; }

        public string? CheckOutTime { get; set; }

        public string Status { get; set; } = "Present";

        public string? Remarks { get; set; }
    }
}

