using Microsoft.AspNetCore.Http;

namespace Library.BLL.CustomModels.Employees
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Department { get; set; }
        public DateOnly PublicationYear { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
