﻿using DataAccessLayer.Models;
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
		public async Task<List<Product>> GetAllProductByCategoryName(string CategoryName)
		{
			var listProduct = await DbContext.Product
											 .Include(x => x.Category)
											 .Include(x => x.Stock)
											 .Where(x => x.Category.Name.Equals(CategoryName))
											 .OrderByDescending(x => x.Id)
											 .ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}

		public async Task<Product> GetProductById(int id)
		{
			var pro = await DbContext.Product
									 .Include(x => x.Category)
									 .Include(x => x.Shop)
									 .Include(x => x.Stock)
									 .FirstOrDefaultAsync(x => x.Id == id);

			if (pro != null)
			{
				pro.Image = ImgUtil.Decompress(pro.Image);

			}
			return pro;
		}

		public async Task<List<Category>> GetCategory()
		{
			return await DbContext.Category.ToListAsync();
		}

		public async Task<List<Shop>> GetShop()
		{
			return await DbContext.Shop.ToListAsync();
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

		public int GetCountProductByCategoryName(string CategoryName)
		{
			return DbContext.Product
							.Include(x => x.Category)
							.Where(x => x.Category.Name.Equals(CategoryName))
							.ToList().Count();
		}

		public async Task AddProduct(Product pro, int quan, int categoryId, int shopId, IFormFile img)
		{
			Stock stock = new Stock { Id = 0, Quantity = quan, CreatedDate = DateTime.Now, LastEditedDate = DateTime.Now };
			DbContext.Stock.Add(stock);
			var category = await DbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
			var Shop = await DbContext.Shop.FirstOrDefaultAsync(x => x.Id == shopId);
			if (pro != null)
			{
				pro.Image = ImgUtil.Compress(img);
				pro.Stock = stock;
				pro.Category = category;
				pro.Shop = Shop;
				DbContext.Product.Add(pro);
				await DbContext.SaveChangesAsync();
			}
		}



		public async Task UpdateProduct(Product pro, int quan, string cate, IFormFile img)
		{
			var product = await DbContext.Product.Include(x => x.Stock).FirstOrDefaultAsync(x => x.Id == pro.Id);
			var catename = await DbContext.Category.FirstOrDefaultAsync(x => x.Name.Equals(cate));

			product.Name = pro.Name;
			product.Description = pro.Description;
			product.Price = pro.Price;
			product.Image = ImgUtil.Compress(img) ?? product.Image;
			product.Category = catename;
			product.Stock.Quantity = quan;
			product.Stock.LastEditedDate = DateTime.Now;
			DbContext.Product.Update(product);
			await DbContext.SaveChangesAsync();

		}

		public async Task Delete(Product pro)
		{
			var product = await DbContext.Product.FirstOrDefaultAsync(x => x.Id == pro.Id);
			if (product != null)
			{
				DbContext.Product.Remove(product);
			}
			await DbContext.SaveChangesAsync();
		}





	}
}
