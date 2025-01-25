using Library.DAL.Entities;
using Library.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL.Persistence.Reposatories._GenericRepos
{
    public class GenericReposatories<T>:IGenericReposatories<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericReposatories(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
        {
            if ( withAsNoTracking)
                return await _dbContext.Set<T>()
                    .Where(X => !X.IsDeleted)
                    .AsNoTracking()
                    .ToListAsync();

            return await _dbContext.Set<T>()
                .Where(X => !X.IsDeleted)
                .ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
            => _dbContext.Set<T>();

        public async Task<T?> GetAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public void Add(T entity)
            => _dbContext.Set<T>().Add(entity);
        

        public void Update(T entity)
            =>_dbContext.Set<T>().Update(entity);
          
        public void Delete(T entity)
        {
            //_dbContext.Set<T>().Remove(entity); دى صح بس انا عايز اشتغل soft delete
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
        }
    }
}
