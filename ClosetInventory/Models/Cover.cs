using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Cover : Clothing
    {
        public string Type { get; set; } //jacket, sweater


    }
}