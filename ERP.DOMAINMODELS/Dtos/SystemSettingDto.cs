namespace ERP.DOMAINMODELS.Dtos
{
    public class SystemSettingDto
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; } = string.Empty;

        public string? CompanyLogo { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Website { get; set; }

        public string? Currency { get; set; } = "INR";

        public string TimeZone { get; set; } =
            "India Standard Time";

        public string DateFormat { get; set; } =
            "dd-MM-yyyy";

        public string Language { get; set; } =
            "en-IN";

        public int PageSize { get; set; }

        public bool EmailNotifications { get; set; }

        public bool TaskNotifications { get; set; }

        public bool StockNotifications { get; set; }

        public bool LowStockAlert { get; set; }

        public bool MaintenanceMode { get; set; }

        public string Theme { get; set; } = "light";

        public bool CompactSidebar { get; set; }

        public bool SidebarCollapsed { get; set; }

        public string DashboardLayout { get; set; } = "default";
        public int MinimumPasswordLength { get; set; }

        public bool RequireUppercase { get; set; }

        public bool RequireNumber { get; set; }

        public bool RequireSpecialCharacter { get; set; }

        public int MaximumLoginAttempts { get; set; }

        public int LockoutDurationMinutes { get; set; }

        public int SessionTimeoutMinutes { get; set; }

        public bool TwoFactorAuthentication { get; set; }

        public bool ForcePasswordChange { get; set; }

    }



    
}