using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Outfit
    {
        [Key]
        public int? Id { get; set; }

        [ForeignKey("Shirt")]
        public int? ShirtId { get; set; }
        public Shirt Shirt { get; set; }

        [ForeignKey("Shoe")]
        public int? ShoeId { get; set; }
        public Shoe Shoe { get; set; }

        [ForeignKey("Pants")]
        public int? PantsId { get; set; }
        public Pants Pants { get; set; }

        [ForeignKey("Dress")]
        public int? DressId { get; set; }
        public Dress Dress { get; set; }

        [ForeignKey("Skirt")]
        public int? SkirtId { get; set; }
        public Skirt Skirt { get; set; }

        [ForeignKey("Cover")]
        public int? CoverId { get; set; }
        public Cover Cover { get; set; }

        public DateTime Date { get; set; }

        public bool WasWorn { get; set; }

        public bool isLiked { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}