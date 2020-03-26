using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.DBEntity
{
    [Table("Product")]
    public class Product : CommonEntity
    {
        public long ID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSubTtile { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int SeenCount { get; set; }
        public string BCategoryID { get; set; }
        public bool Status { get; set; }
    }
}
