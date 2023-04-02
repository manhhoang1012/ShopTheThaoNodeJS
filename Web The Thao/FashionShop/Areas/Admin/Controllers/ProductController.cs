using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FashionShop;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FashionShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private AppDbContext db = new AppDbContext();

        // GET: Admin/Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Branch).Include(p => p.Category);
            return View(products.ToList());
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,Code,Description,Image,Price,Quantity,CategoryId,BranchId")] Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid().ToString();
                if (imgFile != null)
                {
                    var productid = product.Id;
                    var fileName = imgFile.FileName;
                    var filePath = "~/assets/images/" + productid + imgFile.FileName;
                    string extension = Path.GetExtension(imgFile.FileName);
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (imgFile.ContentLength < 1048576 && supportedTypes.Contains(extension.ToLower()))
                    {

                        imgFile.SaveAs(Server.MapPath(filePath));
                        product.Image = productid + fileName;
                        db.Products.Add(product);
                        db.SaveChanges();
                        TempData["success"] = "Thành công!";
                        TempData["subsuccess"] = "Đã tạo mới sản phẩm";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Error";
                        TempData["suberror"] = "File hình ảnh không đúng định dạng hoặc quá kích thước cho phép";
                        ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name", product.BranchId);
                        ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                        return View(product);
                    }
                }
                else
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    TempData["warning"] = "Thành công!";
                    TempData["subwarning"] = "Sản phẩm được tạo mới chưa có hình ảnh";
                    return RedirectToAction("Index");
                }               
            }

            ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name", product.BranchId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.BranchId);
            return View();
        }

        // GET: Admin/Product/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name", product.BranchId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,Description,Image,Price,Quantity,CategoryId,BranchId,CreatedAt,UpdatedAt,DeletedAt,UpdateUserId,CreateUserId")] Product product, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {                      
                if (imgFile != null)
                {
                    var fileName = imgFile.FileName;
                    var filePath = "~/assets/images/" + product.Id + imgFile.FileName;
                    string extension = Path.GetExtension(imgFile.FileName);
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    if (imgFile.ContentLength < 1048576 && supportedTypes.Contains(extension.ToLower()))
                    {
                        imgFile.SaveAs(Server.MapPath(filePath));
                        product.Image = product.Id + fileName;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["success"] = "Thành công!";
                        TempData["subsuccess"] = "Đã cập nhật sản phẩm";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Error";
                        TempData["suberror"] = "File hình ảnh không đúng định dạng hoặc quá kích thước cho phép";
                        ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name", product.BranchId);
                        ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
                        return View(product);
                    }
                }
                else
                {
                    var productEdit = db.Products.Find(product.Id);
                    productEdit.Name= product.Name;
                    productEdit.Code= product.Code;
                    productEdit.Price= product.Price;
                    productEdit.Description= product.Description;
                    productEdit.BranchId = product.BranchId;
                    productEdit.CategoryId= product.CategoryId;
                    productEdit.Quantity= product.Quantity;
                    db.SaveChanges();
                    TempData["success"] = "Thành công!";
                    TempData["subsuccess"] = "Đã cập nhật sản phẩm";
                    return RedirectToAction("Index");
                }
               

            }
            ViewBag.BranchId = new SelectList(db.Branchs, "Id", "Name", product.BranchId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            TempData["success"] = "Thành công!";
            TempData["subsuccess"] = "Sản phẩm đã bị xóa";
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
