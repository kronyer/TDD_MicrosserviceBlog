using Blog.Repositories.UnitOfWork;
using Blog.Services.Interfaces;
using UserMicrosservice.Model;
using UserMicrosservice.Repositories;

namespace Blog.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User CreateUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.Commit();
            return user;
        }
    }
}
