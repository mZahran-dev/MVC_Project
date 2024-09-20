using AutoMapper;
using MVC_Project_DAL.Models;
using MVC_Project_PL.ViewModels;

namespace MVC_Project_PL.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap()/*.ForMember(D=>D.Name, M=>M.MapFrom(s=>s.Name))*/;
        }

    }
}
