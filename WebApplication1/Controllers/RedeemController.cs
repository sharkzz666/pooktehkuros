using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.DB;

namespace WebApplication1.Controllers
{
    public class RedeemController : Controller
    {
        private pooktehlurosEntities db = new pooktehlurosEntities();
        // GET: /Redeem/
        public async Task<ActionResult> Index(string id)
        {
            int temp = Convert.ToInt32(id);
            // ProductTypeID = 2 for redeem, not product
            var purchases = db.Purchases.Include(p => p.Product).Include(p => p.User).Where(p => p.UserID == temp).Where(p => p.Product.ProductTypeID == 2);
            var test = db.Users.Include(q => q.Purchases);
            return View(await test.ToListAsync());
        }
	}
}