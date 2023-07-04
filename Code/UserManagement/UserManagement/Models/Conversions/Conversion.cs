using AutoMapper;
using System.Linq.Expressions;
using UserManagement.Models.Converter;
using UserManagement.Models.DbEntities;
using UserManagement.Models.DTOs;

namespace UserManagement.Models.Conversions
{
    public static class Conversion
    {
        internal static Mapper Mapper => _mapper;

        private static readonly MapperConfiguration _mapperConfiguration = new(e =>
        {
            e.AddProfile<ProjectProfile>();

            e.CreateMap<User, UserDTO>()
                .ForMember(u => u.Fullname, u => u.MapFrom(u => $"{u.FirstName} {u.LastName}"))
                .ForMember(u => u.Username, u => u.MapFrom(u => u.EMail))
                .ForMember(u => u.GroupId, u => u.MapFrom(u => u.UserGroupId))
                .ReverseMap()
                .ForMember(u => u.FirstName, u => u.MapFrom(u => u.Fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries).First()))
                .ForMember(u => u.LastName, u => u.MapFrom(u => u.Fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last()))
                .ForMember(u => u.UserGroupId, u => u.MapFrom(u => u.GroupId));

            e.CreateMap<UserGroup, UserGroupDTO>();
            //e.CreateMap<WorkItem, WorkItemDTO>().ConvertUsing<WorkItemConverter>();
            //e.CreateMap<WorkItemDTO, WorkItem>().ConvertUsing<WorkItemConverter>();

            e.CreateMap<WorkItem, WorkItemDTO>()
                .ForMember(w => w.TimeRemaining, p => p.Ignore())
                //.ForMember(w => w.TimeRemaining, p => p.MapFrom(w => DateTime.UtcNow - w.DueDate))
                .ForMember(w => w.PlannedStartDate, p => p.MapFrom(w => w.MinStartDate /* + TimeSpan.FromMinutes(10)*/))
                .ForMember(w => w.EstimatedTimeEffort, p => p.MapFrom(w => w.EstimatedTimeEffort))
                .ForMember(w => w.Worker, p => p.MapFrom(w => w.User)).ReverseMap();
        });
        private static readonly Mapper _mapper = new Mapper(_mapperConfiguration);

        public static ProjectDTO ToDTO(this Project entity) => _mapper.Map<ProjectDTO>(entity);

        public static void AddMapper(this IServiceCollection services) => services.AddSingleton(_mapper);

        public static UserDTO ToDTO(this User user) => _mapper.Map<UserDTO>(user);
        public static User ToModel(this UserDTO dto) => _mapper.Map<User>(dto);

        public static WorkItemDTO ToDTO(this WorkItem entity) => _mapper.Map<WorkItemDTO>(entity);
        public static WorkItem ToModel(this WorkItemDTO dto) => _mapper.Map<WorkItem>(dto);
    }
}
