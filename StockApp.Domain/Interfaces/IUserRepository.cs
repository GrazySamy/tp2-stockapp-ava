using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string username);
        Task<User> Add(User user);
    }
}
