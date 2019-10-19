using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public class LoginController : Controller
	{
		private readonly Services.IAuthenticationService _authService;

		public LoginController(Services.IAuthenticationService authService)
		{
			_authService = authService;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			var model = new LoginViewModel();

			return LoginView(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
            if (!ModelState.IsValid)
			{
				return LoginView(model);
			}

			try
			{
				var user = _authService.Login(model.Username, model.Password);

				if (user == null)
				{
					throw new KeyNotFoundException(@"Failed to authenticate");
				}

				var userClaims = new List<Claim>
				{
					new Claim("displayName", user.DisplayName),
					new Claim("username", user.Username)
				};

				if (user.IsAdmin)
				{
					var adminClaim = new Claim(ClaimTypes.Role, "Admins");

					userClaims.Add(adminClaim);
				}

				var claimsIdentity = new ClaimsIdentity(userClaims, _authService.GetType().Name);
				var principal = new ClaimsPrincipal(claimsIdentity);

				await HttpContext.SignInAsync("app", principal);

				return Redirect("/");
			}
			catch (Exception exception)
			{
				ModelState.AddModelError(string.Empty, exception.Message);
			}

			return LoginView(model);
		}

		[Authorize(Roles = "timecard_user")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("app");

			return Redirect("/");
		}

		private IActionResult LoginView(LoginViewModel model)
		{
			return View(@"Login", model);
		}
	}
}