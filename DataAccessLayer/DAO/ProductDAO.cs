﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.DataAccess;
using SU24_PRN212_SE1717_Group3.Models;
using Utilites;


namespace SU24_PRN212_SE1717_Group3.DAO
{
	public class ProductDAO(ApplicationDbContext dbContext)
	{
		public async Task<List<Product>> GetAllProduct()
		{
			var listProduct = await dbContext.Product.Include(x => x.Category).Include(x => x.Stock)
				.OrderByDescending(x => x.Id).ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}

		public async Task<List<Product>> GetAllProductBySearchName(string name)
		{
			var listProduct = await dbContext.Product.Include(x => x.Category).Include(x => x.Stock)
				.Where(x => x.Name.Contains(name)).OrderByDescending(x => x.Id).ToListAsync();

			listProduct.ForEach(product => { product.Image = ImgUtil.Decompress(product.Image); });
			return listProduct;
		}

		public async Task<Product> GetProductById(int id)
		{
			var pro = await dbContext.Product.Include(x => x.Category).Include(x => x.Shop).Include(x => x.Stock).FirstOrDefaultAsync(x => x.Id == id);
			if (pro != null)
			{
				pro.Image = ImgUtil.Decompress(pro.Image);

			}
			return pro;
		}

		public async Task<List<Category>> GetCategory()
		{
			return await dbContext.Category.ToListAsync();
		}

		public async Task<List<Shop>> GetShop()
		{
			return await dbContext.Shop.ToListAsync();
		}

		public int GetCountProduct()
		{
			return dbContext.Product.ToList().Count;
		}

		public int GetCountProductBySearchName(string Name)
		{
			return dbContext.Product.Where(x => x.Name.Equals(Name)).ToList().Count;
		}

		public int GetCountProductByCategoryName(string CategoryName)
		{
			return dbContext.Product.Include(x => x.Category).Where(x => x.Category.Name.Equals(CategoryName)).ToList().Count;
		}

		public async Task AddProduct(Product pro, int quan, int categoryId, int shopId, IFormFile img)
		{
			Stock stock = new Stock { Id = 0, Quantity = quan, CreatedDate = DateTime.Now, LastEditedDate = DateTime.Now };
			dbContext.Stock.Add(stock);
			var category = await dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
			var Shop = await dbContext.Shop.FirstOrDefaultAsync(x => x.Id == shopId);
			if (pro != null)
			{
				pro.Image = ImgUtil.Compress(ImgUtil.ConvertIFormFileToByte(img));
				pro.Stock = stock;
				pro.Category = category;
				pro.Shop = Shop;
				dbContext.Product.Add(pro);
				await dbContext.SaveChangesAsync();
			}
		}

		

		public async Task UpdateProduct(Product pro, int quan, string cate, IFormFile img)
		{
			var product = await dbContext.Product.Include(x => x.Stock).FirstOrDefaultAsync(x => x.Id == pro.Id);
			var catename = await dbContext.Category.FirstOrDefaultAsync(x => x.Name.Equals(cate));

			product.Name = pro.Name;
			product.Description = pro.Description;
			product.Price = pro.Price;
			product.Image = ImgUtil.Compress(ImgUtil.ConvertIFormFileToByte(img)) ?? product.Image;
			product.Category = catename;
			product.Stock.Quantity = quan;
			product.Stock.LastEditedDate = DateTime.Now;
			dbContext.Product.Update(product);
			await dbContext.SaveChangesAsync();

		}

		public async Task Delete(Product pro)
		{
			var product = await dbContext.Product.FirstOrDefaultAsync(x => x.Id == pro.Id);
			if (product != null)
			{
				dbContext.Product.Remove(product);
			}
			await dbContext.SaveChangesAsync();
		}

		

		

	}
}
