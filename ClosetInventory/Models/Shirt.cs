using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Shirt : Clothing
    {
        public string SleeveLength { get; set; }

        public bool IsCropped { get; set; }


    }
}