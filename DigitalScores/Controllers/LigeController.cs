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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                if (current.UserPrivilege == Privilege.Admin || current.UserPrivilege == Privilege.SuperAdmin)
                {
                    return View();
                }
            }
            return RedirectToAction("Logoff", "User");
        }

        [HttpGet]
        public ActionResult SelectLeague(int klubId)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            ViewBag.KlubId = klubId;
            return View("SelectLeague");
        }

        [HttpGet]
        public ActionResult SelectLeagueSeason(int seasonId)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            ViewBag.SeasonId = seasonId;
            return View("SelectLeague");
        }
        

        public ActionResult SelectLeague(string nazivLige, string ligaCat, int? klubId=null,int? seasonId=null)
        {
            if (klubId!=null)
            {
                ViewBag.KlubId = klubId;
            }
            if (seasonId!=null)
            {
                ViewBag.SeasonId = seasonId;
            }
            
            return View(FilterLeagues(nazivLige, ligaCat));
        }

        //public ActionResult SelectLeague(string nazivLige, string ligaCat, int seasonId)
        //{
        //    ViewBag.SeasonId = seasonId;
        //    return View(FilterLeagues(nazivLige, ligaCat));
        //}

        private List<Liga> FilterLeagues(string nazivLige, string kategorijaLige)
        {
            Users currentUser = LoginGuard();
            List<Liga> result = new List<Liga>();
            if (currentUser!=null)
            {
                result = LigaDbManager.Current.FindLeagueByNameAndCat(nazivLige, kategorijaLige, currentUser.UserRegion.Id);
            }

            return result;
        }

        public ActionResult LeaguesForCategory(int katId)
        {            
            Users current = LoginGuard();
            if (current != null)
            {
                ViewBag.AdminIme = current.Ime;
                ViewBag.AdminPrezime = current.Prezime;
                switch (current.UserPrivilege)
                {

                    case Privilege.Delegate:
                        ViewBag.DelegatIme = current.Ime;
                        ViewBag.DelegatPrezime = current.Prezime;
                        return View("LigePreviewDelegate", LigaDbManager.Current.GetLeaguesByCategory(katId, current.UserRegion.Id));

                    case Privilege.Admin:
                        ViewBag.KatId = katId;
                        ViewBag.AdminIme = current.Ime;
                        ViewBag.AdminPrezime = current.Prezime;
                        return View("LigePreviewAdmin", LigaDbManager.Current.GetLeaguesByCategory(katId, current.UserRegion.Id));
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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
           

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
