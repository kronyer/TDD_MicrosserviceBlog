namespace UserMicrosservice.Model
{
    public class User
    {
        public Guid UserId { get; set; }
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

        public DateTime LastLogin { get; set; }
        public int FailedLoginAttempts { get; set; }

        public string PasswordResetToken { get; set; }
        public DateTime? TokenExpiration { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }

}
