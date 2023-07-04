namespace UserManagement.Models.DTOs
{
    public class UserGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserDTO> Users { get; set; }
    }
}
