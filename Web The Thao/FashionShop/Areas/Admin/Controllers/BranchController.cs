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

namespace FashionShop.Areas.Admin.Controllers
{
    public class BranchController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Branche
        public ActionResult Index()
        {
            return View(db.Branchs.ToList());
        }

        // GET: Admin/Branche/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branchs.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Admin/Branche/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Branche/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,UpdateUserId,CreateUserId")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                branch.Id = id;
                db.Branchs.Add(branch);
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã tạo mới thương hiệu";
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Admin/Branche/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branchs.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Admin/Branche/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreatedAt,UpdatedAt,DeletedAt,UpdateUserId,CreateUserId")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã cập nhật thương hiệu";
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Admin/Branche/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Branch branch = db.Branchs.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Admin/Branche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Branch branch = db.Branchs.Find(id);
            db.Branchs.Remove(branch);
            db.SaveChanges();
            TempData["success"] = "Thành công!";
            TempData["subsuccess"] = "Thương hiệu đã bị xóa";
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
