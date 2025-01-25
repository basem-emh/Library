using Library.DAL.Entities;

namespace Library.DAL.Persistence.Reposatories._GenericRepos
{
    public interface IGenericReposatories<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
        IQueryable<T> GetAllAsQueryable();
        Task<T?> GetAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
