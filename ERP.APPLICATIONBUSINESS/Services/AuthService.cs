using BC = BCrypt.Net.BCrypt;

using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.APPLICATIONBUSINESS.Security;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly JwtService _jwtService;

        public AuthService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            JwtService jwtService)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _jwtService = jwtService;
        }

        // =========================================================
        // REGISTER
        // =========================================================
        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Check existing email
            var existingUser =
                await _userRepository.GetByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            // =====================================================
            // GET DEFAULT EMPLOYEE ROLE
            // =====================================================

            var roles = await _roleRepository.GetAllAsync();

            var employeeRole = roles.FirstOrDefault(x =>
                x.Name.Equals(
                    "Employee",
                    StringComparison.OrdinalIgnoreCase
                )
                && x.IsActive
            );

            if (employeeRole == null)
            {
                throw new Exception(
                    "Employee role is not configured."
                );
            }

            // =====================================================
            // CREATE USER
            // =====================================================

            var user = new User
            {
                Name = dto.Name.Trim(),

                Email = dto.Email
                    .Trim()
                    .ToLower(),

                PasswordHash =
                    BC.HashPassword(dto.Password),

                IsActive = true
            };

            // =====================================================
            // AUTOMATICALLY ASSIGN EMPLOYEE ROLE
            // =====================================================

            user.UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    RoleId = employeeRole.Id
                }
            };

            // =====================================================
            // SAVE USER + USER ROLE
            // =====================================================

            await _userRepository.AddUserAsync(user);

            return "User Registered Successfully";
        }

        // =========================================================
        // LOGIN
        // =========================================================
        public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
        {
            var user =
                await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception(
                    "Invalid email or password"
                );
            }

            if (!user.IsActive)
            {
                throw new Exception(
                    "User account is inactive"
                );
            }

            var validPassword =
                BC.Verify(
                    dto.Password,
                    user.PasswordHash
                );

            if (!validPassword)
            {
                throw new Exception(
                    "Invalid email or password"
                );
            }

            // =====================================================
            // ROLES
            // =====================================================

            var roles =
                user.UserRoles
                    .Where(x => x.Role != null)
                    .Select(x => x.Role!.Name)
                    .Distinct()
                    .ToList();

            // =====================================================
            // PERMISSIONS
            // =====================================================

            var permissions =
                user.UserRoles
                    .Where(x => x.Role != null)
                    .SelectMany(
                        x => x.Role!.RolePermissions
                    )
                    .Where(x => x.Permission != null)
                    .Select(x => x.Permission!.Name)
                    .Distinct()
                    .ToList();

            // =====================================================
            // JWT
            // =====================================================

            var token =
                _jwtService.GenerateToken(user);

            // =====================================================
            // RESPONSE
            // =====================================================

            return new LoginResponseDto
            {
                Token = token,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Roles = roles,
                Permissions = permissions
            };
        }
    }
}