using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XQW.DataAccess;
using XQW.Model.DBEntity;
using XQW.Model.Model;
using XQW.Utility;

namespace XQW.Web.Controllers
{
    public class ProductController : Controller
    {
        public ProductDal productDal { get; set; } = new ProductDal();

        /// <summary>
        /// 商品详情信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ActionResult ProductDetail(string productid)
        {
            ViewBag.ProductDetail = GetProductDetailById(productid);
            ViewBag.HotProductList = GetHotProductList(productid);
            ViewBag.PreNextProduct = GetHotProductList(productid);
            ViewBag.OtherBuyProductList = GetHotProductList(productid);
            ViewBag.ShopInfo = GetShopInfo();
            return View();
        }

        /// <summary>
        /// 小青蛙详细信息（包括简介、联系方式、二维码、邮箱等等）
        /// </summary>
        /// <param name="bCategoryid"></param>
        /// <returns></returns>
        public ShopInfoModel GetShopInfo()
        {
            var shopmodel = new ShopInfoModel
            {
                ShopName = "小青蛙商学院",
                Phone = "17601492856",
                MailAddress = "1315915446@qq.com",
                WXNumber = "17601492856",
                WXImageUrl = "www.sss.com/sss.jpg",
                Slogan = "学知识，上小青蛙商学院！",
                Introduction = $@"小青蛙商学院，竭力为您提供全方位的学习资源，靠谱的教学资料，优质的原创课程。学知识，上小青蛙商学院！",
            };
            return shopmodel;
        }

        /// <summary>
        /// 根据productid获取热门关联商品
        /// </summary>
        /// <param name="bCategoryid"></param>
        /// <returns></returns>
        public List<ProductModel> GetHotProductList(string productId)
        {
            return GetProductListBybCate("", "B004");
        }

        /// <summary>
        /// 根据productid获取前后顺序商品
        /// </summary>
        /// <param name="bCategoryid"></param>
        /// <returns></returns>
        public List<ProductModel> GetPreNextProduct(string productId)
        {
            return GetProductListBybCate("","B004");
        }


        /// <summary>
        /// 其他人正在买（去重3件）
        /// </summary>
        /// <param name="bCategoryid"></param>
        /// <returns></returns>
        public List<ProductModel> GetOtherBuyProductList()
        {
            return GetProductListBybCate("", "B004");
        }

        public JsonResult GetProductListJsonBybCate(string bCategoryid)
        {
            return Json(GetProductListBybCate("",bCategoryid), JsonRequestBehavior.AllowGet);
        }

        public List<ProductModel> GetProductListBybCate(string aCategoryid, string bCategoryid)
        {
            if(string.IsNullOrWhiteSpace(aCategoryid) && string.IsNullOrWhiteSpace(bCategoryid))
            {
                //log 参数错误
                return new List<ProductModel>();
            }

            var param = new SqlParameter();
            var key = $"productlist.bcategory.";
            var sql = $@"SELECT    p.ProductID,
                                   p.ProductName,
                                   p.ProductTitle,
                                   p.ProductSubTtile,
                                   p.LikeCount,
                                   p.CommentCount,
                                   p.SeenCount,
                                   p.Status,
                                   ac.ACategoryID,
                                   bc.BCategoryID,
                                   p.UpdateTime,
                                   ac.ACategoryName,
                                   bc.BCategoryName
                            FROM   Product p WITH(nolock)
                                   INNER JOIN BCategory bc WITH(nolock)
                                           ON bc.BCategoryID = p.BCategoryID
                                   INNER JOIN ACategory ac WITH(nolock)
                                           ON ac.ACategoryID = bc.ACategoryID
                            WHERE  Status = 1 ";
            if (!string.IsNullOrWhiteSpace(bCategoryid))
            {
                key += bCategoryid;
                param.ParameterName = "@BCategoryID";
                param.Value = bCategoryid;
                sql += " AND bc.BCategoryID = @BCategoryID ";
            }
            else
            {
                key += aCategoryid;
                param.ParameterName = "@ACategoryID";
                param.Value = aCategoryid;
                sql += " AND ac.ACategoryID = @ACategoryID ";
            }

            var cacheResult = (List<ProductModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = productDal.QueryCustom<ProductModel>(sql, param) ?? new List<ProductModel>();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        public ProductModel GetProductDetailById(string productid)
        {
            var key = $"productdetail.{productid}";
            var cacheResult = (ProductModel)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                var sql = $@"SELECT    p.ProductID,
                                       p.ProductName,
                                       p.ProductTitle,
                                       p.ProductSubTtile,
                                       p.LikeCount,
                                       p.CommentCount,
                                       p.SeenCount,
                                       p.Status,
                                       ac.ACategoryID,
                                       bc.BCategoryID,
                                       p.UpdateTime,
                                       ac.ACategoryName,
                                       bc.BCategoryName
                                FROM   Product p WITH(nolock)
                                       INNER JOIN BCategory bc WITH(nolock)
                                               ON bc.BCategoryID = p.BCategoryID
                                       INNER JOIN ACategory ac WITH(nolock)
                                               ON ac.ACategoryID = bc.ACategoryID
                                WHERE  p.ProductID = @ProductID
                                       AND Status = 1 ";
                var param = new SqlParameter
                {
                    ParameterName = "@ProductID",
                    Value = productid
                };
                cacheResult = productDal.QueryCustom<ProductModel>(sql, param)?.FirstOrDefault() ?? new ProductModel();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }
    }
}