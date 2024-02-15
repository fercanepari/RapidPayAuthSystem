namespace RapidPayAuthSystem.Services
{
    public interface IRepository<T>
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        void SaveChanges();
    }
}
