using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.Model
{
    public class EnumModel
    {
        public enum LogLevel
        {
            [Description("正常")]
            Normal = 0,

            [Description("警告")]
            Warning = 1,

            [Description("错误")]
            Error = 2,
        }

        public enum ClickType
        {
            #region 公共页面 10..
            [Description("公共-主页Logo")]
            Common_Logo = 1001,

            [Description("公共-首页按钮")]
            Common_Home = 1002,

            [Description("公共-A级分类")]
            Common_ACate = 1003,

            [Description("公共-搜索")]
            Common_Search = 1004,

            [Description("公共-关于我们")]
            Common_About = 1005,

            #endregion

            #region 首页 11..

            [Description("首页-关于我们")]
            Home_About = 1101,

            [Description("首页-加入会员")]
            Home_VIP = 1102,

            [Description("首页-热门教程")]
            Home_Product = 1103,

            [Description("首页-教程所属分类")]
            Home_Cate = 1104,

            #endregion

            #region 详情页 12..
            [Description("详情-主教程所属分类")]
            Detail_BCate = 1201,

            [Description("详情-上个教程")]
            Detail_Previous = 1202,

            [Description("详情-下个教程")]
            Detail_Next = 1203,

            [Description("详情-推荐")]
            Detail_Recomment = 1204,

            [Description("详情-微信号")]
            Detail_WeiXin = 1205,

            [Description("详情-热门教程")]
            Detail_Hot = 1206,

            [Description("详情-推荐教程所属分类")]
            Detail_Recomment_BCate = 1207,
            #endregion

            #region 分类页 13..
            [Description("分类-全部分类")]
            Cate_All = 1301,

            [Description("分类-B级分类")]
            Cate_BCate = 1302,

            [Description("分类-教程点击")]
            Cate_Product = 1303,

            [Description("分类-教程所属分类")]
            Cate_ProductCate = 1304,

            #endregion

            #region 关于我们 14..
            [Description("关于-教程点击")]
            About_Product = 1401,

            [Description("关于-加入会员")]
            About_VIP = 1402,
            #endregion
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetEnumDesc(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs == null || objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
    }
}
