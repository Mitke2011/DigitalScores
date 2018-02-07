using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class UserController : Controller
    {
        // GET: User Ovde ce view da bude Index.cshtml
        public ActionResult Index()
        {
            return View();
        }

        // POST: User Login //nisam siguran koji view da koristim.
        [HttpPost]
        public ActionResult Login(Users user)
        {
            // dodao sam ovo polje current koje cuva objekat DB manager koji koristim da pristupim metodi VerifyUserByPassword
            //dodao sam metodu VerifyuUserbyPassword u DB manager. Ne znam jel to dobra ideja.
            Users u = UsersDbManager.Current.VerifyUserByPassword(user.Username, user.Password);
            
            if (u != null)
            {
                Session["currentUser"] = u;
                return RedirectToAction("Index");
            }

            ViewBag.LoginValidation = "Login is not successful, please try again";
            return View("Index");
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
