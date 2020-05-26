using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XQW.DataAccess;
using XQW.Model.DBEntity;
using XQW.Model.Model;
using XQW.Utility;

namespace XQW.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.HotProductList = new ProductController().GetHotProductList();
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult VIP()
        {
            return View();
        }
    }
}