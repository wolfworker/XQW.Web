using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XQW.Utility;

namespace XQW.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //获取到HttpUnhandledException异常，这个异常包含一个实际出现的异常
            Exception ex = Server.GetLastError();

            var errorMsg = "";
            var stackTrace = "";
            if (ex.InnerException != null)
            {
                errorMsg = ex.InnerException.Message;
                stackTrace = ex.InnerException.StackTrace;
            }
            else
            {
                errorMsg = ex.Message;
                stackTrace = ex.StackTrace;
            }
            var baseController = new Controllers.BaseController();
            var msgContent = $"时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\r\n错误信息：{ex.Message}\r\n堆栈信息：{ex.StackTrace}\r\n";
            LogUtils.logToTxt(msgContent);
            Server.ClearError();//处理完及时清理异常
        }
    }
}
