using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Shopping.Models;
using Shopping.Models.ViewModels;
using Shopping.Repository;
using System.Security.Claims;

namespace Shopping.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly DataContext _dataContext;
		public CheckoutController(DataContext context)
		{
			_dataContext = context;
		}
		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null)
			{
				return RedirectToAction("Login","Account");
			}

			else
			{

				try
				{ 
					var ordercode = Guid.NewGuid().ToString();
					var orderItem = new OrderModel();
					orderItem.OrderCode = ordercode;
					orderItem.UserName = userEmail;
					orderItem.Status = 1;
					orderItem.CreatedDate = DateTime.Now;
					_dataContext.Add(orderItem);
					await _dataContext.SaveChangesAsync();

					List<CartItemModel> CartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				
					foreach (var cart in CartItems)
					{
						var orderdetails = new OrderDetails();
						orderdetails.UserName = userEmail;
						orderdetails.OrderCode = ordercode;
						orderdetails.ProductId = cart.ProductId;
						orderdetails.Price = cart.Price;
						orderdetails.Quantity = cart.Quantity;

						_dataContext.Add(orderdetails);
						await _dataContext.SaveChangesAsync();
					}
				}
				catch 
				{
					return Ok();
				}


				HttpContext.Session.Remove("Cart");
				TempData["success"] = "Checkout thành công, vui lòng chờ duyệt đơn hàng";
				return RedirectToAction("Index", "Cart");
			}

		}
	}
}
