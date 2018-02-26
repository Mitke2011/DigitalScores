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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View(KlubDbManager.Current.GetSingle(id));
        }

        public ActionResult Create(int ligaId, string nazivLige)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("KlubCreate", new Klub() { LigaKlub = new Liga(ligaId) { Naziv = nazivLige } });
        }

        // POST: Klub/Create
        [HttpPost]
        public ActionResult Create(Klub entry)
        {
            try
            {
                entry.LigaKlub = new Liga(int.Parse(Request.Form["LigaKlub.Id"]));
                entry.KlubSport = new Sport(int.Parse(Request.Form["KlubSport.Id"]));
                KlubDbManager.Current.Insert(entry);

                return RedirectToAction("KlubListing");
            }
            catch (Exception e)
            {
                return View("KlubCreate");
            }
        }

        // GET: Klub/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View(KlubDbManager.Current.GetSingle(id) as Klub);
        }

        //public ActionResult Edit(int idKlub, int idLige, string nazivLige)
        //{
        //    Klub k = KlubDbManager.Current.GetSingle(idKlub) as Klub;
        //    k.LigaKlub = new Liga(idLige) { Naziv = nazivLige };
        //    return View(k);
        //}

        // POST: Klub/Edit/5

        [HttpPost]
        public ActionResult Edit(Klub entry)
        {
            try
            {
                entry.LigaKlub = new Liga(int.Parse(Request.Form["LigaKlub.Id"]));
                entry.KlubSport = new Sport(int.Parse(Request.Form["KlubSport.Id"]));
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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
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
            catch (Exception e)
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
