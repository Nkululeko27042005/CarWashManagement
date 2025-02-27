using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarWashManagement.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string VehicleReg { get; set; }
        public int Points { get; set; }
    }
}