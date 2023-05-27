using ServiceCenter.Domain.Enum;

namespace ServiceCenter.Domain.Entity
{
    public class User
    {
        public uint User_ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
