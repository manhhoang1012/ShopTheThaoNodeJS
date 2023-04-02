using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Shared
{
    public static class Const
    {
        public const string CartSession = "CART";
        public const string AdminSession = "ADMIN";
        public const string AdminIdSession = "ADMINID";
        public const string CustomerSession = "CUS";
        public const string CustomerIdSession = "CUSID";

    }
    public static class StatusConst
    {
        public const string WaitConfirm = "WAITCONFIRM";
        public const string Confirmed = "CONFIRMED";
        public const string Shipping = "SHIPPING";
        public const string Done = "DONE";
    }
   
}