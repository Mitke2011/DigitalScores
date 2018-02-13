using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class LigeController : Controller
    {
        // GET: Lige
        public ActionResult Index()
        {
            
            return View("LigePreview", DbManagers.LigaDbManager.Current.GetLeagues());
        }

        // GET: Lige/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lige/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lige/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Lige/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lige/Edit/5
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

        // GET: Lige/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lige/Delete/5
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
