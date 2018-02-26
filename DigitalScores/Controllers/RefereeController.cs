using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class RefereeController : Controller
    {
        // GET: Referee
        public ActionResult Index()
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("RefereeListing", RefereeDbManager.Current.GetAllReferee());
        }

        // GET: Referee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Referee/Create
        public ActionResult Create()
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("Referees");
        }

        // POST: Referee/Create
        [HttpPost]
        public ActionResult Create(Sudija referee)
        {
            try
            {
                Sudija s = referee;
                // Implement verification if the Referee already exists in the system
                RefereeDbManager.Current.Insert(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Referee/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("RefereeEdit", RefereeDbManager.Current.GetSingle(id));
        }

        // POST: Referee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Sudija entry)
        {
            try
            {
                Sudija sudija = new Sudija(id)
                {
                    Ime = entry.Ime,
                    Prezime = entry.Prezime,
                    Email = entry.Email,
                    Telefon = entry.Telefon,
                    Grad = entry.Grad
                };

                RefereeDbManager.Current.Update(sudija);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Referee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Referee/Delete/5
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
