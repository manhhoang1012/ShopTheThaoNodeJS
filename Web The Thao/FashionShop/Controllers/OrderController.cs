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
    public class OrderController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Order
        public ActionResult Index()
        {
            // Các đơn hàng chưa nhận...
            var cusId = Session[Const.CartSession].ToString();
            var orders = db.Orders.Where(x=>x.CustomerId == cusId && x.Status != StatusConst.Done).Include(x=>x.Product.Branch).Include(x=>x.Customer);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orders = db.Orders.Include(x => x.Customer);
            var orderdetail = orders.FirstOrDefault(x=>x.Id == id);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        // GET: Order/Edit/5
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

            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection form, string orderid)
        {
            if (ModelState.IsValid)
            {
                var orderEdit = db.Orders.Find(orderid);
                if (orderEdit == null)
                {
                    return HttpNotFound();
                }
                if(orderEdit.Status == StatusConst.WaitConfirm)
                {
                    orderEdit.Address = form["address"];
                    orderEdit.Name = form["nameorder"];
                    orderEdit.Phone = form["phone"];
                    orderEdit.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                    TempData["success"] = "Thành công!";
                    TempData["subsuccess"] = "Đã cập nhật đơn hàng";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Không được cập nhật!";
                    TempData["suberror"] = "Đơn hàng của bạn đã được xác nhận vừa song, vui lòng kiểm tra lại!";
                    return RedirectToAction("Index");
                }
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string orderid)
        {
            if (orderid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(orderid);
            if (order == null)
            {
                return HttpNotFound();
            }
            if(order.Status == StatusConst.WaitConfirm || order.Status == StatusConst.Confirmed)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã xóa đơn hàng!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Không được xóa!";
                TempData["suberror"] = "Đơn hàng của bạn đã được xử lí vừa song, vui lòng kiểm tra lại";
                return RedirectToAction("Index");
            }
           
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
