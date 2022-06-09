using Juan_Practic.DAL;
using Juan_Practic.Models;
using Juan_Practic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juan_Practic.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;

        }
        

        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await context.Sliders.ToListAsync();
            List<Product> products = await context.Products.ToListAsync();
            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                Products = products


            };
            return View(model);
        }
    }
}
