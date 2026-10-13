namespace ERP.DOMAINMODELS.Dtos
{
    public class UpdateSystemSettingDto
    {
        // ============================
        // GENERAL
        // ============================

        public string? CompanyName { get; set; } = string.Empty;

        public string? CompanyLogo { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Website { get; set; }

        public string? Currency { get; set; } = "INR";


        // ============================
        // SYSTEM
        // ============================

        public string TimeZone { get; set; } =
            "India Standard Time";

        public string DateFormat { get; set; } =
            "dd-MM-yyyy";

        public string Language { get; set; } =
            "en-IN";

        public int PageSize { get; set; } = 10;


        // ============================
        // NOTIFICATIONS
        // ============================

        public bool EmailNotifications { get; set; } = true;

        public bool TaskNotifications { get; set; } = true;

        public bool StockNotifications { get; set; } = true;

        public bool LowStockAlert { get; set; } = true;

        public bool MaintenanceMode { get; set; }


        // ============================
        // APPEARANCE
        // ============================

        public string Theme { get; set; } = "light";

        public bool CompactSidebar { get; set; }

        public bool SidebarCollapsed { get; set; }

        public string DashboardLayout { get; set; } = "default";


        // ============================
        // SECURITY
        // ============================

        public int MinimumPasswordLength { get; set; } = 8;

        public bool RequireUppercase { get; set; } = true;

        public bool RequireNumber { get; set; } = true;

        public bool RequireSpecialCharacter { get; set; } = true;

        public int MaximumLoginAttempts { get; set; } = 5;

        public int LockoutDurationMinutes { get; set; } = 15;

        public int SessionTimeoutMinutes { get; set; } = 30;

        public bool TwoFactorAuthentication { get; set; } = false;

        public bool ForcePasswordChange { get; set; } = false;
    }
}