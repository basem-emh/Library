using Library.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Employees
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        
        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }

        [Display(Name ="Department")]
        public int? DepartmentId { get; set; }

        public IFormFile? Image  { get; set; }
    }
}
