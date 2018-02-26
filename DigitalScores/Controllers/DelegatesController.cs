using System.Web.Mvc;
using DigitalScores.Models;

namespace DigitalScores.Controllers
{
    public class DelegatesController : Controller
    {
        // GET: Delegates
        public ActionResult Index()
        {
            Users currentDelegate = (Users)Session["currentUser"];
            if (currentDelegate!=null)
            {
                return View("Delegates",currentDelegate);
            }
            return RedirectToAction("Index", "Users");
        }

        // GET: Delegates/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Delegates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Delegates/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Delegates/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Delegates/Edit/5
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

        // GET: Delegates/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delegates/Delete/5
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
