using System.Web.Mvc;
using DigitalScores.DbManagers;
using System.Collections.Generic;
using DigitalScores.Models;
using System;

namespace DigitalScores.Controllers
{
    public class KlubController : Controller
    {
        // GET: Klub
        public ActionResult KlubListing()
        {
            List<Klub> result = new List<Klub>();

            foreach (var item in KlubDbManager.Current.GetAll())
            {
                result.Add(item as Klub);
            }
            return View("KlubListing", result);
        }

        // GET: Klub/Details/5
        public ActionResult Details(int id)
        {
            return View(KlubDbManager.Current.GetSingle(id));
        }

        // GET: Klub/Create
        public ActionResult Create()
        {
            return View("KlubCreate");
        }

        // POST: Klub/Create
        [HttpPost]
        public ActionResult Create(Klub entry)
        {
            try
            {
                KlubDbManager.Current.Insert(entry);

                return RedirectToAction("KlubListing");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Klub/Edit/5
        public ActionResult Edit(int id)
        {            
            return View(KlubDbManager.Current.GetSingle(id));
        }

        // POST: Klub/Edit/5
        [HttpPost]
        public ActionResult Edit(Klub entry)
        {
            try
            {
                KlubDbManager.Current.Update(entry);
                return RedirectToAction("KlubListing");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Klub/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
                        return View(KlubDbManager.Current.GetSingle(id));
        }

        // POST: Klub/Delete/5
        [HttpPost]
        public ActionResult Delete(Klub entry)
        {
            try
            {
                KlubDbManager.Current.DeleteSingle(entry);
                return RedirectToAction("KlubListing");
            }
            catch(Exception e)
            {
                return View();
            }
        }
    }
}
