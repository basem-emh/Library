using Library.DAL.Entities.Departments;

namespace Library.DAL.Entities.Books
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }=null!; 
        public string Description { get; set; } =null!;
        public string Author { get; set; } =null!;
        public DateOnly PublicationYear { get; set; }
        public string? Image { get; set; }
        public int? DepartmentId { get; set; }
        //Nav prop [one]
        public virtual Department? Department { get; set; }
    }
}
