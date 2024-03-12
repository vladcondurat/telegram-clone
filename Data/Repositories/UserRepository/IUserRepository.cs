using Data.Entities;
using Data.Infrastructure.Repository;

namespace Data.Repositories.UserRepository;

public interface IUserRepository : IRepository<User>
{
    User? GetById(int id);
    User? GetByUsername(string username);
}