using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public ProductDal productDal { get; set; } = new ProductDal();
        public ActionResult Index()
        {
            ViewBag.HotProductList = new ProductController().GetHotProductList().Take(10).ToList();
            return View();
        }

        public ActionResult AboutUs()
        {
            //获取数据
            ViewBag.ProductAll = GetProductAllModel();

            ViewBag.ShopInfo = GetShopInfo();
            return View();
        }

        public ActionResult VIP()
        {
            ViewBag.ShopInfo = GetShopInfo();
            return View();
        }

        public ProductAllModel GetProductAllModel()
        {
            var result = new ProductAllModel();
            var param = new SqlParameter();
            var key = AppConst.Cache_AboutUs;
            var sql = $@"SELECT p.ProductName,
                               p.ProductID,
                               p.ProductTitle,
                               ac.ACategoryID,
                               ac.ACategoryName,
                               bc.BCategoryID,
                               bc.BCategoryName
                        FROM   Product p
                               INNER JOIN BCategory bc
                                       ON bc.BCategoryID = p.BCategoryID
                               INNER JOIN ACategory ac
                                       ON ac.ACategoryID = bc.ACategoryID
                        WHERE  p.Status = 1 ";

            var cacheResult = (ProductAllModel)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                var productResult = productDal.QueryCustom<ProductAllOriginalModel>(sql);
                if (productResult?.Any() ?? false)
                {
                    var aGroupData = productResult.GroupBy(p => new { p.ACategoryID, p.ACategoryName });
                    var bGroupData = productResult.GroupBy(p => new { p.BCategoryID, p.BCategoryName });
                    result.ACateCount = aGroupData.Count();
                    result.BCateCount = bGroupData.Count();
                    result.ProductCount = productResult.Count();
                    var aList = new List<ACategoryModel>();
                    foreach (var item in aGroupData)
                    {
                        var acateData = new ACategoryModel();
                        acateData.ACategoryId = item.Key.ACategoryID;
                        acateData.ACategoryName = item.Key.ACategoryName;

                        var bCateList = new List<BCategoryModelWithPro>();
                        var bcateGroup = item.ToList().GroupBy(p => new { p.BCategoryID, p.BCategoryName });
                        foreach (var bcate in bcateGroup)
                        {
                            var bcateData = new BCategoryModelWithPro();
                            bcateData.BCategoryId = bcate.Key.BCategoryID;
                            bcateData.BCategoryName = bcate.Key.BCategoryName;

                            var productList = new List<ProductModel>();
                            foreach (var product in  bcate.ToList())
                            {
                                var productData = new ProductModel();

                                productData.ProductTitle = product.ProductName.Substring(0,4);
                                productData.ProductName = product.ProductName;
                                productData.ProductID = product.ProductID;

                                productList.Add(productData);
                            }
                            bcateData.ProductList = productList;
                            bCateList.Add(bcateData);

                        }
                        acateData.BCategoryModelWithProList = bCateList;
                        aList.Add(acateData);
                    }
                    result.ACategoryList = aList;
                    cacheResult = result;
                }
                else
                {
                    cacheResult = new ProductAllModel();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }
    }
}