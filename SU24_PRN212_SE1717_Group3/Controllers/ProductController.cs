using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.DAO;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
	public class ProductController(ProductDAO productDAO) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var listProduct = await productDAO.GetAllProduct();
			return View(listProduct);
		}
	}
}
