namespace Data.Infrastructure.Repository;

public interface IRepository<T> where T : class
{
    public IEnumerable<T> GetAll();
    public T Find(int id);
    public void Update(T entity);
    public void Delete(T entity);
    public void Add(T entity);
    public void AddRange(IEnumerable<T> entities);
}