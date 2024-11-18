
namespace UserMicrosservice.Domain;

public interface IUserRepository : IRepository<User>
{
    User GetByEmail(string email);
}
