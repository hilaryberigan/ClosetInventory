using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClosetInventory.WorkerClasses
{
    public class CategoryGuesser
    {
        string[] footwear =
        {
            "footwear",
            "foot",
            "boot",
            "shoe",
            "riding boot",
            "work boots",
            "outdoor shoe",
            "motorcycle boot",
            "bridal shoe",
            "high heeled footwear",
            "basic pump",
            "oxford shoe",
            "walking shoe",
            "slipper"
        };

        string[] dresses =
        {
            "gown",
            "day dress",
            "gown",
            "cocktail dress",
            "little black dress",
            "dress",
            "bridal clothing",
            "bridal party dress",
            "dance dress",
            "prom",
            "bridesmaid"
        };

        string[] pants =
        {
          "jeans",
          "khaki",
          "trousers",
          "active pants",
          "pants",
          "shorts",
          "capri"

        };

        string[] shirts =
        {
            "sweater",
            "shirt",
            "t shirt",
            "blouse",
            "sleevless shirt",
            "long sleeved t shirt",
            "dress shirt",
            ""
        };

        string[] covers =
        {
          "overcoat",
          "coat",
          "jacket",
          "blazer",
          "outwear",
          "leather jacket",
          "vest",
          "cardigan"
        };
        string[] formalwear =
        {
            "formal wear",
            "tuxedo",
            "collar",
            "groom",
            "bridesmaid",
            "bride",
            "suit",
            "gown",
            "prom",
            "satin"
        };

        string[] skirts =
        {
            "miniskirt",
            "skirt"
        };


        public class Score
        {
            public float? score { get; set; }
            public string type { get; set; }
        }
        public string DetermineController(IList<EntityAnnotation> descriptions)
        {
            string controllerName = null;

            Score shoe = new Score { score = 0, type = "Shoes" };
            Score shirt = new Score { score = 0, type = "Shirts" };
            Score skirt = new Score { score = 0, type = "Skirts" };
            Score cover = new Score { score = 0, type = "Covers" };
            Score dress = new Score { score = 0, type = "Dresses" };
            Score pant = new Score { score = 0, type = "Pants" };

            float? formalScore = 0;

            for (int i = 0; i < descriptions.Count; i++)
            {
                string description = descriptions[i].Description;
                float? score = descriptions[i].Score * 10;

                if (footwear.Contains(description))
                {
                    shoe.score += score;
                }
                else if (pants.Contains(description))
                {
                    pant.score += score;
                }
                else if (shirts.Contains(description))
                {
                    shirt.score += score;
                }
                else if (skirts.Contains(description))
                {
                    skirt.score += score;
                }
                else if (covers.Contains(description))
                {
                    cover.score += score;
                }
                else if (dresses.Contains(description))
                {
                    dress.score += score;
                }
                if (formalwear.Contains(description))
                {
                    formalScore += score;
                }
            }

            Score[] scores = { shoe, pant, skirt, shirt, cover, dress };
            var maxScore = scores.Max(i => i.score);
            var type = (from a in scores where a.score == maxScore select a.@type).FirstOrDefault();
            return controllerName = type;
        }
    }
}