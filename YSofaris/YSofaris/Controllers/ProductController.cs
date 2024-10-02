using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOFARI;
using SofariDb;
using YSofaris.Core;
using YSofaris.Repositories.Extensions;
using YSofaris.ViewModel;

namespace YSofaris.Controllers
{
    public class ProductController(SOFARIDB sOFARIDB) : Controller
    {
        public async Task<IActionResult> Index(int idCategory, string searchTitle, string searchColor, SortProduct? sortProduct, int? pageIndex = 1)
        {
            var colors = sOFARIDB.Products.Select(x => x.ColorTitleProduct).Distinct();

            var productsQueryable = sOFARIDB.Products
                .Include(product => product.Category)
                .Where(product => product.Category.Id == idCategory);

            await productsQueryable.ForEachAsync(product => product.Title += $" {product.ColorTitleProduct}");

            if (!string.IsNullOrEmpty(searchTitle))
            {
                productsQueryable = productsQueryable.Where(product => product.Title.Contains(searchTitle));
            }

            if (!string.IsNullOrEmpty(searchColor))
            {
                productsQueryable = productsQueryable.Where(product => product.ColorTitleProduct == searchColor);
            }

            if (sortProduct is not null)
            {
                productsQueryable = sortProduct switch
                {
                    SortProduct.AscPrice => productsQueryable.OrderBy(product => product.Price),
                    SortProduct.DescPrice => productsQueryable.OrderByDescending(product => product.Price),
                    _ => productsQueryable
                };
            }

            var titleCategory = await sOFARIDB.Categories
                .Where(category => category.Id == idCategory)
                .Select(category => category.Title)
                .FirstAsync();

            var productsListVM = new ProductsListVM
            {
                IdCategory = idCategory,
                TitleCategory = titleCategory,
                Products = await PaginationList<Product>.CreateAsync(productsQueryable, pageIndex.Value, 3),
                SearchTitle = searchTitle,
                SearchColor = searchColor,
                SortProduct = sortProduct,
                Colors = new(colors),
                PaginationList = null
            };

            return View(productsListVM);
        }

        public async Task<IActionResult> Details(int id)
        {
            var myProduct = await sOFARIDB.Products
                .Where(product => product.Id == id)
                .Include(product => product.ProductImgs)
            .FirstAsync();

            var colors = await sOFARIDB.Products
                .Where(product => product.Title == product.Title)
                .ToListAsync();

            var productDetailsVM = new ProductDetailsVM
            {
                Product = myProduct,
                Colors = colors
            };

            return View(productDetailsVM);
        }

        public async Task<IActionResult> ChangeProduct(string title, string colorTitleProduct)
        {
            var id = await sOFARIDB.Products
                .Where(product => product.Title == title && product.ColorTitleProduct == colorTitleProduct)
                .Select(product => product.Id)
                .FirstAsync();

            return RedirectToAction(nameof(Details), new RouteValueDictionary(
              new
              {
                  controller = nameof(ProductController).GetNameController(),
                  action = nameof(Details),
                  Id = id
              }));
        }

        public async Task<IActionResult> AddWishList(int id)
        {
            var product = await sOFARIDB.Products.SingleOrDefaultAsync(product => product.Id == id)
                ?? throw new NullReferenceException();

            var whishList = new WhishList
            {
                IdUser = 1,
                Product = product
            };

            await sOFARIDB.WhishLists.AddAsync(whishList);
            await sOFARIDB.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new RouteValueDictionary(
              new
              {
                  controller = nameof(ProductController).GetNameController(),
                  action = nameof(Details),
                  Id = id
              }));
        }
    }
}
/*
 * var productDetailsVM=new ProductDetailsVM
{
    ProductController=myProduct,
    Colors =colors
}
            return View();
        }
        public async Task<IActionResult>ChangeProduct(string title,string color)
        {
            sOFARIDB.Products.Where(product => product.Title==title&&product.).Select(product=>product.Id)
        }
 */