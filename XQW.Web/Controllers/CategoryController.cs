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
    public class CategoryController : BaseController
    {
        public BCategoryDal bCategoryDal { get; set; } = new BCategoryDal();

        public ActionResult Index(string acategoryid, string bcategoryid)
        {
            var productList = new ProductController().GetProductListBybCate(acategoryid, bcategoryid);
            ViewBag.ProductList = productList;
            var categoryList = GetCategoryListByaCate(acategoryid);//todo 小分类 选中
            ViewBag.CategoryList = categoryList;//todo 小分类 选中
            ViewBag.ACategoryID = acategoryid;
            ViewBag.BCategoryID = bcategoryid;
            ViewBag.IsAllActive = !categoryList.Any(p => p.BCategoryID.ToLower().Equals(bcategoryid?.ToLower()));
            return View();
        }

        public JsonResult GetAllBCategoryList()
        {
            return Json(GetCategoryListByaCate(""), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllACategoryList()
        {
            return Json(GetAllACategoryInfoList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClearAllCache()
        {
            //清除所有缓存
            CacheHelper.RemoveAllCache();
            return Json("清除缓存成功", JsonRequestBehavior.AllowGet);
        }

        public List<CategoryModel> GetCategoryListByaCate(string acategoryid)
        {
            var param = new SqlParameter();
            var key = AppConst.Cache_CategoryListByACate;
            var sql = $@"SELECT   bc.BCategoryID,
                                  bc.BCategoryName,
                                  ac.ACategoryID,
                                  ac.ACategoryName
                           FROM   BCategory bc WITH(nolock)
                                  INNER JOIN ACategory ac WITH(nolock)
                                          ON ac.ACategoryID = bc.ACategoryID
                           WHERE  1 = 1 ";
            if (!string.IsNullOrWhiteSpace(acategoryid))
            {
                key += acategoryid;
                param.ParameterName = "@ACategoryID";
                param.Value = acategoryid;
                sql += " AND ac.ACategoryID = @ACategoryID ";
            }

            var cacheResult = (List<CategoryModel>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = bCategoryDal.QueryCustom<CategoryModel>(sql, string.IsNullOrWhiteSpace(param.ParameterName) ? null : param) ?? new List<CategoryModel>();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }


        public List<ACategory> GetAllACategoryInfoList()
        {
            var key = AppConst.Cache_ACategoryListAll;
            var cacheResult = (List<ACategory>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = bCategoryDal.QueryAll<ACategory>();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }

        public BCategory GetBCategoryByID(string bcategoryid = "")
        {
            var key = $"bcategory.{bcategoryid}";
            var cacheResult = (BCategory)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                var sql = $@"select * from XQW_Data..BCategory  as p with(nolock) where p.BCategoryID = @BCategoryID";
                var param = new SqlParameter
                {
                    ParameterName = "@BCategoryID",
                    Value = bcategoryid
                };
                cacheResult = (bCategoryDal.QueryCustom<BCategory>(sql, param) ?? new List<BCategory>()).FirstOrDefault();
                CacheHelper.SetCache(key, cacheResult);
            }
            return cacheResult;
        }
    }
}