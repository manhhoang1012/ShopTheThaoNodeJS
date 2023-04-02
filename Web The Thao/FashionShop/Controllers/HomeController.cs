using FashionShop.Helper;
using FashionShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FashionShop.Shared;

namespace FashionShop.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context = new AppDbContext();
        public ActionResult Index()
        {
            var products = context.Products.Take(4).ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            if (!Session[Const.CustomerIdSession].Equals(""))
            {
                return Redirect("~/");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]  
        public ActionResult Login(FormCollection login)
        {
            string phone = login["username"];
            string pass = HelperString.toMD5(login["password"]);
            Customer acc = context.Customers.FirstOrDefault(x => x.Phone == phone && x.Password != null);
            if (acc == null)
            {
                TempData["error"] = "Đăng nhập sai!";
                TempData["suberror"] = "Tài khoản không được tồn tại";
            }
            else
            {
                if (acc.Password.Equals(pass))
                {
                    // đăng nhập đúng
                    string cus = JsonConvert.SerializeObject(acc);
                    Session[Const.CustomerSession] = cus;
                    Session[Const.CustomerIdSession] = acc.Id;
                    Session[Const.CartSession] = acc.Id;
                    TempData["success"] = "Đăng nhập thành công!";
                    return Redirect("~/");
                }
                else
                {
                    TempData["error"] = "Đăng nhập sai!";
                    TempData["suberror"] = "Mật khẩu không chính xác";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session[Const.CustomerSession] = "";
            Session[Const.CustomerIdSession] = "";
            Session[Const.CartSession] = Guid.NewGuid().ToString();    
            return Redirect("~/");
        }
        public ActionResult Register()
        {
            if (!Session[Const.CustomerIdSession].Equals(""))
            {
                return Redirect("~/");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            var name = form["Name"];
            var phone = form["Phone"];
            var address = form["Address"];
            var email = form["Email"];
            var pass = HelperString.toMD5(form["Pass"]);
            string cus_id = Session[Const.CartSession].ToString();
            var check_phone = context.Customers.FirstOrDefault(x => x.Phone == phone && x.Password != null);
            if(check_phone!=null){
                ViewBag.Error = "Số điện thoại đã được đăng ký!";
                return View();
            }
            else
            {
                Customer cus = new Customer()
                {
                    Id = cus_id,
                    Name = name,
                    Phone = phone,
                    Address = address,
                    Email = email,
                    Password = pass
                };
                context.Customers.Add(cus);
                context.SaveChangesAsync();
                TempData["success"] = "Đăng ký thành công!";
                return RedirectToAction("Login", "Home");
            }


        }
        public ActionResult Profile()
        {
            if (Session[Const.CustomerIdSession].Equals(""))
            {
                return Redirect("~/");
            }
            Customer cus = JsonConvert.DeserializeObject<Customer>(Session[Const.CustomerSession].ToString());
            return View(cus);
        }
        /// <summary>
        /// Menu render ở đây
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderMenu()
        {
            var categories = context.Categories.ToList();
            var branchs = context.Branchs.ToList();
            var menu = new Menu{
                Categories= categories,
                Branchs= branchs,
            };  
            return PartialView("_MenuBar",menu);
        }

    }
}