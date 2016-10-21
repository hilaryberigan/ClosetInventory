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

                var shirt = new Shirt { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile };
                db.Shirts.Add(shirt);
                db.SaveChanges();
                return RedirectToAction("Edit", shirt);
            }

            return RedirectToAction("Upload", "Home");
        }

        // GET: Shirts/Edit/5
        public ActionResult Edit(Shirt shirt)
        {
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
        public ActionResult Submit([Bind(Include = "Id,SleeveLength,IsCropped,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit")] Shirt shirt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shirt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", shirt);
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
