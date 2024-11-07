using UserMicrosservice.Repositories;

namespace UserMicrosservice.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        void Commit();
    }
}
