using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XQW.DataAccess;
using XQW.Model.DBEntity;
using XQW.Utility;

namespace XQW.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TODO 分页
            ViewBag.HotProductList = new ProductController().GetHotProductList();
            ViewBag.BannerList = new ProductController().GetHotProductList();
            return View();
        }

        public ActionResult AllProduct()
        {
            //TODO 分页
            ViewBag.HotProductList = new ProductController().GetHotProductList();
            ViewBag.BannerList = new ProductController().GetHotProductList();
            return View();
        }

        public ActionResult ContactUs()
        {
            //TODO 分页
            ViewBag.HotProductList = new ProductController().GetHotProductList();
            ViewBag.BannerList = new ProductController().GetHotProductList();
            return View();
        }
    }
}