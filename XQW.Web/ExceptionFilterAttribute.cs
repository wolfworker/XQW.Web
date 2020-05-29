using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XQW.Web.Controllers;

namespace XQW.Web
{
    public class ExceptionFilterAttribute: HandleErrorAttribute
    {
        private static BaseController baseController = new BaseController();
        public override void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            if (ex != null)
            {
                var errMsg = $"错误信息：{ex.Message}\r\n堆栈信息：{ex.StackTrace}\r\n";
                baseController.WriteErrorLog(errMsg);
            }
            filterContext.ExceptionHandled = true;
        }
    }
}