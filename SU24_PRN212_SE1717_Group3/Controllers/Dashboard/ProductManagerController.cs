using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.DAO;
using DataAccessLayer.Models;

namespace SU24_PRN212_SE1717_Group3.Controllers.Dashboard
{
	public class ProductManagerController(ProductDAO productDAO) : Controller
	{

		[HttpGet]
		public async Task<IActionResult> Index(int CurrentPage)
		{
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
			TempData["Category"] = await productDAO.GetCategory();
			TempData["Shop"] = await productDAO.GetShop();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Product product, int quan, int categoryId, int shopId, IFormFile img)
		{

			if (product != null)
			{
				await productDAO.AddProduct(product, quan, categoryId, shopId, img);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{
			var product = await productDAO.GetProductById(Id);
			TempData["Category"] = await productDAO.GetCategory();
			TempData["Shop"] = await productDAO.GetShop();
			return View(product);
		}

		[HttpPost]
		public async Task<IActionResult> Update(Product pro, int quan, string name, IFormFile img)
		{
			await productDAO.UpdateProduct(pro, quan, name, img);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int Id)
		{
			var pro = await productDAO.GetProductById(Id);
			await productDAO.Delete(pro);
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Search(string name, int CurrentPage)
		{
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
