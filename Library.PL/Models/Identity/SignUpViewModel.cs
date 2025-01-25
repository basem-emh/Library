using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Library.PL.Models.Identity
{
	public class SignUpViewModel
	{
		[Display(Name ="First Name")]
        public string FName { get; set; } = null!;
		
		[Display(Name ="Last Name")]
        public string LName { get; set; } = null!;

		[Display(Name = "User Name")]
		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;

		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display(Name ="Cofirm Password")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="Cofirm Password doesn't match with password")]
		public string ConfirmPassword { get; set; } = null!;
		public bool IsAgree { get; set; }
    }
}
