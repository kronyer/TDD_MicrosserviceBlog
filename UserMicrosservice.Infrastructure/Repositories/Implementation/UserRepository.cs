using UserMicrosservice.Domain;

namespace UserMicrosservice.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(User entity)
    {
        _context.Set<User>().Add(entity);
    }

    public User GetById(Guid id)
    {
        return _context.Set<User>().Find(id);
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Set<User>().ToList();
    }

    public void Update(User entity)
    {
        _context.Set<User>().Update(entity);
    }

    public void Delete(Guid id)
    {
        var user = _context.Set<User>().Find(id);
        if (user != null)
        {
            _context.Set<User>().Remove(user);
        }
    }

    public User GetByEmail(string email)
    {
        return _context.Set<User>().FirstOrDefault(u => u.Email == email);
    }
}
