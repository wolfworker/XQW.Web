using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.DBEntity
{
    [Table("BCategory")]
    public class BCategory : CommonEntity
    {
        public long ID { get; set; }
        public string BCategoryID { get; set; }
        public string BCategoryName { get; set; }
        public string ACategoryID { get; set; }
    }
}
