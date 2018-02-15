using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class KomesariController : Controller
    {
        // GET: Komesari
        public ActionResult Index()
        {
            return View();
        }

        // GET: Komesari/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Komesari/Create
        public ActionResult Create()
        {
            return View("KomesariEntry");
        }

        // POST: Komesari/Create
        [HttpPost]
        public ActionResult Create(Komesari komesar)
        {
            try
            {
                Komesari k = komesar;
                // Implement verification if the Referee already exists in the system
                KomesariDbManager.Current.Insert(k);
                return RedirectToAction("Create");

            }
            catch
            {
                return View();
            }
        }

        // GET: Komesari/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Komesari/Edit/5
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

        // GET: Komesari/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Komesari/Delete/5
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
