using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XQW.DataAccess;
using XQW.Model.DBEntity;
using XQW.Utility;

namespace XQW.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductDal productDal { get; set; } = new ProductDal();

        public ActionResult ProductDetail(string productid)
        {
            var productDetail = GetProductDetailById(productid);
            ViewBag.ProductDetail = productDetail;
            return View();
        }

        public JsonResult GetProductListJsonBybCate(string bCategoryid)
        {
            return Json(GetProductListBybCate(bCategoryid), JsonRequestBehavior.AllowGet);
        }


        public List<Product> GetProductListBybCate(string bCategoryid)
        {
            var key = $"productlist.bcategory.{bCategoryid}";
            var cacheResult = (List<Product>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                var sql = $@"select * from XQW_Data..Product  as p with(nolock) where p.BCategoryID = @BCategoryID";
                var param = new SqlParameter
                {
                    ParameterName = "@BCategoryID",
                    Value = bCategoryid
                };
                cacheResult = productDal.QueryCustom<Product>(sql, param) ?? new List<Product>();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        public Product GetProductDetailById(string productid)
        {
            var key = $"productdetail.{productid}";
            var cacheResult = (Product)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                var sql = $@"select * from XQW_Data..Product  as p with(nolock) where p.ProductID = @ProductID";
                var param = new SqlParameter
                {
                    ParameterName = "@ProductID",
                    Value = productid
                };
                cacheResult = productDal.QueryCustom<Product>(sql, param)?.FirstOrDefault() ?? new Product();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }
    }
}