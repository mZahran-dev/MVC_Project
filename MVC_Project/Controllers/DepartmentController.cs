using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Hosting;
using MVC_Project_BLL.Interfaces;
using MVC_Project_BLL.Repositories;
using MVC_Project_DAL.Models;
using System;

namespace MVC_Project_PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment env;
        //private readonly IDepartmentRepository departmentRepository;

        #endregion

        #region Constructor
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env) // CLR will Create object
        {
            _unitOfWork = unitOfWork;
            this.env = env;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            // GetAll calling
            var departmentIndex = _unitOfWork.DepartmentRepository.GetAll();
            return View(departmentIndex);
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
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Save();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        #endregion

        #region Details Action
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            }
            return View(ViewName, department);
        }
        #endregion

        #region Edit Action

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (!id.HasValue)
            //{
            //    return BadRequest();
            //}
            //var department = departmentRepository.GetById(id.Value);
            //if(department == null)
            //{
            //    return NotFound();
            //}
            //return View(department);

            return Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(department);

            try
            {
                _unitOfWork.DepartmentRepository.update(department);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                // 2. Friendly Message
                // 
                if (env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Update Department");
                }

                return View(department);
            }

        }
        #endregion


        #region Delete Action
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.delete(department);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Delete Department");
                }

            }
            return View(department);
        } 
        #endregion


    }
}
