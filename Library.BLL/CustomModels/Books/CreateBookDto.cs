using Library.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Employees
{
    public class CreateBookDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateOnly PublicationYear { get; set; }
        public int? DepartmentID { get; set; }
        public IFormFile? Image { get; set; }

    }
}
