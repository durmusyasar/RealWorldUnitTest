using Microsoft.AspNetCore.Mvc;
using Moq;
using RealWordUnitTest.Web.Models;
using RealWorldUnitTest.Web.Controllers;
using RealWorldUnitTest.Web.Repository;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTest
    {
        private readonly Mock<IRepository<Product>> _mockRepo;
        private readonly ProductsController _controller;
        private List<Product> products;

        public ProductControllerTest()
        {
            _mockRepo = new Mock<IRepository<Product>>();
            _controller = new ProductsController(_mockRepo.Object);
            products = new List<Product>()
            {
                new Product { Id = 1, Name="Test", Color="Kırmızı", Price=125, Stock =500},
                new Product { Id = 2, Name="Test 2 ", Color="Mavi ", Price=100, Stock =300}
            };
        }

        [Fact]
        public async void Index_ActionExecutes_ReturnView()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
