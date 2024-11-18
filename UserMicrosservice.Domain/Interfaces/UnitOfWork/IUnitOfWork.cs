namespace UserMicrosservice.Domain;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    void Commit();
}
