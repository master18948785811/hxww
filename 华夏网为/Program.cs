using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 华夏网为
{
    static class Program
    {
      
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;            ////获取版本号             

            //CommonData.VersionNumber = Application.ProductVersion;             //检查进程是否已经启动，已经启动则显示报错信息退出程序。    

            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                MessageBox.Show("客户端已经运行！", "消息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                Application.Exit(); return;
            }
           
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
        /// <summary>
        /// 用MD5加密字符串，可选择生成16位或者32位的加密字符串
        /// </summary>
        /// <param name="password">待加密的字符串</param>
        /// <param name="bit">位数，一般取值16 或 32</param>
        /// <returns>返回的加密后的字符串</returns>
        
    }
}
