using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Types.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class AuthService(
        IGenericRepository<User> userRepository,
        IGenericRepository<Role> roleRepository,
        IGenericRepository<UserRole> userRoleRepository,
        IConfiguration configuration) : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IGenericRepository<Role> _roleRepository = roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository = userRoleRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<User> RegisterAsync(RegisterRequestDto requestDto)
        {
            var existingUser = await _userRepository.GetByConditionAsync(u => u.Email == requestDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            var user = new User
            {
                Email = requestDto.Email,
                Firstname = requestDto.FirstName,
                Lastname = requestDto.LastName,
                PhoneNumber = requestDto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password),
                IsActive = true
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // Varsayılan rol "User"
            var userRole = await _roleRepository.GetByConditionAsync(r => r.Name == "User");
            if (userRole != null)
            {
                var newUserRole = new UserRole { UserId = user.Id, RoleId = userRole.Id };
                await _userRoleRepository.AddAsync(newUserRole);
                await _userRoleRepository.SaveChangesAsync();
            }

            return user;
        }

        public async Task<string?> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _userRepository.GetByConditionAsync(u => u.Email == requestDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(requestDto.Password, user.PasswordHash))
                return null;

            var jwtSecret = _configuration["Jwt:Secret"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtSecret))
                throw new InvalidOperationException("JWT Secret configuration is missing.");

            if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException("JWT Issuer/Audience configuration is missing.");

            var key = Encoding.ASCII.GetBytes(jwtSecret);

            var roles = await _userRoleRepository.FindAsync(
                ur => ur.UserId == user.Id,
                q => q.Include(x => x.Role)
            );

            var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new(ClaimTypes.Email, user.Email)
    };

            foreach (var userRole in roles)
            {
                if (userRole.Role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
