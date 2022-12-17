using RealWordUnitTest.Web.Models;

namespace RealWorldUnitTest.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
