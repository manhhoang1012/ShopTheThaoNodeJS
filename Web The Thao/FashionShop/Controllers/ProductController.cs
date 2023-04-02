using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop;
using FashionShop.Models;
using FashionShop.Shared;
namespace FashionShop.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Product
        public ActionResult Index(string brandid = "0", string categoryid ="0", string search="0")
        {
            var products = db.Products.Include(p => p.Branch).Include(p => p.Category);
            if(brandid != "0")
            {
                products = products.Where(x => x.BranchId == brandid);
            }
            if(categoryid != "0")
            {
                products = products.Where(x => x.CategoryId == categoryid);
            }
            if (search != "0")
            {
                products = products.Where(x => x.Name.Contains(search));
            }
            return View(products.ToList());
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Products.Include(p => p.Branch).Include(p => p.Category).FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
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
