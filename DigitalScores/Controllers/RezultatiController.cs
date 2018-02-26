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

        public ActionResult UnosRezultata(int utakmicaId)
        {
            Users u = SessionCheck();
            if (u != null)
            {
                Rezultati re = new Rezultati() { RezultatUtakmica = (Utakmice)UtakmicaDbManager.Current.GetSingle(utakmicaId) };
                ViewBag.DelegatIme = u.Ime;
                ViewBag.DelegatIme = u.Prezime;
                return View("UnosRezultata", re);
            }
            return View("Logoff", "Users");
        }

        public ActionResult PregledRezultataUtakmice(int utakmicaId)
        {
            return View();
        }

        private Users SessionCheck()
        {
            Users result = null;
            result = (Users)Session["currentUser"];
            return result;
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
        public ActionResult Create(Rezultati rezultat, int utakmicaId)
        {
            try
            {
                rezultat.UtakmicaId = utakmicaId;
                RezultatiDbManager.Current.Insert(rezultat);
                Utakmice u = (Utakmice)UtakmicaDbManager.Current.GetSingle(utakmicaId);
                return RedirectToAction("ShowGamesActiveRound", "Utakmica", new { ligaid = u.LigaUtakmice.Id });
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
