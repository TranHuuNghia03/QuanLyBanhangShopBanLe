using Microsoft.EntityFrameworkCore;
using Shopping.Models;

namespace Shopping.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if (!_context.Products.Any())
			{
				CategoryModel macbook = new CategoryModel {Name= "macbook", Slug= "macbook", Description= "macbook is large Brand in the world", Status=1};
				CategoryModel Pc = new CategoryModel { Name = "Pc", Slug = "Pc", Description = "Pc is large Brand in the world", Status = 1 };
				
				BrandModel apple = new BrandModel { Name = "Apple", Slug = "Apple", Description = "Apple is large Brand in the world", Status = 1 };
				BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "Samsung", Description = "Samsung is large Brand in the world", Status = 1 };
				_context.Products.AddRange(

					new ProductModel { Name = "Macbook", Slug = "Macbook", Description = "Macbook is large Brand in the world", Image = "1.jpg", Category = macbook, Brand = apple, Price = 1222 },
					new ProductModel { Name = "PC", Slug = "PC", Description = "PC is large Brand in the world", Image = "1.jpg", Category = Pc, Brand = samsung, Price = 1222 }


				);
				_context.SaveChanges();
			}
		}
	}
}
