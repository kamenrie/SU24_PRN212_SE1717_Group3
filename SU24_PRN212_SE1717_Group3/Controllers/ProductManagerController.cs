using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SU24_PRN212_SE1717_Group3.Dao;
using SU24_PRN212_SE1717_Group3.DAO;
using SU24_PRN212_SE1717_Group3.Models;
using Utilites;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class ProductManagerController(ProductDAO proDao) : Controller
    {

        public async Task<IActionResult> Index(IFormFile img)
        {
            var getPro = await proDao.getAllProduct();
            foreach (var product in getPro)
            {
                product.Image = ImgUtil.Decompress(product.Image);
            }
            return View(getPro);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            TempData["Category"] = await proDao.getCategory();
            TempData["Shop"] = await proDao.getShop();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product, int quan,int categoryId,int shopId, IFormFile img)
        {
            
            if (product != null)
            {
                await proDao.addProduct(product,quan, categoryId,shopId,img);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var product = await proDao.GetProductById(Id);          
                TempData["Category"] = await proDao.getCategory();
                TempData["Shop"] =  await proDao.getShop();
            //if (product == null)
            //{
            //    ViewData["Error"] = "Product not found";
            //    return View("Error");
            //}
            return View(product);
        }

        [HttpPost]

        public async Task<IActionResult> Update(Product pro ,int quan,string name,IFormFile img)
        {
            await proDao.updateProduct(pro,quan,name,img);
                return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Product product)
        {
            var pro = await proDao.GetProductById(product.Id);
            await proDao.Delete(pro);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name ,int curent)
        {
            await proDao.Search(name, curent);
            ViewData["Access"] = "Search?Name=" + name + "&";
            ViewData["EndPage"] = proDao.Search;
            ViewData["CurrentPage"] = proDao.Search;
            ViewData["TotalEntry"] = proDao.Search;
            return RedirectToAction("Index");
        }





    }
}
