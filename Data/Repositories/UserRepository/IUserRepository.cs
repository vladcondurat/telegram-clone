using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.UserRepository;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByUserId(int id);
    User? GetUserByUsername(string username);
    IEnumerable<User> GetAllExceptCurrentUser(int id);
}