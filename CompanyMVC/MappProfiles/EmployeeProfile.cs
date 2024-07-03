using AutoMapper;
using CompanyMVC.ViewModels;
using DAL.Models;

namespace CompanyMVC.MappProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
