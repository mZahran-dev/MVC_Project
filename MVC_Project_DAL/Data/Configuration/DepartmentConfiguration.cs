using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL.Data.Configuration
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIS
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
           
            //specify Relationship
            builder.HasMany(d => d.Employees)
                    .WithOne(E => E.department)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
