using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class KategorijaController : Controller
    {
        // GET: Kategorija
        public ActionResult Index()
        {
            return View("KategorijeListing", KategorijaDbManager.Current.GetKategorije());
        }

        // GET: Kategorija/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Kategorija/Create
        public ActionResult Create()
        {
            return View("KategorijaEntry");
        }

        // POST: Kategorija/Create
        [HttpPost]
        public ActionResult Create(Kategorija kategorija)
        {
            try
            {
                KategorijaDbManager.Current.Insert(kategorija);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Kategorija/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Kategorija/Edit/5
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

        // GET: Kategorija/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Kategorija/Delete/5
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
