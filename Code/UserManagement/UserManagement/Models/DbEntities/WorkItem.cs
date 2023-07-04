namespace UserManagement.Models.DbEntities
{
    public class WorkItem: ChangeTrackingBase
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime MinStartDate { get; set; }

        public TimeSpan EstimatedTimeEffort { get; set; }

        public string? Description { get; set; }

        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
