using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarWashManagement.Models
{
    public class CarWashVM
    {
        [Key]
        public int RefNo { get; set; }
        public string Name { get; set; }
        public string VehicleType { get; set; }
        public string WashType { get; set; }
    }
}