using AutoMapper;
using Moq;
using UserMicrosservice.Application;
using UserMicrosservice.Domain;

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

            var userDomainService = new Mock<UserDomainService>();
            var userService = new UserService(unitOfWork.Object, _mapper, userDomainService.Object);
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
            unitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void UpdateUser_ShouldReturnUpdatedUser_WhenDataIsValid()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);
            var userDomainService = new Mock<IUserDomainService>();

            var userService = new UserService(unitOfWork.Object, _mapper, userDomainService.Object);
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
                UserId = existingUser.UserId, // Certifique-se de que o ID do usuário está correto
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

            var updatedUser = new User
            {
                UserId = updatedUserDto.UserId,
                Username = updatedUserDto.Username,
                Email = updatedUserDto.Email,
                PasswordHash = existingUser.PasswordHash,
                PasswordSalt = existingUser.PasswordSalt,
                FirstName = updatedUserDto.FirstName,
                LastName = updatedUserDto.LastName,
                DateOfBirth = updatedUserDto.DateOfBirth,
                ProfilePictureUrl = updatedUserDto.ProfilePictureUrl,
                Role = updatedUserDto.Role,
                IsActive = updatedUserDto.IsActive,
                IsEmailVerified = updatedUserDto.IsEmailVerified,
                LastLogin = updatedUserDto.LastLogin,
                FailedLoginAttempts = updatedUserDto.FailedLoginAttempts,
                PasswordResetToken = updatedUserDto.PasswordResetToken,
                TokenExpiration = updatedUserDto.TokenExpiration,
                CreatedAt = existingUser.CreatedAt,
                UpdatedAt = DateTime.Now,
                LastModifiedBy = updatedUserDto.LastModifiedBy
            };

            userDomainService.Setup(s => s.UpdateUserDetails(It.IsAny<User>())).Returns(updatedUser);

            // Act
            var result = userService.UpdateUser(updatedUserDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUserDto.Username, result.Username);
            Assert.Equal(updatedUserDto.Email, result.Email);
            Assert.Equal(updatedUserDto.FirstName, result.FirstName);
            Assert.Equal(updatedUserDto.LastName, result.LastName);
            userRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
            unitOfWork.Verify(u => u.Commit(), Times.Once);
        }


        [Fact]
        public void DeleteUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userDomainService = new Mock<IUserDomainService>();
            var userService = new UserService(unitOfWork.Object, _mapper, userDomainService.Object);

            var userId = Guid.NewGuid();
            var user = new User { UserId = userId };
            userRepository.Setup(repo => repo.GetById(userId)).Returns(user);

            // Act
            userService.DeleteUser(userId);

            // Assert
            userRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
            unitOfWork.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userDomainService = new Mock<IUserDomainService>();
            var userService = new UserService(unitOfWork.Object, _mapper, userDomainService.Object);

            var users = new List<User>
            {
                new User { UserId = Guid.NewGuid(), Username = "user1", Email = "user1@example.com" },
                new User { UserId = Guid.NewGuid(), Username = "user2", Email = "user2@example.com" }
            };
            userRepository.Setup(repo => repo.GetAll()).Returns(users);

            // Act
            var result = userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userRepository = new Mock<IUserRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.UserRepository).Returns(userRepository.Object);

            var userDomainService = new Mock<IUserDomainService>();
            var userService = new UserService(unitOfWork.Object, _mapper, userDomainService.Object);

            var userId = Guid.NewGuid();
            var user = new User { UserId = userId, Username = "user", Email = "user@example.com" };
            userRepository.Setup(repo => repo.GetById(userId)).Returns(user);

            // Act
            var result = userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }
    }
}