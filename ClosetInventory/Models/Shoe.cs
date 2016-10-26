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

        [Display(Name = "Dressiness")]
        public int? DressinessRating { get; set; }

        [Display(Name = "Warmth Of Item")]
        public int? WarmthRating { get; set; }

        public string Color { get; set; }

        [Display(Name = "Color Type")]
        public string ColorType { get; set; }//dark, bright, neutral

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Last Worn")]
        public DateTime lastWorn { get; set; }
    }
}