using Microsoft.AspNetCore.Mvc;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class VIew : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Order()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult feedbackmanager()
        {
            return View();
        }

        public IActionResult overviewmangerment() { 
        return View();
        }
        
        public IActionResult overviewuser()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult FogotPass()
        {
            return View();
        }
    }
}
