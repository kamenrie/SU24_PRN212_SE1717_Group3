using DataAccessLayer.DAO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using static NuGet.Packaging.PackagingConstants;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class AdminManagementController(AdminManagementDAO adminManagementDAO,
                                           AccountDAO accountDAO) : Controller
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

        [HttpGet]
        public async Task<IActionResult> OrderManagement()
        {

            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var listOrder = adminManagementDAO.GetAllWaitingOrder();

            return View(listOrder);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptOrder(int id)
        {

            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            adminManagementDAO.AcceptOrder(id);

            return RedirectToAction("OrderManagement");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseOrder(int id)
        {

            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            adminManagementDAO.RefuseOrder(id);

            return RedirectToAction("OrderManagement");
        }

        [HttpGet]
        public async Task<IActionResult> AcceptAllOrder()
        {

            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            adminManagementDAO.AcceptAllOrders();

            return RedirectToAction("OrderManagement");
        }

        [HttpGet]
        public async Task<IActionResult> RefuseAllOrder()
        {

            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            adminManagementDAO.RefuseAllOrders();

            return RedirectToAction("OrderManagement");
        }

        [HttpGet]
        public async Task<IActionResult> DiscountManagement()
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var list = adminManagementDAO.GetAllDiscount();
            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> SearchDiscount(string discountName)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            if (string.IsNullOrEmpty(discountName))
            {
                return RedirectToAction("DiscountManagement");
            }

            var list = adminManagementDAO.GetAllDiscountBySearchName(discountName);
            return View("DiscountManagement", list);
        }

        [HttpGet]
        public async Task<IActionResult> AddDiscount()
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscount(Discount discount)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            discount.Validity = true;
            adminManagementDAO.AddDiscount(discount);
            return RedirectToAction("DiscountManagement");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            var discount = adminManagementDAO.GetDiscountById(id);
            return View(discount);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(Discount discount)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            adminManagementDAO.UpdateDiscount(discount);
            return RedirectToAction("DiscountManagement");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            var discount = adminManagementDAO.GetDiscountById(id);
            adminManagementDAO.DeleteDiscount(discount);
            return RedirectToAction("DiscountManagement");
        }
    }
}