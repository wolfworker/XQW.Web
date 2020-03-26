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
    public class CategoryController : Controller
    {
        public BCategoryDal bCategoryDal { get; set; } = new BCategoryDal();
        public ActionResult AIndex()
        {
            return View();
        }

        public ActionResult BIndex(string bcategoryid)
        {
            var bCategoryInfo = GetBCategoryByID(bcategoryid);
            ViewBag.BCategoryId = bcategoryid;
            ViewBag.BCategoryName = bCategoryInfo.BCategoryName;
            var productList = new ProductController().GetProductListBybCate(bcategoryid);
            ViewBag.ProductList = productList;
            return View();
        }

        public JsonResult GetBCategoryList(string acategoryid = "")
        {
            return Json(GetBCategoryListByaCate(acategoryid), JsonRequestBehavior.AllowGet);
        }

        public List<BCategory> GetBCategoryListByaCate(string acategoryid = "")
        {
            var key = $"bcategorylist.acategory.{acategoryid}";
            var cacheResult = (List<BCategory>)CacheHelper.GetCache(key);
            if (cacheResult == null)
            {
                cacheResult = bCategoryDal.QueryAll<BCategory>() ?? new List<BCategory>();
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