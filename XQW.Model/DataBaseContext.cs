using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XQW.Model.DBEntity;

namespace XQW.Model
{
    /// <summary>
    /// 数据上下文
    /// </summary>
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DataBaseContext")
        {

        }
        #region 数据库 表实体

        /// <summary>
        /// A类
        /// </summary>
        public DbSet<ACategory> ACategory { get; set; }

        /// <summary>
        /// B类
        /// </summary>
        public DbSet<BCategory> BCategory { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public DbSet<Product> Product { get; set; }

        /// <summary>
        /// 评论留言
        /// </summary>
        public DbSet<Comment> Comment { get; set; }

        /// <summary>
        /// 访问业务日志
        /// </summary>
        public DbSet<SYS_RequestLog> RequestLog { get; set; }

        /// <summary>
        /// 错误日志
        /// </summary>
        public DbSet<SYS_DebugLog> DebugLog { get; set; }

        #endregion
    }
}
