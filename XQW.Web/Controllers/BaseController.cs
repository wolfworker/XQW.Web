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
    }
}