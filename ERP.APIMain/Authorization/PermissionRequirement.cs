using Microsoft.AspNetCore.Authorization;

namespace ERP.APIMain.Authorization
{
    public class PermissionRequirement
        : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(
            string permission)
        {
            Permission = permission;
        }
    }
}