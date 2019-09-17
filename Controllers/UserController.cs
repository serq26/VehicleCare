using AlpataProje.GenericRepository;
using AlpataProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataProje.Controllers
{
    public class UserController : Controller
    {
        private IGenericRepository<User> repository = null;

        public static List<Vehicle> vehicles;

        public UserController()
        {
            this.repository = new GenericRepository<User>();
            vehicles = new List<Vehicle>();
        }
        public UserController(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var model = repository.GetAll();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User model)
        {
            if (ModelState.IsValid)
            {
                repository.Insert(model);
                repository.Save();
                return RedirectToAction("Login", "Security");
            }
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditUser()
        {
            //User model = repository.GetById(UserId);
            //return View(model);

            var user = repository.GetById(SecurityController.Userid);
            return View(user);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditUser(User model)
        {
            if (ModelState.IsValid)
            {
                repository.Update(model);
                repository.Save();
                return RedirectToAction("Index", "User");
            }
            else
            {
                return View(model);
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult DeleteUser()
        {
            int userId = SecurityController.Userid;
            User model = repository.GetById(userId);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(int UserID)
        {
            UserID = SecurityController.Userid;

            repository.Delete(UserID);
            repository.Save();
            return RedirectToAction("Login", "Security");
        }

        public ActionResult SignIn()
        {
            return View();
        }
        [Authorize]
        [HttpGet,Authorize]
        public ActionResult UserProfile()
        {
            var deneme = repository.GetById(SecurityController.Userid);         
            return View(deneme);

            //var model = repository.GetAll();


            // var currentUser = model.FirstOrDefault(x => x.UserID == userId);

            //var model = repository.GetAll();
            //foreach (var user in model)
            //{
            //    currentUser = model.FirstOrDefault(x => x.UserID == SecurityController.Userid);
            //    User.Add(currentUser);
            //}

        }
        [Authorize]
        [HttpGet]
        public ActionResult MyVehicles()
        {
            var user = repository.GetById(SecurityController.Userid);

            vehicles.AddRange(user.Vehicles.ToList());

            return View(vehicles);

            //foreach (var item in model)
            //{
            //    if (item.UserID == SecurityController.Userid)
            //    {
            //        vehicles.Add(item);
            //    }
            //}
            //return View(vehicles);
        }

    }
}