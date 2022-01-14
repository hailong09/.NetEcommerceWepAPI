using backend.Models;

namespace backend.Repositories
{
    public interface IJWTRepository
    {
        public string CreateToken(User user);

    }
}