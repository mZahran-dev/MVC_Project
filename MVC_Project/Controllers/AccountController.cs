using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project.Controllers;
using MVC_Project_DAL.Models;
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
    }
}
