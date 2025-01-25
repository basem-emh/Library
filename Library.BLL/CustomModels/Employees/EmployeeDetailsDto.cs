using Library.DAL.Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }

        [Display(Name = ("Created By"))]
        public int CreatedBy { get; set; }

        [Display(Name = ("Created On"))]
        public DateTime CreatedOn { get; set; }

        [Display(Name = ("Last Modified By"))]
        public int LastModifiedBy { get; set; }

        [Display(Name = ("Last Modified On"))]
        public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public string? Department { get; set; }
        public string? Image { get; set; }

    }
}
