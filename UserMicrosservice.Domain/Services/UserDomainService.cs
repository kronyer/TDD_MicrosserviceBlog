namespace UserMicrosservice.Domain;

public class UserDomainService : IUserDomainService
{
    public void ValidateUser(User user)
    {
        if (string.IsNullOrEmpty(user.FirstName))
        {
            throw new ArgumentException("User name cannot be empty");
        }
        // Outras validações e lógica de negócios
    }

    public User CreateUser(User user)
    {
        user.UserId = Guid.NewGuid();
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;
        user.LastModifiedBy = "system";

        ValidateUser(user);
        return user;
    }

    public User UpdateUserDetails(User user)
    {
        user.UpdatedAt = DateTime.Now;
        user.LastModifiedBy = "system";
        return user;
    }
}
