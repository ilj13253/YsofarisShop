using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SofariDb;

namespace YSofaris.Controllers
{
    public class OrderController(SOFARIDB sOFARIDB) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var orders = await sOFARIDB.Orders
                .Where(order => order.IdUser == 1)
                .Include(order => order.Products)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Details()
        {
            var order = await sOFARIDB.Orders
                .Include(order => order.Products)
                .FirstOrDefaultAsync(order => order.Id == 5);

            return View(order);
        }
    }
}
