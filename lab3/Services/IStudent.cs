using lab3.Models;

namespace lab3.Services
{
	public interface IStudent
	{
		public List<Student> GetAllStudents();
		public void AddStudent(Student student);
		public Student GetStudentById(int id);
		public void Update(Student std);
		public void Delete(int id);
		public Student IsExists(string email);
	}
}
