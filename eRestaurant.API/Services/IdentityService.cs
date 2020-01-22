using eRestaurant.API.DTO;
using eRestaurant.API.Entities;
using eRestaurant.API.Repositories;
using eRestaurant.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.API.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _uow;

        public IdentityService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;

            if (_uow.UserManager.FindByNameAsync("admin").Result == null)
            {
                var admin = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "ad@m.in",
                    UserName = "admin"
                };
                _uow.UserManager.CreateAsync(admin, "wh41w0ul6U7");
                _uow.UserManager.AddToRoleAsync(admin, "admin");
                _uow.SaveChangesAsync();
            }
        }

        public async Task<TokenInfo> RegisterAsync(string email, string password)
        {
            var info = new TokenInfo { Expiration = DateTime.Now.AddDays(7) };
            var existingUser = await _uow.UserManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                info.Errors = new[] { "User already exists" };
                return info;
            }

            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _uow.UserManager.CreateAsync(newUser, password);
            if (!createdUser.Succeeded)
            {
                info.Errors = createdUser.Errors.Select(er => er.Description);
                return info;
            }

            _uow.Profiles.Add(new UserProfile { UserId = newUser.Id });
            info.Token = await GenerateJwtToken(newUser);
            return info;
        }

        public async Task<TokenInfo> LoginAsync(string email, string password)
        {
            var info = new TokenInfo { Expiration = DateTime.Now.AddDays(7) };
            var user = await _uow.UserManager.FindByEmailAsync(email);

            if (user == null)
            {
                info.Errors = new[] { "User does not exist" };
                return info;
            }

            var passwordCorrect = await _uow.UserManager.CheckPasswordAsync(user, password);
            if (!passwordCorrect)
            {
                info.Errors = new[] { "Incorrect password" };
                return info;
            }

            info.Token = await GenerateJwtToken(user);
            return info;
        }

        protected async Task<string> GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Secret key for JWT tokens");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
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
