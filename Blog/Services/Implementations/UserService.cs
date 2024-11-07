using AutoMapper;
using Blog.Repositories.UnitOfWork;
using Blog.Services.DTOs;
using Blog.Services.Interfaces;
using UserMicrosservice.Model;
using UserMicrosservice.Repositories;

namespace Blog.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public User CreateUser(CreateUserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.UserId = Guid.NewGuid();
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.LastModifiedBy = "system";

            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
            return user;
        }

        public User UpdateUser(UpdateUserDto userDto)
        {
            var existingUser = _unitOfWork.UserRepository.GetById(userDto.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            _mapper.Map(userDto, existingUser);
            existingUser.UpdatedAt = DateTime.Now;
            existingUser.LastModifiedBy = "system";

            _unitOfWork.UserRepository.Update(existingUser);
            _unitOfWork.Commit();

            return existingUser;
        }
    }
}
