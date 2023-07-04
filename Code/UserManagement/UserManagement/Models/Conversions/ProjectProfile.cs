using AutoMapper;
using UserManagement.Models.DbEntities;
using UserManagement.Models.DTOs;

namespace UserManagement.Models.Conversions
{
    public class ProjectProfile: Profile
    {
        public ProjectProfile() 
        { 
            CreateMap<Project, ProjectDTO>()
                .ForMember(p => p.Title, p => p.MapFrom(p => p.Name))
                .ForMember(p => p.LongDescription, p => p.MapFrom(p => p.Description))
                .ReverseMap();
        }
    }
}
