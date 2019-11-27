using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRestaurant.Services
{
    public interface IIdentityService
    {
        Task<bool> RegisterAsync(string email, string password);

        Task<bool> LoginAsync(string email, string password);
    }
}
