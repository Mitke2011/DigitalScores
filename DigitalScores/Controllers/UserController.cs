using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;

namespace DigitalScores.Controllers
{
    public class UserController : Controller
    {
        // GET: User 
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Listing()
        {
            return View(UsersDbManager.Current.GetAll());
        }
        // POST: User Login
        [HttpPost]
        public ActionResult Login(Users user)
        {
            Users u = UsersDbManager.Current.VerifyUserByPassword(user.Username, user.Password);

            if (u != null)
            {
                Session["currentUser"] = u;
                if (u.Privilege == Privilege.Admin || u.Privilege == Privilege.SuperAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Delegates");
                }

            }

            ViewBag.LoginValidation = "Login is not successful, please try again";
            return View("Login");
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

        [HttpGet]
        public ActionResult Logoff()
        {
            Session["currentUser"] = null;
            return RedirectToAction("Index");
        }
    }
}
