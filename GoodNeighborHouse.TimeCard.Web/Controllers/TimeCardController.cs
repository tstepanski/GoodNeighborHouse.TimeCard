using GoodNeighborHouse.TimeCard.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using GoodNeighborHouse.TimeCard.Data.Repositories;
using GoodNeighborHouse.TimeCard.Data.UnitOfWork;
using GoodNeighborHouse.TimeCard.General;
using TimeCardModel = GoodNeighborHouse.TimeCard.Web.Models.Volunteer;
using TimeCardEntity = GoodNeighborHouse.TimeCard.Data.Entities.Volunteer;


namespace GoodNeighborHouse.TimeCard.Web.Controllers
{
    public class TimeCardController : Controller
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConverter<TimeCardEntity, TimeCardModel> _converter;
        private readonly IMapper<TimeCardModel, TimeCardEntity> _mapper;

        public TimeCardController(IUnitOfWorkFactory unitOfWorkFactory, IConverter<TimeCardEntity, TimeCardModel> converter, IMapper<TimeCardModel, TimeCardEntity> mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _converter = converter;
            _mapper = mapper;
        }

        // GET: TimeCard
        public ActionResult Index()
        {
            //get user timecard info
            var tc = new TimeCard();
            return View(tc);
        }

        // POST: TimeCard/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Guid userID, string punchType)
        {
            try
            {
                // TODO: Add punch to db
                //add punch (userID, punchType, DateTime.Now);

                return View();
                //return RedirectToAction(nameof(Index));
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