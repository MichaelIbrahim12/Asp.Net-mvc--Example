using lab3.Models;

namespace lab3.Services
{
	public interface IDepartment
	{
		public List<Department> GetAllDepartments();
		public void AddDepartment(Department dep);
		public Department GetDepartmentById(int id);
		public void Update(Department dep);
		public void Delete(int id);
	}
}
