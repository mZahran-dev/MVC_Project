using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_DAL.Models;
using MVC_Project_PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MVC_Project_PL.Controllers
{
    public class RoleController : Controller
	{
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper) 
        {
           _roleManager = roleManager;
            _mapper = mapper;
        }
        #region Index
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var Roles = await _roleManager.Roles.Select(U => new RoleViewModel
                {
                    id = U.Id,
                    RoleName = U.Name,
                }).ToListAsync();
                return View(Roles);
            }
            else
            {
                var Role = await _roleManager.FindByIdAsync(email);
                if (Role != null)
                {
                    var mappeduser = new RoleViewModel
                    {
                        id = Role.Id,
                        RoleName = Role.Name,
                    };
                    return View(new List<RoleViewModel> { mappeduser });
                }
            }

            return View(Enumerable.Empty<RoleViewModel>());
        }

        #endregion

        #region Create Action
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedRole =  _mapper.Map<RoleViewModel,IdentityRole>(roleViewModel);
                var result = await _roleManager.CreateAsync(mappedRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(roleViewModel);
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
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
            {
                return NotFound();
            }
            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(ViewName, mappedRole);
        }
        #endregion

        #region Edit Action
        [HttpGet]
        public Task<IActionResult> Edit(string id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(roleViewModel);

            try
            {
                var Role = await _roleManager.FindByIdAsync(id);
                Role.Id = roleViewModel.id;
                Role.Name = roleViewModel.RoleName;
                await _roleManager.UpdateAsync(Role);
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
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RoleViewModel roleViewModel )
        {
            try
            {
                var user = await _roleManager.FindByIdAsync(roleViewModel.id);
                await _roleManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
