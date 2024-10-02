using SOFARI;

namespace YSofaris.ViewModel
{
    public class ProductDetailsVM
    {
        public required Product Product { get; set; }
        public required List<Product> Colors { get; set; }
    }
}
