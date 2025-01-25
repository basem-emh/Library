using Library.DAL.Common.Enums;
using Library.DAL.Entities.Departments;

namespace Library.DAL.Entities.Employees
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public string? Image { get; set; } 
        public int? DepartmentId { get; set;}
        //Nav prop
        public virtual Department? Department { get; set; }

    }
}
