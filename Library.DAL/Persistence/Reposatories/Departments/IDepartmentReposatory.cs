using Library.DAL.Entities.Departments;
using Library.DAL.Persistence.Reposatories._GenericRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Reposatories.Departments
{
    public interface IDepartmentReposatory : IGenericReposatories<Department>
    {
    }
}
