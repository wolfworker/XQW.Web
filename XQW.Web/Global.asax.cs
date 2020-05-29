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
            //��ȡ��HttpUnhandledException�쳣������쳣����һ��ʵ�ʳ��ֵ��쳣
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
            var msgContent = $"ʱ�䣺{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\r\n������Ϣ��{ex.Message}\r\n��ջ��Ϣ��{ex.StackTrace}\r\n";
            LogUtils.logToTxt(msgContent);
            Server.ClearError();//�����꼰ʱ�����쳣
        }
    }
}
