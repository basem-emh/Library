using System.ComponentModel.DataAnnotations;

namespace Library.PL.Models.DepartmentViewModel
{
    public class DepartmentViewModel
    {

        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required !!")]
        public string Description { get; set; } = null!;

    }
}
