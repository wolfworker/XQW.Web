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

        public static string ProductImagePath
        {
            get
            {
                var productImagePath = ConfigurationManager.AppSettings["ProductImagePath"]?.ToString();
                if (string.IsNullOrEmpty(productImagePath))
                {
                    throw new Exception("ProductImagePath 未配置，请前往appconfig配置");
                }
                return productImagePath;
            }
            set {; }
        }

        public static string OtherImagePath
        {
            get
            {
                var otherImagePath = ConfigurationManager.AppSettings["OtherImagePath"]?.ToString();
                if (string.IsNullOrEmpty(otherImagePath))
                {
                    throw new Exception("OtherImagePath 未配置，请前往appconfig配置");
                }
                return otherImagePath;
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
            return ProductImagePath + "/header/" + productid + ".jpg";
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
                detailimgList.Add(ProductImagePath + "/detail/" + productid + "/" + i + ".jpg");
            }
            return detailimgList;
        }


        public static string LogTxtFilePath
        {
            get
            {
                var logTxtFilePath = ConfigurationManager.AppSettings["LogTxtFilePath"]?.ToString();
                if (string.IsNullOrEmpty(logTxtFilePath))
                {
                    throw new Exception("LogTxtFilePath 未配置，请前往appconfig配置");
                }
                return logTxtFilePath;
            }
            set {; }
        }
        #endregion

        #region 缓存key及过期时间等
        public static string Cache_ProductInfoByID = "cache_productinfobyid_";
        public static string Cache_ProductInfoListByBCate = "cache_productlistbybcate_";
        public static string Cache_ProductInfoListByACate = "cache_productlistbyacate_";
        public static string Cache_CategoryListByACate = "cache_categorylistbyacate_";
        public static string Cache_ACategoryListAll = "cache_acategoryall";
        public static string Cache_HotProductListAll = "cache_hotproductall";
        public static string Cache_HotProductList = "cache_hotproduct_";
        public static string Cache_AboutUs = "cache_aboutus_";
        public static string Cache_PreNextProduct = "cache_prenextproduct_";
        public static string Cache_OtherBuyProduct = "cache_otherbuyproduct_";

        #endregion
    }
}
