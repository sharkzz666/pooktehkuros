using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DB;
using PagedList;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private pooktehlurosEntities db = new pooktehlurosEntities();

        // GET: Admin
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
                    tableuser = tableuser.OrderByDescending(s => s.RID);
                    break;
                default:
                    tableuser = tableuser.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int PageNumber = (page ?? 1);

            return View(tableuser.ToPagedList(PageNumber, pageSize));

        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description");
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description");
            return View();
        }

        // POST: Admin/Create
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
            return View(user);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description", user.RoleID);
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description", user.StatusID);
            return View(user);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,RoleID,ParentID,Username,Password,Name,Email,Phone,IC,RID,Address,Epoint,Rpoint,BonusStar,StatusID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(db.Ref_Role, "ID", "Description", user.RoleID);
            ViewBag.StatusID = new SelectList(db.Ref_Status, "ID", "Description", user.StatusID);
            return View(user);
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
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
