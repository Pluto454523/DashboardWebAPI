namespace Domain
{
    public class UserPermission : BaseEntity, IBaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        // กำหนดสิทธิ์แยกสำหรับผู้ใช้ใน Permission นั้นๆ
        public bool IsReadable { get; set; }
        public bool IsDeletable { get; set; }
        public bool IsWritable { get; set; }
    }
}
