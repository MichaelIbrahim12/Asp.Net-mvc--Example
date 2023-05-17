namespace lab3.Models
{
    public class Courses
    {
        public int Crs_Id { get; set; }
        public string Crs_Name { get; set; }
        public int Lect_Hours { get; set; }
        public int Lab_Hours { get; set; }

        public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<StudentCourses> StudentCourses { get; set; } = new HashSet<StudentCourses>();

    }
}
