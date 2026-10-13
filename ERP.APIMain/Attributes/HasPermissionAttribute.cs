using Microsoft.AspNetCore.Authorization;

namespace ERP.APIMain.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = permission;
        }
    }
}
