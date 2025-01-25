using System.ComponentModel.DataAnnotations;

namespace Library.PL.Models.BookViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateOnly PublicationYear { get; set; }

        [Display(Name = "Department")]
        [Required]
        public int? DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
