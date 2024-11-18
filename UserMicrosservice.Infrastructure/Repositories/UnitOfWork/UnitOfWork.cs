using UserMicrosservice.Domain;

namespace UserMicrosservice.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository _userRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository ??= new UserRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
