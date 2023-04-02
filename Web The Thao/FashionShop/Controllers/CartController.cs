using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FashionShop;
using FashionShop.Models;
using Newtonsoft.Json;
using FashionShop.Shared;
namespace FashionShop.Controllers
{
    public class CartController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public const string CARTKEY = "cart";

        public ActionResult Index()
        {
            return View(GetCart());
        }
        /* // Lấy cart từ Session (danh sách CartItem)
         List<CartItem> GetCartItems()
         {
             var jsoncart = (string)Session[CARTKEY];
             if (jsoncart != null)
             {
                 return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
             }
             return new List<CartItem>();
         }*/
        List<Cart> GetCart()
        {
            string sessionId = Session[Const.CartSession].ToString();
            var carts = db.Carts.Where(x => x.SessionId == sessionId).Include(x => x.Product).OrderByDescending(x => x.CreatedAt).ToList();
            return carts;
        }   
        // Xóa cart khỏi session
        void ClearCart()
        {
            string sessionId = Session[Const.CartSession].ToString();
            var carts = db.Carts.Where(x => x.SessionId == sessionId).ToList();
            foreach(var item in carts)
            {
                db.Carts.Remove(item);
            }
            db.SaveChanges();
        }


        // Lưu Cart (Danh sách CartItem) vào session
  /*      void SaveCartSession(List<CartItem> ls)
        {
            string jsoncart = JsonConvert.SerializeObject(ls);
            Session[CARTKEY] = jsoncart ;
        }*/
/*        [HttpPost]
        public ActionResult UpdateCart(string productid, int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity = quantity;
                TempData["success"] = "Cập nhật số lượng thành công!";
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return new HttpStatusCodeResult(HttpStatusCode.OK);  // ;
        }*/
        [HttpPost]
        public ActionResult UpdateCart(string productid, int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            string sessionId = Session[Const.CartSession].ToString();
            var carts = GetCart();
            var cartitem = carts.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
                cartitem.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã cập nhật số lượng";
            }
            else
            {
                 TempData["error"] = "Không Thành công!";
                TempData["suberror"] = "Vui lòng thử lại";
            }
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return new HttpStatusCodeResult(HttpStatusCode.OK);  // ;
        }
/*        [HttpPost]
        public ActionResult AddToCart(FormCollection form)
        {
            var x = form["productId"].ToString();
            var prod = db.Products
                .FirstOrDefault(p => p.Id == x);
            if (prod == null)
                return HttpNotFound();

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == form["productId"]);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity += Int32.Parse(form["quantity"]);
            }
            else
            { 
                //  Thêm mới
                cart.Add(new CartItem()
                {
                    Quantity = Int32.Parse(form["quantity"]),
                    Product = prod
                });
            }
            // Lưu cart vào Session
            SaveCartSession(cart);

            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Index");
        }*/
        [HttpPost]
        public ActionResult AddToCart(FormCollection form)
        {
            var proId = form["productId"].ToString();
            var prod = db.Products.FirstOrDefault(p => p.Id == proId);
            if (prod == null)
                return HttpNotFound();

            // Xử lý đưa vào Cart ...
            var carts = GetCart();
            var cartitem = carts.Find(p => p.Product.Id == proId);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity += Int32.Parse(form["quantity"]);
                cartitem.UpdatedAt= DateTime.Now;
                TempData["info"] = "Thành công!";
                TempData["subinfo"] = "Sản phầm đã được thêm vào giỏ hàng trước đó, số lượng cập nhật đã được cập nhật";
            }
            else
            {
                //  Thêm mới
                db.Carts.Add(new Cart()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = proId,
                    Quantity = Int32.Parse(form["quantity"]),
                    SessionId = Session[Const.CartSession].ToString(),
                    CreatedAt = DateTime.Now,
                });
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã thêm sản phẩm vào giỏ hàng";
            }
            // Lưu cart
            db.SaveChanges();
   
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Index");
        }


/*        public ActionResult RemoveCart(string productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }*/
        public ActionResult RemoveCart(string productid)
        {
            var carts = GetCart();
            var cartitem = carts.Find(p => p.Product.Id == productid);
            if (cartitem != null)
            {
                db.Carts.Remove(cartitem);
                db.SaveChanges();
                TempData["success"] = "Thành công!";
                TempData["subsuccess"] = "Đã xóa sản phẩm khỏi giỏ hàng";
            }
            else
            {
                TempData["error"] = "Không thành công";
                TempData["suberror"] = "Vui lòng thử lại";
            }
            return RedirectToAction("Index");
        }
        public ActionResult CheckOut() 
        {
            ViewBag.Name = "";
            ViewBag.Phone = "";
            ViewBag.Address = "";
            if (!Session[Const.CustomerIdSession].Equals(""))
            {
                Customer cus = JsonConvert.DeserializeObject<Customer>(Session[Const.CustomerSession].ToString());
                ViewBag.Name = cus.Name;
                ViewBag.Phone = cus.Phone;
                ViewBag.Address = cus.Address;
            }
            
            return View(GetCart());
        }
        [HttpPost]
        public async Task<ActionResult> CheckOut(FormCollection form)
        {
            var name = form["Name"].Trim();
            var phone = form["Phone"].Trim();
            var address = form["Address"].Trim();
            if (Session[Const.CustomerIdSession].Equals("")) 
            {
                string cus_id = Session[Const.CartSession].ToString();
                Customer cus = new Customer()
                {
                    Id = cus_id,
                    Name = name,
                    Phone = phone,
                    Address = address
                };
                db.Customers.Add(cus);
                await db.SaveChangesAsync();
            }
            
            var cart = GetCart();
            foreach (var item in cart)
            {
                try
                {
                    decimal? discount = item.Product.Discount != null ? item.Product.Discount * item.Quantity : 0;
                    var total = (item.Product.Price * item.Quantity) - discount;
                    Order order = new Order()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = name,
                        Phone = phone,
                        Address = address,
                        Quantity = item.Quantity,
                        ProductId = item.Product.Id,
                        Discount = discount,
                        Total = total,
                        CustomerId = Session[Const.CartSession].ToString(),
                        CreatedAt= DateTime.Now,
                    };
                    db.Orders.Add(order);
                    await db.SaveChangesAsync();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
               
            }
            TempData["success"] = "Đặt hàng thành công!";
            TempData["subsuccess"] = "Cảm ơn quý khách hàng đã ủng hộ";
            ClearCart();
            return RedirectToAction("Index", "Home");
        }

    }
}
