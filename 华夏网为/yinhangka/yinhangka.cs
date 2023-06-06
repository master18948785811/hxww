using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.yinhangka
{
    public partial class yinhangka : Form
    {
        public yinhangka()
        {
            InitializeComponent();
        }
        //Classid
        MethodInfo mi = null;
        Type classid_type = null;    //获取classid句柄
        object classid_obj = null;   //
        string classid = "24E2CB24-9256-461B-A39D-13CA71545348";
        Appcode.Class2 gj = new Appcode.Class2();
        public struct BANKCARD
        {
            public string YHK_WPBZH;    //银行卡编号
            public string XM;           //姓名
            public string YHK_WPMC;     //银行卡名称
            public string YHKLXDM;      //银行卡类型代码
            public string ZJ_YXQJZRQ;   //证件有效期截止日期
            public string KHYHMC;       //开户行
            public string CYZJLXDM;     //证件类型代码
            public string BLXX_JYQK;    //保留信息
            public string KHHHBH;       //开户行号
            public string KHRQ;         //开户日期
            public string CXRQ;         //销户日期
            public string s_bill;
        }
        BANKCARD s_bankcard = new BANKCARD();
        public struct BILL
        {
            public string YHKKH;            //银行卡号
            public string JYRQ;             //交易日期
            public string JYSJ;             //交易时间
            public string SQJE;             //授权金额
            public string QTJE;             //其它金额
            public string JYDD_XZQHDM;      //交易地点行政区划
            public string JYHBDM;           //交易货币代码
            public string SH_DWMC;          //商户名称
            public string JYLXDM;           //交易类型代码
            public string YYJYJSQ;          //应用交易计数器

        }
        BILL[] s_bill = new BILL[10];
     

        private void bankcard_Load(object sender, EventArgs e)
        {

            classid = "13c9cead-2c0a-4e82-bb39-10b9d403cd17";
            GetClassId();
            string[] k = { quanjubianliang.rybh, gj.getdburl() };
            string flag = getFunc("initBankCardOCX", k);
            XmlDocument cshfh = new XmlDocument();
            cshfh.LoadXml(flag);
         
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
            return mi.Invoke(classid_obj, para).ToString();
        }

        //解析银行卡
        private int GetXML(string xml)
        {
            //gj.Writefile1(@"bank.xml", xml);
            int count = 0;
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(xml);//加载xml

            XmlNode FirstNode = xx.SelectSingleNode("root");
            XmlNode Node1 = FirstNode.FirstChild;

            XmlElement xe = null;
            foreach (XmlNode xxNode in Node1)
            {
                xe = (XmlElement)xxNode;
                if (xe.Name == "flag")
                    Console.Write(xxNode.InnerText);
                else if (xe.Name == "message")
                    Console.Write(xxNode.InnerText);

                if ("ERROR" == xxNode.InnerText || "EXCEPTION" == xxNode.InnerText)
                {
                    return 0;
                }
            }
            //银行卡
            XmlNode Node2 = FirstNode.FirstChild.NextSibling;
            foreach (XmlNode xxNode in Node2)
            {
                xe = (XmlElement)xxNode;
                switch (xe.Name)
                {
                    case "YHK_WPBZH":
                        s_bankcard.YHK_WPBZH = xxNode.InnerText;
                        break;
                    case "XM":
                        s_bankcard.XM = xxNode.InnerText;
                        break;
                    case "YHK_WPMC":
                        s_bankcard.YHK_WPMC = xxNode.InnerText;
                        break;
                    case "YHKLXDM":
                        s_bankcard.YHKLXDM = xxNode.InnerText;
                        break;
                    case "ZJ_YXQJZRQ":
                        s_bankcard.ZJ_YXQJZRQ = xxNode.InnerText;
                        break;
                    case "KHYHMC":
                        s_bankcard.KHYHMC = xxNode.InnerText;
                        break;
                    case "CYZJLXDM":
                        s_bankcard.CYZJLXDM = xxNode.InnerText;
                        break;
                    case "BLXX_JYQK":
                        s_bankcard.BLXX_JYQK = xxNode.InnerText;
                        break;
                    case "KHHHBH":
                        s_bankcard.KHHHBH = xxNode.InnerText;
                        break;
                    case "KHRQ":
                        s_bankcard.KHRQ = xxNode.InnerText;
                        break;
                    case "CXRQ":
                        s_bankcard.CXRQ = xxNode.InnerText;
                        break;
                    case "JYJL":
                        //账单
                        XmlNode Node3 = xxNode.FirstChild.NextSibling;
                        for (int i = 0; i < 10; i++)
                        {
                            if (null == Node3)
                                break;
                            foreach (XmlNode xxNode1 in Node3)
                            {
                                xe = (XmlElement)xxNode1;
                                switch (xe.Name)
                                {
                                    case "YHKKH":
                                        s_bill[i].YHKKH = xxNode1.InnerText;
                                        count++;
                                        break;
                                    case "JYRQ":
                                        s_bill[i].JYRQ = xxNode1.InnerText;
                                        break;
                                    case "JYSJ":
                                        s_bill[i].JYSJ = xxNode1.InnerText;
                                        break;
                                    case "SQJE":
                                        s_bill[i].SQJE = xxNode1.InnerText;
                                        break;
                                    case "QTJE":
                                        s_bill[i].QTJE = xxNode1.InnerText;
                                        break;
                                    case "JYDD_XZQHDM":
                                        s_bill[i].JYDD_XZQHDM = xxNode1.InnerText;
                                        break;
                                    case "JYHBDM":
                                        s_bill[i].JYHBDM = xxNode1.InnerText;
                                        break;
                                    case "SH_DWMC":
                                        s_bill[i].SH_DWMC = xxNode1.InnerText;
                                        break;
                                    case "JYLXDM":
                                        s_bill[i].JYLXDM = xxNode1.InnerText;
                                        break;
                                    case "YYJYJSQ":
                                        s_bill[i].YYJYJSQ = xxNode1.InnerText;
                                        break;
                                }
                            }
                            Node3 = Node3.NextSibling;
                        }
                        break;
                }
            }

            return count;
        }

       

      

    }
}
