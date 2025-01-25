using Library.DAL.Entities.Departments;
using Library.DAL.Persistence.Data;
using Library.DAL.Persistence.Reposatories._GenericRepos;

namespace Library.DAL.Persistence.Reposatories.Departments
{
    public class DepartmentReposatory : GenericReposatories<Department> , IDepartmentReposatory
    {
        public DepartmentReposatory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
