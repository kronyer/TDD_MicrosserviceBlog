using UserMicrosservice.Model;

namespace Blog.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(User user);
    }
}
