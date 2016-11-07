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
    public class PantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pants
        public ActionResult Index()
        {
            return View(db.Pants.ToList());
        }

        // GET: Pants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pants pants = db.Pants.Find(id);
            if (pants == null)
            {
                return HttpNotFound();
            }
            return View(pants);
        }

        // GET: Pants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var pants = new Pants { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile, UserId = userId, lastWorn = DateTime.Today };
                db.Pants.Add(pants);
                db.SaveChanges();
                return RedirectToAction("Edit", pants);
            }

            return RedirectToAction("Upload", "Home");
        }

        // GET: Pants/Edit/5
        public ActionResult Edit(Pants pants)
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

            pants.Dressiness = dressiness;
            pants.WarmthType = warmthRating;
            pants.ColorTypes = colorTypes;

            if (pants == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
    
            return View(pants);
        }

        // POST: Pants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "Id,isCapri,IsHighWaist,IsSkinny,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit,UserId,lastWorn")] Pants pants)
        {
            if (ModelState.IsValid)
            {
                pants.lastWorn = DateTime.Today;
                db.Entry(pants).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", pants);
        }

        // GET: Pants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pants pants = db.Pants.Find(id);
            if (pants == null)
            {
                return HttpNotFound();
            }
            return View(pants);
        }

        // POST: Pants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pants pants = db.Pants.Find(id);
            db.Pants.Remove(pants);
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
