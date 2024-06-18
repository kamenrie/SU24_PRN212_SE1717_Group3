using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.DataAccess;
using SU24_PRN212_SE1717_Group3.Models;
using Utilites;


namespace SU24_PRN212_SE1717_Group3.DAO
{
    public class ProductDAO(ApplicationDbContext dbContext)
    {
        public async Task<List<Product>> getAllProduct()
        {
           
            return await dbContext.Product.Include(x => x.Category).Include(x=> x.Stock).ToListAsync();
        }

        public async Task<List<Category>> getCategory()
        {
            return await dbContext.Category.ToListAsync();
        }

        public async Task<List<Shop>> getShop()
        {
            return await dbContext.Shop.ToListAsync();
        }

        public async Task addProduct(Product pro, int quan, int categoryId,int shopId, IFormFile img)
        {
            Stock stock = new Stock { Id = 0, Quantity = quan, CreatedDate = DateTime.Now,LastEditedDate = DateTime.Now };
            dbContext.Stock.Add(stock);
            var category = await dbContext.Category.FirstOrDefaultAsync(x => x.Id == categoryId);
            var Shop = await dbContext.Shop.FirstOrDefaultAsync(x => x.Id == shopId);
            if(pro != null)
            {
                pro.Image = ImgUtil.Compress(ImgUtil.ConvertIFormFileToByte(img));
                pro.Stock = stock;
                pro.Category = category;
                pro.Shop = Shop;
                dbContext.Product.Add(pro);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Product> GetProductById(int id )
        {
            var pro = await dbContext.Product.Include(x=>x.Category).Include(x=> x.Shop).Include(x=>x.Stock).FirstOrDefaultAsync(x => x.Id == id);
            if (pro != null)
            {
                pro.Image = ImgUtil.Decompress(pro.Image);
                
            }
            return pro;
        }
        
        public async Task updateProduct(Product pro,int quan,string cate, IFormFile img)
        {
            var product = await dbContext.Product.Include(x=>x.Stock).FirstOrDefaultAsync(x => x.Id == pro.Id);
            var catename = await dbContext.Category.FirstOrDefaultAsync(x => x.Name.Equals(cate));

            product.Name = pro.Name;
            product.Description = pro.Description;
            product.Price = pro.Price;
            product.Image = ImgUtil.Compress(ImgUtil.ConvertIFormFileToByte(img))??product.Image;
            product.Category = catename;
            product.Stock.Quantity = quan;
            dbContext.Product.Update(product);
            await dbContext.SaveChangesAsync();

        }

        public async Task Delete(Product pro)
        {
            var product =  await dbContext.Product.FirstOrDefaultAsync(x=> x.Id==pro.Id);
            if (product != null)
            {
                
                    dbContext.Product.Remove(product);
                
            }
            await dbContext.SaveChangesAsync();
        }
        
        public async Task Search(string name,int CurrentPage)
        {
            int endpage = (int)Math.Ceiling((double) dbContext.Product.Where(x=>x.Name != null && x.Name.Contains(name)).Count()/8);
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage > endpage ? endpage : CurrentPage;
            
            var product =  dbContext.Product.OrderByDescending(x => x.Id).Where(x => x.Name == null && x.Name.Contains(name)).Skip((CurrentPage-1)*6).Take(6).ToList();

         
        }

        


    }
}
