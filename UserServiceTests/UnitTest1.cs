using AutoMapper;
using Blog.Services.DTOs;
using Blog.Services.Implementations;
using Blog.Services.Mapping;
using Moq;
using UserMicrosservice.Model;
using UserMicrosservice.Repositories;

namespace UserServiceTests
{
    public class UnitTest1
    {
        private readonly IMapper _mapper;
        public UnitTest1()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void CreateUser_ShouldReturnUser_WhenDataIsValid()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userService = new UserService(unitOfWork.Object, _mapper);
            var newUserDto = new CreateUserDto
            {
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
                IsEmailVerified = false
            };

            // Act
            var createdUser = userService.CreateUser(newUserDto);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(newUserDto.Username, createdUser.Username);
            Assert.Equal(newUserDto.Email, createdUser.Email);
            userRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void UpdateUser_ShouldReturnUpdatedUser_WhenDataIsValid()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userService = new UserService(unitOfWork.Object, _mapper);
            var existingUser = new User
            {
                UserId = Guid.NewGuid(),
                Username = "existinguser",
                Email = "existinguser@example.com",
                PasswordHash = "hashedpassword",
                PasswordSalt = "salt",
                FirstName = "Existing",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                ProfilePictureUrl = "http://example.com/pic.jpg",
                Role = "User",
                IsActive = true,
                IsEmailVerified = false,
                LastLogin = DateTime.Now,
                FailedLoginAttempts = 0,
                PasswordResetToken = null,
                TokenExpiration = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                LastModifiedBy = "system"
            };

            userRepository.Setup(repo => repo.GetById(existingUser.UserId)).Returns(existingUser);

            var updatedUserDto = new UpdateUserDto
            {
                UserId = existingUser.UserId,
                Username = "updateduser",
                Email = "updateduser@example.com",
                FirstName = "Updated",
                LastName = "User",
                DateOfBirth = new DateTime(1990, 1, 1),
                ProfilePictureUrl = "http://example.com/updatedpic.jpg",
                Role = "Admin",
                IsActive = true,
                IsEmailVerified = true,
                LastLogin = DateTime.Now,
                FailedLoginAttempts = 0,
                PasswordResetToken = null,
                TokenExpiration = null,
                LastModifiedBy = "system"
            };

            // Act
            var result = userService.UpdateUser(updatedUserDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUserDto.Username, result.Username);
            Assert.Equal(updatedUserDto.Email, result.Email);
            Assert.Equal(updatedUserDto.FirstName, result.FirstName);
            Assert.Equal(updatedUserDto.LastName, result.LastName);
            userRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
        }
    }
}