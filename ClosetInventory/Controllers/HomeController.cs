using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.IO;
using Microsoft.AspNet.Identity;

using ClosetInventory.WorkerClasses;

namespace ClosetInventory.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Get:
        public ActionResult UserHomePage()
        {
            var userId = User.Identity.GetUserId();
            TotalViewModel model = new TotalViewModel();
            DateTime date = DateTime.Today;

            model.User = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            model.Covers = db.Covers.Where(m => m.UserId == userId).ToList();
            model.Pants = db.Pants.Where(m => m.UserId == userId).ToList();
            model.Shoes = db.Shoes.Where(m => m.UserId == userId).ToList();
            model.Skirts = db.Skirts.Where(m => m.UserId == userId).ToList();
            model.Shirts = db.Shirts.Where(m => m.UserId == userId).ToList();
            model.Dresses = db.Dresses.Where(n => n.UserId == userId).ToList();
            
            //model.Dresses = (from b in db.Dresses where b.UserId == userId select b).ToList();

            var Outfits = db.Outfits.Where(n => n.UserId == userId).ToList();

            var outfit = Outfits.Where(m=>m.Date == date).FirstOrDefault();

            if (outfit != null)
            {
                model.Outfit = outfit;
            }
            else
            {
                OutfitGenerator og = new OutfitGenerator();
                model.Outfit = og.MakeOutfit(model.User, db);
            }

            return View(model);
        }


        [HttpPost]
        public ActionResult HomeCovers(Outfit outfit)
        {
            var covers = db.Covers.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Covers = covers };
            return View(model);
        }
        [HttpPost]
        public ActionResult HomeDresses(Outfit outfit)
        {
            var dresses = db.Dresses.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Dresses = dresses };
            return View(model);
        }
        [HttpPost]
        public ActionResult HomePants(Outfit outfit)
        {
            var pants = db.Pants.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Pants = pants };
            return View(model);
        }
        [HttpPost]
        public ActionResult HomeShirts(Outfit outfit)
        {
            var shirts = db.Shirts.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Shirts = shirts };
            return View(model);
        }
        [HttpPost]
        public ActionResult HomeShoes(Outfit outfit)
        {
            var shoes = db.Shoes.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Shoes = shoes };
            return View(model);
        }
        [HttpPost]
        public ActionResult HomeSkirts(Outfit outfit)
        {
            var skirts = db.Skirts.Where(m => m.UserId == outfit.UserId).ToList();
            TotalViewModel model = new TotalViewModel { Outfit = outfit, Skirts = skirts };
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

                    System.Drawing.Image image = imageManager.ScaleImage(sourceImage, 75);
                    var thumbPath = Path.Combine(Server.MapPath("~/Images/Thumbs/"), fileName);
                    image.Save(thumbPath);
                    final = imageManager.HandleIncomingImage(path);
                    string smallFile = "/Images/Thumbs/" + fileName;
                    model.SmallFile = smallFile;
                    model.Color = final.ImageProps.dominantColors[0].colorName;
                    model.ControllerName = final.ImageLabels.ControllerSuggestion;
                }
                return View(model);
            }
            return RedirectToAction("Upload");

           
        }

   

    }
}