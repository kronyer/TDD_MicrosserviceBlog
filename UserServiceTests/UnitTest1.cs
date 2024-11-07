using Blog.Services.Implementations;
using Moq;
using UserMicrosservice.Model;
using UserMicrosservice.Repositories;

namespace UserServiceTests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateUser_ShouldReturnUser_WhenDataIsValid()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userService = new UserService(unitOfWork.Object);
            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "hashedpassword",
                PasswordSalt = "salt",
                FirstName = "Test",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                ProfilePictureUrl = "http://example.com/pic.jpg",
                Role = "User",
                IsActive = true,
                IsEmailVerified = false,
                LastLogin = DateTime.Now,
                FailedLoginAttempts = 0,
                PasswordResetToken = null, // Tornar anulável
                TokenExpiration = null, // Tornar anulável
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                LastModifiedBy = "system"
            };

            // Act
            var createdUser = userService.CreateUser(newUser);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUser.Username, createdUser.Username);
            Assert.Equal(newUser.Email, createdUser.Email);
            userRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        }
    }
}