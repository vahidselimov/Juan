
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Juan_Practic.Models;

namespace Juan_Practic.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

       
        
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
