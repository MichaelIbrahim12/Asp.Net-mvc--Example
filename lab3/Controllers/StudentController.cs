using lab3.Models;
using lab3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab3.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        IStudent st;
        IDepartment dp;
        ITIContext db=new ITIContext();

        public StudentController( IStudent _st , IDepartment _dp)

        {
            st= _st;
            dp= _dp;
        }
        public IActionResult Index()
        {
            //cookies
            int i = 1;
            string name = "michael";
            Response.Cookies.Append("Id", i.ToString());
            Response.Cookies.Append("Name", name);
            //sessions
            HttpContext.Session.SetInt32("Session ID", i);
            HttpContext.Session.SetString("Session Name", name + " " + "session");

            return View(st.GetAllStudents());
        }

        //Test cookies & session
        public IActionResult Test()
        {
            int i = int.Parse(Request.Cookies["Id"]);
            string name = Request.Cookies["Name"];

            int x = HttpContext.Session.GetInt32("Session ID").Value;
            string y = HttpContext.Session.GetString("Session Name");

            return Content($" Id:{i} , Name:{name}/ n Id: {x}, Name: {y}  "); //  
        }
        [HttpGet]
        public IActionResult Create()
        { 
            ViewBag.depts= new SelectList(dp.GetAllDepartments().ToList(), "ID", "Name"); 

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Create(Student stud,IFormFile stdimg)
        {
            //if(stdimg.Length > 25)
            //{
            //    ModelState.AddModelError("ImgName", "Check File Size");
            //}
           /* else*/ if (ModelState.IsValid)
            {
                st.AddStudent(stud);
                string imgName = stud.Id +"."+ stdimg.FileName.Split(".").Last();
                using(var obj=new FileStream(@".\wwwroot\images\"+imgName,FileMode.Create))
                {
                    await stdimg.CopyToAsync(obj);
                    stud.ImgName= imgName;
                    st.Update(stud);
                }
                return RedirectToAction("Index");
            }
            ViewBag.depts = new SelectList(dp.GetAllDepartments().ToList(), "ID", "Name",stud.Department.ID);
            return View(stud);
        }
        public IActionResult Download()
        {
            return File("images/lect1.pdf", "application/pdf","File1.pdf");
        }
        public IActionResult CheckEmail(string Email, string Name, int Age)
        {
            if (st.IsExists(Email) != null)
            {
                return Json("you can use email" + Name + Age + "@iti.gov.eg");
            }
            else
                return Json(true);
        }
        public IActionResult Details(int? id)
        {

            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Student student = st.GetStudentById(id.Value);
                if (student is null)
                {
                    return NotFound();
                }
                else
                {
                    return View(student);
                }
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                Student student = st.GetStudentById(id.Value);
                if (student is null)
                {
                    return NotFound();
                }
                else
                {
                    ViewBag.depts = new SelectList(dp.GetAllDepartments().ToList(), "ID", "Name", student.Department.ID);
                    return View(student);
                }
            }

        }
        [HttpPost]
        public IActionResult Edit(Student stud)
        {
            st.Update(stud);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student stud = st.GetStudentById(id);
            return View(stud);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            st.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
