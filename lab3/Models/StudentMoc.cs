using lab3.Services;
using Microsoft.EntityFrameworkCore;

namespace lab3.Models
{
    
    public class StudentMoc:IStudent
    {
        ITIContext db;

        public StudentMoc(ITIContext _db )
        {
            db= _db;
        }

        public List<Student> GetAllStudents()
        {
            return db.Students.Include(a=>a.Department).ToList();
        }
        public void AddStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }
        public Student GetStudentById(int id)
        {

			return db.Students.Include(a=>a.Department).FirstOrDefault(a => a.Id == id);
        }
        public void Update(Student std)
        {
            Student old = db.Students.FirstOrDefault(a => a.Id == std.Id);
            old.Name = std.Name;
            old.Age = std.Age;
            old.Email = std.Email;
            old.deptId = std.deptId;
            old.Password = std.Password;

            db.SaveChanges();
            
        }
        public void Delete(int id)
        {
            Student old = db.Students.FirstOrDefault(a => a.Id ==id);
            db.Students.Remove(old);
            db.SaveChanges() ;
        }
        public Student IsExists(string email)
        {
            return db.Students.FirstOrDefault(a => a.Email == email);
        }
    }
}
