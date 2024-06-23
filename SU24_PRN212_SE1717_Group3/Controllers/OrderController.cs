using DataAccessLayer.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
	public class OrderController(AccountDAO accountDAO, OrderDAO orderDAO, ProductDAO productDAO) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}

			var Order = await orderDAO.GetOrderByAccount(account);
			if (Order == null)
			{
				await orderDAO.CreateNewOrder(account);
				Order = await orderDAO.GetOrderByAccount(account);
			}

			var ListOrderDetail = await orderDAO.GetAllOrderDetailByOrder(Order);
			return View(ListOrderDetail);
		}

		[HttpPost]
		public async Task<IActionResult> Add(int productId, int sizeId, int amount)
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}

			var Order = await orderDAO.GetOrderByAccount(account);
			if (Order == null)
			{
				await orderDAO.CreateNewOrder(account);
				Order = await orderDAO.GetOrderByAccount(account);
			}

			var product = await productDAO.GetProductById(productId);
			var size = await orderDAO.GetSizeById(sizeId);

			var orderDetail = await orderDAO.GetOrderDetailByOrderAndProductAndSize(Order, product, size);
			if (orderDetail == null)
			{
				await orderDAO.AddOrderDetail(Order, product, size, amount);
			}
			else
			{
				await orderDAO.UpdateOrderDetail(orderDetail, orderDetail.Amount + amount);
			}

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Update(int productId, int sizeId, int amount)
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}
			var Order = await orderDAO.GetOrderByAccount(account);
			var product = await productDAO.GetProductById(productId);
			var size = await orderDAO.GetSizeById(sizeId);

			var orderDetail = await orderDAO.GetOrderDetailByOrderAndProductAndSize(Order, product, size);
			if (amount <= 0)
			{
				await orderDAO.DeleteOrderDetail(orderDetail);
			}
			else
			{
				await orderDAO.UpdateOrderDetail(orderDetail, amount);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int productId, int sizeId)
		{
			var account = await accountDAO.GetAccountById(HttpContext.Session.GetInt32("accountId"));
			if (account == null)
			{
				return RedirectToAction("Login", "Auth");
			}

			var Order = await orderDAO.GetOrderByAccount(account);
			var product = await productDAO.GetProductById(productId);
			var size = await orderDAO.GetSizeById(sizeId);

			var orderDetail = await orderDAO.GetOrderDetailByOrderAndProductAndSize(Order, product, size);
			await orderDAO.DeleteOrderDetail(orderDetail);
			return RedirectToAction("Index");
		}

	}
}
