using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.DBEntity
{
    [Table("Comment")]
    public class Comment : CommonEntity
    {
        public long ID { get; set; }
        public string ProductID { get; set; }
        public string NickName { get; set; }
        public string ContactNo { get; set; }
        public string CommentContent { get; set; }
    }
}
