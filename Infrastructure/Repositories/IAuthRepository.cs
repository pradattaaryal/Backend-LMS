using Domain.Entities;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> CreateUserrepo(User user);
        Task<User> GetUserByNamerepo(string username);
    }
}
