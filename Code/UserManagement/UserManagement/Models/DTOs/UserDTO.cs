namespace UserManagement.Models.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public int? GroupId { get; set; }
    }
}
