using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Repository;

namespace Shopping.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext context)
        {
            _dataContext = context;
        }
		public IActionResult Index(string Slug ="")
        {
            CategoryModel category = _dataContext.Categories.Where(c => c.Slug == Slug).FirstOrDefault();

            if (category == null) return RedirectToAction("Index");
           



            var productsByCategory = _dataContext.Products.Include("Category").Include("Brand").Where(p => p.CategoryId == category.Id)
               .OrderByDescending(x => x.Id).ToList();

                

			return View(productsByCategory);
        }
    }
}
