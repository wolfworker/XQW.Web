using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XQW.Utility
{
    public class LogUtils
    {
        public static void logToTxt(string content)
        {
            Task.Run(() =>
            {
                string strFileName = AppConst.LogTxtFilePath;
                //判断是否存在
                if (File.Exists(strFileName))
                {
                    //存在
                    StreamWriter wlog;
                    wlog = File.AppendText(strFileName);
                    wlog.WriteLine(content);
                    wlog.Flush();
                    wlog.Close();
                }
                else
                {
                    //不存在
                    FileStream fs1 = new FileStream(strFileName, FileMode.Create, FileAccess.Write);//创建写入文件 
                    StreamWriter sw = new StreamWriter(fs1);
                    sw.WriteLine(content);//开始写入值
                    sw.Close();
                    fs1.Close();

                }
            });
        }
    }
}
