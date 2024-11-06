namespace Domain
{
    public class Permission : BaseEntity, IBaseEntity
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
    }
}
