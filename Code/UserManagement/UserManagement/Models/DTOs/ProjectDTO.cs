namespace UserManagement.Models.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? LongDescription { get; set; }
    }
}
