using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealWordUnitTest.Web.Models;
using RealWorldUnitTest.Web.Controllers;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTestWithInMemory : ProductControllerTest
    {
        public ProductControllerTestWithInMemory()
        {
            SetContextOptions(new DbContextOptionsBuilder<UnitTestDBContext>().UseInMemoryDatabase("SqlConStr").Options);
        }

        [Fact]
        public async Task Create_ModelValidProduct_ReturnRedirectToActionWithSaveProduct()
        {
            var newProduct = new Product { Name = "Kalem 30", Price = 200, Stock = 100, Color="Kırmızı" };
            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var category = context.Category.First();

                newProduct.CategoryId = category.Id;

                //var repository = new repository<Product>(context);
                var controller = new ProductsController(context);

                var result = await controller.Create(newProduct);

                var redirect = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirect.ActionName);
            }

            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var product = context.Product.FirstOrDefault(x => x.Name == newProduct.Name);

                Assert.Equal(newProduct.Name, product.Name);
            }
        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeletedAllProducts(int categoryId)
        {
            using (var context = new UnitTestDBContext(_contextOptions))

            {
                var category = await context.Category.FindAsync(categoryId);

                context.Category.Remove(category);

                context.SaveChanges();
            }

            using (var context = new UnitTestDBContext(_contextOptions))
            {
                var t = context.Product.ToList();
                var products = await context.Product.Where(x => x.CategoryId == categoryId).ToListAsync();

                Assert.Empty(products);
            }
        }
    }
}
