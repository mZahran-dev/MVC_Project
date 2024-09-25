using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MVC_Project_DAL.Models;
using MVC_Project_PL.ViewModels;

namespace MVC_Project_PL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap()/*.ForMember(D=>D.Name, M=>M.MapFrom(s=>s.Name))*/;
            CreateMap<ApplicationUser, UserViewModel>().ForMember(d=>d.Fname,o=>o.MapFrom(d=>d.UserName)).ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>().ForMember(d=>d.RoleName,o=>o.MapFrom(d=>d.Name)).ReverseMap();
        }

    }
}
