using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Project_DAL.Models;
using MVC_Project_PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project_PL.Controllers
{
	public class UserController : Controller
	{
		
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index(string email)
		{
			if (string.IsNullOrEmpty(email))
			{
				var users = await _userManager.Users.Select(U => new UserViewModel
				{
					Id = U.Id,
					Fname = U.FName,
					Lname = U.LName,
					phoneNumber = U.PhoneNumber,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(users);
			}
			else
			{
				var user = await _userManager.FindByIdAsync(email);
				if (user != null)
				{
					var mappeduser = new UserViewModel
					{
						Id = user.Id,
						Fname = user.FName,
						Lname = user.LName,
						phoneNumber = user.PhoneNumber,
						Email = user.Email,
						Roles = await _userManager.GetRolesAsync(user),
					};
					return View(new List<UserViewModel> { mappeduser });
				}
			}

			return View(Enumerable.Empty<UserViewModel>());
		}





		//public async Task<IActionResult> Index(string email)
		//{
		//	if (string.IsNullOrEmpty(email))
		//	{
		//		// Fetch users first, without using async in Select
		//		var users = await _userManager.Users.ToListAsync();

		//		// Now asynchronously fetch roles for each user
		//		var userViewModels = new List<UserViewModel>();
		//		foreach (var user in users)
		//		{
		//			var userViewModel = new UserViewModel
		//			{
		//				Id = user.Id,
		//				Fname = user.FName,
		//				Lname = user.LName,
		//				phoneNumber = user.PhoneNumber,
		//				Email = user.Email,
		//				Roles = await _userManager.GetRolesAsync(user)  // Use await properly here
		//			};
		//			userViewModels.Add(userViewModel);
		//		}

		//		return View(userViewModels);
		//	}
		//	else
		//	{
		//		var user = await _userManager.FindByIdAsync(email);
		//		if (user != null)
		//		{
		//			var mappedUser = new UserViewModel
		//			{
		//				Id = user.Id,
		//				Fname = user.FName,
		//				Lname = user.LName,
		//				phoneNumber = user.PhoneNumber,
		//				Email = user.Email,
		//				Roles = await _userManager.GetRolesAsync(user),  // Use await
		//			};
		//			return View(new List<UserViewModel> { mappedUser });
		//		}
		//	}

		//	return View(Enumerable.Empty<UserViewModel>());
	

	}
}
