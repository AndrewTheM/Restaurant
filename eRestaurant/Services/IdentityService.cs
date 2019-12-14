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
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            var existingUser = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (existingUser != null)
                return new AuthenticationResult { Errors = new[] { "User already exists" } };

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _unitOfWork.UserManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
                return new AuthenticationResult { Errors = createdUser.Errors.Select(er => er.Description) };
            return new AuthenticationResult { Success = true, Token = await GenerateJwtToken(newUser) };
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (user == null)
                return new AuthenticationResult { Errors = new[] { "User not found" } };

            var passwordCorrect = await _unitOfWork.UserManager.CheckPasswordAsync(user, password);
            if (!passwordCorrect)
                return new AuthenticationResult { Errors = new[] { "Incorrect password" } };
            return new AuthenticationResult { Success = true, Token = await GenerateJwtToken(user) };
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

            var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _unitOfWork.UserManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _unitOfWork.RoleManager.FindByNameAsync(userRole);
                if (role == null)
                    continue;

                var roleClaims = await _unitOfWork.RoleManager.GetClaimsAsync(role);
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
