using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    [AllowAnonymous]
    public class KioskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}