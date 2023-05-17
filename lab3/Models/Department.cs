using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab3.Models
{
    public class Department
    {
		public int ID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Student> Students { get; set; }=new HashSet<Student>(); 
        public virtual ICollection<Courses> Courses { get; set; }=new HashSet<Courses>(); 
        
    }
}
