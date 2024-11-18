
using UserMicrosservice.Domain;

namespace UserMicrosservice.Application;

public interface IUserService
{
    User CreateUser(CreateUserDto userDto);
    User UpdateUser(UpdateUserDto userDto);
}
