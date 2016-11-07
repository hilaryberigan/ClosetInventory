using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClosetInventory.Models
{
    public class Clothing
    {
        [Key]
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
        public IEnumerable<SelectListItem> ColorTypes { get; set; }

        [Display(Name = "Color Type")]
        public string ColorType { get; set; }//dark, bright, neutral

        public bool IsTightFit { get; set; }

        public bool HasPattern { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Last Worn")]
        public DateTime lastWorn { get; set; }

        [Display(Name = "Occasion Type (How Dressy is the Item)")]
        public IEnumerable<SelectListItem> Dressiness { get; set; }

        [Display(Name = "Temperature of Where You'll Wear the Item")]
        public IEnumerable<SelectListItem> WarmthType { get; set; }

    }
}