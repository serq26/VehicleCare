using AlpataProje.GenericRepository;
using AlpataProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AlpataProje.Controllers
{
    public class SecurityController : Controller
    {
        private IGenericRepository<User> repository = null;

        public static int Userid;
        public SecurityController()
        {
            this.repository = new GenericRepository<User>();
        }
        public SecurityController(IGenericRepository<User> repository)
        {
            this.repository = repository;

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User LoginUser)
        {
            IEnumerable<User> users = repository.GetAll();

            User user = users.FirstOrDefault(x => x.Email == LoginUser.Email && x.Password == LoginUser.Password);

            if (user!=null)
            {
                Userid = user.UserID;
                FormsAuthentication.SetAuthCookie(user.Email, false);
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.Message = "E-posta veya Şifre yanlış";
                return View();
            }          
        }     

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}