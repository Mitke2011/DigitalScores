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
            if (LoginGuard()!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }

        }
        public ActionResult ShowGamesActiveRound(int ligaId)
        {
            Users current  = LoginGuard();
            if (current!=null)
            {             
                if (current.UserPrivilege == Privilege.Delegate)
                {
                    ViewBag.DelegatIme = current.Ime;
                    ViewBag.DelegatPrezime = current.Prezime;
                    return View("GamePreview", UtakmicaDbManager.Current.GetGamesByLeagueDelegate(ligaId,current.Id));
                }
                else
                {
                    // You are not allowed to see the content of this page!
                    return null;
                }
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }


        }

        public ActionResult ShowGamesAllRounds(int ligaId)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
                ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;                

                if (current.UserPrivilege == Privilege.Admin)
                {
                    ViewBag.LigaId = ligaId;
                    ViewBag.AdminIme = current.Ime;
                    ViewBag.AdminPrezime = current.Prezime;

                    return View("GamePreviewAdmin", UtakmicaDbManager.Current.GetGamesByLeagueAdmin(ligaId));
                }

                // You are not allowed to see the content of this page!
                return null;
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // GET: Utakmica/Details/5
        public ActionResult Details(int id)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // GET: Utakmica/Create
        public ActionResult Create(int ligaId)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
                ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
                
                if (current != null)
                {
                    if (current.UserPrivilege == Privilege.Admin)
                    {
                        ViewBag.LigaId = ligaId;
                        return View("GameEntry");
                    }
                }
                // You are not allowed to see the content of this page!
                return null;
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // POST: Utakmica/Create
        [HttpPost]
        public ActionResult Create(Utakmice utakmica, int ligaId)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                try
                {
                    utakmica.ligaId = ligaId;
                    UtakmicaDbManager.Current.Insert(utakmica);
                    return RedirectToAction("ShowGamesAllRounds", "Utakmica", new { ligaId = ligaId });

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // GET: Utakmica/Edit/5
        public ActionResult Edit(int id)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // POST: Utakmica/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Users current = LoginGuard();
            if (current!=null)
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
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // GET: Utakmica/Delete/5
        public ActionResult Delete(int id)
        {
            Users current = LoginGuard();
            if (current!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logoff", "User");
            }
        }

        // POST: Utakmica/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Users current = LoginGuard();
            if (current!=null)
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
            else
            {
                return RedirectToAction("Logoff", "User");
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
