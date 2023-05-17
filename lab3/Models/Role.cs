using lab3.Migrations;

namespace lab3.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; } = new HashSet<User>();
    }
}
