namespace UserManagement.Models.DbEntities
{
    /// <summary>
    /// This is the base class, used for change tracking inside the databse.
    /// It exposes 4 properties: CreatedAt, ModifiedAt, CreatedBy, ModifiedBy.
    /// These properties will be set automatically by the DbContext.
    /// </summary>
    public abstract class ChangeTrackingBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? ModifiedBy { get; set; }
    }
}
