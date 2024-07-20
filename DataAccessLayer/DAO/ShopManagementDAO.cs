using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
#pragma warning disable

namespace DataAccessLayer.DAO
{
	public class ShopManagementDAO(ApplicationDbContext DbContext)
	{
		int YearOfLastMonth = DateTime.Now.AddMonths(-1).Year;
		int LastMonth = DateTime.Now.AddMonths(-1).Month;
		int YearOfThisMonth = DateTime.Now.Year;
		int ThisMonth = DateTime.Now.Month;

		public List<int> GetChartProduct(int year)
		{
			var list = new List<int>();
			for (int month = 1; month <= 12; month++)
			{
				DateTime firstDayOfMonth = new DateTime(year, month, 1);
				DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

				var productSold = DbContext.Order
					.Include(x => x.Status)
					.Where(x => x.OrderDate >= firstDayOfMonth && x.OrderDate <= lastDayOfMonth && x.Status.Id == 3)
					.Sum(x => x.Quantity);

				list.Add(productSold ?? 0);
			}
			return list;
		}

		public List<double> GetChartRevenue(int year)
		{
			var list = new List<double>();
			for (int month = 1; month <= 12; month++)
			{
				DateTime firstDayOfMonth = new DateTime(year, month, 1);
				DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

				var revenue = DbContext.Order
					.Include(x => x.Status)
					.Where(x => x.OrderDate >= firstDayOfMonth && x.OrderDate <= lastDayOfMonth && x.Status.Id == 3)
					.Sum(x => x.Total);

				list.Add(revenue ?? 0);
			}
			return list;
		}

		public int GetProductSold()
		{
			var productSold = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.Status.Id == 3)
				.Sum(x => x.Quantity);
			return productSold ?? 0;
		}

		public int GetProductLastMonth()
		{
			DateTime firstDayOfLastMonth = new DateTime(YearOfLastMonth, LastMonth, 1);
			DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
			var productSoldLastMonth = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.OrderDate >= firstDayOfLastMonth && x.OrderDate <= lastDayOfLastMonth && x.Status.Id == 3)
				.Sum(x => x.Quantity);
			return productSoldLastMonth ?? 0;
		}

		public double GetProductThisMonth()
		{
			DateTime firstDayOfThisMonth = new DateTime(YearOfThisMonth, ThisMonth, 1);
			DateTime lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
			var revenueLastMonth = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.OrderDate >= firstDayOfThisMonth && x.OrderDate <= lastDayOfThisMonth && x.Status.Id == 3)
				.Sum(x => x.Quantity);
			return revenueLastMonth ?? 0;
		}

		public double GetRevenue()
		{
			var revenue = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.Status.Id == 3)
				.Sum(x => x.Total);
			return revenue ?? 0;
		}

		public double GetRevenueLastMonth()
		{
			DateTime firstDayOfLastMonth = new DateTime(YearOfLastMonth, LastMonth, 1);
			DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
			var revenueLastMonth = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.OrderDate >= firstDayOfLastMonth && x.OrderDate <= lastDayOfLastMonth && x.Status.Id == 3)
				.Sum(x => x.Total);
			return revenueLastMonth ?? 0;
		}

		public double GetRevenueThisMonth()
		{
			DateTime firstDayOfThisMonth = new DateTime(YearOfThisMonth, ThisMonth, 1);
			DateTime lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
			var revenueLastMonth = DbContext.Order
				.Include(x => x.Status)
				.Where(x => x.OrderDate >= firstDayOfThisMonth && x.OrderDate <= lastDayOfThisMonth && x.Status.Id == 3)
				.Sum(x => x.Total);
			return revenueLastMonth ?? 0;
		}

		public  Task<Product?> GetMostSoldProduct()
		{
			DateTime firstDayOfThisMonth = new DateTime(YearOfThisMonth, ThisMonth, 1);
			DateTime lastDayOfThisMonth = firstDayOfThisMonth.AddMonths(1).AddDays(-1);
			var mostSoldProduct = DbContext.OrderDetail
				.Include(x => x.Product)
				.Include(x => x.Order)
				.Where(x => x.Order.OrderDate >= firstDayOfThisMonth && x.Order.OrderDate <= lastDayOfThisMonth && x.Order.Status.Id == 3)
				.GroupBy(x => x.ProductId)
				.Select(x => x.Key)
				.FirstOrDefault();
			return  mostSoldProduct == 0 ? null : new ProductDAO(DbContext).GetProductById(mostSoldProduct);
		}

	}
}
