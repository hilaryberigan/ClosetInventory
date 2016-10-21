using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosetInventory.Models;

namespace ClosetInventory.WorkerClasses
{
    public class OutfitGenerator
    {

  
        public Outfit MakeOutfit(ApplicationUser user, ApplicationDbContext db)
        {
            Random random = new Random();
            Outfit Outfit = new Outfit();

            var Covers = (from x in db.Covers where x.UserId == user.Id select x).ToList();
            var Pants = (from y in db.Pants where y.UserId == user.Id select y).ToList();
            var Shoes = (from z in db.Shoes where z.UserId == user.Id select z).ToList();
            var Skirts = (from j in db.Skirts where j.UserId == user.Id select j).ToList();
            var Shirts = (from k in db.Shirts where k.UserId == user.Id select k).ToList();
            var Dresses = (from l in db.Dresses where l.UserId == user.Id select l).ToList();

            if (Covers.Count > 0)
            {
                int a = random.Next(0, Covers.Count);
                Outfit.Cover = Covers[a];
            }
            if (Pants.Count > 0)
            {
                int b = random.Next(0, Pants.Count);
                Outfit.Pants = Pants[b];
            }

            if (Shoes.Count > 0)
            {
                int c = random.Next(0, Shoes.Count);
                Outfit.Shoe = Shoes[c];
            }
            if (Skirts.Count > 0)
            {
                int d = random.Next(0, Skirts.Count);
                Outfit.Skirt = Skirts[d];
            }
            if (Shirts.Count > 0)
            {
                int e = random.Next(0, Shirts.Count);
                Outfit.Shirt = Shirts[e];
            }
            if (Dresses.Count > 0)
            {
                int f = random.Next(0, Dresses.Count);
                Outfit.Dress = Dresses[f];
            }
            Outfit.Date = DateTime.Today;
           

            return Outfit;
        }
    }
}