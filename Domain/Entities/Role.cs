namespace Domain
{
    public class Role : BaseEntity, IBaseEntity
    {
        public int RoleId { get; set; } 
        public string RoleName { get; set; }
    }
}
