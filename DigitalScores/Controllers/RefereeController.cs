using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class RefereeController : Controller
    {
        // GET: Referee
        public ActionResult Index()
        {
            return View();
        }

        // GET: Referee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Referee/Create
        public ActionResult Create()
        {
            return View("Referees");
        }

        // POST: Referee/Create
        [HttpPost]
        public ActionResult Create(Sudija referee)
        {
            try
            {
                Sudija s = referee;
                // Implement verification if the Referee already exists in the system
                RefereeDbManager.Current.Insert(s);
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }
        }

        // GET: Referee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Referee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Referee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Referee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
