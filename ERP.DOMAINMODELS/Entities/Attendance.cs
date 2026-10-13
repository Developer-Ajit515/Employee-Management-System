
using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class Attendance
    {
        public int AttendanceId { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }

        public DateTime AttendanceDate { get; set; }

        public TimeSpan? CheckInTime { get; set; }

        public TimeSpan? CheckOutTime { get; set; }

        public string Status { get; set; } = "Present";

        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }
    }
}

