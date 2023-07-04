namespace UserManagement.Models.DbEntities
{
    public class User: ChangeTrackingBase
    {
        public User()
        {
            WorkItems = new List<WorkItem>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string EMail { get; set; } = null!;

        public int? UserGroupId { get; set; }
        public virtual UserGroup? UserGroup { get; set; }

        public virtual ICollection<WorkItem> WorkItems { get; set; }
    }
}
