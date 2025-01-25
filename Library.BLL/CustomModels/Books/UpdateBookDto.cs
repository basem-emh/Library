using Microsoft.AspNetCore.Http;

namespace Library.BLL.CustomModels.Employees
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateOnly PublicationYear { get; set; }
        public int? DepartmentID { get; set; }
        public IFormFile? Image { get; set; }

    }
}
