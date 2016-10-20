using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Shoe 
    {
        public int? Id { get; set; }

        public string SmallFile { get; set; }

        public string LargeFile { get; set; }

        public bool IsFavorite { get; set; }

        public int? DressinessRating { get; set; }

        public int? WarmthRating { get; set; }

        public string Color { get; set; }

        public string ColorType { get; set; }//dark, bright, neutral


    }
}