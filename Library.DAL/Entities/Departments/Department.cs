using Library.DAL.Entities.Books;
using Library.DAL.Entities.Employees;

namespace Library.DAL.Entities.Departments
{
    public class Department : BaseEntity 
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!; 
        public int? EmployeeId { get; set; }
        public int? BookId { get; set; }
        //nav prop [many]
        public virtual ICollection<Employee> Emplyees { get; set; } = new HashSet<Employee>();
        public virtual ICollection<Book> Books{ get; set; } = new HashSet<Book>();

    }
}
