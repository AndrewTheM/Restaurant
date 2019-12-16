using eRestaurant.DTO;
using eRestaurant.Entities;
using eRestaurant.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _uow;

        public IdentityService(IUnitOfWork unitOfWork) => _uow = unitOfWork;

        public async Task<AuthResponse> RegisterAsync(string email, string password)
        {
            var existingUser = await _uow.UserManager.FindByEmailAsync(email);

            if (existingUser != null)
                return new AuthResponse { Errors = new[] { "User already exists" } };

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _uow.UserManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
                return new AuthResponse { Errors = createdUser.Errors.Select(er => er.Description) };
            _uow.Profiles.Add(new UserProfile { UserId = newUser.Id });
            return new AuthResponse { Success = true, Token = await GenerateJwtToken(newUser) };
        }

        public async Task<AuthResponse> LoginAsync(string email, string password)
        {
            var user = await _uow.UserManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthResponse { Errors = new[] { "User not found" } };

            var passwordCorrect = await _uow.UserManager.CheckPasswordAsync(user, password);
            if (!passwordCorrect)
                return new AuthResponse { Errors = new[] { "Incorrect password" } };
            return new AuthResponse { Success = true, Token = await GenerateJwtToken(user) };
        }

        protected async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Secret key for JWT tokens");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id)
            };

            var userClaims = await _uow.UserManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _uow.UserManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _uow.RoleManager.FindByNameAsync(userRole);
                if (role == null)
                    continue;

                var roleClaims = await _uow.RoleManager.GetClaimsAsync(role);
                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;
                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
