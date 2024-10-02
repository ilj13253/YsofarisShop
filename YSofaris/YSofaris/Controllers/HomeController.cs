using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SofariDb;
using YSofaris.Models;

namespace YSofaris.Controllers;

public class HomeController(SOFARIDB context) : Controller
{
    public async Task<IActionResult> Index(int idCategory)
    {
        var products = await context.Products
            .Include(product => product.Category)
            .Where(product => product.Category.Id == idCategory)
            .ToListAsync();

        products.ForEach(product => product.Title += " " + product.ColorTitleProduct);

        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
