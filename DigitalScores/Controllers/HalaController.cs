﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.DbManagers;
using DigitalScores.Models;

namespace DigitalScores.Controllers
{
    public class HalaController : Controller
    {
        // GET: Hala
        public ActionResult Index()
        {
            return View("HalaListing", HalaDbManager.Current.GetHale());
        }

        // GET: Hala/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hala/Create
        public ActionResult Create()
        {
            return View("HalaEntry");
        }

        // POST: Hala/Create
        [HttpPost]
        public ActionResult Create(Hala hala)
        {
            try
            {

                HalaDbManager.Current.Insert(hala);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Hala/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hala/Edit/5
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

        // GET: Hala/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hala/Delete/5
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