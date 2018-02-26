using System.Web.Mvc;
using DigitalScores.DbManagers;
using DigitalScores.Models;
using System;

namespace DigitalScores.Controllers
{
    public class UtakmicaController : Controller
    {
        // GET: Utakmice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowGamesActiveRound(int ligaId)
        {
           
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                if (current.UserPrivilege == Privilege.Delegate)
                {
                    ViewBag.DelegatIme = current.Ime;
                    ViewBag.DelegatPrezime = current.Prezime;
                    return View("GamePreview", UtakmicaDbManager.Current.GetGamesByLeague(ligaId));
                }
            }
            return View("logoff", "user");
        }

        public ActionResult ShowGamesAllRounds(int ligaId)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                if (current.UserPrivilege == Privilege.Admin)
                {
                    ViewBag.LigaId = ligaId;
                    ViewBag.AdminIme = current.Ime;
                    ViewBag.AdminPrezime = current.Prezime;
                    return View("GamePreviewAdmin", UtakmicaDbManager.Current.GetGamesByLeagueAdmin(ligaId));
                }
            }
            return View("logoff", "user");
        }

        // GET: Utakmica/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Utakmica/Create
        public ActionResult Create(int ligaId)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            Users current = (Users)Session["currentUser"];
            if (current != null)
            {
                if (current.UserPrivilege == Privilege.Admin)
                {
                    ViewBag.LigaId = ligaId;
                    return View("GameEntry");
                }
            }
            return View("logoff", "user");
        }

        // POST: Utakmica/Create
        [HttpPost]
        public ActionResult Create(Utakmice utakmica, int ligaId)
        {
            try
            {
                
                utakmica.ligaId = ligaId;
                UtakmicaDbManager.Current.Insert(utakmica);
                return RedirectToAction("ShowGamesAllRounds", "Utakmica", new { ligaId = ligaId });

            }
            catch(Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                return View();
            }
        }

        private void LoginGuard()
        {
            Users current = Session["currentUser"] as Users;
            if (current == null)
            {
                RedirectToAction("Logoff", "User");
            }
        }
    }
}
