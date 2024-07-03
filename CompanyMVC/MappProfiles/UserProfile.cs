using AutoMapper;
using CompanyMVC.ViewModels;
using DAL.Models;
using System.Runtime.InteropServices;

namespace CompanyMVC.MappProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
