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
    public class DressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dresses
        public ActionResult Index()
        {
            return View(db.Dresses.ToList());
        }

        // GET: Dresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dress dress = db.Dresses.Find(id);
            if (dress == null)
            {
                return HttpNotFound();
            }
            return View(dress);
        }

        // GET: Dresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var dress = new Dress { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile, UserId = userId };
                db.Dresses.Add(dress);
                db.SaveChanges();
                return RedirectToAction("Edit", dress);
            }

            return RedirectToAction("Upload", "Home");
        }

        // GET: Dresses/Edit/5
        public ActionResult Edit(Dress dress)
        {
            if (dress == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View(dress);
        }

        // POST: Dresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "Id,isLong,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit,UserId")] Dress dress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", dress);
        }

        // GET: Dresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dress dress = db.Dresses.Find(id);
            if (dress == null)
            {
                return HttpNotFound();
            }
            return View(dress);
        }

        // POST: Dresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dress dress = db.Dresses.Find(id);
            db.Dresses.Remove(dress);
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
