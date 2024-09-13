using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project_DAL.Models;
using System;

namespace MVC_Project_DAL.Data.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
            builder.Property(E => E.gender).HasConversion(
            (gender) => gender.ToString(),
            (genderAsString) => (Gender)Enum.Parse(typeof(Gender), genderAsString, true)
            );
                
        }
    }
}
