using BookStore.DAL.Models;
using BookStore.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using BookStore.PL.Helpers;

namespace BookStore.PL.Areas.Identity.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _usermanger;
		private readonly SignInManager<ApplicationUser> _signmanger;

		public AccountController(UserManager<ApplicationUser> usermanger, SignInManager<ApplicationUser> signmanger)
		{
			_usermanger = usermanger;
			_signmanger = signmanger;
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid) // Server Side Validation
			{
				var User = new ApplicationUser()
				{
					UserName = model.Email.Split("@")[0],
					Fname = model.FName,
					Lname = model.LName,
					Email = model.Email,
					IsAgree = model.IsAgree,
				};
				var Result = await _usermanger.CreateAsync(User, model.Password);
				if (Result.Succeeded)
					return RedirectToAction("Login");
				else
					foreach (var error in Result.Errors)
						Console.WriteLine(error.Description);
			}
			return View(model);
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _usermanger.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var flag = await _usermanger.CheckPasswordAsync(User, model.Password);
					if (flag)
					{
						var Result = await _signmanger.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
						if (Result.Succeeded) return RedirectToAction("Index", "Home", new {area = "Customer"});
					}
					else
						ModelState.AddModelError(string.Empty, "Password is not Correct");
				}
				else
					ModelState.AddModelError(string.Empty, "Email is Not Found");
			}
			return View();
		}

		public new async Task<IActionResult> SignOut()
		{
			await _signmanger.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var User = await _usermanger.FindByEmailAsync(model.Email);
				if (User is not null)
				{
					var token = await _usermanger.GeneratePasswordResetTokenAsync(User); // valid for one time for this user

					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = model.Email, Token = token }, Request.Scheme);
					var email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = ResetPasswordLink,
					};
					EmailSetting.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				else
					ModelState.AddModelError(string.Empty, "Email is not Existed");
			}
			return View(nameof(ForgetPassword), model);

		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}

		public IActionResult ResetPassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				string email = TempData["email"] as string;
				string token = TempData["token"] as string;
				var User = await _usermanger.FindByEmailAsync(email);
				var result = await _usermanger.ResetPasswordAsync(User, token, model.newpassword);
				if (result.Succeeded)
					return RedirectToAction(nameof(Login));
				else
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}

	}
}
