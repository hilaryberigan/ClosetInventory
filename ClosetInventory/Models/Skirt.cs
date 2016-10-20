using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Skirt : Clothing
    {
        public bool isLong { get; set; }
        
        public bool IsHighWaist { get; set; }



    }
}