using System.ComponentModel.DataAnnotations;

namespace MVC_Project_PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password), ErrorMessage = "confirm password doesn't match password")]
		[DataType(DataType.Password)]
		public string NewPassoword { get; set; }
	}
}
