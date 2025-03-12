using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Repository;

namespace Shopping.Controllers
{
    public class ProductController : Controller
    {
		private readonly DataContext _dataContext;

		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> Details(int Id)
		{
			if (Id == null) return RedirectToAction("Index");

			var productsById = _dataContext.Products.Include("Category").Include("Brand").Where(p => p.Id == Id).OrderByDescending(x => x.Id).ToList().FirstOrDefault();

			return View(productsById);
		}
	}
}
