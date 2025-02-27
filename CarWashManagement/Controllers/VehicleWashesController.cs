using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarWashManagement.Models;

namespace CarWashManagement.Controllers
{
    public class VehicleWashesController : Controller
    {
        private CarWashContext db = new CarWashContext();

        // GET: VehicleWashes
        public ActionResult Index()
        {
            var vehicleWashes = db.VehicleWashes.Include(v => v.client).Include(v => v.vehicle).Include(v => v.wash);
            return View(vehicleWashes.ToList());
        }

        // GET: VehicleWashes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleWash vehicleWash = db.VehicleWashes.Find(id);
            if (vehicleWash == null)
            {
                return HttpNotFound();
            }
            return View(vehicleWash);
        }

        // GET: VehicleWashes/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.clients, "ClientID", "Name");
            ViewBag.VehicleID = new SelectList(db.vehicles, "VehicleID", "VehicleType");
            ViewBag.WashID = new SelectList(db.washes, "WashId", "WashType");
            return View();
        }

        // POST: VehicleWashes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefNo,ClientID,VehicleID,WashID,Make,VehicleReg")] VehicleWash vehicleWash)
        {
            if (ModelState.IsValid)
            {
                vehicleWash.RefNo = vehicleWash.CalcRefNo();
                vehicleWash.cost = vehicleWash.CalcCost();
                vehicleWash.VehicleReg = vehicleWash.PullVehReg();
                vehicleWash.Date = vehicleWash.pulldate();
                vehicleWash.AddPoints();
                vehicleWash.RemovePoints();
                db.VehicleWashes.Add(vehicleWash);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.clients, "ClientID", "Name", vehicleWash.ClientID);
            ViewBag.VehicleID = new SelectList(db.vehicles, "VehicleID", "VehicleType", vehicleWash.VehicleID);
            ViewBag.WashID = new SelectList(db.washes, "WashId", "WashType", vehicleWash.WashId);
            return View(vehicleWash);
        }

        // GET: VehicleWashes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleWash vehicleWash = db.VehicleWashes.Find(id);
            if (vehicleWash == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.clients, "ClientID", "Name", vehicleWash.ClientID);
            ViewBag.VehicleID = new SelectList(db.vehicles, "VehicleID", "VehicleType", vehicleWash.VehicleID);
            ViewBag.WashID = new SelectList(db.washes, "WashId", "WashType", vehicleWash.WashId);
            return View(vehicleWash);
        }

        // POST: VehicleWashes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefNo,ClientID,VehicleID,WashID,Make,VehicleReg")] VehicleWash vehicleWash)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleWash).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.clients, "ClientID", "Name", vehicleWash.ClientID);
            ViewBag.VehicleID = new SelectList(db.vehicles, "VehicleID", "VehicleType", vehicleWash.VehicleID);
            ViewBag.WashID = new SelectList(db.washes, "WashId", "WashType", vehicleWash.WashId);
            return View(vehicleWash);
        }

        // GET: VehicleWashes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleWash vehicleWash = db.VehicleWashes.Find(id);
            if (vehicleWash == null)
            {
                return HttpNotFound();
            }
            return View(vehicleWash);
        }

        // POST: VehicleWashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VehicleWash vehicleWash = db.VehicleWashes.Find(id);
            db.VehicleWashes.Remove(vehicleWash);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
