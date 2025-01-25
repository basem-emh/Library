using Library.BLL.CustomModels.Employees;

namespace Library.BLL.Services.Books
{
    public interface IBookServices
    {
        Task<IEnumerable<BookDto>> GetBooksAsync(string search);
        Task<BookDetailsDto?> GetBookByIdAsync(int id);
        Task<int> CreateBookAsync(CreateBookDto bookDto);
        Task<int> UpdateBookAsync(UpdateBookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}
