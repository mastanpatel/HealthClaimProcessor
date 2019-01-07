using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace HclaimProcessor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(userprofile objUser)
        {
            if (IsValid(objUser.UserName, objUser.Password))
            {
                FormsAuthentication.SetAuthCookie(objUser.UserName, true);
                
                return RedirectToAction("UserDashBoard", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login details are wrong.");
            }

            //if (ModelState.IsValid)
            //{
            //using (mydbEntities db = new mydbEntities())
            //    {

            //        var obj = db.userprofiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
            //        if (obj != null)
            //        {
            //            Session["userid"] = obj.UserId;
            //            Session["username"] = obj.UserName;
            //            return RedirectToAction("userdashboard");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Login details are wrong.");
            //        }
            //    }
            //}
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            //if (Session["SessionId"] != null)
            if(User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(userprofile  user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //mydbEntities db = new mydbEntities()
                    using (var db = new mydbEntities())
                    {
                        var newUser = db.userprofiles.Create();
                        newUser.UserName = user.UserName;
                        newUser.Password = user.Password;

                        newUser.IsActive = true;
                        
                        db.userprofiles.Add(newUser);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool IsValid(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool IsValid = false;

            using (var db = new mydbEntities())
            {
                var user = db.userprofiles.FirstOrDefault(u => u.UserName == username);
                if (user != null)
                {
                   // if (user.Password == crypto.Compute(password, user.PasswordSalt))
                   // {
                        IsValid = true;
                    //}
                }
            }
            return IsValid;
        }
    }
}