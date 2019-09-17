using AlpataProje.GenericRepository;
using AlpataProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataProje.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private IGenericRepository<Vehicle> repository = null;

        List<Vehicle> vehicles;

        public VehicleController()
        {
            this.repository = new GenericRepository<Vehicle>();
            vehicles = new List<Vehicle>();
        }
        public VehicleController(IGenericRepository<Vehicle> repository)
        {
            this.repository = repository;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var model = repository.GetAll();

            foreach (var item in model)
            {
                if (item.UserID == SecurityController.Userid)
                {
                    vehicles.Add(item);
                }
            }
            return View(vehicles);
        }
        [HttpGet]
        public ActionResult AddVehicle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddVehicle(Vehicle model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = SecurityController.Userid;
                repository.Insert(model);
                repository.Save();
                return RedirectToAction("Index", "Vehicle");
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditVehicle(int VehicleID)
        {
            var vehicle = repository.GetById(VehicleID);
            return View(vehicle);
        }

        [HttpPost]
        public ActionResult EditVehicle(Vehicle model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = SecurityController.Userid;
                repository.Update(model);
                repository.Save();
                return RedirectToAction("Index", "Vehicle");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult DeleteVehicle(int VehicleID)
        {
            Vehicle model = repository.GetById(VehicleID);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int VehicleId)
        {
           
            repository.Delete(VehicleId);
            repository.Save();
            return RedirectToAction("Index", "Vehicle");
        }


        [HttpGet, Authorize]
        public ActionResult UserProfile()
        {
            var deneme = repository.GetById(SecurityController.Userid);
            return View(deneme);
        }


    }
}