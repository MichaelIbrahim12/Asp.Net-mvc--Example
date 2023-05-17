using lab3.Models;
using lab3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace lab3.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartment dp;
        ITIContext data=new ITIContext();


        public DepartmentController( IDepartment _dp)
        {
            dp= _dp;
        }
        [Authorize(Roles = "instructor")]
        [HttpPost]
        public IActionResult UpdatStudentDegree(int id, int crsId, Dictionary<int,int> stdDegree)
        {
            foreach(var item in stdDegree)
            {
                var std=data.StudentCourses.FirstOrDefault(a=>a.CrsId==crsId && a.StudentId==item.Key);

                if (std == null)
                {
                    var stddeg = new StudentCourses() {CrsId= crsId, StudentId=item.Key,Degree=item.Value };
                    data.StudentCourses.Add(stddeg);
                }
                else if(std!=null)
                {
                    std.Degree = item.Value;
                }
                
            }
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UpdatStudentDegree(int id, int crsId)
        {
            var model = data.Students.Include(a=>a.StudentCourses).Where(a => a.deptId == id).ToList();
            ViewBag.crsid = crsId;
            return View(model);
        }
        public IActionResult UpdateCourses(int ID)
        {
            var dept = (from d in data.Departments where d.ID == ID select d).Include("Courses").FirstOrDefault();

            var allCourses = data.Courses.ToList();
            var deptCourses = dept.Courses.ToList();
            var depNotCourses = allCourses.Except(deptCourses).ToList();

            ViewBag.In = new SelectList(deptCourses, "Crs_Id", "Crs_Name"); ;
            ViewBag.Out=new SelectList(depNotCourses, "Crs_Id", "Crs_Name");
            ViewBag.id = ID;
            return View() ;
        }
        [HttpPost]
        public IActionResult UpdateCourses(int ID, int[] Add, int[] Remove)
        {
            var dept=data.Departments.FirstOrDefault(a=>a.ID==ID);
            
            foreach(var item in Add)
            {
                var a= (from d in data.Courses where d.Crs_Id==item select d).FirstOrDefault();
                dept.Courses.Add(a);
            }           
            foreach(var item in Remove)
            {
                var a= (from d in data.Courses where d.Crs_Id==item select d).FirstOrDefault();
                dept.Courses.Remove(a);
            }
            data.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ShowCourses( int ID)
        {
            var dept = data.Departments.Include(a => a.Courses).FirstOrDefault(a=>a.ID==ID);

            return View(dept);
        }
            public IActionResult Index()
        { 
            return View(dp.GetAllDepartments());
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dep)
        {
            dp.AddDepartment(dep);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Department dep =dp.GetDepartmentById(id.Value);
                if (dep is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(dep);
                }
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Department dep = dp.GetDepartmentById(id.Value);
                if (dep is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(dep);
                }
            }

        }
        [HttpPost]
        public IActionResult Edit(Department dep)
        {
            dp.Update(dep);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Department dep = dp.GetDepartmentById(id);
            return View(dep);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            dp.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
