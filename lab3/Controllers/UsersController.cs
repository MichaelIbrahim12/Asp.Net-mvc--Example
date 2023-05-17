using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab3.Models;
using System.Runtime.Intrinsics.Arm;

namespace lab3.Controllers
{
    public class UsersController : Controller
    {
        private ITIContext _context = new ITIContext();

/*        public UsersController(ITIContext context)
        {
            _context = context;
        }*/

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var roles = _context.Role; 
            ViewBag.roles = new SelectList(roles.ToList(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password")] User user, int[] rolesid)
        {
            if (ModelState.IsValid)
            {
/*                var userr=_context.User.Include(a=>a.Role).FirstOrDefault(a=>a.Id==user.Id);
*/                _context.Add(user);
                if (rolesid.Length > 0)
                {
                    foreach (var item in rolesid) 
                    {
                        var ro = _context.Role.FirstOrDefault(a => a.Id == item);
/*                        userr.Role.Add(ro);*/
                        user.Role.Add(ro);
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var User = _context.User.Include(a => a.Role).FirstOrDefault(a => a.Id == id);
            var allroles = _context.Role.ToList();
            var rolesInUser = User.Role.ToList();
            var rolesNotInUser = allroles.Except(rolesInUser).ToList();
            ViewBag.rolesInUser = new SelectList(rolesInUser, "Id", "Name");
            ViewBag.rolesNotInUser = new SelectList(rolesNotInUser, "Id", "Name");
            ViewBag.userId = User.Id;

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,UserName,Password")] User user, int[] rolesToAdd, int[] rolesToRemove)
        {
            if (Id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    foreach (var item in rolesToRemove)
                    {
                        var role = _context.Role.FirstOrDefault(a => a.Id == item);
                        user.Role.Remove(role);
                    }
                    foreach (var item in rolesToAdd)
                    {
                        var role = _context.Role.FirstOrDefault(a => a.Id == item);
                        user.Role.Add(role);
                    }

                    _context.User.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'ITIContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return _context.User.Any(e => e.Id == id);
        }
    }
}
