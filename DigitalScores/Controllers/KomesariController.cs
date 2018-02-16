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
            return View();
        }

        // POST: Komesari/Edit/5
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
