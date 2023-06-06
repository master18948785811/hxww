using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace 华夏网为.zhizhangwen
{
    public partial class zhizhangwen : Form
    {
        public zhizhangwen()
        {
            InitializeComponent();
        }
        Appcode.Class2 gj = new Appcode.Class2();
        //private static int flag = 1;//标记是否正在解析数据（0：解析中 1：解析完成）,默认解析完成
        private string fileName = "";
        int keys = 0;
        //Classid
        MethodInfo mi = null;
        Type classid_type = null;    //获取classid句柄
        object classid_obj = null;   //
        string classid = "5A1A8AE6-7726-4DB3-9BD8-137451C3591B";
       

        private void Fig_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Fig_Load(object sender, EventArgs e)
        {
           
            GetClassId();
            string[] k = {quanjubianliang.rybh, gj.getdburl() };
            string flag = getFunc("initFingerPlamOCX", k);//初始化人像设备
        }
        /// <summary>
        /// 读取配件表获取已启用设备classid
        /// </summary>
        public string ReadClassId()
        {
            string device = "";
            string sql = "";
            sql = "select PARETS_PLUG from LEDEN_EQUIPMENT_PARETS where NODE_CODE='4' and DELETE_FLAG='0'";
            device = gj.getback(sql);
            return device;
        }
        /// <summary>
        /// 获取classid
        /// </summary>
        private void GetClassId()
        {
            //根据classId获取ActiveX类
            classid_type = Type.GetTypeFromCLSID(new Guid(classid));
            //创建类的实例，第二个参数是object数组，就是你的构造方法里面的参数，
            //null即为无参构造方法，也可以这么写：
            // object obj = Activator.CreateInstance(type);
            classid_obj = Activator.CreateInstance(classid_type, null);
            //把ActiveX控件添加到窗体;
            Control con = (Control)classid_obj;
            con.Dock = DockStyle.Fill;
            panel1.Controls.Add(con);
        }
        /// <summary>
        /// 获取classid 里面的方法
        /// </summary>
        /// <param name="FuncName">方法名称</param>
        /// <param name="param">方法参数</param>
        private string getFunc(string FuncName, object param)
        {
            //  获取实例的指定方法，根据方法名，还有其他重载，也可以根据参数找
            mi = classid_type.GetMethod(FuncName);
            // 调用该方法的参数，按顺序
            object[] para = null;
            if (null != param)
            {
                para = new object[] { param };
            }
            // 调用方法，返回值是object，我的方法返回void，所以不写
            return (string)mi.Invoke(classid_obj, para).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
