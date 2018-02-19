using System.Web.Mvc;
using DigitalScores.Models;
using DigitalScores.DbManagers;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View(FillUsers(UsersDbManager.Current.GetAll()));
        }

        private List<Users> FillUsers(List<object> list)
        {
            List<Users> result = new List<Users>();
            foreach (var item in list)
            {
                result.Add((Users)item);
            }
            return result;
        }

        // POST: User Login
        [HttpPost]
        public ActionResult Login(Users user)
        {
            Users u = UsersDbManager.Current.VerifyUserByPassword(user.Username, user.Password);

            if (u != null)
            {
                Session["currentUser"] = u;
                
                switch (u.UserPrivilege)
                {
                    case Privilege.SuperAdmin:                        
                    case Privilege.Admin:
                        return RedirectToAction("Index", "Admin");
                    case Privilege.Delegate:
                        return RedirectToAction("Index", "Delegates");
                    case Privilege.Invalid:
                        return RedirectToAction("Logoff");
                    default:
                        break;
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
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            return View();
        }

        // POST: User/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        User u = new User() {
        //            Username = collection["username"],
        //            Password = collection["password"],
        //            Ime = collection["ime"],
        //            Prezime = collection["prezime"],
        //            Email = collection["email"],
        //            UserPrivilege = SetPrivilege(collection["privileges"]),
        //            Grad = collection["grad"],
        //            Region = collection["region"],
        //            Telefon = collection["telefon"]
        //        };
        //        UsersDbManager.Current.Insert(u);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        public ActionResult Create(Users entry)
        {
            try
            {
                ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
                ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
                entry.UserPrivilege = SetPrivilege(Request.Form["privileges"]);
                UsersDbManager.Current.Insert(entry);
                return RedirectToAction("Listing");
            }
            catch
            {
                return View();
            }
        }
        private Privilege SetPrivilege(string selection)
        {
            switch (selection)
            {
                case "Admin":
                    return Privilege.Admin;
                case "Delegat":
                    return Privilege.Delegate;
                default:
                    return Privilege.Invalid;
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
            Users u = UsersDbManager.Current.GetSingle(id) as Users;
            SetUserPrivilegeForEdit(u.UserPrivilege);
            return View(u);
        }

        private void SetUserPrivilegeForEdit(Privilege selectedPrivilege)
        {

            IEnumerable<Privilege> values =

                              Enum.GetValues(typeof(Privilege))

                              .Cast<Privilege>();

            IEnumerable<SelectListItem> items =

                from value in values
                where value != Privilege.SuperAdmin
                select new SelectListItem

                {

                    Text = value.ToString(),

                    Value = value.ToString(),

                    Selected = value == selectedPrivilege,

                };

            ViewBag.SelectedPrivilege = items;

        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                Users u = new Users(int.Parse(collection["id"]))
                {
                    Username = collection["username"],
                    Password = collection["password"],
                    Ime = collection["ime"],
                    Prezime = collection["prezime"],
                    Email = collection["email"],
                    UserPrivilege = SetPrivilege(collection["privileges"]),
                    Grad = collection["grad"],
                    Region = collection["region"],
                    Telefon = collection["telefon"]
                };
                UsersDbManager.Current.Update(u);
                return RedirectToAction("Listing");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.AdminIme = (Session["currentUser"] as Users).Ime;
            ViewBag.AdminPrezime = (Session["currentUser"] as Users).Prezime;
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
