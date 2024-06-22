using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DAO;
using DataAccessLayer.Models;
using Utilites;

namespace SU24_PRN212_SE1717_Group3.Controllers.Dashboard
{
	public class ProductManagerController(AccountDAO accountDAO, ProductDAO productDAO) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index(int CurrentPage)
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null || account.Role.Name != "Shop")
			{
				return RedirectToAction("Index", "Home");
			}

            int endpage = (int)Math.Ceiling(productDAO.GetCountProduct() / 6.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await productDAO.GetAllProduct();
			listProduct = listProduct.Skip((CurrentPage - 1) * 6).Take(6).ToList();

			ViewData["Access"] = "Index?";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View(listProduct);
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Category"] = await productDAO.GetAllCategory();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Product product, int quantity, int categoryId, int shopId, IFormFile img)
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            product.Image = ImgUtil.Compress(ImgUtil.ConvertIFromFileToByte(img));
			var Category = await productDAO.GetCategoryById(categoryId);
			await productDAO.AddProduct(product, Category, account.Shop, quantity);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            var product = await productDAO.GetProductById(Id);
			ViewData["Category"] = await productDAO.GetAllCategory();
			ViewData["ShopName"] = account.Shop.Name;

            return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Product product, int quantity, int categoryId, IFormFile img)
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            product.Image = ImgUtil.Compress(ImgUtil.ConvertIFromFileToByte(img));

			var Category = await productDAO.GetCategoryById(categoryId);
			await productDAO.UpdateProduct(product, Category, quantity);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            var pro = await productDAO.GetProductById(Id);
			await productDAO.Delete(pro);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Search(string name, int CurrentPage)
		{
            var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
            if (account == null || account.Role.Name != "Shop")
            {
                return RedirectToAction("Index", "Home");
            }

            int endpage = (int)Math.Ceiling(productDAO.GetCountProductBySearchName(name) / 6.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await productDAO.GetAllProductBySearchName(name);

			listProduct = listProduct.Skip((CurrentPage - 1) * 6).Take(6).ToList();


			ViewData["Access"] = "Search?Name=" + name + "&";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View("Index", listProduct);
		}
	}
}
