using System.ComponentModel.DataAnnotations;

namespace BookStore.PL.ViewModels
{
	public class ForgetPasswordViewModel
	{

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
