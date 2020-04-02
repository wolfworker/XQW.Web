using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.DBEntity
{
    [Table("ACategory")]
    public class ACategory : CommonEntity
    {
        public long ID { get; set; }
        public string ACategoryName { get; set; }
        public string ACategoryID { get; set; }
    }
}
