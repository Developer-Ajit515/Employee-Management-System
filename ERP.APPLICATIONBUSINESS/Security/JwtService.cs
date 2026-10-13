using ERP.DOMAINMODELS.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ERP.APPLICATIONBUSINESS.Security
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.Name
                ),

                new Claim(
                    ClaimTypes.Email,
                    user.Email
                )
            };

            // Add roles
            foreach (var userRole in user.UserRoles)
            {
                if (userRole.Role == null)
                    continue;

                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        userRole.Role.Name
                    )
                );

                // Add permissions
                foreach (var rolePermission
                    in userRole.Role.RolePermissions)
                {
                    if (rolePermission.Permission == null)
                        continue;

                    claims.Add(
                        new Claim(
                            "Permission",
                            rolePermission.Permission.Name
                        )
                    );
                }
            }

            var duration =
                Convert.ToDouble(
                    _config["Jwt:DurationInMinutes"] ?? "60"
                );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}