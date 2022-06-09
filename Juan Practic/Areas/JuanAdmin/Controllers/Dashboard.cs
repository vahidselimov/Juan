using Microsoft.AspNetCore.Mvc;

namespace Juan_Practic.Areas.JuanAdmin.Controllers
{
    [Area("JuanAdmin")]
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
