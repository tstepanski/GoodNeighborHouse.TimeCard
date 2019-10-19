using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class TimeCardController : Controller
    {
        // GET: TimeCard
        public ActionResult Index()
        {
            return View();
        }

        // POST: TimeCard/AddPunch
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPunch(Guid userID, string punchType)
        {
            try
            {
                // TODO: Add punch to db

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeCard/Edit/5
        public ActionResult Edit(Guid userID)
        {
            return View();
        }

        // POST: TimeCard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid userID, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TimeCard/Delete/5
        public ActionResult Delete(Guid userID)
        {
            return View();
        }

        // POST: TimeCard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid userID, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}