using Blog.Services.DTOs;
using UserMicrosservice.Model;

namespace Blog.Services.Interfaces
{
    public interface IUserService
    {
        User CreateUser(CreateUserDto userDto);
        User UpdateUser(UpdateUserDto userDto);
    }
}
