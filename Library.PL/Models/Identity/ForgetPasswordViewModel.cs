using System.ComponentModel.DataAnnotations;

namespace Library.PL.Models.Identity
{
    public class ForgetPasswordViewModel
    {
        [EmailAddress]
        public string Email { get; set; } = null!; 
    }
}
