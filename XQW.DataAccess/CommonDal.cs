using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XQW.Model;
using XQW.Model.DBEntity;
using XQW.Model.Model;
using static XQW.Model.Model.EnumModel;

namespace XQW.DataAccess
{
    public class CommonDal
    {
        public T Add<T>(T t) where T : class
        {
            using (var context = new DataBaseContext())
            {
                var entity = context.Set<T>().Add(t);
                context.SaveChanges();
                return entity;
            }
        }

        public bool Update<T>(T t) where T : class
        {
            var result = false;
            using (var context = new DataBaseContext())
            {
                var entity = context.Entry(t);
                entity.State = EntityState.Modified;
                var rows = context.SaveChanges();
                result = rows > 0;
            }
            return result;
        }

        public bool Delete<T>(int id) where T : class
        {
            var result = false;
            using (var context = new DataBaseContext())
            {
                var entity = context.Set<T>().Find(id);
                context.Set<T>().Attach(entity);
                context.Set<T>().Remove(entity);
                var rows = context.SaveChanges();
                result = rows > 0;
            }
            return result;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> QueryAll<T>() where T : class
        {
            var result = new List<T>();
            using (var context = new DataBaseContext())
            {
                var entityAll = context.Set<T>();
                if (entityAll != null)
                {
                    result = entityAll.ToList();
                }
                return result;
            }
        }

        /// <summary>
        /// 根据主键查数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query<T>(int id) where T : class
        {
            using (var context = new DataBaseContext())
            {
                return context.Set<T>().Find(id);
            }
        }

        /// <summary>
        /// 自定义SQL，查询泛型数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public List<T> QueryCustom<T>(string sql, params object[] param) where T : class
        {
            var result = new List<T>();
            using (var context = new DataBaseContext())
            {
                var response = context.Database.SqlQuery<T>(sql, param);
                result = response.ToList();
            }
            return result;
        }

        /// <summary>
        /// 直接执行SQL的写操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExcuteSql(string sql, params object[] param)
        {
            using (var context = new DataBaseContext())
            {
                return context.Database.ExecuteSqlCommand(sql, param);
            }
        }

        public void WriteLog(string logContent, LogLevel levelEnum = LogLevel.Normal)
        {
            try
            {
                var debugLog = new SYS_DebugLog
                {
                    ID = Guid.NewGuid().ToString().Replace("-", ""),
                    LogLevel = (byte)levelEnum,
                    LogContent = logContent
                };
                Add(debugLog);
            }
            catch (Exception ex)
            {
                //log error
            }
        }

        /// <summary>
        /// 写入系统请求日志（用户操作记录）
        /// </summary>
        /// <param name="logContent"></param>
        /// <param name="level">日志等级   1：错误  2：警告  3：正常</param>
        public void SaveClickTypeLog(int clickType, string userIp, string userCityName, string bussiesValue , string remark )
        {
            Task.Run(() =>
            {
                try
                {
                    var requestLog = new SYS_RequestLog
                    {
                        ID = Guid.NewGuid().ToString().Replace("-",""),
                        UserID = 0,
                        LogType = clickType.ToString(),
                        LogTypeName = GetEnumDesc((ClickType)clickType),
                        BussiessValue = bussiesValue,
                        CreatedUser = "sysadmin",
                        UpdateUser = "sysadmin",
                        CreatedTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Remark = remark,
                        UserIp = userIp,
                        UserCityName = userCityName,
                    };
                    Add(requestLog);
                }
                catch (Exception ex)
                {
                    //log error
                    WriteLog($"WriteRequestLog失败：message：{ex.Message}，stack：{ex.StackTrace}", LogLevel.Error);
                }
            });
        }
    }
}
