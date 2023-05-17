using lab3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace lab3.Controllers
{
    public class AccountController : Controller
    {
        public ITIContext db=new ITIContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model, string returnurl)
        {

            if (ModelState.IsValid)
            {
                var user = db.User.Include(a => a.Role).FirstOrDefault(a => a.UserName == model.Name && a.Password == model.Password);

                
                if (user != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Name, user.UserName);
                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    var U = db.User.Include(a => a.Role).FirstOrDefault(a => a.UserName == model.Name);
                    var R = U.Role.Select(r => r.Id).ToList();

                    foreach(var r in R)
                    {
                        if(r == 1)
                        {
                            ci.AddClaim(new Claim(ClaimTypes.Role, "student"));
                        }else if (r == 2)
                        {
                            ci.AddClaim(new Claim(ClaimTypes.Role, "instructor"));
                        }else if(r == 3)
                        {
                            ci.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                        }
                    }
                    ClaimsPrincipal cp = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cp);
                    if (returnurl != null)
                    {
                        return LocalRedirect(returnurl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "home"); ;
                    }

                }
                else
                {
                    return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "invalid user name or password");
                return View(model);

            }



        }
        public async Task<IActionResult> Logout(LoginViewModel model, string returnurl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
