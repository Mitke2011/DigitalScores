using System;
using System.Web.Mvc;
using DigitalScores.DbManagers;
using DigitalScores.Models;
using System.Collections.Generic;


namespace DigitalScores.Controllers
{
    public class LigeController : Controller
    {
        // GET: Lige       
        public ActionResult Index()
        {
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                if (current.Privilege == Privilege.Admin || current.Privilege == Privilege.SuperAdmin)
                {
                    return View();
                }
            }
            return RedirectToAction("Logoff", "User");
        }

        [HttpGet]
        public ActionResult SelectLeague(int klubId)
        {
            ViewBag.KlubId = klubId;
            return View();
        }

        public ActionResult SelectLeague(string nazivLige, string ligaCat, int klubId)
        {
            ViewBag.KlubId = klubId;
            return View(FilterLeagues(nazivLige, ligaCat));
        }

        private List<Liga> FilterLeagues(string nazivLige, string kategorijaLige)
        {
            return LigaDbManager.Current.FindLeagueByNameAndCat(nazivLige, kategorijaLige);
        }

        public ActionResult LeaguesForCategory(int katId)
        {
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                switch (current.Privilege)
                {

                    case Privilege.Delegate:
                        ViewBag.DelegatIme = current.Ime;
                        ViewBag.DelegatPrezime = current.Prezime;
                        return View("LigePreviewDelegate", LigaDbManager.Current.GetLeaguesByCategory(katId));

                    case Privilege.Admin:
                        ViewBag.KatId = katId;
                        ViewBag.AdminIme = current.Ime;
                        ViewBag.AdminPrezime = current.Prezime;
                        return View("LigePreviewAdmin", LigaDbManager.Current.GetLeaguesByCategory(katId));
                }
            }
            return RedirectToAction("Logoff", "User");
        }

        // GET: Lige/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lige/Create
        public ActionResult Create(int katId)
        {
            ViewBag.katId = katId;
            return View("LigeEntry");
        }

        // POST: Lige/Create
        [HttpPost]
        public ActionResult Create(Liga liga, int katId)
        {
            try
            {
                liga.kategorijaId = katId;
                LigaDbManager.Current.Insert(liga);
                return RedirectToAction("LeaguesForCategory", "Lige", new {katId = katId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Lige/Edit/5
        public ActionResult Edit(int id)
        {

            return View("LigeEdit", LigaDbManager.Current.GetSingle(id));
        }

        // POST: Lige/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Liga entry)
        {
            try
            {
                Liga liga = (Liga)LigaDbManager.Current.GetSingle(id);
                liga.Naziv = entry.Naziv;
                LigaDbManager.Current.Update(liga);

                return RedirectToAction("LeaguesForCategory", "Lige", new { katId = liga.LigaKategorija.Id});
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
