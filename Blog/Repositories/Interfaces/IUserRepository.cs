using UserMicrosservice.Model;

namespace UserMicrosservice.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
