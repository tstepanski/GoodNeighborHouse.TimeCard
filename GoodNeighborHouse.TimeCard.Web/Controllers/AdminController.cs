using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GoodNeighborHouse.TimeCard.Web.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        public AdminController(IUnitOfWorkFactory unitOfWorkFactory) {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult Kiosk(CancellationToken cancellationToken = default)
        {
            return RedirectToAction("Index", "Kiosk");
        }

        [HttpGet()]
        public IActionResult Admin(CancellationToken cancellationToken = default)
        {
            return View("Admin");
        }

    }
}
