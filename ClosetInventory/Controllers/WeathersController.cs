﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosetInventory.Models;
using System.Threading.Tasks;
using ClosetInventory.WorkerClasses;

namespace ClosetInventory.Controllers
{
    public class WeathersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Weathers
        public ActionResult Index()
        {

            return View(db.Weathers.ToList());
        }

        // GET: Weathers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // GET: Weathers/Create
        public async Task<ActionResult> Create()
        {
            WeatherSearch ws = new WeatherSearch();
            //var results = await ws.GetRootObject();
            return View("Create");
        }

        // POST: Weathers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Temperature,SkyConditions")] Weather weather)
        {
           

            if (ModelState.IsValid)
            {
                db.Weathers.Add(weather);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(weather);
        }

        // GET: Weathers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Temperature,SkyConditions")] Weather weather)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weather).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(weather);
        }

        // GET: Weathers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Weather weather = db.Weathers.Find(id);
            if (weather == null)
            {
                return HttpNotFound();
            }
            return View(weather);
        }

        // POST: Weathers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Weather weather = db.Weathers.Find(id);
            db.Weathers.Remove(weather);
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
