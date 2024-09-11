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
        private readonly IDepartmentRepository departmentRepository;
        private readonly IWebHostEnvironment env;

        public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment env) // CLR will Create object
        {
            departmentRepository = repository;
            this.env = env;
        }
        public IActionResult Index()
        {
            // GetAll calling
            var departmentIndex =  departmentRepository.GetAll();
            return View(departmentIndex);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid)
            {
                var count = departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = departmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            } 
            return View(ViewName,department);
        }


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

            return Details(id,"Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Department department )
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid)
                return View(department);

            try
            {
                departmentRepository.update(department);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
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
    }
}
