using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace ClosetInventory.Controllers
{
    public class ShoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Shoes
        public ActionResult Index()
        {
            return View(db.Shoes.ToList());
        }

        // GET: Shoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe shoe = db.Shoes.Find(id);
            if (shoe == null)
            {
                return HttpNotFound();
            }
            return View(shoe);
        }

        // GET: Shoes/Create

       
public ActionResult Create()
        {
            Shoe shoe = new Shoe();
            return View(shoe);
        }

        // POST: Shoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var shoe = new Shoe { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile, UserId = userId, lastWorn = DateTime.Today };
                db.Shoes.Add(shoe);
                db.SaveChanges();
                return RedirectToAction("Edit", shoe);
            }

            return RedirectToAction("Upload", "Home");
        }
        // GET: Shoes/Edit/5
        public ActionResult Edit(Shoe shoe)
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

            shoe.Dressiness = dressiness;
            shoe.WarmthType = warmthRating;
            shoe.ColorTypes = colorTypes;

            if (shoe == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(shoe);
        }

        // POST: Shoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "Id,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,UserId,lastWorn")] Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                shoe.lastWorn = DateTime.Today;
                db.Entry(shoe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", shoe);
        }

        // GET: Shoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shoe shoe = db.Shoes.Find(id);
            if (shoe == null)
            {
                return HttpNotFound();
            }
            return View(shoe);
        }

        // POST: Shoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shoe shoe = db.Shoes.Find(id);
            db.Shoes.Remove(shoe);
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
