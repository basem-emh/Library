using Library.DAL.Entities.Employees;
using Library.DAL.Persistence.Data;
using Library.DAL.Persistence.Reposatories._GenericRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Reposatories.Employees
{
    public class EmployeeReposatory : GenericReposatories<Employee> , IEmployeeReposatory
    {
        public EmployeeReposatory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
