using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Nager.AmazonProductAdvertising.Model;

namespace ClosetInventory.Models
{
    public class WishListItem
    {
        [Key]
        public int? Id { get; set; } 
        public string Url { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        
    }
}