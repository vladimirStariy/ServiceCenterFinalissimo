using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceCenter.Domain.Entity
{
    public class Employee
    {
        public uint Employee_ID { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        
        public uint? User_ID { get; set; }
        [ForeignKey("User_ID")]
        public virtual User User { get; set; }
    }
}
