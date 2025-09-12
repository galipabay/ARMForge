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
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IGenericRepository<UserRole> _userRoleRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IGenericRepository<User> userRepository,
            IGenericRepository<Role> roleRepository,
            IGenericRepository<UserRole> userRoleRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _configuration = configuration;
        }
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
            {
                return null; // Kimlik doğrulama başarısız
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var roles = await _userRoleRepository.GetByConditionAsync(
                ur => ur.UserId == user.Id,
                include: q => q.Include(ur => ur.Role));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            if (roles != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.Role.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
