using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.Model
{
    public class ProductModel
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubTtile { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int SeenCount { get; set; }
        /// <summary>
        /// 详情页有几张图片（便于直接生成详情页图片链接）
        /// </summary>
        public int DetailPicCount { get; set; }
        /// <summary>
        /// 主图url
        /// </summary>
        public string HeaderImageUrl { get; set; }

        /// <summary>
        /// 详情图url（分号;分隔）
        /// </summary>
        public List<string> DetailImageUrlList { get; set; }

        public string ACategoryID { get; set; }
        public string BCategoryID { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ACategoryName { get; set; }
        public string BCategoryName { get; set; }
    }
}
