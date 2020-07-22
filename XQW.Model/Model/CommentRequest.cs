using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.Model
{
    public class CommentRequest
    {
        public string ProductID { get; set; }
        public string NickName { get; set; }
        public string ContactNo { get; set; }
        public string CommentContent { get; set; }
    }
}
