namespace UserMicrosservice.Application;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public bool IsEmailVerified { get; set; }
}