using lab3.Services;
using System.Runtime.Intrinsics.Arm;

namespace lab3.Models
{
    public class DepartmentMoc:IDepartment
    {
        ITIContext db;
        public DepartmentMoc( ITIContext _db)
        {
            db = _db;
        }
        public List<Department> GetAllDepartments()
        {
            return db.Departments.ToList();
        }
        public void AddDepartment(Department dep)
        {
            db.Departments.Add(dep);
            db.SaveChanges();
        }
        public Department GetDepartmentById(int id)
        {
            

             return db.Departments.FirstOrDefault(a => a.ID == id); ;
        }
        public void Update(Department dep)
        {
			Department oldd = db.Departments.FirstOrDefault(a => a.ID == dep.ID);
			oldd.Name = dep.Name;
			oldd.Capacity = dep.Capacity;
			db.SaveChanges();
		}
        public void Delete(int id)
        {
            Department old = db.Departments.FirstOrDefault(a => a.ID == id);
            db.Departments.Remove(old);
            db.SaveChanges();
        }
    }
}
