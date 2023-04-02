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

namespace FashionShop.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Order
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Product.Branch).Include(o => o.Product.Category);
            return View(orders.ToList());
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o=>o.Customer).Include(o=>o.Product).Include(o=>o.Account).FirstOrDefault(o=>o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Order/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.StatusId = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Chờ xác nhận", Value = StatusConst.WaitConfirm},
                new SelectListItem { Selected = false,Text = "Xác nhận", Value = StatusConst.Confirmed},
                new SelectListItem { Selected = false, Text = "Đang giao", Value = StatusConst.Shipping},
                new SelectListItem { Selected = false, Text = "Hoàn thành", Value = StatusConst.Done},
            };
            return View();
        }

        // POST: Admin/Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Discount,Phone,ProductId,Quantity,Address,CreateAt,UpdatedAt,DeletedAt,UpdateUserId,CreateUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                decimal? discount = order.Discount != null ? order.Discount : 0;
                var product = db.Products.Find(order.ProductId);
                decimal? total = (product.Price * order.Quantity) - discount;
                Customer cus = new Customer()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = order.Name,
                    Phone = order.Phone,
                    Address = order.Address
                };
                order.Total = total;
                order.Id = Guid.NewGuid().ToString();
                order.CreatedAt = DateTime.Now;
                order.CustomerId = cus.Id;
                order.CreateUserId = Session[Const.AdminIdSession].ToString();
                order.UpdateUserId = Session[Const.AdminIdSession].ToString();
                db.Customers.Add(cus);
                db.Orders.Add(order);
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã tạo mới đơn hàng";
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", order.ProductId);
            ViewBag.StatusId = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Chờ xác nhận", Value = StatusConst.WaitConfirm},
                new SelectListItem { Selected = false,Text = "Xác nhận", Value = StatusConst.Confirmed},
                new SelectListItem { Selected = false, Text = "Đang giao", Value = StatusConst.Shipping},
                new SelectListItem { Selected = false, Text = "Hoàn thành", Value = StatusConst.Done},
            };
            return View(order);
        }

        // GET: Admin/Order/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.UpdateUserId = new SelectList(db.Accounts, "Id", "Name", order.UpdateUserId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", order.ProductId);
            ViewBag.StatusId = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Chờ xác nhận", Value = StatusConst.WaitConfirm},
                new SelectListItem { Selected = false,Text = "Xác nhận", Value = StatusConst.Confirmed},
                new SelectListItem { Selected = false, Text = "Đang giao", Value = StatusConst.Shipping},
                new SelectListItem { Selected = false, Text = "Hoàn thành", Value = StatusConst.Done},
            };
            return View(order);
        }

        // POST: Admin/Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Discount,Name, Phone, Address, Status, Shipdate,Total,Quantity,CustomerId,ProductId,CreatedAt,UpdatedAt,DeletedAt,UpdateUserId,CreateUserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đơn hàng được cập nhật";
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            ViewBag.UpdateUserId = new SelectList(db.Accounts, "Id", "Name", order.UpdateUserId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", order.ProductId);
            ViewBag.StatusId = new List<SelectListItem>
            {
                new SelectListItem { Selected = true, Text = "Chờ xác nhận", Value = StatusConst.WaitConfirm},
                new SelectListItem { Selected = false,Text = "Xác nhận", Value = StatusConst.Confirmed},
                new SelectListItem { Selected = false, Text = "Đang giao", Value = StatusConst.Shipping},
                new SelectListItem { Selected = false, Text = "Hoàn thành", Value = StatusConst.Done},
            };
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Include(o => o.Customer).Include(o => o.Product).Include(o=>o.Account).FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            TempData["success"] = "Thành công!";
            TempData["subsuccess"] = "Đơn hàng đã bị xóa";
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
