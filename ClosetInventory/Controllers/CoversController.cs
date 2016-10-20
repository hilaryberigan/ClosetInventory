﻿using System;
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


        [HttpPost]
        public ActionResult SetImageType(HttpPostedFileBase file)
        {
            
            UploadViewModel model = new UploadViewModel();
            ImageManager imageManager = new ImageManager();
            FinalImageResult final = new FinalImageResult();
            System.Drawing.Image sourceImage = null;

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                sourceImage = System.Drawing.Image.FromStream(file.InputStream);
                var path = Path.Combine(Server.MapPath("~\\Images\\"), fileName);
                file.SaveAs(path);
                model.LargeFile = path;

                System.Drawing.Image image = imageManager.ScaleImage(sourceImage, 75);
                var thumbPath = Path.Combine(Server.MapPath("~\\Images\\Thumbs\\"), fileName);
                image.Save(thumbPath);
                final = imageManager.HandleIncomingImage(path);
                model.SmallFile = thumbPath;
                model.Color = final.ImageProps.dominantColors[0].colorName;
                model.ControllerName = final.ImageLabels.ControllerSuggestion;
            }


            //need large function for figuring out if it is footwear, etc.
            return View(model);
        }

        // POST: Covers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(UploadViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //db.Covers.Add(cover);
        //        //db.SaveChanges();
        //        //return RedirectToAction("Index");
        //    }

        //    return View(cover);
        //}

        // GET: Covers/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Covers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,SmallFile,LargeFile,IsFavorite,DressinessRating,WarmthRating,Color,ColorType,IsTightFit")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cover);
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