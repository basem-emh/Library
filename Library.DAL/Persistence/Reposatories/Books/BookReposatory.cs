using Library.DAL.Entities.Books;
using Library.DAL.Persistence.Data;
using Library.DAL.Persistence.Reposatories._GenericRepos;

namespace Library.DAL.Persistence.Reposatories.Books
{
    public class BookReposatory : GenericReposatories<Book> , IBookReposatory
    {
        public BookReposatory(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
