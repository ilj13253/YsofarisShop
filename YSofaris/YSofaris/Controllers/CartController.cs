using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOFARI;
using SofariDb;
using YSofaris.Repositories.Extensions;
using YSofaris.Repositories.Interfaces;

namespace YSofaris.Controllers
{
    public class CartController(IRepositoryCard repositoryCart,SOFARIDB sOFARIDB) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int id)
        {
            var product = await sOFARIDB.Products.SingleOrDefaultAsync(product => product.Id == id) ??
                throw new NullReferenceException();

            repositoryCart.Add(id, product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            repositoryCart.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> PlaceOrder()
        {
            var products = new List<Product>();

            foreach (var id in repositoryCart.Ids())
            {
                var product = await sOFARIDB.Products.SingleOrDefaultAsync(product => product.Id == id)
                    ?? throw new NullReferenceException();

                products.Add(product);
            }

            var order = new Order
            {
                Price = repositoryCart.Price,
                IdUser = 1,
                PurchaseDate = DateTime.Now,
                Products = products,
            };

            repositoryCart.Clear();

            await sOFARIDB.Orders.AddAsync(order);
            await sOFARIDB.SaveChangesAsync();


            return RedirectToAction(nameof(Index), nameof(HomeController).GetNameController());
        }
    }
}
