namespace UserManagement.Models.DbEntities
{
    public class Project: ChangeTrackingBase
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }


    }
}
