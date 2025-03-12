using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.ViewModels;
using Shopping.Repository;

namespace Shopping.Controllers
{
    public class CartController : Controller
    {
		private readonly DataContext _dataContext;
		public CartController(DataContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index()
		{
			List<CartItemModel> CartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart")?? new List<CartItemModel>();
			CartItemViewModel CartVM = new()
			{
				CartItems = CartItems, 
				GrandTotal = CartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(CartVM);
		}
		public async Task<IActionResult> Add(int Id)
		{
			ProductModel Product = await _dataContext.Products.FindAsync(Id);

			List<CartItemModel> Cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

			CartItemModel cartItems = Cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItems == null) { 

				Cart.Add( new CartItemModel(Product));
			}
			else
			{
				cartItems.Quantity += 1;
			}
			HttpContext.Session.SetJson("Cart", Cart);
			TempData["success"] = "Thêm sản phẩm thành công ";

            return Redirect(Request.Headers["Referer"].ToString());
		}
		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> Cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = Cart.Where(c=>c.ProductId == Id).FirstOrDefault();

			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else 
			{ 
			Cart.RemoveAll(p => p.ProductId == Id);
			}
			if (Cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else {
				HttpContext.Session.SetJson("Cart", Cart);
			}
            TempData["success"] = "Giảm số lượng sản phẩm thành công ";
            return RedirectToAction("Index");
		}
		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> Cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			CartItemModel cartItem = Cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				Cart.RemoveAll(p => p.ProductId == Id);
			}
			if (Cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", Cart);
			}
            TempData["success"] = "Tăng số lượng sản phẩm thành công ";
            return RedirectToAction("Index");
		}
		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> Cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");

			Cart.RemoveAll(p => p.ProductId == Id);

			if (Cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", Cart);
			}
            TempData["success"] = "Xóa sản phẩm thành công ";
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Clear(int Id)
		{
			HttpContext.Session.Remove("Cart");
            TempData["success"] = "Xóa tất cả sản phẩm thành công ";
            return RedirectToAction("Index");
		}
	}
}
