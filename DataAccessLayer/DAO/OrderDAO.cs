
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Utilites;

namespace DataAccessLayer.DAO
{
	public class OrderDAO(ApplicationDbContext DbContext)
	{
		public async Task<Order> GetOrderByAccount(Account account)
		{
			var Order = await DbContext.Order
				.Include(x => x.Status)
				.Include(x => x.Account)
				.FirstOrDefaultAsync(x => x.Account == account && x.Status.Id == 1);
			return Order;
		}

		public async Task CreateNewOrder(Account account)
		{
			var Status = await DbContext.Status.FirstOrDefaultAsync(x => x.Id == 1);
			var Order = new Order { Account = account, Status = Status, Quantity = 0, Total = 0 };
			await DbContext.Order.AddAsync(Order);
			await DbContext.SaveChangesAsync();
		}
		private async Task UpdateOrder(Order order, int? quantity, double? total)
		{
			order.Quantity = quantity;
			order.Total = total;
			DbContext.Update(order);
		}

		public async Task<List<OrderDetail>> GetAllOrderDetailByOrder(Order order)
		{
			var ListOrderDetail = await DbContext.OrderDetail
												 .Include(x => x.Order)
												 .Include(x => x.Product).Include(x => x.Product.Category).Include(x => x.Product.Stock)
												 .Include(x => x.Size)
												 .Where(x => x.Order == order).ToListAsync();
			ListOrderDetail.ForEach(orderDetail => orderDetail.Product.Image = ImgUtil.Decompress(orderDetail.Product.Image));
			return ListOrderDetail;
		}

		public async Task<OrderDetail> GetOrderDetailByOrderAndProductAndSize(Order order, Product product, Size size)
		{
			var orderDetail = await DbContext.OrderDetail
				.Include(x => x.Order)
				.Include(x => x.Product)
				.Include(x => x.Size)
				.FirstOrDefaultAsync(x => x.Order == order
									   && x.Product == product
									   && x.Size == size);
			return orderDetail;
		}

		public async Task AddOrderDetail(Order order, Product product, Size size, int amount)
		{
			var orderDetail = new OrderDetail
			{
				Order = order,
				Product = product,
				Size = size,
				Amount = amount,
				Subtotal = amount * product.Price
			};

			await DbContext.OrderDetail.AddAsync(orderDetail);
			await UpdateOrder(order,
							  order.Quantity + orderDetail.Amount,
							  order.Total + orderDetail.Subtotal);
			await DbContext.SaveChangesAsync();
		}

		public async Task DeleteOrderDetail(OrderDetail orderDetail)
		{
			DbContext.OrderDetail.Remove(orderDetail);
			await UpdateOrder(orderDetail.Order,
							  orderDetail.Order.Quantity - orderDetail.Amount,
							  orderDetail.Order.Total - orderDetail.Subtotal);
			await DbContext.SaveChangesAsync();
		}

		public async Task UpdateOrderDetail(OrderDetail orderDetail, int? amount)
		{
			var newSubtotal = amount * orderDetail.Product.Price;

			await UpdateOrder(orderDetail.Order,
							  orderDetail.Order.Quantity - orderDetail.Amount + amount,
							  orderDetail.Order.Total - orderDetail.Subtotal + newSubtotal);

			orderDetail.Amount = amount;
			orderDetail.Subtotal = newSubtotal;
			DbContext.OrderDetail.Update(orderDetail);
			await DbContext.SaveChangesAsync();
		}

		public async Task<Size> GetSizeById(int id)
		{
			var size = await DbContext.Size.FirstOrDefaultAsync(x => x.Id == id);
			return size;
		}

	}
}