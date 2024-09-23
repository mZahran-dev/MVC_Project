using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Controllers;
using MVC_Project_DAL.Models;
using MVC_Project_PL.Helpers;
using MVC_Project_PL.ViewModels;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace MVC_Project_PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region SignUp Action
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                // Manual mapping
                var user = new ApplicationUser()
                {
                    UserName = signUpViewModel.Email.Split("@")[0],
                    Email = signUpViewModel.Email,
                    FName = signUpViewModel.FName,
                    LName = signUpViewModel.LName,
                    IsAgree = signUpViewModel.IsAgree,

                };
                var Result = await _userManager.CreateAsync(user, signUpViewModel.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn)); //SignIn Action    
                }
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(signUpViewModel);
        }
        #endregion

        #region SignIn Action

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signInViewModel.Email);
                if (user is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);
                    if (flag)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(user,signInViewModel.Password,signInViewModel.RememberMe,lockoutOnFailure:false);
                        if (Result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index),"Home");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(signInViewModel);
        }
        #endregion

        #region Sign Out
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordUrl = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email,/*token = token*/ });
                    var email = new Email()
                    {
                        Subject = "Reset Your Password",
                        Receipents = forgetPasswordViewModel.Email,
                        Body = ResetPasswordUrl

                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckInbox));    

                }
                ModelState.AddModelError(string.Empty, "Invalid");
            }
            return View(forgetPasswordViewModel);
        }



        public IActionResult CheckInbox()
        {
            return View();
        }
        #endregion

        #region Reset Password
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = await _userManager.FindByNameAsync(email);
                var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.NewPassoword);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(resetPasswordViewModel);
        } 
        #endregion
    }
}
