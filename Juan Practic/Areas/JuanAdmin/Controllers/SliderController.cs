using Juan_Practic.DAL;
using Juan_Practic.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Pronia_start.Extensions;
using Pronia_start.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Juan_Practic.Areas.JuanAdmin.Controllers
{
    [Area("JuanAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHost;

        public SliderController(AppDbContext context, IWebHostEnvironment webHost)
        {
            this.context = context;
            this.webHost = webHost;
        }
   
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await context.Sliders.ToListAsync();
            return View(sliders);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            if (slider.Photo!=null)
            {

                if (!slider.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo","Please enter photo");
                    return View();

                }
                string fileName = slider.Photo.FileName;
                string path = Path.Combine(webHost.WebRootPath, "img", "slider");
                slider.Image = await slider.Photo.FileCreate(webHost.WebRootPath, @"assets\img\slider");
                await context.Sliders.AddAsync(slider);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                    
                
               
            }
            else
            {
                ModelState.AddModelError("Photo","Please enter Image");
                return View();
                    
            }
           



        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();

            context.Sliders.Remove(slider);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Slider slider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            Slider existedSlider = await context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsOkay(1))
                {
                    string path = webHost.WebRootPath + @"\assets\img\slider\" + existedSlider.Image;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    existedSlider.Image = await slider.Photo.FileCreate(webHost.WebRootPath, @"assets\img\slider\");

                }
                else
                {
                    ModelState.AddModelError("Photo", "Selected image is not valid!");
                    return View(slider);
                }
            }
            existedSlider.Title = slider.Title;
            existedSlider.SubTitle = slider.SubTitle;
            existedSlider.Discount = slider.Discount;
            existedSlider.DiscoverUrl = slider.DiscoverUrl;
            existedSlider.Order = slider.Order;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
