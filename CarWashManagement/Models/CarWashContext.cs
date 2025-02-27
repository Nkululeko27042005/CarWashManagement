using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CarWashManagement.Models
{
    public class CarWashContext:DbContext
    {
        public CarWashContext(): base("CarWashDB")
        {

        }

        public DbSet<Client> clients { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        public DbSet<Wash> washes { get; set; }
        public DbSet<VehicleWash> VehicleWashes { get; set; }

        public System.Data.Entity.DbSet<CarWashManagement.Models.CarWashVM> CarWashVMs { get; set; }
    }
}