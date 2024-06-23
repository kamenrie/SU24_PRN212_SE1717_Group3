using DataAccessLayer.DAO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Utilites;
using Utilities;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
	public class HomeController(AccountDAO accountDAO, ProductDAO productDAO, FeedbackDAO feedbackDAO) : Controller
	{
		IEmailSender emailSender = new EmailSender();

		public async Task<IActionResult> Index()
		{
			var ListTop10MostSold = await productDAO.GetTop10MostSoldProduct();
			ViewData["ListTop10MostSold"] = ListTop10MostSold;
			var ListTop4New = await productDAO.GetTop4NewProduct();
			ViewData["ListTop4New"] = ListTop4New;
			return View();
		}

		[HttpGet]
		public IActionResult Contact()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Contact(string name, string email, string phone, string message, IFormFile img)
		{
			var account = await accountDAO.GetAccountByEmail(email);
			if (account == null)
			{
				ViewData["Error"] = "We cant support user that is not in our system. Please sign up";
			}
			else
			{
				string html = "<html>" +
					"Name: " + name +
					"<br/>Email: " + email +
					"<br/>Phone: " + phone +
					"<br/>" + message +
					"</html>";
				((EmailSender)emailSender).SendEmailAsync("DuyLPCE181153@fpt.edu.vn", "Email from " + email, html, ImgUtil.ConvertIFromFileToByte(img));
			}
			return View();

		}

		[HttpGet]
		public IActionResult About()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Feedback()
		{
			List<GeneralFeedback> ListFeedback = await feedbackDAO.GetAllFeedback();
			return View(ListFeedback);
		}

		[HttpPost]
		public async Task<IActionResult> Feedback(string comment)
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}
			await feedbackDAO.AddFeedback(account, comment);
			return RedirectToAction("Feedback");
		}
	}
}
