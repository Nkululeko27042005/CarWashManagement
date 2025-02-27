using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarWashManagement.Models
{
    public class VehicleWash
    {
        [Key]
        public string RefNo { get; set; }
        public virtual int ClientID { get; set; }
        public Client client { get; set; }
        public virtual int VehicleID { get; set; }
        public Vehicle vehicle { get; set; }
        public virtual int WashId { get; set; }
        public Wash wash { get; set; }
        public string Make {  get; set; }
        public string VehicleReg { get; set; }
        public DateTime? Date { get; set; }
        public double cost { get; set; }

        public string CalcRefNo()
        {
            string Ref = "";

            Ref = ClientID + VehicleReg + DateTime.Now.TimeOfDay + DateTime.Now.Date;

            return Ref;
        }

        public double CalcCost()
        {
            double cost = 0;
            double cst = 0;


            CarWashContext db = new CarWashContext();
            var wash = (from w in db.washes
                        where WashId == w.WashId
                        select w.Cost).FirstOrDefault();

            CarWashContext DB = new CarWashContext();
            var Point = (from p in db.clients
                         where ClientID == p.ClientID
                         select p.Points).FirstOrDefault();

            cst = Point * 5;

            if (cst >= wash)
            {
                cost = 0;
            }
            else
            {
                cost = wash;
            }

            return cost;
        }

        public string PullVehReg()
        {
            string reg = "";

            CarWashContext db = new CarWashContext();
            var Reg = (from r in db.clients
                       where ClientID == r.ClientID
                       select r.VehicleReg).FirstOrDefault();

            reg = Reg;

            return reg;
        }

        public void AddPoints()
        {
            CarWashContext db = new CarWashContext();
            var Point = (from p in db.clients
                         where ClientID == p.ClientID
                         select p).FirstOrDefault();

            Point.Points = Point.Points + 1;

            db.SaveChanges();
        }

        public void RemovePoints()
        {
            int cost = 0;

            CarWashContext db = new CarWashContext();
            var Point = (from p in db.clients
                         where ClientID == p.ClientID
                         select p).FirstOrDefault();

            cost = Point.Points * 5;

            if (cost >= CalcCost())
            {
                Point.Points = Point.Points - Point.Points;
                db.SaveChanges();
            }
        }

        public DateTime pulldate()
        {
            return DateTime.Now;
        }
    }
}