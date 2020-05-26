using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Model.DBEntity
{
    public class SYS_RequestLog : CommonEntity
    {
        public string ID { get; set; }
        public long UserID { get; set; }
        public string LogType { get; set; }
        public string LogTypeName { get; set; }
        public string BussiessValue { get; set; }
        public string Remark { get; set; }
        public string UserIp { get; set; }
        public string UserCityName { get; set; }
    }
}
