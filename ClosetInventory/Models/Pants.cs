using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClosetInventory.Models
{
    public class Pants : Clothing
    {
        public bool isCapri { get; set; }

        public bool IsHighWaist { get; set; }

        public bool IsSkinny { get; set; }

    }
}