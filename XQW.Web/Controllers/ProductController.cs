using Newtonsoft.Json;
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
    public class ProductController : BaseController
    {
        public ProductDal productDal { get; set; } = new ProductDal();
        public CommentDal commentDal { get; set; } = new CommentDal();
        
        /// <summary>
        /// 商品详情信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public ActionResult ProductDetail(string productid)
        {
            ViewBag.ProductDetail = GetProductDetailById(productid);
            ViewBag.HotProductList = GetHotProductList(productid).Take(5).ToList();
            ViewBag.PreNextProduct = GetPreNextProductList(productid);
            ViewBag.OtherBuyProductList = GetOtherBuyProductList(productid);
            ViewBag.ShopInfo = GetShopInfo();
            return View();
        }

        /// <summary>
        /// 根据productid获取上一个下一个商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductModel> GetPreNextProductList(string productId)
        {
            var param = new SqlParameter();
            var key = AppConst.Cache_PreNextProduct+ productId;
            var sql = $@"SELECT*
                            FROM  (SELECT TOP 1 p.ProductID,
                                                p.ProductName,
                                                p.ProductTitle,
                                                p.ProductSubTtile,
                                                p.LikeCount,
                                                p.CommentCount,
                                                p.SeenCount,
                                                p.DetailPicCount,
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
                                   WHERE  @ProductID > p.ProductID
                                   ORDER  BY p.ProductID DESC) t
                            UNION
                            SELECT TOP 1 p.ProductID,
                                         p.ProductName,
                                         p.ProductTitle,
                                         p.ProductSubTtile,
                                         p.LikeCount,
                                         p.CommentCount,
                                         p.SeenCount,
                                         p.DetailPicCount,
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
                            WHERE  @ProductID < p.ProductID ";

            var cacheResult = (List<ProductModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                param.ParameterName = "@ProductID";
                param.Value = productId;

                cacheResult = productDal.QueryCustom<ProductModel>(sql, param);
                if (cacheResult == null || !cacheResult.Any())
                {
                    cacheResult = new List<ProductModel>();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }


        /// <summary>
        /// 根据productid获取同类目其他商品
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="topCount">默认取3个</param>
        /// <returns></returns>
        public List<ProductModel> GetOtherBuyProductList(string productId,int topCount=3)
        {
            var param = new SqlParameter();
            var key = AppConst.Cache_OtherBuyProduct + productId + topCount;
            var sql = $@"SELECT TOP {topCount} p.ProductID,
             p.ProductName,
             p.ProductTitle,
             p.ProductSubTtile,
             p.LikeCount,
             p.CommentCount,
             p.SeenCount,
             p.DetailPicCount,
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
WHERE  bc.BCategoryID = (SELECT p.BCategoryID
                         FROM   Product p WITH(nolock)
                         WHERE  p.ProductID = @ProductID) 
						 and p.ProductID != @ProductID";

            

            var cacheResult = (List<ProductModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                param.ParameterName = "@ProductID";
                param.Value = productId;

                cacheResult = productDal.QueryCustom<ProductModel>(sql, param);
                if (cacheResult?.Any() ?? false)
                {
                    cacheResult.ForEach(p =>
                    {
                        p.HeaderImageUrl = AppConst.GetProductHeaderImgUrl(p.ProductID);
                    });
                }
                else
                {
                    cacheResult = new List<ProductModel>();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        /// <summary>
        /// 根据productid获取热门关联商品
        /// </summary>
        /// <param name="productId">暂时没有根据productId获取热门商品，预留后续扩展</param>
        /// <returns></returns>
        public List<ProductModel> GetHotProductList(string productId = "")
        {
            var param = new SqlParameter();
            var key = AppConst.Cache_HotProductListAll;
            var sql = $@"SELECT    p.ProductID,
                                   p.ProductName,
                                   p.ProductTitle,
                                   p.ProductSubTtile,
                                   p.LikeCount,
                                   p.CommentCount,
                                   p.SeenCount,
                                   p.DetailPicCount,
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
                            WHERE  p.IsHot = 1 ";

            var cacheResult = (List<ProductModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = productDal.QueryCustom<ProductModel>(sql);
                if (cacheResult?.Any() ?? false)
                {
                    cacheResult.ForEach(p =>
                    {
                        p.HeaderImageUrl = AppConst.GetProductHeaderImgUrl(p.ProductID);
                    });
                }
                else
                {
                    cacheResult = new List<ProductModel>();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        public List<ProductModel> GetProductListBybCate(string aCategoryid, string bCategoryid)
        {
            if(string.IsNullOrWhiteSpace(aCategoryid) && string.IsNullOrWhiteSpace(bCategoryid))
            {
                //log 参数错误
                return new List<ProductModel>();
            }

            var param = new SqlParameter();
            var key = "";
            var sql = $@"SELECT    p.ProductID,
                                   p.ProductName,
                                   p.ProductTitle,
                                   p.ProductSubTtile,
                                   p.LikeCount,
                                   p.CommentCount,
                                   p.SeenCount,
                                   p.DetailPicCount,
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
                key = AppConst.Cache_ProductInfoListByBCate + bCategoryid;
                param.ParameterName = "@BCategoryID";
                param.Value = bCategoryid;
                sql += " AND bc.BCategoryID = @BCategoryID ";
            }
            else
            {
                key = AppConst.Cache_ProductInfoListByACate + aCategoryid;
                param.ParameterName = "@ACategoryID";
                param.Value = aCategoryid;
                sql += " AND ac.ACategoryID = @ACategoryID ";
            }

            var cacheResult = (List<ProductModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = productDal.QueryCustom<ProductModel>(sql, param);
                if (cacheResult?.Any() ?? false)
                {
                    cacheResult.ForEach(p =>
                    {
                        p.HeaderImageUrl = AppConst.GetProductHeaderImgUrl(p.ProductID);
                    });
                }
                else
                {
                    cacheResult = new List<ProductModel>();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        public ProductModel GetProductDetailById(string productid)
        {
            var key = AppConst.Cache_ProductInfoByID + productid;
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
                                       p.DetailPicCount,
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
                cacheResult = productDal.QueryCustom<ProductModel>(sql, param)?.FirstOrDefault();
                if (cacheResult != null)
                {
                    //头图和详情图 url
                    cacheResult.DetailImageUrlList = AppConst.GetProductDetailImgUrl(productid, cacheResult.DetailPicCount);
                }
                else
                {
                    cacheResult = new ProductModel();
                }
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        [HttpPost]
        public JsonResult CommitComment(CommentRequest request)
        {
            try
            {
                var comment = new Comment();
                comment.CommentContent = request.CommentContent;
                comment.ContactNo = request.ContactNo;
                comment.NickName = request.NickName;
                comment.ProductID = request.ProductID;
                commentDal.Add(comment);
            }
            catch(Exception ex)
            {
                WriteErrorLog($"留言失败：请求参数：{JsonConvert.SerializeObject(request)}，错误：{ex.Message}");
            }
            return Json("success");
        }
    }
}