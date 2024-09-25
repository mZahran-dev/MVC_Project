using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Models;
using MVC_Project_PL.Helpers;
using MVC_Project_PL.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MVC_Project_PL.Controllers
{
	//[Authorize]
	public class EmployeeController : Controller
    {

        #region Properties
        //private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IDepartmentRepository _departmentRepository; 
        #endregion

        #region Constructor
        public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper)
        {       
            _env = env;
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion  

        #region Index
        //[HttpGet]
        public IActionResult Index(string SearchInput)
        {
            if (string.IsNullOrEmpty(SearchInput))
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll();
                var mappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
                
                return View(mappedEmp);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchInput);
                var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                
                return View(mappedEmp);
            }

        } 
        #endregion

        #region Create Action
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVm)
        {
            if (ModelState.IsValid)
            {
                // manual mapping
                //var mappedEmp = new Employee()
                //{
                //    Name = employeeVm.Name,
                //    Address = employeeVm.Address,
                //    Age = employeeVm.Age,
                //    Salary = employeeVm.Salary,
                //    Email = employeeVm.Email,
                //    HireDate = employeeVm.HireDate,
                //    isActive = employeeVm.isActive,
                //    PhoneNumber = employeeVm.PhoneNumber,

                //};
                employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                var count = _unitOfWork.Save(); // SaveChanges
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employeeVm);
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
            var emp = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (emp == null)
            {
                return NotFound();
            }
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);  
            return View(ViewName, mappedEmp);
        } 
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeVm)
        {
            if (id != employeeVm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(employeeVm);

            try
            {
                employeeVm.ImageName = DocumentSettings.UploadFile(employeeVm.Image, "Images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.update(mappedEmp);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Update Employee");
                }
                return View(employeeVm);
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
        public IActionResult Delete(EmployeeViewModel employeeVm)
        {
            try
            {
                var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVm);
                _unitOfWork.EmployeeRepository.delete(mappedEmployee);
                _unitOfWork.Save();
                DocumentSettings.DeleteFile(employeeVm.ImageName, "Images");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Delete Employee");
                }
                return View(employeeVm);
            }
        }
        #endregion
     

    }
}
