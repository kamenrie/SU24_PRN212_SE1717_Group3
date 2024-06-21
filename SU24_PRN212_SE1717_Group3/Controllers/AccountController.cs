using Microsoft.AspNetCore.Mvc;
using SU24_PRN212_SE1717_Group3.Dao;
using SU24_PRN212_SE1717_Group3.Models;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class AccountController(AccountDAO accountDAO) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int? accountId = HttpContext.Session.GetInt32("accountid");
            var account = await accountDAO.GetAccountById(accountId);
            if (account != null)
            {
                return View("Profile", account.Profile);
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public async Task<IActionResult> Profile(Profile pro, IFormFile img)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountid"));
            if (account != null)
            {
                accountDAO.UpdateProfile(pro, img, account);
                return RedirectToAction("Profile");
            }
            return RedirectToAction("Login", "Auth");
           
        }
       
    }
}
