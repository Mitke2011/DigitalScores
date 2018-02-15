﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.DbManagers;


namespace DigitalScores.Controllers
{
    public class UtakmicaController : Controller
    {
        // GET: Utakmice

        public ActionResult Index(int ligaId)
        {
            UtakmicaDbManager udb = new UtakmicaDbManager();
            return View("GamePreview", udb.GetGamesByLeague(ligaId));
        }

        // GET: Utakmica/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Utakmica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utakmica/Create
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

        // GET: Utakmica/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Utakmica/Edit/5
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

        // GET: Utakmica/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Utakmica/Delete/5
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
