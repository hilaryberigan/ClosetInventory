﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.IO;
using Microsoft.AspNet.Identity;

using ClosetInventory.WorkerClasses;
using System.Threading.Tasks;

namespace ClosetInventory.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ChoicePage()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            ChoiceViewModel model = new ChoiceViewModel();

            model.Choice = user.Dressiness;
            return View(model);
        }

        //Get:
        [HttpPost]
        public ActionResult ChooseDressiness(string Id)
        {
            DateTime date = DateTime.Today;
            var userId = User.Identity.GetUserId();
            var weather = db.Weathers.Where(m => m.Date == date).FirstOrDefault();

            double temperature = 70;
                try
            {
                temperature = weather.Temperature;
            }
            catch { };
            var user = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            var Outfits = db.Outfits.Where(n => n.UserId == userId).ToList();

            var outfit = Outfits.Where(m => m.Id == user.CurrentOutfitId).FirstOrDefault();


            if (outfit != null && user.Dressiness != Id)
            {
                user.Dressiness = Id;

                OutfitGenerator og = new OutfitGenerator();
                Outfit Outfit = og.MakeOutfit(user, db, temperature, Id);
                user.CurrentOutfitId = outfit.Id;
                db.Outfits.Add(outfit);
            }



            return Json(String.Format("'Success': 'true'"));
        }

        [Authorize]
        public async Task<ActionResult> UserHomePage()
        {
            
            WeatherSearch weather = new WeatherSearch();        
            var userId = User.Identity.GetUserId();
            TotalViewModel model = new TotalViewModel();
            DateTime date = DateTime.Today;
            model.Dressiness = "business casual";
            model.User = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            model.Covers = db.Covers.Where(m => m.UserId == userId).ToList();
            model.Pants = db.Pants.Where(m => m.UserId == userId).ToList();
            model.Shoes = db.Shoes.Where(m => m.UserId == userId).ToList();
            model.Skirts = db.Skirts.Where(m => m.UserId == userId).ToList();
            model.Shirts = db.Shirts.Where(m => m.UserId == userId).ToList();
            model.Dresses = db.Dresses.Where(n => n.UserId == userId).ToList();
            if (model.User.Dressiness != null)
            {
                model.Dressiness = model.User.Dressiness;
            }
            model.Dresses = (from b in db.Dresses where b.UserId == userId select b).ToList();

            var Outfits = db.Outfits.Where(n => n.UserId == userId).ToList();

            var outfit = Outfits.Where(m=>m.Id == model.User.CurrentOutfitId || m.Date == date).FirstOrDefault();

            if (outfit != null)
            {
                model.Outfit = outfit;
                model.User.CurrentOutfitId = outfit.Id;
            }
           
            else
            {
                var temperature = weather.GetTemperature();
                var temp = await temperature;

                OutfitGenerator og = new OutfitGenerator();
                model.Outfit = og.MakeOutfit(model.User, db, temp, model.Dressiness);
            }

            
            return View(model);
        }
        

   
        public ActionResult HomeCovers(TotalViewModel model)
        {
            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Covers = db.Covers.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }
        
        public ActionResult HomeDresses(TotalViewModel model)
        {

            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Dresses = db.Dresses.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }
       
        public ActionResult HomePants(TotalViewModel model)
        {
            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Pants = db.Pants.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateOutfit(int? outfitId, int? Id, string clothingType)
        {
            Outfit outfit = db.Outfits.Where(m => m.Id == outfitId).FirstOrDefault();
            var entry = db.Entry(outfit);

            db.Outfits.Attach(outfit);


            switch (clothingType)
            {


                case "pants":
                    outfit.Pants = db.Pants.Where(m => m.Id == Id).FirstOrDefault();
                    outfit.PantsId = Id;
                    entry.Property(e => e.PantsId).IsModified = true;
                    break;
                case "dress":
                    outfit.Dress = db.Dresses.Where(m => m.Id == Id).FirstOrDefault();
                    outfit.DressId = Id;
                    entry.Property(e => e.DressId).IsModified = true;

                    break;
                case "shirt":
                    outfit.Shirt = db.Shirts.Where(m => m.Id == Id).FirstOrDefault();
                    outfit.ShirtId = Id;
                    entry.Property(e => e.ShirtId).IsModified = true;
                    break;
                case "shoe":
                    outfit.Shoe = db.Shoes.Where(m => m.Id == Id).FirstOrDefault();
                    outfit.ShoeId = Id;
                    entry.Property(e => e.ShoeId).IsModified = true;
                    break;
                case "cover":
                    outfit.Cover = db.Covers.Where(m => m.Id == Id).FirstOrDefault();
                    outfit.CoverId = Id;
                    entry.Property(e => e.CoverId).IsModified = true;
                    break;
                default:
                    break;
            }



            db.SaveChanges();            

            return Json(String.Format("'Success': 'true'"));

        }
        //[HttpPost]
        //public ActionResult HomePants()

        public ActionResult HomeShirts(TotalViewModel model)
        {
            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Shirts = db.Shirts.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }
      
        public ActionResult HomeShoes(TotalViewModel model)
        {
            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Shoes = db.Shoes.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }
      
        public ActionResult HomeSkirts(TotalViewModel model)
        {
            model.Outfit = db.Outfits.Include("Pants").Include("Skirt").Include("Shirt").Include("Cover").Include("Shoe").Include("Dress").Where(m => m.Id == model.Outfit.Id).FirstOrDefault();
            model.Skirts = db.Skirts.Where(m => m.UserId == model.Outfit.UserId).ToList();

            return View(model);
        }


        [HttpPost]
        public ActionResult UserHomePage(Outfit outfit)
        {
       
                db.Outfits.Add(outfit);
                db.SaveChanges();

            return View(outfit);
            
        }

        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Shop()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Upload()
        {
            UploadViewModel model = new UploadViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult SetImageType(HttpPostedFileBase file, UploadViewModel model)
        {
            if (file != null)
            {

                ImageManager imageManager = new ImageManager();
                FinalImageResult final = new FinalImageResult();
                System.Drawing.Image sourceImage = null;

                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    sourceImage = System.Drawing.Image.FromStream(file.InputStream);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    string largeFile = "/Images/" + fileName;
                    model.LargeFile = largeFile;

                    System.Drawing.Image image = imageManager.ScaleImage(sourceImage, 200);
                    var thumbPath = Path.Combine(Server.MapPath("~/Images/Thumbs/"), fileName);
                    image.Save(thumbPath);
                    final = imageManager.HandleIncomingImage(path);
                    string smallFile = "/Images/Thumbs/" + fileName;
                    model.SmallFile = smallFile;
                    model.Color = final.ImageProps.dominantColors[0].colorName;
                    model.ControllerName = final.ImageLabels.ControllerSuggestion;

                    if (model.Color == "black")
                    {
                        model.ColorType = "dark";
                    }
                }
                return View(model);
            }
            return RedirectToAction("Upload");

           
        }

   

    }
}