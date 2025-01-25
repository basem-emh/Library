using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Employees
{
    public class BookDetailsDto {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Author { get; set; } = null!;

        [Display(Name = "Publication Year")]
        public DateOnly PublicationYear { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "Last Modified By")]
        public int LastModifiedBy { get; set; }

        [Display(Name = "Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
        public string? Department { get; set; }
        public string? Image  { get; set; }

    }
}
