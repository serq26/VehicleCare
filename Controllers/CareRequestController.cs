using AlpataProje.GenericRepository;
using AlpataProje.Models.Context;
using AlpataProje.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataProje.Controllers
{
    [Authorize]
    public class CareRequestController : Controller
    {
        private IGenericRepository<CareRequests> repository = null;

        List<CareRequests> UserRequests;

        List<CareRequests> UnconfirmedRequests;

        SelectList list;

        enum RequestStatus
        {
            Confirmed,
            Unconfirmed
        }


        public CareRequestController()
        {
            this.repository = new GenericRepository<CareRequests>();
            UserRequests = new List<CareRequests>();
            UnconfirmedRequests = new List<CareRequests>();
            list = new SelectList("");
        }
        public CareRequestController(IGenericRepository<CareRequests> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = repository.GetAll();

            foreach (var item in model)
            {
                if (item.UserID == SecurityController.Userid && item.RequestStatus == RequestStatus.Unconfirmed.ToString())  
                {
                    UserRequests.Add(item);
                }
            }

            return View(UserRequests);
        }

        [HttpGet]
        public ActionResult ConfirmedRequests()
        {
            var model = repository.GetAll();

            foreach (var item in model)
            {
                if (item.UserID == SecurityController.Userid && item.RequestStatus == RequestStatus.Confirmed.ToString())
                {
                    UserRequests.Add(item);
                }
            }

            return View(UserRequests);
        }

        [HttpGet]
        public ActionResult AddRequest()
        {
            DatabaseContext myentity = new DatabaseContext();

            List<Vehicle> MyVehicles = new List<Vehicle>();

            var vehicles = myentity.Vehicles.ToList();

            foreach (var item in vehicles)
            {
                if (item.UserID == SecurityController.Userid)
                {
                    MyVehicles.Add(item);
                }
            }
            list = new SelectList(MyVehicles, "VehicleID", "Brand");
            
            ViewBag.vehiclesName = list;
            
            return View();
        }

        [HttpPost]
        public ActionResult AddRequest(CareRequests model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = SecurityController.Userid;
                int id = Convert.ToInt32(Request.Form["VehicleList"]);
                model.VehicleID = id;
                model.RequestStatus = RequestStatus.Unconfirmed.ToString();
                
                repository.Insert(model);
                repository.Save();
                return RedirectToAction("Index", "CareRequest");
            }
            return View();
        }

        //[HttpGet]
        //public ActionResult EditRequest(int RequestID)
        //{
        //    var request = repository.GetById(RequestID);
        //    return View(request);
        //}

        //[HttpPost]
        //public ActionResult EditRequest(CareRequests model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.UserID = SecurityController.Userid;
                
        //        model.VehicleID = id;
        //        repository.Update(model);
        //        repository.Save();
        //        return RedirectToAction("Index", "CareRequest");
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}

        [HttpGet]
        public ActionResult DeleteRequest(int RequestID)
        {
            CareRequests model = repository.GetById(RequestID);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int RequestId)
        {
            repository.Delete(RequestId);
            repository.Save();
            return RedirectToAction("Index", "CareRequest");
        }


        [HttpGet]
        public ActionResult Requests()
        {

            var model = repository.GetAll();
            foreach (var item in model)
            {
                if (item.RequestStatus == RequestStatus.Unconfirmed.ToString())
                {
                    UnconfirmedRequests.Add(item);
                }
            }
            return View(UnconfirmedRequests);
        }

        [HttpGet]
        public ActionResult Confirm(int RequestId)
        {
            var request = repository.GetById(RequestId);
            return View(request);
        }

        [HttpPost]
        public ActionResult Confirm(CareRequests model)
        {            
            var request = repository.GetById(model.RequestID);
            request.RequestStatus = RequestStatus.Confirmed.ToString();
            repository.Save();
            return RedirectToAction("Requests", "CareRequest");
        }
        //public ActionResult Confirm(CareRequests model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.RequestStatus = "Onaylandı";
        //        repository.Update(model);
        //        repository.Save();
        //        return RedirectToAction("Requests", "CareRequest");
        //    }
        //    else
        //    {
        //        return View(model);
        //    }
        //}
    }
}