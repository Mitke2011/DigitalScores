﻿using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DigitalScores.Controllers
{
    public class KomesariController : Controller
    {
        // GET: Komesari
        public ActionResult Index()
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("KomesariListing", KomesariDbManager.Current.GetAllKomesari());
        }

        // GET: Komesari/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Komesari/Create
        public ActionResult Create()
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("KomesariEntry");
        }

        // POST: Komesari/Create
        [HttpPost]
        public ActionResult Create(Komesari komesar)
        {
            try
            {
                bool postoji = KomesariDbManager.Current.CheckIfKomesarExists(komesar);
                if (postoji)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    KomesariDbManager.Current.Insert(komesar);
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Komesari/Edit/5
        public ActionResult Edit(int id)
        {
            Users u = LoginGuard();
            ViewBag.AdminIme = u.Ime;
            ViewBag.AdminPrezime = u.Prezime;
            ViewBag.Lige = LigaDbManager.Current.GetLeagues(u.UserRegion.Id);
            Komesari k = (Komesari)KomesariDbManager.Current.GetSingle(id);
            SetSelectedLeague(k.Liga,u.UserRegion.Id);
            return View("EditKomesara", k);
        }


        private void SetSelectedLeague(Liga liga, int userRegionId)
        {
            List<Liga> values = new List<Liga>();

            foreach (var item in LigaDbManager.Current.GetLeagues(userRegionId))
            {
                values.Add((Liga)item);
            }

            IEnumerable<SelectListItem> items =

                from value in values
                select new SelectListItem

                {

                    Text = value.Naziv,

                    Value = value.Id.ToString(),

                    Selected = value.Id == liga.Id,

                };
            ViewBag.KomesarLig = items;

        }

        // POST: Komesari/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Komesari k = new Komesari(id)
                {
                    Ime = collection["ime"],
                    Prezime = collection["prezime"],
                    Email = collection["email"],
                    Telefon = collection["telefon"],
                    Liga = (Liga)LigaDbManager.Current.GetSingle(int.Parse(collection["ligakomesar"]))


                };

                KomesariDbManager.Current.Update(k);

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

        private Users LoginGuard()
        {
            Users result = null;
            Users current = Session["currentUser"] as Users;
            if (current != null && current.UserPrivilege == Privilege.Invalid)
            {
                result = current;
            }

            return result;
        }
    }
}
