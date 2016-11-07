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
    public class WishListItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WishListItems
        public ActionResult WishListIndex()
        {
            return View(db.WishListItems.ToList());
        }

        // GET: WishListItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // GET: WishListItems/Create
        public ActionResult CreateWishListItem()
        {
            return View();
        }

        // POST: WishListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateWishListItem(AmazonSearchModel model)
        {
            WishListItem wListItem = new WishListItem();
            wListItem.ImageUrl = model.Item.MediumImage.ToString();
            wListItem.Url = model.Item.DetailPageURL;
            wListItem.Title = model.Item.ItemAttributes.Title;

            if (ModelState.IsValid)
            {
                db.WishListItems.Add(wListItem);
                db.SaveChanges();
              
            }

            return RedirectToAction("WishListIndex");
        }

        // GET: WishListItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // POST: WishListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Url,Title,ImageUrl")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishListItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("WishListIndex");
            }
            return View(wishListItem);
        }

        // GET: WishListItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }


        // POST: WishListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishListItem wishListItem = db.WishListItems.Find(id);
            db.WishListItems.Remove(wishListItem);
            db.SaveChanges();
            return RedirectToAction("WishListIndex");
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
