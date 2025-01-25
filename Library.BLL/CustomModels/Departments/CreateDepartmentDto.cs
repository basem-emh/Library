using System.ComponentModel.DataAnnotations;

namespace Library.BLL.CustomModels.Departments
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required !!")]
        public string Description { get; set; } = null!;
    }
}
