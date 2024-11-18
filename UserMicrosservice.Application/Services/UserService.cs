using AutoMapper;
using UserMicrosservice.Domain;

namespace UserMicrosservice.Application;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserDomainService _userDomainService;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IUserDomainService userDomainService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userDomainService = userDomainService;
    }

    public User CreateUser(CreateUserDto userDto)
    {
        User userToCreate = _mapper.Map<User>(userDto);
        var user = _userDomainService.CreateUser(userToCreate);
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

        var updatedUser = _mapper.Map<User>(userDto);
        var user =  _userDomainService.UpdateUserDetails(updatedUser);

        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.Commit();
        return user;
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var users = _unitOfWork.UserRepository.GetAll();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public UserDto GetUserById(Guid userId)
    {
        var user = _unitOfWork.UserRepository.GetById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return _mapper.Map<UserDto>(user);
    }

    public void DeleteUser(Guid userId)
    {
        var user = _unitOfWork.UserRepository.GetById(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        _unitOfWork.UserRepository.Delete(user.UserId);
        _unitOfWork.Commit();
    }
}
