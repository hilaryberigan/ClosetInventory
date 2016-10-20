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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,isLong,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit")] Dress dress)
        {
            if (ModelState.IsValid)
            {
                db.Dresses.Add(dress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dress);
        }

        // GET: Dresses/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Dresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,isLong,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit")] Dress dress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dress);
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
