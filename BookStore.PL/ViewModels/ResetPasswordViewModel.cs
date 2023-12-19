using System.ComponentModel.DataAnnotations;

namespace BookStore.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "New Password is Required")]
		public string newpassword { get; set; }

		[DataType(DataType.Password)]

		[Required(ErrorMessage = "Confirm Password is Required")]
		[Compare("newpassword", ErrorMessage = "Password does'mt match")]
		public string confirmpassword { get; set; }
	}
}
