using Library.BLL.Common.Helpers.Attachments;
using Library.BLL.CustomModels.Employees;
using Library.DAL.Entities.Books;
using Library.DAL.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.Books
{
    public class BookServices : IBookServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentSettings _attachment;

        public BookServices(IUnitOfWork unitOfWork,
            IAttachmentSettings attachment) 
        {
            _unitOfWork = unitOfWork;
            _attachment = attachment;
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync(string search)
        {
            var book = _unitOfWork.BookReposatory
                .GetAllAsQueryable()
                .Where(b => !b.IsDeleted && (string.IsNullOrEmpty(search) || b.Title.ToUpper().Contains(search.ToUpper())))
                .Include(b => b.Department)
                .Select(book => new BookDto()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    PublicationYear = book.PublicationYear,
                    Department = book.Department.Name
                }).ToListAsync();
            return await book;
        }
        public async Task<BookDetailsDto?> GetBookByIdAsync(int id)
        {
            var book = await _unitOfWork.BookReposatory.GetAsync(id);
            if (book == null)
                return null;

            return new BookDetailsDto()
            {
                Id = book.Id,
                Title= book.Title,
                Author= book.Author,
                Description = book.Description,
                PublicationYear= book.PublicationYear,
                CreateOn= DateTime.UtcNow,
                Department = book.Department?.Name,
                CreatedBy = book.CreatedBy,
                LastModifiedBy = book.LastModifiedBy,
                LastModifiedOn = book.LastModifiedOn,
                Image = book.Image,
            };
        }

        public async Task<int> CreateBookAsync(CreateBookDto bookDto)
        {
            var recievdImage = string.Empty;

            if (bookDto.Image is not null)
                recievdImage = _attachment.Upload(bookDto.Image, "images");

            var book = new Book()
            {
                Title=bookDto.Title,
                Author=bookDto.Author,
                Description=bookDto.Description,
                PublicationYear=bookDto.PublicationYear,
                DepartmentId = bookDto.DepartmentID,
                Image = recievdImage,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
                CreateOn = DateTime.UtcNow,
            };

            _unitOfWork.BookReposatory.Add(book);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateBookAsync(UpdateBookDto bookDto)
        {
            var book = await _unitOfWork.BookReposatory.GetAsync(bookDto.Id);
            if (book is null)
            {
                throw new Exception("Employee not found");
            }

            var recievdImage = book.Image;
            if (bookDto.Image is not null)
            {
                recievdImage = _attachment.Upload(bookDto.Image, "images");
            }

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Description= bookDto.Description;
            book.DepartmentId = bookDto.DepartmentID;
            book.LastModifiedBy = 1;
            book.LastModifiedOn = DateTime.UtcNow;
            book.Image = recievdImage;

            _unitOfWork.BookReposatory.Update(book);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookRepo = _unitOfWork.BookReposatory;
            var book = await bookRepo.GetAsync(id);
            if (book is { })
                bookRepo.Delete(book);
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
