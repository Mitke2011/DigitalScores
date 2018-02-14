﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class RezultatiController : Controller
    {
        // GET: Rezultati
        public ActionResult Index()
        {
            return View("UnosRezultata");
        }

        [ActionName("IndexId")]
        public ActionResult Index(int utakmicaId)
        {
            return View("UnosRezultata", utakmicaId);
        }

        // GET: Rezultati/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rezultati/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rezultati/Create
        [HttpPost]
        public ActionResult Create(Rezultati rezultat)
        {
            try
            {
                // TODO: Add insert logic here
                RezultatiDbManager.Current.Insert(rezultat);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezultati/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rezultati/Edit/5
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

        // GET: Rezultati/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rezultati/Delete/5
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
