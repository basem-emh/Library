using Library.BLL.Common.Helpers.Attachments;
using Library.BLL.CustomModels.Employees;
using Library.BLL.Services.Books;
using Library.BLL.Services.Departments;
using Library.PL.Models.BookViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Library.PL.Controllers
{
    public class BookController : Controller
    {
        #region Services
        private readonly IBookServices _bookServices;
        private readonly ILogger<BookController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentServices _departmentServices;
        private readonly IAttachmentSettings _attachment;

        public BookController(IBookServices bookServices,
            ILogger<BookController> logger,
            IWebHostEnvironment webHostEnvironment,
            IDepartmentServices departmentServices,
            IAttachmentSettings attachment)
        {
            _bookServices = bookServices;
            _logger = logger;
            _environment = webHostEnvironment;
            _departmentServices = departmentServices;
            _attachment = attachment;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var books = await _bookServices.GetBooksAsync(search);
            return View(books);
        }
        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var book = await _bookServices.GetBookByIdAsync(id.Value);

            if (book is null)
                return NotFound();

            return View(book);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookVM)
        {
            var message = string.Empty;
            try
            {
                if (!ModelState.IsValid)
                    return View(bookVM);


                var createdBook = new CreateBookDto
                {
                    Title = bookVM.Title,
                    Author = bookVM.Author, 
                    DepartmentID = bookVM.DepartmentId,
                    Description = bookVM.Description,
                    PublicationYear = bookVM.PublicationYear,
                    Image = bookVM.Image,
                };

                var results = await _bookServices.CreateBookAsync(createdBook);
                if (results > 0)
                    return RedirectToAction(nameof(Index));

                message = "Book Is Not Added";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during adding book :(";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(bookVM);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var book = await _bookServices.GetBookByIdAsync(id.Value);
                
            if (book is null)
                return NotFound();

            return View(new BookViewModel()
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                PublicationYear = book.PublicationYear,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, BookViewModel bookVM)
        {
            var message = string.Empty;

            if (!ModelState.IsValid)
                return View(bookVM);
            try
            {
                var updatedBook = new UpdateBookDto
                {
                    Id = id,
                    Title = bookVM.Title,
                    Author = bookVM.Author,
                    Description = bookVM.Description,
                    PublicationYear = bookVM.PublicationYear,
                    DepartmentID = bookVM.DepartmentId,
                    Image = bookVM.Image,
                };

                var updeted = await _bookServices.UpdateBookAsync(updatedBook) > 0;
                if (updeted)
                    return RedirectToAction(nameof(Index));

                message = "An error has occured during update book :(";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during updating department :(";

            }

            ModelState.AddModelError(string.Empty, message);
            return View(bookVM);
        }

        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _bookServices.DeleteBookAsync(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "An error has occured during deteting Book :(";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                //2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error has occured during deleting Book :(";

            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
