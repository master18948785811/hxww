using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.shengwen
{
    public partial class swzc : Form
    {
        public swzc()
        {
            InitializeComponent();
        }
        public struct swzcxx
        {
        
            public string deviceId;
            public string orgCode;
            public string orgName;
            public string orgFullName;
            public string ip;
            public string mac;
            public string deviceVender;


        }
        Appcode.Class2 gj = new Appcode.Class2();
        private void button1_Click(object sender, EventArgs e)
        {
            swzcxx swxx = new swzcxx();
            string sysId = gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            string sysPass= gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            swxx.deviceId=this.deviceId.Text ;
            swxx.orgCode = this.orgCode.Text ;
            swxx.orgName = this.orgName.Text ;
            swxx.orgFullName = this.orgFullName.Text ;
            swxx.ip = this.ip.Text = gj.GetIP();
            swxx.mac = this.mac.Text = gj.GetMAC();
            swxx.deviceVender=this.deviceVender.Text;
            string strSerializeJSON = JsonConvert.SerializeObject(swxx);
            MessageBox.Show(strSerializeJSON);
            //MessageBox.Show(strSerializeJSON);
            //通过webservice发送给服务器
            string result = null;        //接受服务器信息
            result = Appcode.SD_WebSvcCaller.QueryPostWebService2("http://" + quanjubianliang.webserviceip + "/pics/api/external/voice/voiceDeviceReg",strSerializeJSON,sysId,sysPass);
            MessageBox.Show(result);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sysId = gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            string sysPass = gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            XmlDocument sb = new XmlDocument();
            sb.Load(@"sbid.xml");
            gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            MessageBox.Show(Appcode.SD_WebSvcCaller.QueryGetWebService("http://" + quanjubianliang.webserviceip + "/pics/api/external/voice/voiceDeviceReg", sysId, sysPass, sb.SelectSingleNode("root//sbid").InnerText));
        }
        private void swzc_Load(object sender, EventArgs e)
        {

            XmlDocument sb = new XmlDocument();
            sb.Load(@"sbid.xml");
            this.deviceId.Text = sb.SelectSingleNode("root//sbid").InnerText;
            this.orgCode.Text = sb.SelectSingleNode("root//orgCode").InnerText;
            this.orgName.Text = sb.SelectSingleNode("root//orgName").InnerText;
            this.orgFullName.Text = sb.SelectSingleNode("root//orgFullName").InnerText;
            this.ip.Text = gj.GetIP();
            this.mac.Text = gj.GetMAC().Replace(":","-");
            this.deviceVender.Text = sb.SelectSingleNode("root//deviceVender").InnerText;
        }
    }
}
