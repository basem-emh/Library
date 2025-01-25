using Library.DAL.Persistence.Reposatories.Books;
using Library.DAL.Persistence.Reposatories.Departments;
using Library.DAL.Persistence.Reposatories.Employees;

namespace Library.DAL.Persistence.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IDepartmentReposatory DepartmentReposatory { get;}
        public IEmployeeReposatory EmployeeReposatory { get;}
        public IBookReposatory BookReposatory { get;}
        Task<int> CompleteAsync();
    }
}
