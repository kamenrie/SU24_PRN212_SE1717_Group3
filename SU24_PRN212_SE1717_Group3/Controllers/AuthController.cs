using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAO;
using DataAccessLayer.Models;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class AuthController(AuthDao authDAO) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( string email ,string pass)
        {
            var acc = await authDAO.Login(email, pass); 
            
            if (acc == null)
            {
                TempData["AuthError"] = "Wrong username or password";
                return RedirectToAction("Login", "Auth");
            }
            HttpContext.Session.SetInt32("accountid", acc.Id);
            if (acc.Role.Name == "Admin")
            {
				return RedirectToAction("Index", "Home");
			} else if (acc.Role.Name == "Shop")
            {
				return RedirectToAction("Index", "Home");
			} else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account acc) 
        {
            
            await authDAO.CreateAccount(acc); 
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Logout() 
        {
            HttpContext.Session.Remove("accountid");
         
            return RedirectToAction("Index", "Home");
        }
        

    }
}
