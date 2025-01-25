using Library.DAL.Common.Enums;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Library.PL.Models.EmployeeViewModel
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Department")]
        [Required]
        public int? DepartmentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
