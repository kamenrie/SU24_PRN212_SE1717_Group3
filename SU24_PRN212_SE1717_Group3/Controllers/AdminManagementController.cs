using DataAccessLayer.DAO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class AdminManagementController(AdminManagementDAO adminManagementDAO, AccountDAO accountDAO) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> OverviewManagement()
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["CountUser"] = adminManagementDAO.GetCountAccount();
            ViewData["AccountBuyMost"] = adminManagementDAO.GetAccountBuyMost();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FeedbackManagement()
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var feedback = adminManagementDAO.GetAllFeedback();
            return View(feedback);
        }

        [HttpPost]
        public async Task<IActionResult> FeedbackManagement(GeneralFeedback feedback, string response)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            adminManagementDAO.SendFeedbackResponse(feedback, response);
            return RedirectToAction("FeedbackManagement");
        }
        [HttpGet]
        public async Task<IActionResult> IgnoreFeedback(int Id)
        {
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null || account.Role.Name != "Admin")
			{
				return RedirectToAction("Index", "Home");
			}

            adminManagementDAO.IgnoreFeedback(Id);
			return RedirectToAction("FeedbackManagement");

		}
	}
}