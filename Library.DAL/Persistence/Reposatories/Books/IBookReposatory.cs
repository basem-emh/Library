using Library.DAL.Entities.Books;
using Library.DAL.Persistence.Reposatories._GenericRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.Persistence.Reposatories.Books
{
    public interface IBookReposatory : IGenericReposatories<Book>
    {
    }
}
