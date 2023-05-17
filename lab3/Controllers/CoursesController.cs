using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab3.Models;

namespace lab3.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ITIContext _context=new ITIContext();

/*        public CoursesController(ITIContext context)
        {
            _context = context;
        }*/

        // GET: Courses
        public async Task<IActionResult> Index()
        {
              return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Crs_Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Crs_Id,Crs_Name,Lect_Hours,Lab_Hours")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Crs_Id,Crs_Name,Lect_Hours,Lab_Hours")] Courses courses)
        {
            if (id != courses.Crs_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesExists(courses.Crs_Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courses);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var courses = await _context.Courses
                .FirstOrDefaultAsync(m => m.Crs_Id == id);
            if (courses == null)
            {
                return NotFound();
            }

            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'ITIContext.Courses'  is null.");
            }
            var courses = await _context.Courses.FindAsync(id);
            if (courses != null)
            {
                _context.Courses.Remove(courses);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesExists(int id)
        {
          return _context.Courses.Any(e => e.Crs_Id == id);
        }
    }
}
