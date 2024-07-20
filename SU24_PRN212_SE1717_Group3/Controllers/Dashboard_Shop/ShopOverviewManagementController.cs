using DataAccessLayer.DAO;
using Microsoft.AspNetCore.Mvc;

namespace SU24_PRN212_SE1717_Group3.Controllers.Dashboard_Shop
{
	public class ShopOverviewManagementController(ShopManagementDAO shopManagementDAO, AccountDAO accountDAO) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null || account.Role.Name != "Shop")
			{
				return RedirectToAction("Index", "Home");
			}

			ViewData["MostSoldProduct"] =  shopManagementDAO.GetMostSoldProduct();

			ViewData["ProductSold"] = shopManagementDAO.GetProductSold();
			ViewData["ProductSoldLastMonth"] = shopManagementDAO.GetProductLastMonth();
			ViewData["ProductSoldThisMonth"] = shopManagementDAO.GetProductThisMonth();

			ViewData["Revenue"] = shopManagementDAO.GetRevenue();
			ViewData["RevenueLastMonth"] = shopManagementDAO.GetRevenueLastMonth();
			ViewData["RevenueThisMonth"] = shopManagementDAO.GetRevenueThisMonth();

			ViewData["ChartProduct"] = shopManagementDAO.GetChartProduct(DateTime.Now.Year);
			ViewData["ChartRevenue"] = shopManagementDAO.GetChartRevenue(DateTime.Now.Year);
			return View("~/Views/Dashboard_Shop/ShopOverviewManagement.cshtml");
		}
	}
}
