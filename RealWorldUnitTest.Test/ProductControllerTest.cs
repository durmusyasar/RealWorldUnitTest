using Microsoft.EntityFrameworkCore;
using RealWordUnitTest.Web.Models;
using RealWorldUnitTest.Web.Models;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTest
    {
        protected DbContextOptions<UnitTestDBContext> _contextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<UnitTestDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
            Seed();
        }

        public void Seed()
        {
            using (UnitTestDBContext context = new UnitTestDBContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Category.Add(new Category { Name = "Kalemler" });
                context.Category.Add(new Category { Name = "Defterler" });

                context.SaveChanges();

                context.Product.Add(new Product { CategoryId = 1, Name = "Kalem 1", Price = 100, Stock = 250, Color = "Kırmızı" });
                context.Product.Add(new Product { CategoryId = 1, Name = "Kalem 2", Price = 100, Stock = 250, Color = "Mavi" });
                context.SaveChanges();
            }
        }

    }
}
