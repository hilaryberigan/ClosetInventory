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

            model.User = (from a in db.Users where a.Id == userId select a).FirstOrDefault();
            model.Covers = (from b in db.Covers where b.UserId == userId select b).ToList();
            model.Pants = (from b in db.Pants where b.UserId == userId select b).ToList();
            model.Shoes = (from b in db.Shoes where b.UserId == userId select b).ToList();
            model.Skirts = (from b in db.Skirts where b.UserId == userId select b).ToList();
            model.Shirts = (from b in db.Shirts where b.UserId == userId select b).ToList();
            model.Dresses = db.Dresses.Where(n => n.UserId == userId).ToList();
            
            //model.Dresses = (from b in db.Dresses where b.UserId == userId select b).ToList();

            var Outfits = (from a in db.Outfits where a.UserId == userId select a).ToList();

            var outfit = (from b in Outfits where b.Date == date select b).FirstOrDefault();

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
        public ActionResult UserHomePage(TotalViewModel model)
        {
       
                db.Outfits.Add(model.Outfit);
                db.SaveChanges();

            return View(model);
            
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