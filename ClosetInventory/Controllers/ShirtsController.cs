using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using Microsoft.AspNet.Identity;

namespace ClosetInventory.Controllers
{
    public class ShirtsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shirts
        public ActionResult Index()
        {
            return View(db.Shirts.ToList());
        }

        // GET: Shirts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shirt shirt = db.Shirts.Find(id);
            if (shirt == null)
            {
                return HttpNotFound();
            }
            return View(shirt);
        }

        // GET: Shirts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shirts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var shirt = new Shirt { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile, UserId = userId, lastWorn = DateTime.Today };

                return RedirectToAction("Edit", shirt);
            }

            return RedirectToAction("Upload", "Home");
        }


        // GET: Shirts/Edit/5
        public ActionResult Edit(Shirt shirt)
        {
            List<SelectListItem> dressiness = new List<SelectListItem>();
            List<SelectListItem> warmthRating = new List<SelectListItem>();
            List<SelectListItem> colorTypes = new List<SelectListItem>();
            dressiness.Add(new SelectListItem { Text = "Very Casual", Value = "1" });
            dressiness.Add(new SelectListItem { Text = "Casual", Value = "2" });
            dressiness.Add(new SelectListItem { Text = "Somewhat Casual", Value = "3" });
            dressiness.Add(new SelectListItem { Text = "Could be casual or dressy", Value = "4" });
            dressiness.Add(new SelectListItem { Text = "Somewhat Dressy", Value = "5" });
            dressiness.Add(new SelectListItem { Text = "Dressy", Value = "6" });
            dressiness.Add(new SelectListItem { Text = "Very Dressy", Value = "7" });

            warmthRating.Add(new SelectListItem { Text = "Really Hot Weather", Value = "1" });
            warmthRating.Add(new SelectListItem { Text = "Hot Weather", Value = "2" });
            warmthRating.Add(new SelectListItem { Text = "Warm Weather", Value = "3" });
            warmthRating.Add(new SelectListItem { Text = "Cool Weather", Value = "4" });
            warmthRating.Add(new SelectListItem { Text = "Cold Weather", Value = "5" });
            warmthRating.Add(new SelectListItem { Text = "Really Cold Weather", Value = "6" });

            colorTypes.Add(new SelectListItem { Text = "Dark", Value = "dark" });
            colorTypes.Add(new SelectListItem { Text = "Bright", Value = "bright" });
            colorTypes.Add(new SelectListItem { Text = "Neutral", Value = "neutral" });

            shirt.Dressiness = dressiness;
            shirt.WarmthType = warmthRating;
            shirt.ColorTypes = colorTypes;

            if (shirt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(shirt);

        }

        // POST: Shirts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(Shirt shirt)
        {
                shirt.UserId = User.Identity.GetUserId();
                shirt.lastWorn = DateTime.Today;
                db.Shirts.Add(shirt);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = shirt.Id });
            
        }

        // GET: Shirts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shirt shirt = db.Shirts.Find(id);
            if (shirt == null)
            {
                return HttpNotFound();
            }
            return View(shirt);
        }

        // POST: Shirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shirt shirt = db.Shirts.Find(id);
            db.Shirts.Remove(shirt);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
