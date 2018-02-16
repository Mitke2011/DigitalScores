using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.DbManagers;
using DigitalScores.Models;

namespace DigitalScores.Controllers
{
    public class KoloController : Controller
    {
        // GET: Kolo
        public ActionResult Index()
        {
            return View("KoloPreview", KoloDbManager.Current.GetRounds());
        }

        // GET: Kolo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kolo/Create
        public ActionResult Create()
        {
            return View("KoloEntry");
        }

        // POST: Kolo/Create
        [HttpPost]
        public ActionResult Create(Kolo kolo)
        {
            try
            {
                KoloDbManager.Current.Insert(kolo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kolo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kolo/Edit/5
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

        // GET: Kolo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kolo/Delete/5
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
