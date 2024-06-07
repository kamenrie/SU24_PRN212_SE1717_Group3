using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.Dao;
using SU24_PRN212_SE1717_Group3.DataAccess;
using SU24_PRN212_SE1717_Group3.Models;

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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Account acc) 
        {
            
            await authDAO.createAccount(acc); 
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Logout() 
        {
            HttpContext.Session.Remove("accountid");
         
            return RedirectToAction("Index", "Home");
        }
        

    }
}
