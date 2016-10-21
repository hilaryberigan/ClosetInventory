using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;

namespace ClosetInventory.Controllers
{
    public class OutfitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Outfits
        public ActionResult Index()
        {
            var outfits = db.Outfits.Include(o => o.Cover).Include(o => o.Dress).Include(o => o.Pants).Include(o => o.Shirt).Include(o => o.Shoe).Include(o => o.Skirt).Include(o => o.User);
            return View(outfits.ToList());
        }

        // GET: Outfits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }
        //[HttpPost]
        //public ActionResult Create(TotalViewModel model)
        //{
        //    Outfit

        //    model.User = (from a in db.Users where a.Id == userId select a).FirstOrDefault();
        //    model.Covers = (from b in db.Covers where b.UserId == userId select b).ToList();
        //    model.Pants = (from b in db.Pants where b.UserId == userId select b).ToList();
        //    model.Shoes = (from b in db.Shoes where b.UserId == userId select b).ToList();
        //    model.Skirts = (from b in db.Skirts where b.UserId == userId select b).ToList();
        //    model.Shirts = (from b in db.Shirts where b.UserId == userId select b).ToList();
        //    model.Dresses = (from b in db.Dresses where b.UserId == userId select b).ToList();

        //    return View(model);
        //}
        // GET: Outfits/Create
        public ActionResult Create()
        {
            ViewBag.CoverId = new SelectList(db.Covers, "Id", "Type");
            ViewBag.DressId = new SelectList(db.Dresses, "Id", "SmallFile");
            ViewBag.PantsId = new SelectList(db.Pants, "Id", "SmallFile");
            ViewBag.ShirtId = new SelectList(db.Shirts, "Id", "SleeveLength");
            ViewBag.ShoeId = new SelectList(db.Shoes, "Id", "SmallFile");
            ViewBag.SkirtId = new SelectList(db.Skirts, "Id", "SmallFile");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Outfits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ShirtId,ShoeId,PantsId,DressId,SkirtId,CoverId,Date,WasWorn,isLiked,UserId")] Outfit outfit)
        {
            if (ModelState.IsValid)
            {
                db.Outfits.Add(outfit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoverId = new SelectList(db.Covers, "Id", "Type", outfit.CoverId);
            ViewBag.DressId = new SelectList(db.Dresses, "Id", "SmallFile", outfit.DressId);
            ViewBag.PantsId = new SelectList(db.Pants, "Id", "SmallFile", outfit.PantsId);
            ViewBag.ShirtId = new SelectList(db.Shirts, "Id", "SleeveLength", outfit.ShirtId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "Id", "SmallFile", outfit.ShoeId);
            ViewBag.SkirtId = new SelectList(db.Skirts, "Id", "SmallFile", outfit.SkirtId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", outfit.UserId);
            return View(outfit);
        }

        // GET: Outfits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoverId = new SelectList(db.Covers, "Id", "Type", outfit.CoverId);
            ViewBag.DressId = new SelectList(db.Dresses, "Id", "SmallFile", outfit.DressId);
            ViewBag.PantsId = new SelectList(db.Pants, "Id", "SmallFile", outfit.PantsId);
            ViewBag.ShirtId = new SelectList(db.Shirts, "Id", "SleeveLength", outfit.ShirtId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "Id", "SmallFile", outfit.ShoeId);
            ViewBag.SkirtId = new SelectList(db.Skirts, "Id", "SmallFile", outfit.SkirtId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", outfit.UserId);
            return View(outfit);
        }

        // POST: Outfits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ShirtId,ShoeId,PantsId,DressId,SkirtId,CoverId,Date,WasWorn,isLiked,UserId")] Outfit outfit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outfit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoverId = new SelectList(db.Covers, "Id", "Type", outfit.CoverId);
            ViewBag.DressId = new SelectList(db.Dresses, "Id", "SmallFile", outfit.DressId);
            ViewBag.PantsId = new SelectList(db.Pants, "Id", "SmallFile", outfit.PantsId);
            ViewBag.ShirtId = new SelectList(db.Shirts, "Id", "SleeveLength", outfit.ShirtId);
            ViewBag.ShoeId = new SelectList(db.Shoes, "Id", "SmallFile", outfit.ShoeId);
            ViewBag.SkirtId = new SelectList(db.Skirts, "Id", "SmallFile", outfit.SkirtId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", outfit.UserId);
            return View(outfit);
        }

        // GET: Outfits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outfit outfit = db.Outfits.Find(id);
            if (outfit == null)
            {
                return HttpNotFound();
            }
            return View(outfit);
        }

        // POST: Outfits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outfit outfit = db.Outfits.Find(id);
            db.Outfits.Remove(outfit);
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
