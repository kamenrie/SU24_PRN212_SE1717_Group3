using Microsoft.AspNetCore.Mvc;

namespace SU24_PRN212_SE1717_Group3.Controllers
{
    public class Feedback : Controller
    {
        public IActionResult feedback()
        {
            return View();
        }
    }
}
