using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.shoujicaiji
{
    public partial class shoujicaiji : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        KNMFLauncherXLib.RunAdapter jn = new KNMFLauncherXLib.RunAdapter();
        string rycjbh = "";
        public shoujicaiji()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(SJHM.Text.Length>0)
            {  jn.phoneDeviceInit(SetXML());}
            else
            { MessageBox.Show("手机号码不能为空"); }
           

           
           
        }
        //打包传入数据
        private string SetXML()
        {
            //获取配置文件手机BCP包路径
            string BcpPath =  Application.StartupPath + "\\mobile";
            string xml = "";
            string sql = "select * from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
            xml += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<root>";
            xml += "<PERSON>";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                xml += "<RYJCXXCJBH>" + rs["rycjbh"].ToString() + "</RYJCXXCJBH>";
                xml += "<CJXXYYDM></CJXXYYDM>";
                xml += "<XM>" + rs["xm"].ToString() + "</XM>";
                xml += "<GMSFHM>" + rs["shenfengzshengzhaopian"].ToString() + "</GMSFHM>";
                xml += "<SJHM>" + SJHM.Text + "</SJHM>";
                xml += "<MZDM>" + rs["mzdm"].ToString() + "</MZDM>";
                xml += "<RYLBDM>" + rs["renyuanleibie"].ToString() + "</RYLBDM>";
                xml += "<AJLBDM></AJLBDM>";
                xml += "<CJRXM>" + gj.getback("select PersonalName from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</CJRXM>";
                xml += "<CJRJH>" + gj.getback("select PoliceNumber from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</CJRJH>";
                xml += "<CJRGMSFHM>" + gj.getback("select IDcardnumber from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</CJRGMSFHM>";
                xml += "<CJDWDM>" + gj.getback("select Address from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</CJDWDM>";
                xml += "<CJDWMC>" + gj.getback("select UnitName from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</CJDWMC>";
                xml += "<FILELOCATION>" + BcpPath + "</FILELOCATION>";
                xml += "</PERSON>";
            }
          
            xml += "</root>";

            return xml;
        }

        private void shoujicaiji_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
