﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalScores.Controllers
{
    public class DelegatesController : Controller
    {
        // GET: Delegates
        public ActionResult Index()
        {
            return View();
        }

        // GET: Delegates/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Delegates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Delegates/Create
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

        // GET: Delegates/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Delegates/Edit/5
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

        // GET: Delegates/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delegates/Delete/5
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
