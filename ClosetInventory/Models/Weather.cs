using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Weather
    {
        [Key]
        public int? Id { get; set; }
        public string Temperature { get; set; }
        public string SkyConditions { get; set; }
    }
}