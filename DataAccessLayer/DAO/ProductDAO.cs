using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utilites;


namespace DataAccessLayer.DAO
{
	public class ProductDAO(ApplicationDbContext DbContext)
	{
		public async Task<List<Product>> GetAllProduct()
		{
			var listProduct = await DbContext.Product
											  .Include(x => x.Category)
											 .Include(x => x.Stock)
											 .OrderByDescending(x => x.Id)
											 .ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}

		public async Task<List<Product>> GetAllProductBySearchName(string Name)
		{
			var listProduct = await DbContext.Product
											 .Include(x => x.Category)
											 .Include(x => x.Stock)
											 .Where(x => x.Name.Contains(Name))
											 .OrderByDescending(x => x.Id)
											 .ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}
		public async Task<List<Product>> GetAllProductByCategoryName(string categoryName)
		{
			var listProduct = await DbContext.Product
											 .Include(x => x.Category)
											 .Include(x => x.Stock)
											 .Where(x => x.Category.Name.Equals(categoryName))
											 .OrderByDescending(x => x.Id)
											 .ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}

		public async Task<List<Product>> GetTop10MostSoldProduct()
		{
			var ListProductId = await DbContext.OrderDetail
										.Include(x => x.Product)
										.Include(x => x.Order).ThenInclude(x => x.Status)
										.Where(x => x.Order.Status.Id == 3)
										.GroupBy(x => x.ProductId)
										.OrderByDescending(x => x.Count())
										.Select(x => x.Key)
										.Take(10)
										.ToListAsync();
			var productTasks = ListProductId.Select(productId => GetProductById(productId)).ToList();
			var ListProduct = await Task.WhenAll(productTasks);
			return ListProduct.ToList();
		}

		public async Task<List<Product>> GetTop4NewProduct()
		{
			var ListProduct = await DbContext.Product
				.Include(x => x.Category)
				.OrderByDescending(x => x.Id)
				.Take(4).ToListAsync();

			ListProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return ListProduct;
		}

		public async Task<Product> GetProductById(int Id)
		{
			var pro = await DbContext.Product
									 .Include(x => x.Category)
									 .Include(x => x.Shop)
									 .Include(x => x.Stock)
									 .FirstOrDefaultAsync(x => x.Id == Id);
			pro.Image = ImgUtil.Decompress(pro.Image);
			return pro;
		}

		public int GetCountProduct()
		{
			return DbContext.Product.ToList().Count();
		}

		public int GetCountProductBySearchName(string Name)
		{
			return DbContext.Product
							.Where(x => x.Name.Contains(Name))
							.ToList().Count();
		}

		public int GetCountProductByCategoryName(string categoryName)
		{
			return DbContext.Product
							.Include(x => x.Category)
							.Where(x => x.Category.Name.Equals(categoryName))
							.ToList().Count();
		}

		public async Task AddProduct(Product product, Category category, Shop shop, int quantity)
		{
			var stock = new Stock { Id = 0, Quantity = quantity, CreatedDate = DateTime.Now, LastEditedDate = DateTime.Now };

			product.Stock = stock;
			product.Category = category;
			product.Shop = shop;

			DbContext.Product.Add(product);
			await DbContext.SaveChangesAsync();
		}

		public async Task UpdateProduct(Product product, Category category, int quantity)
		{
			var GetProduct = await DbContext.Product.Include(x => x.Stock).FirstOrDefaultAsync(x => x.Id == product.Id);

			GetProduct.Name = product.Name;
			GetProduct.Description = product.Description;
			GetProduct.Price = product.Price;
			GetProduct.Image = product.Image ?? GetProduct.Image;
			GetProduct.Category = category;
			GetProduct.Stock.Quantity = quantity;
			GetProduct.Stock.LastEditedDate = DateTime.Now;

			DbContext.Product.Update(GetProduct);
			await DbContext.SaveChangesAsync();
		}

		public async Task Delete(Product product)
		{
			var GetProduct = await DbContext.Product.FirstOrDefaultAsync(x => x.Id == product.Id);

			DbContext.Product.Remove(GetProduct);
			await DbContext.SaveChangesAsync();
		}

		public async Task<List<Category>> GetAllCategory()
		{
			return await DbContext.Category.ToListAsync();
		}

		public async Task<Category> GetCategoryById(int id)
		{
			var category = await DbContext.Category.FirstOrDefaultAsync(x => x.Id == id);
			return category;
		}
	}
}
