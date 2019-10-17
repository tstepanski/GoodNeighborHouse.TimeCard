﻿using System.Diagnostics;
using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}