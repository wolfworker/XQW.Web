using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XQW.DataAccess;
using XQW.Model.DBEntity;
using XQW.Model.Model;
using XQW.Utility;

namespace XQW.Web.Controllers
{
    [ExceptionFilter(ExceptionType = typeof(Exception))]
    public class BaseController : Controller
    {
        public static CommonDal commonDal = new CommonDal();
        public static DebugLogDal debugLogDal = new DebugLogDal();


        public JsonResult SaveClickTypeLog(int clickType, string userIp, string userCityName,string bussiesValue = "", string remark = "")
        {
            commonDal.SaveClickTypeLog(clickType, userIp, userCityName, bussiesValue, remark);
            return Json("Success");
        }

        public void WriteInfoLog(string logContent)
        {
            debugLogDal.WriteLog(logContent, EnumModel.LogLevel.Normal);
        }

        public void WriteWarnLog(string logContent)
        {
            debugLogDal.WriteLog(logContent, EnumModel.LogLevel.Warning);
        }

        public void WriteErrorLog(string logContent)
        {
            debugLogDal.WriteLog(logContent, EnumModel.LogLevel.Error);
        }

        /// <summary>
        /// 小青蛙详细信息（包括简介、联系方式、二维码、邮箱等等）
        /// </summary>
        /// <returns></returns>
        public ShopInfoModel GetShopInfo()
        {
            var shopmodel = new ShopInfoModel
            {
                ShopName = "小青蛙商学院",
                Phone = "17601492856",
                QQNumber = "1315915446",
                MailAddress = "1315915446@qq.com",
                WXNumber = "17601492856",
                Slogan = "学知识，上小青蛙商学院！",
                WXImageUrl = @AppConst.OtherImagePath + "/xqw_vip.jpg",
                //Introduction = $@"小青蛙商学院，竭力为您提供全方位的学习资源，靠谱的教学资料，优质的原创课程。学知识，上小青蛙商学院！",
            };
            return shopmodel;
        }
    }
}