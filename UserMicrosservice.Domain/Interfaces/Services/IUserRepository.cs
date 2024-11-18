namespace UserMicrosservice.Domain;

public interface IUserDomainService
{
    void ValidateUser(User user);
    User CreateUser(User user);
    User UpdateUserDetails(User user);
}
