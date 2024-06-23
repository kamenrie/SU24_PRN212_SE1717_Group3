
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Utilites;

namespace DataAccessLayer.DAO
{
	public class OrderDetailDTO
	{
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int TotalProduct { get; set; }
	}

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
		private void UpdateOrder(Order order, int? quantity, double? total)
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
		public async Task<List<OrderDetail>> GetAllOrderDetailByOrderIdAndProductId(int orderId, int productId)
		{
			var ListOrderDetail = await DbContext.OrderDetail
												 .Include(x => x.Order)
												 .Include(x => x.Product).Include(x => x.Product.Stock)
												 .Include(x => x.Size)
												 .Where(x => x.Order.Id == orderId && x.Product.Id == productId)
												 .ToListAsync();
			return ListOrderDetail;
		}

		public async Task<List<OrderDetailDTO>> GetAllOrderDetailInCartByProductExceedingStock(Product product)
		{
			var ListOrderDetail = await DbContext.OrderDetail
												 .Include(x => x.Order).ThenInclude(x => x.Status)
												 .Include(x => x.Product).ThenInclude(x => x.Stock)
												 .Where(x => x.Order.Status.Id == 1
														  && x.Product == product)
												 .GroupBy(x => new { x.ProductId, x.OrderId, x.Product.Stock.Quantity })
												 .Where(x => x.Sum(y => y.Amount) > x.Key.Quantity)
												 .Select(x =>
															 new OrderDetailDTO
															 {
																 OrderId = x.Key.OrderId,
																 ProductId = x.Key.ProductId,
																 TotalProduct = x.Count()
															 }
												 ).ToListAsync();
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
			UpdateOrder(order,
						order.Quantity + orderDetail.Amount,
						order.Total + orderDetail.Subtotal);
			await DbContext.SaveChangesAsync();
		}

		public async Task DeleteOrderDetail(OrderDetail orderDetail)
		{
			DbContext.OrderDetail.Remove(orderDetail);
			UpdateOrder(orderDetail.Order,
						orderDetail.Order.Quantity - orderDetail.Amount,
						orderDetail.Order.Total - orderDetail.Subtotal);
			await DbContext.SaveChangesAsync();
		}

		public async Task UpdateOrderDetail(OrderDetail orderDetail, int? amount)
		{
			var newSubtotal = amount * orderDetail.Product.Price;

			UpdateOrder(orderDetail.Order,
						orderDetail.Order.Quantity - orderDetail.Amount + amount,
						orderDetail.Order.Total - orderDetail.Subtotal + newSubtotal);

			orderDetail.Amount = amount;
			orderDetail.Subtotal = newSubtotal;
			DbContext.OrderDetail.Update(orderDetail);
			await DbContext.SaveChangesAsync();
		}

		public async Task Checkout(Order order, ShippingInformation shipping, Delivery delivery, Discount discount)
		{
			shipping.Delivery = delivery;
			await DbContext.ShippingInformation.AddAsync(shipping);
			order.ShippingInformation = shipping;
			order.Discount = discount;
			order.OrderDate = DateTime.Now;
			order.Status = await DbContext.Status.FirstOrDefaultAsync(x => x.Id == 2);
			UpdateOrder(order,
						order.Quantity,
						order.Total * (1 - (discount.Percent / 100)) + delivery.Price);
			await DbContext.SaveChangesAsync();
			AfterCheckout(order);
		}

		public async Task AfterCheckout(Order order)
		{
			var ListOrderDetailOfOrder = await GetAllOrderDetailByOrder(order);
			ListOrderDetailOfOrder.ForEach(async orderDetail =>
			{
				orderDetail.Product.Stock.Quantity -= orderDetail.Amount;
				DbContext.Stock.Update(orderDetail.Product.Stock);

				var stockQuantity = orderDetail.Product.Stock.Quantity;
				var ListOrderDetailExceedStock = await GetAllOrderDetailInCartByProductExceedingStock(orderDetail.Product);
				ListOrderDetailExceedStock.ForEach(async orderDetailExceedStock =>
				{
					var CovertedListOrderDetailExceedStock = await GetAllOrderDetailByOrderIdAndProductId(orderDetailExceedStock.OrderId,
																										   orderDetailExceedStock.ProductId);
					CovertedListOrderDetailExceedStock.ForEach(async convertedOrderDetailExceedStock =>
					{
						if (stockQuantity == 0 || stockQuantity < orderDetailExceedStock.TotalProduct)
						{
							await DeleteOrderDetail(convertedOrderDetailExceedStock);
						}
						else
						{
							await UpdateOrderDetail(convertedOrderDetailExceedStock, (int)Math.Floor((double)stockQuantity / orderDetailExceedStock.TotalProduct));
						}

					});
				});
			});
			await DbContext.SaveChangesAsync();
		}

		public async Task<List<Delivery>> GetAllDelivery()
		{
			var ListDelivery = await DbContext.Delivery.ToListAsync();
			return ListDelivery;
		}

		public async Task<Delivery> GetDeliveryById(int Id)
		{
			var Delivery = await DbContext.Delivery.FirstOrDefaultAsync(x => x.Id == Id);
			return Delivery;
		}

		public async Task<Size> GetSizeById(int Id)
		{
			var Size = await DbContext.Size.FirstOrDefaultAsync(x => x.Id == Id);
			return Size;
		}

		public async Task<List<Discount>> GetUnusedDiscountByAccount(Account account)
		{
			var ListUsed = await DbContext.Mydiscount
											.Include(x => x.Discount)
											.Include(x => x.Account)
											.Where(x => x.Account == account)
											.Select(x => x.Discount).ToListAsync();

			var ListDiscount = await DbContext.Discount
												.Where(x => x.Validity == true
														 && x.Quantity != 0)
												.ToListAsync();
			ListDiscount = ListDiscount.Except(ListUsed).ToList();
			return ListDiscount;
		}

		public async Task<Discount> GetDiscountById(int Id)
		{
			var Discount = await DbContext.Discount.FirstOrDefaultAsync(x => x.Id == Id);
			return Discount;
		}
	}
}