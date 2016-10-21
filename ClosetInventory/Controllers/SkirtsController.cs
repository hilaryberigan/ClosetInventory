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
    public class SkirtsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Skirts
        public ActionResult Index()
        {
            return View(db.Skirts.ToList());
        }

        // GET: Skirts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skirt skirt = db.Skirts.Find(id);
            if (skirt == null)
            {
                return HttpNotFound();
            }
            return View(skirt);
        }

        // GET: Skirts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skirts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {

                var skirt = new Skirt { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile };
                db.Skirts.Add(skirt);
                db.SaveChanges();
                return RedirectToAction("Edit", skirt);
            }

            return RedirectToAction("Upload", "Home");

        }

        // GET: Skirts/Edit/5
        public ActionResult Edit(Skirt skirt)
        {
            if (skirt == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
      
            return View(skirt);
        }

        // POST: Skirts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "Id,isLong,IsHighWaist,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit,HasPattern")] Skirt skirt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(skirt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", skirt);
        }

        // GET: Skirts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skirt skirt = db.Skirts.Find(id);
            if (skirt == null)
            {
                return HttpNotFound();
            }
            return View(skirt);
        }

        // POST: Skirts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skirt skirt = db.Skirts.Find(id);
            db.Skirts.Remove(skirt);
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
