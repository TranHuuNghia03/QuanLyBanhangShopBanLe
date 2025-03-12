using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Repository;

namespace Shopping.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;

		public BrandController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index(string Slug = "")
		{
			BrandModel brand = _dataContext.Brands.Where(c => c.Slug == Slug).FirstOrDefault();

			if (brand == null) return RedirectToAction("Index");




			var productsByBrand = _dataContext.Products.Include("Category").Include("Brand").Where(p => p.BrandId == brand.Id)
			   .OrderByDescending(x => x.Id).ToList();



			return View(productsByBrand);
		}
	}
}
