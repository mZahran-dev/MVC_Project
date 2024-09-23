using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace MVC_Project_PL.ViewModels
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage = "First Name is Required")]
		public string FName { get; set; }
		[Required(ErrorMessage = "Last Name is Required")]
		public string LName { get; set; }	
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage ="Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare(nameof(Password) , ErrorMessage ="confirm password doesn't match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassoword { get; set; }

		[Required(ErrorMessage ="you have to Aggree to Terms&Conditions")]
		public bool IsAgree { get; set; }


	}
}
