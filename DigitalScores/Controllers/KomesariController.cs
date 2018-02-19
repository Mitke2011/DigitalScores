using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View("EditKomesara", KomesariDbManager.Current.GetSingle(id));
        }

        // POST: Komesari/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Komesari komesar)
        {
            try
            {
                Komesari ek = (Komesari)KomesariDbManager.Current.GetSingle(id);

                ek.Ime = komesar.Ime;
                ek.Prezime = komesar.Prezime;
                ek.Email = komesar.Email;
                ek.Telefon = komesar.Telefon;
                ek.LigaId = komesar.LigaId;

                KomesariDbManager.Current.Update(ek);

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
    }
}
