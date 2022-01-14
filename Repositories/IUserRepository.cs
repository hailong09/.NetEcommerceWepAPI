using backend.Models;

namespace backend.Repositories
{
    public interface IUserRepository
    {
        Task Register(User user);
        Task<User> GetUser(string email);

    }
}