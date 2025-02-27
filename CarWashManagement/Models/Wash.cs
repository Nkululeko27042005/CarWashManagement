using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarWashManagement.Models
{
    public class Wash
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WashId { get; set; }
        public string WashType { get; set; }
        public double Cost { get; set; }
    }
}