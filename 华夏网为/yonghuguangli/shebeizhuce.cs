using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.yonghuguangli
{
    public partial class shebeizhuce : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public shebeizhuce()
        {
            InitializeComponent();
        }

        private void getMAC_Click(object sender, EventArgs e)
        {

        }

        private void getIP_Click(object sender, EventArgs e)
        {

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void shebeizhuce_Load(object sender, EventArgs e)
        {
            string sql = "select * from zd_danweidaima order by code";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                this.HouseholdDivision.Items.Add(rs[2].ToString());
                this.HouseholdDivision.AutoCompleteCustomSource.Add(rs[2].ToString());
            }
            this.MAC.Text = gj.GetMAC();
            this.IP.Text = gj.GetIP();
            this.SBID.Text = quanjubianliang.ythsbid;
        }
        public struct zcgetback
        {

            public string flag { get; set; }
            public string msg { get; set; }
            public getdata gdata;
        }
        public struct getdata
        {
            public string pointCode { get; set; }
            public string secrecy { get; set; }
            public string sysId { get; set; }
            public string pass { get; set; }

        }
        private void Register_Click(object sender, EventArgs e)
        {
            if (quanjubianliang.djms == 0)
            {
                string str = "select count(*) from shebaizhuce where Address='" + gj.GetIP() + "'  and mac='" + gj.GetMAC() + "'";
                if (Convert.ToInt32(gj.getback(str)) == 0)
                {
                    string userName = "huaxiawangwei";
                    string passWord = "huaxiawangwei2020";
                    string sql = "insert into shebaizhuce(IP,MAC,Address,userName,passWord,state)values('"
                      + IP.Text + "','"                   //IP
                      + MAC.Text + "','"                  //MAC
                      + gj.getback("select code from zd_danweidaima where name='" + HouseholdDivision.Text + "'") + "','"    //单位代码
                      + userName + "','"                  //用户名
                      + passWord + "','"                  //密码
                      + "1" + "')";


                    //上传注册信息,并获取采集点编号存入数据库
                    string cjdNo = DataToJSON();

                    if (null != cjdNo)
                    {
                        gj.sqliteexcu(sql);
                        string sqlNo = "update shebaizhuce set cjdNo='" + cjdNo + "' where IP='" + IP.Text + "' and MAC='" + MAC.Text + "'";
                        gj.sqliteexcu(sqlNo);

                    }
                    this.createfiles(cjdNo);
                    this.Close();
                }
                else
                {

                    MessageBox.Show("设备已注册，检测ip是否更改！");
                }
            }
            else
            {


            
                string str = "select count(*) from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "' ";
                if (Convert.ToInt32(gj.getback(str)) == 0)
                {
                    newsb nb = new newsb();
                    nb.fpDeviceId = quanjubianliang.ythsbid;
                    string orgCode= gj.getback("select code from zd_danweidaima where name='"+HouseholdDivision.Text+"'");
                    nb.orgCode = orgCode;
                    nb.deviceVenderCode = "HXWW";
                    string placeType = "";
                    switch(this.comboBox1.Text)
                    {
                        case "标采室":
                            placeType = "11";
                            break;
                        case "审讯室":
                            placeType = "12";
                            break;
                        case "办案中心":
                            placeType = "13";
                            break;
                        case "治安岗亭":
                            placeType = "21";
                            break;
                        case "卡口":
                            placeType = "22";
                            break;
                        case "移动警务":
                            placeType = "23";
                            break;
                        case "其他场地":
                            placeType = "99";
                            break;

                    }
                    nb.pointIp = IP.Text.Trim();
                    nb.pointMac = MAC.Text.Trim();
                    nb.placeType = placeType;
                    nb.adminName = this.fzrxm.Text.ToString();
                    nb.adminPhone = this.fzrdhhm.Text.ToString();
                    nb.adminPoliceid = this.fzrjh.Text.ToString();
                    nb.adminIdcard = this.fzrsfzh.Text.ToString();
                   string  strSerializeJSON = JsonConvert.SerializeObject(nb);
                    //MessageBox.Show(strSerializeJSON);
                    //MessageBox.Show(strSerializeJSON);
                    //通过webservice发送给服务器
                    string result = null;        //接受服务器信息
                    result = Appcode.SD_WebSvcCaller.QueryPostWebService("http://"+quanjubianliang.webserviceip+"/pics/api/data/registPoint", strSerializeJSON);
                   
                    if (null != result)
                    {
                        JObject jb = JObject.Parse(result);
                        string flag = (string)jb["flag"];
                        string msg = (string)jb["msg"]; //采集点编号
                   
                        if ("1" == flag)
                        {
                           
                       string sql = "insert into shebaizhuce(IP,MAC,Address,userName,passWord,sbid,sysid,cjdNo,state)values('"
                      + IP.Text + "','"                   //IP
                      + MAC.Text + "','"                  //MAC
                      + nb.orgCode + "','"    //单位代码
                      + ((JObject)jb["data"])["secrecy"].ToString() + "','"                  //用户名
                      + ((JObject)jb["data"])["pass"].ToString() + "','"
                       + this.SBID.Text + "','"
                        + ((JObject)jb["data"])["sysId"].ToString() + "','"
                         + ((JObject)jb["data"])["pointCode"].ToString() + "','" //密码
                      + "1" + "')";
                         
                            gj.sqliteexcu("delete from shebaizhuce");
                            gj.sqliteexcu(sql);
                            MessageBox.Show("注册成功");

                        }
                        else
                        {
                            MessageBox.Show(flag + ":" + msg);
                        }
                    }
                    this.Close();
                }
                else
                {

                    MessageBox.Show("设备已注册，检测设备编号是否更改！");
                }

            }
        }
        private string DataToJSON()
        {
            string value = null;
            string status = null;
            string deviceAuthorize = null;
            try
            {
                Redata data = new Redata();
                data.unitCode = gj.getback("select code from zd_danweidaima where name='" + HouseholdDivision.Text + "'");
                data.ip = IP.Text;
                data.mac = MAC.Text;
                string strSerializeJSON = JsonConvert.SerializeObject(data);
                strSerializeJSON = "unitCode=" + data.unitCode + "&" + "ip=" + data.ip + "&" + "mac=" + data.mac;
                MessageBox.Show(strSerializeJSON);
                //通过webservice发送给服务器
                string result = null;        //接受服务器信息
                result = Appcode.SD_WebSvcCaller.QueryPostWebService("http://10.48.21.54:58888/htmisWebService/register/postRegisterClient", strSerializeJSON);
               
                if (null != result)
                {
                    JObject jb = JObject.Parse(result);
                    status = (string)jb["status"];
                    value = (string)jb["value"]; //采集点编号
                    deviceAuthorize = (string)jb["deviceAuthorize"];
                    if ("1" == status)
                    {
                        MessageBox.Show(deviceAuthorize);
                        return value;
                    }
                    else if ("error" == status)
                    {
                        MessageBox.Show("服务器异常，请联系管理员");
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(status + ":" + value);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        

            return value;
        }
        //在FTP上建以采集点为名称的文件夹
        private void createfiles(string creatfilenames)
        {

            XmlDocument fhxml = new XmlDocument();
            fhxml.Load(@"ftp.xml");
            string hostname = fhxml.SelectSingleNode("//fpt//hostname").InnerText;
            //FileInfo fileinfo = new FileInfo(gj.getdburl1() + "ZIP//" + helpuser.userid + ".zip");//zip路径
            string targetDir = fhxml.SelectSingleNode("//fpt//targetDir").InnerText;
            string username = fhxml.SelectSingleNode("//fpt//username").InnerText;
            string password = fhxml.SelectSingleNode("//fpt//password").InnerText;
            string ports = fhxml.SelectSingleNode("//fpt//ports").InnerText;
            HXWW_FTP.ftpcreat ftpc = new HXWW_FTP.ftpcreat();
            HXWW_FTP.ftpcountect ftpcon = new HXWW_FTP.ftpcountect();
            string outmessage;
            if (ftpcon.CheckFtp(hostname.Split(':')[0].ToString(), username, password, out outmessage, Convert.ToInt32(ports)))
            {
                string[] filenames = ftpc.GetDirectoryList(ftpc.GetFilesDetailList("", hostname, "", username, password));
                for (int i = 0; i < filenames.Length; i++)
                {
                    if (creatfilenames == filenames[i].ToString())
                    {

                        return;
                    }

                }
                ftpc.MakeDir(creatfilenames, hostname, "", username, password);
            }
            else
            {

                MessageBox.Show("ftp服务器有问题，请联系管理员");
            }


        }
    }
}
