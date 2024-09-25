using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVC_Project_DAL.Models;
using MVC_Project_PL.Helpers;
using MVC_Project_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Project_PL.Controllers
{
	public class UserController : Controller
	{
		
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;
		private readonly IHostEnvironment _env;

		public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IHostEnvironment env)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_mapper = mapper;
			_env = env;
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
  

        #region Create Action
        [HttpGet]
		public async Task<IActionResult> Create()
		{
			return  View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserViewModel userViewModel)
		{
			if (ModelState.IsValid)
			{	
				var mappedUser = _mapper.Map<UserViewModel,ApplicationUser>(userViewModel);
				var result = await _userManager.CreateAsync(mappedUser);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(userViewModel);
		}

		#endregion

		#region Details Action
		[HttpGet]
		public async Task<IActionResult> Details(string id, string ViewName = "Details")
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest();
			}
			var user = await _userManager.FindByIdAsync(id);
			if (user is null)
			{
				return NotFound();
			}
			var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
			return View(ViewName, mappedUser);
		}
		#endregion

		#region Edit Action
		[HttpGet]
		public  Task<IActionResult> Edit(string id)
		{
			return  Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userViewModel)
		{
			if (id != userViewModel.Id)
			{
				return BadRequest();
			}
			if (!ModelState.IsValid)
				return View(userViewModel);

			try
			{
				var user = await _userManager.FindByIdAsync(id);
				user.FName = userViewModel.Fname;
				user.PhoneNumber = userViewModel.phoneNumber;
				await _userManager.UpdateAsync(user);
				return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}

		#endregion

		#region Delete Action
		[HttpGet]

		public Task<IActionResult> Delete(string id)
		{
			return  Details(id, "Delete");
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(UserViewModel userViewModel)
		{
			try
			{
				var user = await _userManager.FindByIdAsync(userViewModel.Id);
				await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
        #endregion


        #region Another Approach of Index Action

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
        #endregion


    }
}
