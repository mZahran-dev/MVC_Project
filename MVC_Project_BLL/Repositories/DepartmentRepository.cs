﻿using Microsoft.EntityFrameworkCore;
using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Data;
using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository

    {
        //private readonly AppDbContext dbContext;
        public DepartmentRepository(AppDbContext _dbContext) :base(_dbContext)
        {
        }
    }
}
