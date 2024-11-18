namespace UserMicrosservice.Domain;

public interface IRepository<T> where T : class
{
    void Add(T entity);
    T GetById(Guid id);
    IEnumerable<T> GetAll();
    void Update(T entity);
    void Delete(Guid id);
}
