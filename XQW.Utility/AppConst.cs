using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Utility
{
    public class AppConst
    {
        #region 商品图片相关
        public static string ProductImgePath
        {
            get
            {
                var picPath = ConfigurationManager.AppSettings["ProductImagePath"]?.ToString();
                if (string.IsNullOrEmpty(picPath))
                {
                    throw new Exception("ProductImagePath 未配置，请前往appconfig配置");
                }
                return picPath;
            }
            set {; }
        }

        /// <summary>
        /// 商品头图url
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static string GetProductHeaderImgUrl(string productid)
        {
            return ProductImgePath + "/header/" + productid + ".jpg";
        }

        /// <summary>
        /// 商品详情url list 
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static List<string> GetProductDetailImgUrl(string productid, int imgcount)
        {
            var detailimgList = new List<string>();
            for (int i = 1; i <= imgcount; i++)
            {
                detailimgList.Add(ProductImgePath + "/detail/" + productid + "/" + i + ".jpg");
            }
            return detailimgList;
        }
        #endregion

        #region 缓存key及过期时间等
        public static string Cache_ProductInfoByID = "cache_productinfobyid_";
        public static string Cache_ProductInfoListByBCate = "cache_productlistbybcate_";
        public static string Cache_ProductInfoListByACate = "cache_productlistbyacate_";
        public static string Cache_CategoryListByACate = "cache_categorylistbyacate_";
        public static string Cache_ACategoryListAll = "cache_acategoryall";

        #endregion
    }
}
