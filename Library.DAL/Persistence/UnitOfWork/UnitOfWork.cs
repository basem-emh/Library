using Library.DAL.Persistence.Data;
using Library.DAL.Persistence.Reposatories.Books;
using Library.DAL.Persistence.Reposatories.Departments;
using Library.DAL.Persistence.Reposatories.Employees;

namespace Library.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IDepartmentReposatory DepartmentReposatory => new DepartmentReposatory(_dbContext);
        public IEmployeeReposatory EmployeeReposatory => new EmployeeReposatory(_dbContext);
        public IBookReposatory BookReposatory => new BookReposatory (_dbContext);
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ في سجل الأخطاء أو طباعة التفاصيل
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                return 0;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
