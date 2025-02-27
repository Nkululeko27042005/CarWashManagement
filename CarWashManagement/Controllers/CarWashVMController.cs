using CarWashManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarWashManagement.Controllers
{
    public class CarWashVMController : Controller
    {
        // GET: CarWashVM
        public ActionResult PullData()
        {
            CarWashContext db = new CarWashContext();

            List<CarWashVM> CarList = new List<CarWashVM>();

            var WashListing = (from cw in db.VehicleWashes
                               join cli in db.clients on cw.ClientID equals cli.ClientID
                               join veh in db.vehicles on cw.VehicleID equals veh.VehicleID
                               join wash in db.washes on cw.WashId equals wash.WashId
                               where wash.WashType == "Full House"
                               select new
                               {
                                   cw.RefNo,
                                   cli.Name,
                                   veh.VehicleType,
                                   wash.WashType
                               }).ToList();

            foreach (var item in WashListing)
            {
                CarWashVM vm = new CarWashVM();
                vm.Name = item.Name;
                vm.VehicleType = item.VehicleType;
                vm.WashType = item.WashType;
            }

            return View(WashListing);
        }
    }
}