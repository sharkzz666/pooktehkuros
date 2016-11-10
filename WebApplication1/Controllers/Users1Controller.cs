using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DB;
using WebApplication1.Models;
using PagedList;


namespace WebApplication1.Controllers
{
    public class Users1Controller : Controller
    {
        
        private pooktehlurosEntities db = new pooktehlurosEntities();
        
        // GET: Users1
        public ActionResult Index(string sortOrder, string searchstring, string currentfilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.DateSortParm = sortOrder == "RID" ? "RID" : "RID";

            if (searchstring != null)
            {
                page = 1;
            }
            else
            {
                searchstring = currentfilter;
            }

            ViewBag.currentfilter = currentfilter;

            var tableuser = from s in db.Users select s;

            if (!String.IsNullOrEmpty(searchstring))
            {
                tableuser = tableuser.Where(s => s.Name.Contains(searchstring) || s.RID.Contains(searchstring));
            }

            switch (sortOrder)
            {
                case "Name":
                    tableuser = tableuser.OrderByDescending(s => s.Name);
                    break;
                case "RID":
                    tableuser = tableuser.OrderBy(s => s.RID);
                    break;
                default:
                    tableuser = tableuser.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int PageNumber = (page ?? 1);

            return View(tableuser.ToPagedList(PageNumber, pageSize));

        }

        public ActionResult SearchIndex1(string sortOrder, string searchstring, string currentfilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.DateSortParm = sortOrder == "RID" ? "RID" : "RID";

            if(searchstring != null)
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

       
      
        // GET: Users1/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users1/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description");
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description");
            ViewBag.statuspurchase = new SelectList(db.Purchases, "Status");
            return View();
        }

        // POST: Users1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleID,ParentID,Username,Password,Name,Email,Phone,IC,RID,Address,Epoint,Rpoint,BonusStar,StatusID")] User user)
        {
            if (ModelState.IsValid)
            {

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description", user.RoleID);
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description", user.StatusID);
            ViewBag.statuspurchase = new SelectList(db.Purchases, "Status");
            return View(user);
        }

        // GET: Users1/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description", user.RoleID);
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description", user.StatusID);
            return View(user);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleID,ParentID,Username,Password,Name,Email,Phone,IC,RID,Address,Epoint,Rpoint,BonusStar,StatusID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description", user.RoleID);
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description", user.StatusID);
            return View(user);
        }

        // GET: Users1/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
