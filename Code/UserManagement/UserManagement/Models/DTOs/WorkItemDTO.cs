namespace UserManagement.Models.DTOs
{
    public class WorkItemDTO
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public TimeSpan TimeRemaining { get; set; }

        public DateTime PlannedStartDate { get; set; }

        public TimeSpan EstimatedTimeEffort { get; set; }

        public string? Description { get; set; }

        public UserDTO Worker { get; set; }
    }
}
