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
    public class AuthorizedController : Controller
    {
        private IGenericRepository<AuthorizedService> repository = null;

        public static int AuthorizedId;

        public AuthorizedController()
        {
            this.repository = new GenericRepository<AuthorizedService>();
        }

        public AuthorizedController(IGenericRepository<AuthorizedService> repository)
        {
            this.repository = repository;
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddAuthorized()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthorized(AuthorizedService model)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(model);
                repository.Save();
                return RedirectToAction("Login", "Authorized");
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditAuthorized()
        {
            var authorized = repository.GetById(AuthorizedId);
            return View(authorized);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditAuthorized(AuthorizedService model)
        {
            if (ModelState.IsValid)
            {
                repository.Update(model);
                repository.Save();
                return RedirectToAction("Index", "Authorized");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthorizedService LoginAuthorized)
        {
            IEnumerable<AuthorizedService> authorizeds = repository.GetAll();

            AuthorizedService authorizedService = authorizeds.FirstOrDefault(x => x.Email == LoginAuthorized.Email && x.Password == LoginAuthorized.Password);

            if (authorizedService != null)
            {
                AuthorizedId = authorizedService.AuthorizedID;
                FormsAuthentication.SetAuthCookie(authorizedService.Email, false);
                return RedirectToAction("Index", "Authorized");
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