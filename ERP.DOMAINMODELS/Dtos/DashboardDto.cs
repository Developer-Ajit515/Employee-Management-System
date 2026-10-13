namespace ERP.DOMAINMODELS.Dtos
{
    public class DashboardDto
    {
        public int TotalUsers { get; set; }

        public int TotalEmployees { get; set; }

        public int TotalProjects { get; set; }

        public int TotalTasks { get; set; }

        public int CompletedTasks { get; set; }

        public int PendingTasks { get; set; }

        public int ActiveUsers { get; set; }

        public int ActiveEmployees { get; set; }

        public int ActiveProjects { get; set; }


        // ============================
        // ANALYTICS
        // ============================

        public ProjectAnalyticsDto ProjectAnalytics { get; set; }
            = new();

        public TaskAnalyticsDto TaskAnalytics { get; set; }
            = new();


        // ============================
        // DEPARTMENTS
        // ============================

        public List<EmployeeDepartmentDto> EmployeeDepartments { get; set; }
            = new();


        // ============================
        // ACTIVITIES
        // ============================

        public List<RecentActivityDto> RecentActivities { get; set; }
            = new();


        // ============================
        // LOW STOCK
        // ============================

        public List<LowStockProductDto> LowStockProducts { get; set; }
            = new();


        // ============================
        // RECENT PROJECTS
        // ============================

        public List<RecentProjectDto> RecentProjects { get; set; }
            = new();


        // ============================
        // RECENT EMPLOYEES
        // ============================

        public List<RecentEmployeeDto> RecentEmployees { get; set; }
            = new();
    }


    // =====================================================
    // PROJECT ANALYTICS
    // =====================================================

    public class ProjectAnalyticsDto
    {
        public int Total { get; set; }

        public int Active { get; set; }

        public int Inactive { get; set; }
    }


    // =====================================================
    // TASK ANALYTICS
    // =====================================================

    public class TaskAnalyticsDto
    {
        public int Total { get; set; }

        public int Completed { get; set; }

        public int Pending { get; set; }
    }


    // =====================================================
    // EMPLOYEE DEPARTMENT
    // =====================================================

    public class EmployeeDepartmentDto
    {
        public string Department { get; set; }
            = string.Empty;

        public int Count { get; set; }
    }


    // =====================================================
    // RECENT ACTIVITY
    // =====================================================

    public class RecentActivityDto
    {
        public string Type { get; set; }
            = string.Empty;

        public string Title { get; set; }
            = string.Empty;

        public string Description { get; set; }
            = string.Empty;

        public DateTime CreatedAt { get; set; }
    }


    // =====================================================
    // RECENT PROJECT
    // =====================================================

    public class RecentProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
            = string.Empty;

        public bool IsActive { get; set; }

        public int TaskCount { get; set; }

        public int CompletedTasks { get; set; }
    }


    // =====================================================
    // RECENT EMPLOYEE
    // =====================================================

    public class RecentEmployeeDto
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; }
            = string.Empty;

        public string Department { get; set; }
            = string.Empty;

        public string Designation { get; set; }
            = string.Empty;

        public DateTime JoiningDate { get; set; }

        public bool Status { get; set; }
    }
}