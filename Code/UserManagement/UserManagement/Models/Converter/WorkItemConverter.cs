using AutoMapper;
using System.Diagnostics.Tracing;
using UserManagement.Models.DbEntities;
using UserManagement.Models.DTOs;

namespace UserManagement.Models.Converter
{
    public class WorkItemConverter : ITypeConverter<WorkItem, WorkItemDTO>, ITypeConverter<WorkItemDTO, WorkItem>
    {
        public WorkItemDTO Convert(WorkItem source, WorkItemDTO destination, ResolutionContext context)
        {
            return new()
            {
                Id = source.Id,
                DueDate = source.DueDate,
                TimeRemaining = DateTime.UtcNow - source.DueDate,
                PlannedStartDate = source.MinStartDate + TimeSpan.FromMinutes(10),
                EstimatedTimeEffort = source.EstimatedTimeEffort,
                Description = source.Description,
                Worker = context.Mapper.Map<UserDTO>(source.User),
            };
        }

        public WorkItem Convert(WorkItemDTO source, WorkItem destination, ResolutionContext context)
        {
            return new WorkItem()
            {
                Id = source.Id,
            };
        }
    }
}
