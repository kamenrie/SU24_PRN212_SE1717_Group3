using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.Dao;
using SU24_PRN212_SE1717_Group3.DAO;
using SU24_PRN212_SE1717_Group3.Models;
using Utilites;

namespace SU24_PRN212_SE1717_Group3.Controllers.Dashboard
{
	public class ProductManagerController(ProductDAO proDAO) : Controller
	{

		[HttpGet]
		public async Task<IActionResult> Index(int CurrentPage)
		{
			int endpage = (int)Math.Ceiling(proDAO.GetCountProduct() / 6.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await proDAO.GetAllProduct();
			listProduct.Skip((CurrentPage - 1) * 6).Take(6).ToList();

			ViewData["Access"] = "Index?";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View(listProduct);
		}
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			TempData["Category"] = await proDAO.GetCategory();
			TempData["Shop"] = await proDAO.GetShop();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Product product, int quan, int categoryId, int shopId, IFormFile img)
		{

			if (product != null)
			{
				await proDAO.AddProduct(product, quan, categoryId, shopId, img);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{
			var product = await proDAO.GetProductById(Id);
			TempData["Category"] = await proDAO.GetCategory();
			TempData["Shop"] = await proDAO.GetShop();
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Product pro, int quan, string name, IFormFile img)
		{
			await proDAO.UpdateProduct(pro, quan, name, img);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var pro = await proDAO.GetProductById(Id);
			await proDAO.Delete(pro);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Search(string name, int CurrentPage)
		{
			int endpage = (int)Math.Ceiling(proDAO.GetCountProductBySearchName(name) / 6.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await proDAO.GetAllProductBySearchName(name);

			listProduct.Skip((CurrentPage - 1) * 6).Take(6).ToList();


			ViewData["Access"] = "Search?Name=" + name + "&";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View("Index", listProduct);
		}







	}
}
