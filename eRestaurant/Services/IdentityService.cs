using eRestaurant.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IdentityService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var existingUser = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (existingUser != null) return false;

            var newUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email
            };

            var createdUser = await _unitOfWork.UserManager.CreateAsync(newUser, password);

            return createdUser.Succeeded;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (user == null) return false;

            return await _unitOfWork.UserManager.CheckPasswordAsync(user, password);

        }
    }
}
