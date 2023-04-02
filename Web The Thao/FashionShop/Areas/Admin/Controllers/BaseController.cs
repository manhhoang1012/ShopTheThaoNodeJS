using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FashionShop.Shared;
namespace FashionShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session[Const.AdminIdSession].Equals(""))
            {
                TempData["error"] = "Cảnh báo!";
                TempData["suberror"] = "Vui lòng đăng nhập để sử dụng";
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
              
            }
        }
    }
}