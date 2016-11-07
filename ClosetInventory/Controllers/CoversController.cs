using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Services;
using Google.Apis.Vision.v1;
using Google.Apis.Vision.v1.Data;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ClosetInventory.WorkerClasses;
using Microsoft.AspNet.Identity;

namespace ClosetInventory.Controllers
{
    public class CoversController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Covers
        public ActionResult Index()
        {
            return View(db.Covers.ToList());
        }

        // GET: Covers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // GET: Covers/Create
        public ActionResult Create()
       {
            UploadViewModel model = new UploadViewModel();
            return View(model);
        }
        //public ActionResult SetImageType()


       

        //POST: Covers/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(UploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var cover = new Cover { Color = model.Color, SmallFile = model.SmallFile, LargeFile = model.LargeFile, UserId = userId, lastWorn = DateTime.Today };
                db.Covers.Add(cover);
                db.SaveChanges();
                return RedirectToAction("Edit", cover);
            }

            return RedirectToAction("Upload", "Home");
        }

        // GET: Covers/Edit/5
        public ActionResult Edit(Cover cover)
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

            cover.Dressiness = dressiness;
            cover.WarmthType = warmthRating;
            cover.ColorTypes = colorTypes;

            if (cover == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(cover);
        }

        // POST: Covers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit([Bind(Include = "Id,Type,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit,UserId,lastWorn")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                cover.lastWorn = DateTime.Today;
                db.Entry(cover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Upload", "Home");
            }
            return RedirectToAction("Edit", cover);
        }

        // GET: Covers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.Covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // POST: Covers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cover cover = db.Covers.Find(id);
            db.Covers.Remove(cover);
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
