using AutoMapper;
using CompanyMVC.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CompanyMVC.MappProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewModel>().ForMember(o => o.RoleName, s => s.MapFrom(s => s.Name)).ReverseMap();
        }
    }
}
