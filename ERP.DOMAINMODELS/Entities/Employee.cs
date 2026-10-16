using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation Property
        public Department? Department { get; set; }

        public string Designation { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public int? UserId { get; set; }

        public User? User { get; set; }

        public DateTime JoiningDate { get; set; }

        public bool Status { get; set; } = true;
    }
}