using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using MVC_Project_BLL.Interfaces;
using MVC_Project_BLL.Repositories;

namespace MVC_Project_PL.Extentions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
