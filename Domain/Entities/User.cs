namespace Domain
{
    public class User : BaseEntity, IBaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        // RoleId เพื่อระบุบทบาทของผู้ใช้
        public int RoleId { get; set; }
        public Role Role { get; set; }

        // รายการสิทธิ์เฉพาะของ User แต่ละคน
        public ICollection<UserPermission> UserPermissions { get; set; }
    }

}
