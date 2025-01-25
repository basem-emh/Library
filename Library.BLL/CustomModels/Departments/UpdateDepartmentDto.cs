using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Departments
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required !!")]
        public string Description { get; set; } = null!;
        public DateTime CreateOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
    }
}
