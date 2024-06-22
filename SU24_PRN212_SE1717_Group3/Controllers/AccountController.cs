using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAO;
using DataAccessLayer.Models;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class AccountController(AccountDAO accountDAO, FeedbackDAO feedbackDAO) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            int? accountId = HttpContext.Session.GetInt32("accountId");
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
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account != null)
            {
                accountDAO.UpdateProfile(pro, img, account);
                return RedirectToAction("Profile");
            }
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> MyFeedback()
        {
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}
            var ListFeedback = await feedbackDAO.GetAllFeedbackByAccount(account);
            return View("~/Views/Home/Feedback.cshtml", ListFeedback);
		}
       
    }
}
