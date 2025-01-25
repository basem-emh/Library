using Library.DAL.Entities.Employees;
using Library.DAL.Persistence.Reposatories._GenericRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Reposatories.Employees
{
    public interface IEmployeeReposatory : IGenericReposatories<Employee>
    {
    }
}
