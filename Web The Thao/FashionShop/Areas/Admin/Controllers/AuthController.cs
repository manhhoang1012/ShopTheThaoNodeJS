using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Helper;
using FashionShop.Models;
using FashionShop.Shared;
using Newtonsoft.Json;

namespace FashionShop.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        AppDbContext context = new AppDbContext();
        // GET: Admin/Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            if (!Session[Const.AdminIdSession].Equals(""))
            {
                return Redirect("~/Admin/Dashboard");
            }
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection login)
        {
            string emailOrPhone = login["username"];
            string pass = HelperString.toMD5(login["password"]);
            Account account = context.Accounts.FirstOrDefault(x=>x.Email == emailOrPhone || x.Phone == emailOrPhone);
            if (account == null)
            {
                TempData["error"] = "Đăng nhập sai!";
                TempData["suberror"] = "Tài khoản không được tồn tại";
            }
            else
            {
                if (account.Password.Equals(pass))
                {
                    // đăng nhập đúng
                    string acc = JsonConvert.SerializeObject(account);
                    Session[Const.AdminSession] = acc;
                    Session[Const.AdminIdSession] = account.Id;
                    TempData["success"] = "Đăng nhập thành công!";
                    return Redirect("~/Admin/Dashboard");
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
            Session[Const.AdminSession] = "";
            Session[Const.AdminIdSession] = "";
            TempData["error"] = "Đăng xuất!";
            TempData["suberror"] = "Vui lòng đăng nhập";
            return  Redirect("~/Admin/Login");
        }
        public ActionResult Profile()
        {
            if (Session[Const.AdminIdSession].Equals(""))
            {
                return Redirect("~/Admin/Login");
            }
            Account acc = JsonConvert.DeserializeObject<Account>(Session[Const.AdminSession].ToString());
            return View(acc);
        }
    }
}