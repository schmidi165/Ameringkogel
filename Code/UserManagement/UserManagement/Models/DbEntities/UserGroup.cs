namespace UserManagement.Models.DbEntities
{
    public class UserGroup: ChangeTrackingBase
    {
        public UserGroup()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
