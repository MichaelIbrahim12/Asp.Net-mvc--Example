using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab3.Models
{
    public class Student
    {
            public int Id { get; set; }
        [Required(ErrorMessage = "*"), StringLength(10, MinimumLength = 3)]
        public string? Name { get; set; }
        [Range(10, 50)]
        public int Age { get; set; }
		[Required]
		[RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}")]
		[Remote("CheckEmail", "student", AdditionalFields = "Name,Age")]
		public string Email { get; set; }

		[StringLength(10, MinimumLength = 3)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Compare("Password")]
		[DataType(DataType.Password)]
		[NotMapped]
		public string CPassword { get; set; }
		public string UserName { get; set; }

		[ForeignKey("Department")]
		public int? deptId { get; set; }

		public string ImgName { get; set; }
		public virtual Department Department { get; set; }
        public virtual ICollection<StudentCourses> StudentCourses { get; set; } = new HashSet<StudentCourses>();


    }
}
