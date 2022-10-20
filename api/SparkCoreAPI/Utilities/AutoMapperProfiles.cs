using AutoMapper;
using DataLayer.DTOs;
using DataLayer.Entities;

namespace SparkCoreAPI.Utilities
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Employee, EmployeeShortDTO>();
            CreateMap<Employee, EmployeeLongDTO>();
            CreateMap<EmployeeLongDTO, Employee>();
            CreateMap<Employee, AccountInfoShort>();
            CreateMap<Employee, UserInfoDTO>(MemberList.Destination).ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Email))
                                                                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Users.FirstOrDefault().Username))
                                                                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.Users.FirstOrDefault().IsActive));

            CreateMap<User, UserInfoDTO>(MemberList.Destination).ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
                                                                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
                                                                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));
            CreateMap<UserCreationDTO, User>();
            

            CreateMap<Bank, BanksDropDownItemDTO>(MemberList.Destination).ForMember(dest => dest.Value,   opt => opt.MapFrom(src => src.BankId))
                                                                         .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.BankName));

            CreateMap<JobTitle, DropDownItemDTO>(MemberList.Destination).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.JobId))
                                                                        .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.Title));

            CreateMap<Site, DropDownItemDTO>(MemberList.Destination).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.SiteId))
                                                                    .ForMember(dest => dest.Display, opt => opt.MapFrom(src => src.SiteName));

            CreateMap<Employee, DropDownItemDTO>(MemberList.Destination).ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Workday))
                                                                    .ForMember(dest => dest.Display, opt => opt.MapFrom(src => $"{ src.FirstName } {src.LastName}"));

            CreateMap<Role, RoleShortDTO>(MemberList.Destination).ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => $"{src.Creator.Employee.FirstName} {src.Creator.Employee.LastName}"))
                                                                 .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => $"{src.Modifier.Employee.FirstName} {src.Modifier.Employee.LastName}"));
        }
    }
}
