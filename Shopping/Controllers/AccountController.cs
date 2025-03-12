﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.ViewModels;

namespace Shopping.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;
		
		public AccountController(SignInManager<AppUserModel> signInManager, UserManager<AppUserModel> userManager)
		{ 
			_signInManager = signInManager;
			_userManager = userManager;
		}
	
		public IActionResult Login(string returnUrl)
		{
		return View(new LoginViewModel { ReturnURL = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginVM)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.Username, loginVM.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(loginVM.ReturnURL ?? "/");
				}
				ModelState.AddModelError("", "Username hoặc Password bị sai");
			}
			return View(loginVM);
		}
		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid) 
			{ 
			AppUserModel newUser = new AppUserModel { UserName = user.Username, Email = user.Email};
				IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
				if (result.Succeeded) 
				{
					TempData["success"] = "Tạo User thành công";
					return Redirect("/account/login");
				}
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View();
		}
		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
