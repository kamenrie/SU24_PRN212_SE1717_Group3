using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAO;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
	public class AuthController(AuthDao authDAO, AccountDAO accountDAO, IEmailSender emailSender) : Controller
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
		public async Task<IActionResult> Login(string email, string pass)
		{
			var acc = await authDAO.Login(email, pass);

			if (acc == null)
			{
				TempData["AuthError"] = "Wrong username or password";
				return RedirectToAction("Login", "Auth");
			}
			HttpContext.Session.SetInt32("accountId", acc.Id);
			if (acc.Role.Name == "Admin")
			{
				return RedirectToAction("OverviewManagement", "AdminManagement");
			}
			else if (acc.Role.Name == "Shop")
			{
				return RedirectToAction("Index", "ShopOverviewManagement");
			}
			else
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

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("accountId");
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(string email)
		{
			var account = await accountDAO.GetAccountByEmail(email);
			if (account == null)
			{
				TempData["Error"] = "This email is not in our system";
				return RedirectToAction("Login");
			}

			Response.Cookies.Append("Email", email, new CookieOptions { Expires = DateTime.Now.AddMinutes(5) });

			return RedirectToAction("OtpVerify");
		}

		[HttpGet]
		public async Task<IActionResult> OtpVerify()
		{
			var email = Request.Cookies["Email"];
			if (email == null)
			{
				TempData["Error"] = "Email is expired. Please send again";
				return RedirectToAction("Login");
			}

			string OTP = new Random().Next(1000000).ToString("D6");
			await emailSender.SendEmailAsync(email, "LiuBiu Shop", "Your OTP is: " + OTP);

			Response.Cookies.Append("OTP", OTP, new CookieOptions { Expires = DateTime.Now.AddMinutes(5) });
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> OtpVerify(string otp)
		{
			var email = Request.Cookies["Email"];
			var OTP = Request.Cookies["OTP"];
			if (email == null || OTP == null)
			{
				TempData["Error"] = "Email or OTP is expired. Please send again";
				return RedirectToAction("Login");
			}

			if (otp != OTP)
			{
				TempData["Error"] = "OTP is not correct. Please check your email again";
				return RedirectToAction("OTP");
			}

			var account = await accountDAO.GetAccountByEmail(email);
			string newPassword = Convert.ToBase64String(Guid.NewGuid().ToByteArray()[..16]);
			authDAO.ChangePassword(account, newPassword);

			await emailSender.SendEmailAsync(email, "LiuBiu Shop", "Your new password is: " + newPassword);
			return RedirectToAction("Login");
		}


		[AllowAnonymous]
		public async Task LoginWithGoogle()
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
			{
				RedirectUri = Url.ActionLink("GoogleCallBackAPI")
			});
		}

		public async Task<IActionResult> GoogleCallBackAPI()
		{
			var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (!authenticateResult.Succeeded)
			{
				return RedirectToAction("Login", "Auth");
			}

			var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

			var account = await accountDAO.GetAccountByEmail(email);

			if (account == null)
			{
				account = new Account { Username = email, Email = email, Password = Convert.ToBase64String(Guid.NewGuid().ToByteArray())[..16] };
				await authDAO.CreateAccount(account);
			}

			HttpContext.Session.SetInt32("accountId", account.Id);

			return RedirectToAction("Index", "Home");
		}

	}
}
