using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private ApplicationDbContext _userContext;

        public UserRepository(ApplicationDbContext context)
        {
            _userContext = context;
        }
        public async Task<User> GetByUsername(string username)
        {
            var user = await _userContext.Users.FindAsync(username);
            return user;
        }
        public async Task<User> Add(User user)
        {
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            _userContext.Add(user);
            await _userContext.SaveChangesAsync();
            return user;
        }
    }
}
