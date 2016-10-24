using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Shirt : Clothing
    {
        [Display(Name = "Sleeve Length")]
        public string SleeveLength { get; set; }

        public bool IsCropped { get; set; }


    }
}