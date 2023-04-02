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
using Newtonsoft.Json;

namespace FashionShop.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Account
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,Password,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                account.Id = id;
                account.Password = Helper.HelperString.toMD5(account.Password);
                db.Accounts.Add(account);
                db.SaveChanges();
                TempData["success"] = "Thành công";
                TempData["subsuccess"] = "Tạo tài khoản mới";
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,Password,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                var acc = db.Accounts.Find(account.Id);
                acc.Name = account.Name;
                acc.Email = account.Email;
                acc.Phone = account.Phone;
                acc.Role = account.Role;
                db.SaveChanges();
                TempData["success"] = "Thành công";
                TempData["subsuccess"] = "Đã cập nhật tài khoản";
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            TempData["success"] = "Thành công";
            TempData["subsuccess"] = "Tài khoản đã bị xóa";
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
