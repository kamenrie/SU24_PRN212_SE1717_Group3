using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAO;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
	public class ProductController(ProductDAO productDAO) : Controller
	{
		public async Task<IActionResult> Index(int CurrentPage)
		{
			int endpage = (int)Math.Ceiling(productDAO.GetCountProduct() / 8.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await productDAO.GetAllProduct();

			listProduct = listProduct.Skip((CurrentPage - 1) * 8).Take(8).ToList();

			ViewData["Access"] = "?";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View(listProduct);
		}

		public async Task<IActionResult> Category(int CurrentPage, string CategoryName)
		{
			int endpage = (int)Math.Ceiling(productDAO.GetCountProductByCategoryName(CategoryName) / 8.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await productDAO.GetAllProductByCategoryName(CategoryName);

			listProduct = listProduct.Skip((CurrentPage - 1) * 8).Take(8).ToList();

			ViewData["Access"] = "Category?CategoryName=" + CategoryName + "&";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View("Index", listProduct);
		}

		public async Task<IActionResult> Search(int CurrentPage, string Name)
		{
			if (String.IsNullOrEmpty(Name)) { 
				return RedirectToAction("Index");
			}
			int endpage = (int)Math.Ceiling(productDAO.GetCountProductBySearchName(Name) / 8.0);
			CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
			var listProduct = await productDAO.GetAllProductBySearchName(Name);

			listProduct = listProduct.Skip((CurrentPage - 1) * 8).Take(8).ToList();

			ViewData["Access"] = "Search?Name=" + Name + "&";
			ViewData["EndPage"] = endpage;
			ViewData["CurrentPage"] = CurrentPage;
			return View("Index", listProduct);
		}

		public async Task<IActionResult> Detail(int Id)
		{
			var Product = await productDAO.GetProductById(Id);
			var ListProduct = await productDAO.GetAllProductByCategoryName(Product.Category.Name);
			ViewData["ListSimilarProduct"] = ListProduct.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
			return View(Product);
		}
	}
}
