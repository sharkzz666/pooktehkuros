using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DB;
using PagedList;

namespace WebApplication1.Controllers
{
    public class ManagePurchaseController : Controller
    {
        private pooktehlurosEntities db = new pooktehlurosEntities();

        // GET: ManagePurchase
        public ActionResult Index(string sortOrder, string searchstring, string currentfilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Description" : "";
            ViewBag.DateSortParm = sortOrder == "Status" ? "Status" : "Status";

            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }

            ViewBag.currentfilter = currentfilter;

            var tablepurchase = from s in db.Purchases select s;

            if (!String.IsNullOrEmpty(searchstring))
            {
                tablepurchase = tablepurchase.Where(s => s.Description.Contains(searchstring) || s.Status.Contains(searchstring));
            }

            switch (sortOrder)
            {
                case "Description":
                    tablepurchase = tablepurchase.OrderByDescending(s => s.Description);
                    break;
                case "Status":
                    tablepurchase = tablepurchase.OrderBy(s => s.Status);
                    break;
                default:
                    tablepurchase = tablepurchase.OrderBy(s => s.Description);
                    break;
            }

            int pageSize = 10;
            int PageNumber = (page ?? 1);

            return View(tablepurchase.ToPagedList(PageNumber, pageSize));

        }

        // GET: ManagePurchase/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: ManagePurchase/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "NameProduct");
            ViewBag.UserID = new SelectList(db.Users, "ID", "ParentID");
            return View();
        }

        // POST: ManagePurchase/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductID,UserID,Description,Quantity,Status")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "NameProduct", purchase.ProductID);
            ViewBag.UserID = new SelectList(db.Purchases, "ID", "ParentID", purchase.UserID);
            return View(purchase);
        }

        // GET: ManagePurchase/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "NameProduct", purchase.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "ParentID", purchase.UserID);
            return View(purchase);
        }

        // POST: ManagePurchase/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductID,UserID,Description,Quantity,Status")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { @id = Session["UserID"] });
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "NameProduct", purchase.ProductID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "ParentID", purchase.UserID);
            return View(purchase);
        }

        // GET: ManagePurchase/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: ManagePurchase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
