using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.xinxizonglan
{
    public partial class xinxizonglan : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        int sfscwc = 0;//0完成
        public xinxizonglan()
        {
            InitializeComponent();
        }
        private void xinxizonglan_Load(object sender, EventArgs e)
        {
            try
            {

                //if (Convert.ToUInt32(gj.getback("select shifoushangchuan from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "' ")) == 1)
                //{
                //    this.Upload.Visible = false;

                //}
                Complete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        //检测是否有采集完成
        private void Complete()
        {

            string sql = "select *  from daohang  where shifuoxianshi=1";
            System.Data.DataTable db1 = gj.gettable(sql);
            foreach (DataRow rs in db1.Rows)
            {

                if (rs["mingcheng"].ToString() == "jibenxinxi")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "基本信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("rycjbh", quanjubianliang.rybh, "renyuanjibenxinxi") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);


                }
                if (rs["mingcheng"].ToString() == "renxiang")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "人像信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("rycjbh", quanjubianliang.rybh, "sanmianzhao") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);

                }
                if (rs["mingcheng"].ToString() == "wuzheng")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "物证信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("rycjbh", quanjubianliang.rybh, "wuzheng") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);
                }

                if (rs["mingcheng"].ToString() == "zhiwen")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "指纹信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("RYJCXXCJBH", quanjubianliang.rybh, "LEDEN_COLLECT_FINGER") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);
                }
                if (rs["mingcheng"].ToString() == "timao")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "体表特征信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("rycjbh", quanjubianliang.rybh, "teshutezheng") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);




                }
                if (rs["mingcheng"].ToString() == "DNA")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "DNA信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("rycjbh", quanjubianliang.rybh, "DNA") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);



                }

                if (rs["mingcheng"].ToString() == "hongmo")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "虹膜信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("RYJCXXCJBH ", quanjubianliang.rybh, "hongmu") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);



                }
                if (rs["mingcheng"].ToString() == "zuji")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "足迹信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("RYJCXXCJBH ", quanjubianliang.rybh, "zuji") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);



                }
                if (rs["mingcheng"].ToString() == "shengwen")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "声纹信息";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (gj.getbool("RYJCXXCJBH ", quanjubianliang.rybh, "LEDEN_COLLECT_VOICEPRINT") > 0)
                    {
                        cb.Checked = true;
                    }

                    this.flowLayoutPanel1.Controls.Add(cb);



                }
                if (rs["mingcheng"].ToString().Trim() == "shouji")
                {
                    System.Windows.Forms.CheckBox cb = new System.Windows.Forms.CheckBox();
                    cb.Text = "手机采集";
                    cb.RightToLeft = RightToLeft.No;
                    cb.Font = new System.Drawing.Font("微软雅黑", 24);
                    cb.Size = new Size(350, 100);
                    if (File.Exists(Application.StartupPath + "\\mobile" + quanjubianliang.rybh + ".bcp"))
                    {
                       
                        cb.Checked = true;
                       

                    }
                    this.flowLayoutPanel1.Controls.Add(cb);
                }



            }


        }
        private void Upload_Click(object sender, EventArgs e)
        {
            try
            {

                if (quanjubianliang.cjrbh == gj.getback("select caijirendaima from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "' "))
                {
                    this.Upload.Enabled = false;
                    UpLoadData();//打包并上传至FTP
                    this.Upload.Enabled = true;
                    if (sfscwc == 0)
                    {
                        MessageBox.Show("上传完成");
                    }
                }
                else
                {

                    MessageBox.Show("采集人和登录人不一致,不能上传");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("请检查网络\r\n" + ex.ToString());
                throw;
            }
        }
        //打包并上传服务器
        private void UpLoadData()
        {
            try
            {
                if (quanjubianliang.djms == 0)
                {
                    weitefile_Fig();
                    PackXmlData();//生产XML
                                  //打包zip
                    if (Appcode.zips.ZipDirectory(@"ZIP//" + quanjubianliang.rybh, @"ZIP//" + quanjubianliang.rybh + ".zip", null))
                    {
                        //MessageBox.Show("打包完成");
                    }
                    Redata data = new Redata();
                    data.cjdNo = gj.getback("select cjdNo from shebaizhuce where IP='" + gj.GetIP() + "'and MAC='" + gj.GetMAC() + "'");
                    data.dir = "/hxww/" + data.cjdNo + "/" + quanjubianliang.rybh + ".zip";
                    data.userName = "huaxiawangwei";
                    data.passWord = "huaxiawangwei2020";
                    data.rybh = quanjubianliang.rybh;
                    //上传至FTP
                    XmlDocument fhxml = new XmlDocument();
                    fhxml.Load(@"ftp.xml");
                    string hostname = fhxml.SelectSingleNode("//fpt//hostname").InnerText;
                    FileInfo fileinfo = new FileInfo(gj.getdburl1() + "ZIP//" + quanjubianliang.rybh + ".zip");//zip路径
                    string targetDir = data.cjdNo;
                    string username = fhxml.SelectSingleNode("//fpt//username").InnerText;
                    string password = fhxml.SelectSingleNode("//fpt//password").InnerText;
                    string ports = fhxml.SelectSingleNode("//fpt//ports").InnerText;
                    HXWW_FTP.ftpcountect FTP1 = new HXWW_FTP.ftpcountect();
                    string outmessage;
                    if (FTP1.CheckFtp(hostname.Split(':')[0].ToString(), username, password, out outmessage, Convert.ToInt32(ports)))
                    {
                        HXWW_FTP.ftpcreat FFFF = new HXWW_FTP.ftpcreat();
                        FFFF.UploadFile(fileinfo, targetDir, hostname, username, password);
                    }
                    else
                    {
                        MessageBox.Show(outmessage);
                        sfscwc = 1;
                        return;
                    }
                    //数据上报
                    string strSerializeJSON = "userName=" + data.userName + "&" + "passWord=" + data.passWord + "&" + "rybh=" + data.rybh + "&" + "cjdNo=" + data.cjdNo + "&" + "dir=" + data.dir;
                    string result = null;        //接受服务器信息
                    result = Appcode.SD_WebSvcCaller.QueryPostWebService("http://10.48.21.54:58888/htmisWebService/comparison/postUploadData", strSerializeJSON);
                    JObject jb = JObject.Parse(result);
                    string status = (string)jb["status"];
                    string value = (string)jb["value"];
                    if ("-1" == status || "-2" == status || "-3" == status || "-4" == status || "-5" == status || "-6" == status)
                    {
                        MessageBox.Show(value);
                        sfscwc = 1;
                        return;
                    }
                    else if ("error" == status)
                    {
                        MessageBox.Show("服务器异常，请联系管理员");
                        sfscwc = 1;
                        return;
                    }
                    else
                    {
                        //设置数据库上传状态
                        string sql = "update renyuanjibenxinxi set shifoushangchuan ='1' where rycjbh='" + quanjubianliang.rybh + "'";
                        gj.sqliteexcu(sql);
                    }
                }
                else
                {

                    if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh))
                    {
                        System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh);
                    }
                    writeuploadfile();
                    PackJsonData();
                    string yysdwj = System.Windows.Forms.Application.StartupPath.ToString() + "\\upload\\" + quanjubianliang.rybh;

                    string yshdwj = System.Windows.Forms.Application.StartupPath.ToString() + "\\upload\\" + quanjubianliang.rybh + ".zip";
                    if (System.IO.File.Exists(yshdwj))
                    {
                        File.Delete(yshdwj);
                    }

                    if (Appcode.ZipHelper.ZipDirectory(yysdwj, yshdwj, ""))
                    {
                        //MessageBox.Show("打包完成");
                    }
                    string sysid = gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
                    string password = gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
                    string result = Appcode.SD_WebSvcCaller.QueryPostWebService1("http://"+quanjubianliang.webserviceip +"/pics/api/data/importData", gj.SaveImage(System.Windows.Forms.Application.StartupPath.ToString() + "\\upload\\" + quanjubianliang.rybh + ".zip"), sysid, password);
                    JObject jb = JObject.Parse(result);
                    string flag = (string)jb["flag"];
                    string msg = (string)jb["msg"];
                    if (flag!="1")
                    {
                        MessageBox.Show(result);
                        sfscwc = 1;
                        return;
                    }
                   
                    else
                    {
                        //设置数据库上传状态
                        string sql = "update renyuanjibenxinxi set shifoushangchuan ='1' where rycjbh='" + quanjubianliang.rybh + "'";
                        gj.sqliteexcu(sql);
                    }
                }

            }
            catch (System.Exception ex)
            {
                sfscwc = 1;
                MessageBox.Show(ex.ToString());
            }

        }
        //打包xml
        private void PackXmlData()
        {
            try
            {
                string alldata = "";
                alldata = alldata + "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>";
                alldata = alldata + "<MIS>";
                PackBasicInfo(ref alldata);         //基础信息
                PackGoodsInfo(ref alldata);         //物证信息
                PackBodySignInfo(ref alldata);      //体征信息
                PackIrisInfo(ref alldata);
                PackVoiceInfo(ref alldata);

                alldata = alldata + "</MIS>";
                string fileName = @"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + ".xml";
                if (System.IO.File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
                {
                    file.Write(alldata);
                    file.Close();
                }
            }
            catch (System.Exception ex)
            {
                sfscwc = 1;
                MessageBox.Show(ex.ToString());
            }


        }
        //打包基础信息xml
        private void PackBasicInfo(ref string alldata)
        {
            alldata = alldata + "<PersonInfo>";//*
            alldata = alldata + "<PersonID>" + quanjubianliang.rybh + "</PersonID>";//*人员编号
            alldata = alldata + "<Card_N>" + quanjubianliang.rybh.ToString().Substring(1, quanjubianliang.rybh.Length - 1) + "</Card_N>";//*指纹编号
            alldata = alldata + "<DNA_N>" + gj.getback("select DNANumber from DNA where rycjbh='" + quanjubianliang.rybh + "'") + "</DNA_N>";//*DNA实验室编号
            string sql = "select * from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
            string sqlX = "select * from sanmianzhao where rycjbh='" + quanjubianliang.rybh + "'";
            string sqlXX = "select * from xingti where rycjbh='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                alldata = alldata + "<PersonName>" + rs["xm"].ToString() + "</PersonName>";//*人员姓名
                alldata = alldata + "<PersonAlias></PersonAlias>";//别名
                alldata = alldata + "<Birthday>" + rs["csrq"].ToString().Replace("-", "") + "</Birthday>";//出生日期
                alldata = alldata + "<SexCode>" + rs["xbdm"].ToString() + "</SexCode>";//*性别代码
                foreach (DataRow rsX in gj.gettable(sqlXX).Rows)
                {
                    alldata = alldata + "<Height>" + rsX["shengao"].ToString() + ".00" + "</Height>";//*身高
                    alldata = alldata + "<Weight>" + rsX["tizhong"].ToString() + ".00" + "</Weight>";//*体重
                    alldata = alldata + "<FootLength>" + rsX["zuchang"].ToString() + ".00" + "</FootLength>";//*足长
                }
                alldata = alldata + "<CountryCode>" + rs["gjdm"].ToString() + "</CountryCode>";//国籍代码
                alldata = alldata + "<Education></Education>";//文化程度代码
                alldata = alldata + "<Political></Political>";//政治面貌代码
                alldata = alldata + "<PlaceOfOrigin>" + rs["hjdz_xzqhdm"].ToString() + "</PlaceOfOrigin>";//籍贯代码
                alldata = alldata + "<Profession>" + rs["zhiyeleibie"].ToString() + "</Profession>";//*职业代码
                alldata = alldata + "<Workplace>" + rs["fwcs"].ToString() + "</Workplace>";//*工作单位
                alldata = alldata + "<Mobile1>" + rs["yddh"].ToString() + "</Mobile1>";//*手机号码一
                alldata = alldata + "<Mobile2>" + "" + "</Mobile2>";//手机号码二
                alldata = alldata + "<Mobile3>" + "" + "</Mobile3>";//手机号码三
                alldata = alldata + "<Telephone>" + rs["gddh"] + "</Telephone>";//座机号码
                alldata = alldata + " <SimmessageFlag>0</SimmessageFlag>";
                alldata = alldata + "<NationCode>" + rs["mzdm"].ToString() + "</NationCode>";//民族代码
                alldata = alldata + "<IdentityCardNum>" + rs["gmsfzhm"].ToString() + "</IdentityCardNum>";//*居民身份证号
                alldata = alldata + "<CertifyTypeCode>" + rs["cyzjdm"].ToString() + "</CertifyTypeCode>";//其它证件类别代码
                alldata = alldata + "<CertifyNum>" + rs["zjhm"].ToString() + "</CertifyNum>";//其它证件号码
                alldata = alldata + "<RegAdminDivCode>" + rs["hjdz_xzqhdm"].ToString() + "</RegAdminDivCode>";//*户籍地行政区划代码
                alldata = alldata + "<RegAdminDivName>" + gj.getback("select mingcheng from zd_hujidixingzhengquhua where bianhao='" + rs["hjdz_xzqhdm"].ToString() + "'") + "</RegAdminDivName>";//*户籍地行政区划
                alldata = alldata + "<RegAddress>" + rs["hjdz_dzmc"].ToString() + "</RegAddress>";//*户籍地详址
                alldata = alldata + "<DwellAdminDivCode>" + rs["xzz_xzqhdm"].ToString() + "</DwellAdminDivCode>";//*现住地行政区划代码
                alldata = alldata + "<DwellAdminDivName>" + gj.getback("select mingcheng from zd_hujidixingzhengquhua where bianhao='" + rs["xzz_xzqhdm"].ToString() + "'") + "</DwellAdminDivName>";//*现住地行政区划
                alldata = alldata + "<DwellAddress>" + rs["xzz_dzmc"].ToString() + "</DwellAddress>";//*现住地详址
                alldata = alldata + "<PersonTypeCode>" + rs["renyuanleibie"].ToString() + "</PersonTypeCode>";//*人员类别代码
                alldata = alldata + "<CaseType1Code></CaseType1Code>";//案件类别代码1
                alldata = alldata + "<CaseType2Code>" + "" + "</CaseType2Code>";//案件类别代码2
                alldata = alldata + "<CaseType3Code>" + "" + "</CaseType3Code>";//案件类别代码3
                alldata = alldata + "<CrimeRecordFlag>" + "" + "</CrimeRecordFlag>";//前科标识代码0：不属于前科库人员，1：属于前科库人
                alldata = alldata + "<CrimeRecordFlagName>" + "" + "</CrimeRecordFlagName>";//前科标识
                alldata = alldata + "<Reason>" + gj.getback("select mingcheng from zd_caijiyuanyin where bianhao='" + rs["caijiyuanyin"].ToString() + "'") + "</Reason>";//*被采集原因


                alldata = alldata + "<ScanPersonCode>" + gj.getback("select  PoliceNumber  from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanPersonCode>";//*警号必填
                alldata = alldata + "<ScanLXDH>" + gj.getback("select  Telnumber  from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanLXDH>";//*采集民警的电话号码必填


                alldata = alldata + "<ScanUnitCode>" + gj.getback("select  Address  from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanUnitCode>";//*采集单位代码
                alldata = alldata + "<ScanUnitName>" + gj.getback("select  UnitName   from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanUnitName>";//采集单位名称
                alldata = alldata + "<PlaceUnitCode>" + gj.getback("select cjdNo from shebaizhuce where IP='" + gj.GetIP() + "' and MAC='" + gj.GetMAC() + "'") + "</PlaceUnitCode>";//*采集地点代码
                alldata = alldata + "<ScanPersonName>" + gj.getback("select  PersonalName   from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanPersonName>";//*采集民警    
                alldata = alldata + "<ScanCard>" + gj.getback("select  IDcardnumber   from yonghu where id='" + rs["caijirendaima"].ToString() + "'") + "</ScanCard>";//*采集民警身份证号
                alldata = alldata + "<ScanDate>" + System.DateTime.Now.ToString("yyyyMMdd") + "</ScanDate>";//*采集日期
                alldata = alldata + "<Remark>" + "" + "</Remark>";//备注
                string path = @"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_IDCARD.JPG";
                if (System.IO.File.Exists(path))
                {
                    alldata = alldata + "<IdCardPhoto>" + quanjubianliang.rybh + "_IDCARD.JPG" + "</IdCardPhoto>";//身份证照片
                }
                else
                {
                    alldata = alldata + "<IdCardPhoto>" + "" + "</IdCardPhoto>";//身份证照片
                }
                alldata = alldata + "<AGENCY></AGENCY>";//身份证签发机关
                alldata = alldata + "<EXPIRESTART></EXPIRESTART>";//身份证有效期起始日期
                alldata = alldata + "<EXPIREEND></EXPIREEND>";//身份证有效期截止日期
                alldata = alldata + "<SpecialtyKind></SpecialtyKind>";//个人特长代码
            }
            alldata = alldata + "</PersonInfo>";


        }
        //打包物证信息xml
        private void PackGoodsInfo(ref string alldata)
        {
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "wuzheng") > 0)

            {
                alldata = alldata + "<GoodsInfo>";//*

                string sqltrait = "select * from wuzheng where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sqltrait).Rows)
                {
                    if ("" != rs["tupian"].ToString())
                    {
                        alldata = alldata + "<GDSInfo>";//*
                        alldata = alldata + "<Name>" + rs["mingcheng"].ToString() + "</Name>";//*物品名称
                        alldata = alldata + "<Type></Type>";//物品型号
                        alldata = alldata + "<ESN></ESN>";//电子串号
                        alldata = alldata + "<ColorCode></ColorCode>";//物品颜色代码
                        alldata = alldata + "<Color></Color>";//物品颜色描述
                        alldata = alldata + "<Factory></Factory>";//生产厂家
                        alldata = alldata + "<Field></Field>";//产地
                        alldata = alldata + "<Phone></Phone>";//联系电话
                        alldata = alldata + "<Remark></Remark>";//备注
                        alldata = alldata + "<ImageInfo>";//*
                        alldata = alldata + "<Image>" + rs["bianhao"] + ".jpg" + "</Image>";//*文件名
                        alldata = alldata + "<ImageRemark>" + "" + "</ImageRemark>";//图片备注
                        alldata = alldata + "</ImageInfo>";//*
                        alldata = alldata + "</GDSInfo>";//*
                    }
                }
                alldata = alldata + "</GoodsInfo>";//*
            }
        }
        //打包体征信息xml
        private void PackBodySignInfo(ref string alldata)
        {
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "teshutezheng") > 0)

            {
                alldata = alldata + "<NewDataSet>";//*
                string sqltrait = "select * from teshutezheng where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sqltrait).Rows)
                {
                    if ("" != rs["tezhengdaxiao"].ToString())//查看标记位是否存在
                    {
                        alldata = alldata + "<BodySignInfo>";//*
                        alldata = alldata + "<PositionCode>" + rs["tezhengbuwei"].ToString() + "</PositionCode>";//*体表部位代码
                        alldata = alldata + "<OrientationCode>" + rs["tezhengfangwei"].ToString() + "</OrientationCode>";//*体表方位代码
                        alldata = alldata + "<AmountCode>" + rs["tezhengshuliang"].ToString() + "</AmountCode>";//*数量代码
                        alldata = alldata + "<SignCode>" + rs["tezhengmingcheng"].ToString() + "</SignCode>";//*体表标记代码
                        alldata = alldata + "<Remark>" + "" + "</Remark>";//备注
                        alldata = alldata + "<Photo>" + rs["bianhao"].ToString() + ".jpg" + "</Photo>";//*文件名
                        alldata = alldata + "</BodySignInfo>";//*
                    }
                }
                alldata = alldata + "</NewDataSet>";//*
            }
        }
        //打包虹膜信息
        private void PackIrisInfo(ref string alldata)
        {
            alldata = alldata + "<IrisInfos>";//*
            string sqltrait = "select * from hongmu where RYJCXXCJBH='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sqltrait).Rows)
            {
                if ("" != rs[7].ToString())//查看标记位是否存在
                {

                    alldata = alldata + " <IrisInfo>";//*
                    alldata = alldata + "<IrisIndex>" + rs["HMYWDM"].ToString() + "</IrisIndex>";//*眼位代码
                    alldata = alldata + "<Score>" + rs["XXZLDF"].ToString() + "</Score>";//*分数
                    alldata = alldata + "<reason>" + rs["HMQSQKDM"].ToString() + "</reason>";//*确实情况代码
                    alldata = alldata + "<cjtphs>" + rs["HMCJHS"].ToString() + "</cjtphs>";//*采集花的时间
                    alldata = alldata + "<cjbs>3</cjbs>";//
                    alldata = alldata + "<sbxh>" + rs["HMCJSBXHDM"].ToString() + "</sbxh>";//*设备型号
                    alldata = alldata + "<sbbh>" + rs["SBBH"].ToString() + "</sbbh>";//*设备编号
                    alldata = alldata + "<sbcs>" + rs["SBCSDM"].ToString() + "</sbcs>";//*设备厂商
                    alldata = alldata + " </IrisInfo>";//*
                }
            }
            alldata = alldata + "</IrisInfos>";//*
        }
        //打包声纹信息
        private void PackVoiceInfo(ref string alldata)
        {
          
            alldata = alldata + "<VoiceInfo>";//*
            string sqlvoice = "select * from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sqlvoice).Rows)
            {
                if (DBNull.Value != rs["YPSJ"] && ((byte[])rs["YPSJ"]).Length > 0)
                {
                    alldata = alldata + "<SpeakTypeCode>" + rs["FYFSDM"].ToString() + "</SpeakTypeCode>";//发音方式01念读，02自述03交谈
                    alldata = alldata + "<SpeakTypeName>" + gj.getback("select name from zd_FYFS where code='" + rs["FYFSDM"].ToString() + "'") + "</SpeakTypeName>";
                    alldata = alldata + "<LanguageTypeCode>" + rs["LYYZDM"].ToString() + "</LanguageTypeCode>";//语种类别
                    alldata = alldata + "<LanguageTypeName>" + gj.getback("select name from zd_LYYZH where code='" + rs["LYYZDM"].ToString() + "'") + "</LanguageTypeName>";
                    alldata = alldata + "<DialectCode>" + rs["HYFYDM"].ToString() + "</DialectCode>";//方言类别
                    alldata = alldata + "<DialectName>" + gj.getback("select name from zd_HYFY where code='" + rs["HYFYDM"].ToString() + "'") + "</DialectName>";
                    alldata = alldata + "<DeviceID>" + rs["LYSB"].ToString() + "</DeviceID>";//设备型号
                    alldata = alldata + "<InvalidDuration>" + rs["YXYP_SC"].ToString() + "</InvalidDuration>";//有效时长
                    alldata = alldata + "<ClipPercentage>" + 0 + "</ClipPercentage>";//截幅比例
                    alldata = alldata + "<SnrEst>" + rs["XZB"].ToString() + "</SnrEst>";//信噪比
                    alldata = alldata + "<SpeechAvgEnergy>" + rs["NLZ"].ToString() + "</SpeechAvgEnergy>";//平均能量
                    alldata = alldata + "<SpeakerCount>" + 1 + "</SpeakerCount>";//说话人数量

                }
            }
            alldata = alldata + "</VoiceInfo>";//*
        }
        //打包手机信息
        private void PackPhoneInfo(ref string alldata)
        {

            alldata = alldata + "<VoiceInfo>";//*
            string sqlvoice = "select * from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sqlvoice).Rows)
            {
                if (DBNull.Value != rs["YPSJ"] && ((byte[])rs["YPSJ"]).Length > 0)
                {
                    alldata = alldata + "<SpeakTypeCode>" + rs["FYFSDM"].ToString() + "</SpeakTypeCode>";//发音方式01念读，02自述03交谈
                    alldata = alldata + "<SpeakTypeName>" + gj.getback("select name from zd_FYFS where code='" + rs["FYFSDM"].ToString() + "'") + "</SpeakTypeName>";
                    alldata = alldata + "<LanguageTypeCode>" + rs["LYYZDM"].ToString() + "</LanguageTypeCode>";//语种类别
                    alldata = alldata + "<LanguageTypeName>" + gj.getback("select name from zd_LYYZH where code='" + rs["LYYZDM"].ToString() + "'") + "</LanguageTypeName>";
                    alldata = alldata + "<DialectCode>" + rs["HYFYDM"].ToString() + "</DialectCode>";//方言类别
                    alldata = alldata + "<DialectName>" + gj.getback("select name from zd_HYFY where code='" + rs["HYFYDM"].ToString() + "'") + "</DialectName>";
                    alldata = alldata + "<DeviceID>" + rs["LYSB"].ToString() + "</DeviceID>";//设备型号
                    alldata = alldata + "<InvalidDuration>" + rs["YXYP_SC"].ToString() + "</InvalidDuration>";//有效时长
                    alldata = alldata + "<ClipPercentage>" + 0 + "</ClipPercentage>";//截幅比例
                    alldata = alldata + "<SnrEst>" + rs["XZB"].ToString() + "</SnrEst>";//信噪比
                    alldata = alldata + "<SpeechAvgEnergy>" + rs["NLZ"].ToString() + "</SpeechAvgEnergy>";//平均能量
                    alldata = alldata + "<SpeakerCount>" + 1 + "</SpeakerCount>";//说话人数量
                }
            }
            alldata = alldata + "</VoiceInfo>";//*
        }
        //将指纹图像写入文件夹
        private void weitefile_Fig()
        {
            try
            {
                string sql = "select * from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + quanjubianliang.rybh + "'and ZW_TXYSFFMS='0000'";
                string path = "";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (DBNull.Value != rs["ZW_TXSJ"] && ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZW_TXSJ"];
                        switch (rs["ZWZWDM"].ToString())
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                            case "10":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_RP_" + rs["ZWZWDM"].ToString() + ".Bmp";
                                break;
                            case "11":
                            case "12":
                            case "13":
                            case "14":
                            case "15":
                            case "16":
                            case "17":
                            case "18":
                            case "19":
                            case "20":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_FP_" + (Convert.ToInt32(rs["ZWZWDM"].ToString()) - 10).ToString() + ".Bmp";
                                break;
                            default: break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }
                sql = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + quanjubianliang.rybh + "'and ZHW_TXYSFSMS='0000'";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (null != rs["ZHW_TXSJ"] || ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZHW_TXSJ"];
                        switch (rs["ZHWZHWDM"].ToString())
                        {
                            case "31":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PM_R.bmp";
                                break;
                            case "32":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PM_L.bmp";
                                break;
                            case "33":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PW_R.bmp";
                                break;
                            case "34":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PW_L.bmp";
                                break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private void writeuploadfile()
        {

            weitefile_pho();
            weitefile_Fig1();
            weitefile_foot();
            weitefile_voice();
            weitefile_iris();
            weitefile_goods();
        }
        //将三面照图像写入文件夹
        private void weitefile_pho()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//face"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//face");
            }
            string sql = "select * from sanmianzhao where rycjbh='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["zhengmiaozhao"] && ((byte[])rs["zhengmiaozhao"]).Length > 0)
                {
                    FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//face//photo.jpg", FileMode.Create);
                    fs.Write((byte[])rs["zhengmiaozhao"], 0, ((byte[])rs["zhengmiaozhao"]).Length);
                    fs.Close();
                }
                if (DBNull.Value != rs["zuocemiaozhao"] && ((byte[])rs["zuocemiaozhao"]).Length > 0)
                {
                    FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//face//photo_l.jpg", FileMode.Create);
                    fs.Write((byte[])rs["zuocemiaozhao"], 0, ((byte[])rs["zuocemiaozhao"]).Length);
                    fs.Close();
                }
                if (DBNull.Value != rs["youcemianzhao"] && ((byte[])rs["youcemianzhao"]).Length > 0)
                {
                    FileStream fs = new FileStream(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//face//photo_r.jpg", FileMode.Create);
                    fs.Write((byte[])rs["youcemianzhao"], 0, ((byte[])rs["youcemianzhao"]).Length);
                    fs.Close();
                }
            }
        }
        //将指纹图像写入文件夹
        private void weitefile_Fig1()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger");
            }
            try
            {
                string sql = "select * from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + quanjubianliang.rybh + "'and ZW_TXYSFFMS='1419'";
                string path = "";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (DBNull.Value != rs["ZW_TXSJ"] && ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZW_TXSJ"];
                        switch (rs["ZWZWDM"].ToString())
                        {
                            case "1":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_1.wsq";
                                break;
                            case "2":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_2.wsq";
                                break;

                            case "3":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_3.wsq";
                                break;
                            case "4":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_4.wsq";
                                break;
                            case "5":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_5.wsq";
                                break;
                            case "6":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_6.wsq";
                                break;
                            case "7":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_7.wsq";
                                break;
                            case "8":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_8.wsq";
                                break;
                            case "9":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_9.wsq";
                                break;
                            case "10":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_10.wsq";
                                break;
                            case "11":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_11.wsq";
                                break;
                            case "12":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_12.wsq";
                                break;
                            case "13":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_13.wsq";
                                break;
                            case "14":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_14.wsq";
                                break;
                            case "15":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_15.wsq";
                                break;
                            case "16":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_16.wsq";
                                break;
                            case "17":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_17.wsq";
                                break;
                            case "18":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_18.wsq";
                                break;
                            case "19":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_19.wsq";
                                break;
                            case "20":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_20.wsq";
                                break;
                            default: break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }
                sql = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + quanjubianliang.rybh + "'and ZHW_TXYSFSMS='1419'";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (null != rs["ZHW_TXSJ"] || ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZHW_TXSJ"];
                        switch (rs["ZHWZHWDM"].ToString())
                        {
                            case "31":

                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_31.wsq";
                                break;
                            case "32":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_32.wsq";

                                break;
                            case "33":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_33.wsq";
                                break;
                            case "34":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//finger//finger_34.wsq";
                                break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        //将物证图像写入文件夹
        private void weitefile_goods()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//goods"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//goods");
            }
            string sql = "select * from wuzheng where rycjbh='" + quanjubianliang.rybh + "'";
            string path = "";
            int i = 1;
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["tupian"] && ((byte[])rs["tupian"]).Length > 0)
                {
                    path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//goods//good_" + i.ToString() + ".jpeg";
                    i++;
                    FileStream fs = new FileStream(path, FileMode.Create);
                    fs.Write((byte[])rs["tupian"], 0, ((byte[])rs["tupian"]).Length);
                    fs.Close();
                }


            }
        }
        //将足迹图像写入文件夹
        private void weitefile_foot()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//foot"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//foot");
            }
            string sql = "select * from zuji where RYJCXXCJBH='" + quanjubianliang.rybh + "'";
            string path = "";
            int i = 1;
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["ZJSJ"] && ((byte[])rs["ZJSJ"]).Length > 0)
                {
                    if (Convert.ToInt32(rs["ZJBWDM"].ToString()) == 0)
                    {
                        path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//foot//foot_l.jpg";
                    }
                    if (Convert.ToInt32(rs["ZJBWDM"].ToString()) == 1)
                    {
                        path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//foot//foot_r.jpg";
                    }

                }
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write((byte[])rs["ZJSJ"], 0, ((byte[])rs["ZJSJ"]).Length);
                fs.Close();

            }

        }
        //将声纹数据写入文件夹
        private void weitefile_voice()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//voice"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//voice");
            }
            string sql = "select * from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh + "'";

            string path = "";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["YPSJ"] && ((byte[])rs["YPSJ"]).Length > 0)
                {

                    path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//voice//" + quanjubianliang.rybh + ".wav";

                }
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write((byte[])rs["YPSJ"], 0, ((byte[])rs["YPSJ"]).Length);
                fs.Close();

            }

        }
        //将虹膜图片写入文件夹
        private void weitefile_iris()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//iris"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//iris");
            }
            string sql = "select * from  hongmu where RYJCXXCJBH='" + quanjubianliang.rybh + "'";

            string path = "";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["HMSJ"] && ((byte[])rs["HMSJ"]).Length > 0)
                {

                    if (Convert.ToInt32(rs["HMYWDM"].ToString()) == 1)
                    {
                        path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//iris//iris_r.bmp";
                    }
                    if (Convert.ToInt32(rs["HMYWDM"].ToString()) == 2)
                    {
                        path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//iris//iris_l.bmp";
                    }

                }
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write((byte[])rs["HMSJ"], 0, ((byte[])rs["HMSJ"]).Length);
                fs.Close();

            }


        }
        //将体貌特征图片写入文件夹
        private void weitefile_tz()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//tmtz"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//tmtz");
            }
            string sql = "select * from teshutezheng where rycjbh='" + quanjubianliang.rybh + "'";
            string path = "";
            int i = 1;
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (DBNull.Value != rs["tezhengtupian"] && ((byte[])rs["tezhengtupian"]).Length > 0)
                {
                    path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//tmtz//tmtz_" + i.ToString() + ".jpeg";
                    i++;
                    FileStream fs = new FileStream(path, FileMode.Create);
                    fs.Write((byte[])rs["tezhengtupian"], 0, ((byte[])rs["tezhengtupian"]).Length);
                    fs.Close();
                }


            }

        }
        //将手机数据写入文件夹
        private void weitefile_phone()
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//phone"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//phone");
            }

            if(Appcode.zips.UnZip(System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001.zip", System.Windows.Forms.Application.StartupPath.ToString() + "//",""))
            {


            }
           

        }
        private void PackJsonData()
        {

            string JSONdata = "{";
            PackfingerJson(ref JSONdata);
            PackEffectsJson(ref JSONdata);
            PackfootJson(ref JSONdata);
            PackDNAJson(ref JSONdata);
            PackPHOJson(ref JSONdata);
            PackIrisJson(ref JSONdata);
            PackVoiceJson(ref JSONdata);
            PackBaseJson(ref JSONdata);
            JSONdata = JSONdata + "}";
            gj.Writefile1(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh+"//"+ quanjubianliang.rybh+".json",JSONdata);
        }
        public struct baseJSON
        {
            public string rybh;
            public string zwbh;
            public string lybs;
            public string xm;
            public string xmhypy;
            public string cym;
            public string wwxm;
            public string bmch;
            public string sg;
            public string tz;
            public string zc;
            public string gmsfhm;
            public string gmsfZw1;
            public string gmsfZw2;
            public string gmsfQfjg;
            public string gmsfYxq;
            public string gmsfLrfsdm;
            public string csrq;
            public string cyzjdm;
            public string zjhm;
            public string zzmmdm;
            public string xbdm;
            public string gjdm;
            public string mzdm;
            public string hyzkdm;
            public string zjxydm;
            public string xldm;
            public string grsfdm;
            public string tssfdm;
            public string jgdm;
            public string rylbdm;
            public string zydm;
            public string hjdxzqhdm;
            public string hjdxz;
            public string xzdxzqhdm;
            public string xzdxz;
            public string csdssxdm;
            public string csdxz;
            public string ajbh;
            public string ajlb1;
            public string ajlb2;
            public string ajlb3;
            public string lxdh;
            public string fwcs;
            public string cjrxm;
            public string cjrsfhm;
            public string cjrjyh;
            public string cjdwdm;
            public string cjdwmc;
            public string cjsj;
            public string cjcslxdm;
            public string cjsbbh;
            public string cjsbrjbbh;
            public string cjyy;
            public string cjdd;
            public string pointCode;

        }
        private void PackBaseJson(ref string JSONdata)
        {

            baseJSON bjson = new baseJSON();
            string sql = "select * from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
            string sqlXX = "select * from xingti where rycjbh='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                bjson.rybh = quanjubianliang.rybh;
                bjson.zwbh = "";
                bjson.lybs = "8";
                bjson.xm = rs["xm"].ToString();
                bjson.xmhypy = "";
                bjson.cym = "";
                bjson.wwxm = "";
                bjson.bmch = "";
                foreach (DataRow rsX in gj.gettable(sqlXX).Rows)
                {
                    bjson.sg = rsX["shengao"].ToString();//*身高
                    bjson.tz = rsX["tizhong"].ToString();//*体重
                    bjson.zc = rsX["zuchang"].ToString();//*足长
                }

                bjson.gmsfhm = rs["gmsfzhm"].ToString();
                bjson.gmsfZw1 = "";
                bjson.gmsfZw2 = "";
                bjson.gmsfQfjg = "";
                bjson.gmsfYxq = "";
                bjson.gmsfLrfsdm = "0";
                bjson.csrq = rs["csrq"].ToString().Replace("-", "");
                //bjson.csrq = rs["csrq"].ToString();
                bjson.cyzjdm = "";
                bjson.zjhm = "";
                bjson.zzmmdm = "";
                bjson.xbdm = rs["xbdm"].ToString();
                bjson.gjdm = rs["gjdm"].ToString();
                bjson.mzdm = rs["mzdm"].ToString();
                bjson.hyzkdm = "";
                bjson.zjxydm = "";
                bjson.xldm = "";
                bjson.grsfdm = "";
                bjson.tssfdm = "";
                bjson.jgdm = "";
                bjson.rylbdm = rs["caijiyuanyin"].ToString();
                bjson.zydm = "";
                bjson.hjdxzqhdm = rs["hjdz_xzqhdm"].ToString();
                bjson.hjdxz = rs["hjdz_dzmc"].ToString();
                bjson.xzdxzqhdm = rs["xzz_xzqhdm"].ToString();
                bjson.xzdxz = rs["xzz_dzmc"].ToString();
                bjson.csdssxdm = rs["hjdz_xzqhdm"].ToString();
                bjson.csdxz = rs["hjdz_dzmc"].ToString();
                bjson.ajbh = "";
                bjson.ajlb1 = "";
                bjson.ajlb2 = "";
                bjson.ajlb3 = "";
                bjson.lxdh = rs["yddh"].ToString();
                bjson.fwcs = rs["fwcs"].ToString();
                bjson.cjrxm = gj.getback("select  PersonalName   from yonghu where id='" + rs["caijirendaima"].ToString() + "'");
                bjson.cjrsfhm = gj.getback("select  IDcardnumber   from yonghu where id='" + rs["caijirendaima"].ToString() + "'");
                bjson.cjrjyh = gj.getback("select  PoliceNumber  from yonghu where id='" + rs["caijirendaima"].ToString() + "'");
                bjson.cjdwdm = gj.getback("select  Address  from yonghu where id='" + rs["caijirendaima"].ToString() + "'");
                bjson.cjdwmc = gj.getback("select  UnitName   from yonghu where id='" + rs["caijirendaima"].ToString() + "'");
                bjson.cjsj = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                bjson.cjcslxdm ="11";
                bjson.cjsbbh = "";
                bjson.cjsbrjbbh = "";
                bjson.cjyy = "";
                bjson.cjdd = "";
                bjson.pointCode = gj.getback("select cjdNo from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
            }
            string strSerializeJSON = JsonConvert.SerializeObject(bjson);
            JSONdata = JSONdata + "\"personInfo\":" + strSerializeJSON ;

        }
        public struct phoJSON
        {
            public string rybh;
            public string xm;
            public string gmsfhm;
            public string ryzplxdm;
            public string xxzldf;
            public string ryzpsj;
            public string sizeRyzpsj;
            public string forceFlag;
            public string forceCode;
            public string forceReason;
            public string cjsblx;
            public string cjsb;
            public string updateUser;
            public string createUser;


        }
        private void PackPHOJson(ref string JSONdata)
        {
            if (Convert.ToInt32(gj.getback("select count(*) from sanmianzhao where rycjbh='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personPhotos\":[";
                string sql = "select * from sanmianzhao where rycjbh='" + quanjubianliang.rybh + "'";

                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    phoJSON pjson1 = new phoJSON();
                    pjson1.rybh = quanjubianliang.rybh;
                    pjson1.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson1.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson1.ryzplxdm = "11";

                    if (DBNull.Value != rs["zhengmiaozhao"] && ((byte[])rs["zhengmiaozhao"]).Length > 0)
                    {

                        pjson1.ryzpsj = "photo.jpg";
                        pjson1.xxzldf = gj.getsj();
                    }
                    else
                    {
                        pjson1.ryzpsj = "";
                        pjson1.xxzldf = "";


                    }
                    pjson1.sizeRyzpsj = "";
                    pjson1.forceCode = "";
                    pjson1.forceFlag = "0";
                    pjson1.forceReason = "";
                    pjson1.cjsb = "";
                    pjson1.cjsblx = "";
                    pjson1.createUser = "";
                    pjson1.updateUser = "";
                    JSONdata = JSONdata + JsonConvert.SerializeObject(pjson1) + ",";

                    phoJSON pjson2 = new phoJSON();
                    pjson2.rybh = quanjubianliang.rybh;
                    pjson2.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson2.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson2.ryzplxdm = "12";

                    if (DBNull.Value != rs["zuocemiaozhao"] && ((byte[])rs["zuocemiaozhao"]).Length > 0)
                    {
                        pjson2.ryzpsj = "photo_l.jpg";
                        pjson2.xxzldf = gj.getsj();
                    }
                    else
                    {
                        pjson2.ryzpsj = "";
                        pjson2.xxzldf = "";
                    }
                    pjson2.sizeRyzpsj = "";
                    pjson2.forceCode = "";
                    pjson2.forceFlag = "0";
                    pjson2.forceReason = "";
                    pjson2.cjsb = "";
                    pjson2.cjsblx = "";
                    pjson2.createUser = "";
                    pjson2.updateUser = "";
                    JSONdata = JSONdata + JsonConvert.SerializeObject(pjson2) + ",";

                    phoJSON pjson3 = new phoJSON();
                    pjson3.rybh = quanjubianliang.rybh;
                    pjson3.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson3.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    pjson3.ryzplxdm = "13";


                    if (DBNull.Value != rs["youcemianzhao"] && ((byte[])rs["youcemianzhao"]).Length > 0)
                    {
                        pjson3.ryzpsj = "photo_r.jpg";
                        pjson3.xxzldf = gj.getsj();
                    }
                    else
                    {
                        pjson3.ryzpsj = "";
                        pjson3.xxzldf = "";

                    }
                    pjson3.sizeRyzpsj = "";
                    pjson3.forceCode = "";
                    pjson3.forceFlag = "0";
                    pjson3.forceReason = "";
                    pjson3.cjsb = "";
                    pjson3.cjsblx = "";
                    pjson3.createUser = "";
                    pjson3.updateUser = "";
                    JSONdata = JSONdata + JsonConvert.SerializeObject(pjson3);


                }
                JSONdata = JSONdata + "],";
            }
        }
        public struct voiceJSON
        {
            public string rybh;
            public string xm;
            public string gmsfhm;
            public string cyly;
            public string zsc;
            public string yxsc;
            public string xzb;
            public string nlz;
            public string xxzldf;
            public string lyyzdm;
            public string lysb;
            public string fyfsdm;
            public string xddm;
            public string hyfydm;
            public string swsj;
            public string sizeSwsj;
            public string wjgs;
            public string cyl;
            public string ws;
            public string sdsl;
            public string syjl;
            public string ylnr;
            public string zycd;
            public string jkqk;
            public string qxqk;
            public string cjcd;
            public string cjsblx;
            public string cjsb;
            public string bcjrylbdm;
           

        }
        private void PackVoiceJson(ref string JSONdata)
        {
            
            if (Convert.ToInt32(gj.getback("select count(*) from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personVoices\":[";

                string sql = "select * from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh + "'";

                voiceJSON vjson = new voiceJSON();
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {

                    vjson.rybh = quanjubianliang.rybh;
                    vjson.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    vjson.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                    vjson.cyly = "";
                    vjson.zsc = rs["QBYP_SC"].ToString().Split('.')[0].ToString();
                    vjson.yxsc = Convert.ToInt32(rs["YXYP_SC"].ToString()).ToString();
                    vjson.nlz = rs["NLZ"].ToString().Split('.')[0].ToString();
                    vjson.xzb = rs["XZB"].ToString().Split('.')[0].ToString();

                    vjson.lysb = rs["LYSB"].ToString();
                    vjson.lyyzdm = rs["LYYZDM"].ToString();
                    vjson.fyfsdm = rs["FYFSDM"].ToString();
                    vjson.xddm = rs["XDDM"].ToString();
                    vjson.hyfydm = rs["HYFYDM"].ToString();
                    if (DBNull.Value != rs["YPSJ"] && ((byte[])rs["YPSJ"]).Length > 0)
                    {
                        vjson.swsj = quanjubianliang.rybh.ToString() + ".wav";
                        vjson.xxzldf = gj.getsj();
                    }
                    else
                    {
                        vjson.swsj = "";
                        vjson.xxzldf = "";
                    }

                    vjson.wjgs = "";
                    vjson.cyl = "";
                    vjson.ws = "";
                    vjson.sdsl = "";
                    vjson.syjl = "";
                    vjson.ylnr = "";
                    vjson.zycd = "";
                    vjson.jkqk = "";
                    vjson.qxqk = "";
                    vjson.cjcd = "";
                    vjson.cjsblx = "";
                    vjson.cjsb = rs["LYSB"].ToString();
                    vjson.bcjrylbdm = "0900";
                    vjson.sizeSwsj = getFileVersionInfo(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//voice//" + quanjubianliang.rybh + ".wav");

                }

                JSONdata = JSONdata + JsonConvert.SerializeObject(vjson) + "";
                JSONdata = JSONdata + "],";
            }
        }
        private string getFileVersionInfo(string path)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            return fileInfo.Length.ToString();
        }
        public struct irisJSON
        {
            public string rybh;
            public string xm;
            public string gmsfhm;
            public string hmywdm;
            public string hmqsqkdm;
            public string xxzldf;
            public string cjtphs;
            public string hmsj;
            public string sizeHmsj;
            public string cjsblx;
            public string cjsb;

        }
        private void PackIrisJson(ref string JSONdata)
        {
            if (Convert.ToInt32(gj.getback("select count(*) from hongmu where RYJCXXCJBH='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personIrises\":[";
                string sql = "select * from hongmu where RYJCXXCJBH='" + quanjubianliang.rybh + "' order by HMYWDM";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (Convert.ToInt32(rs["HMYWDM"]) - 1 == 0)
                    {
                        irisJSON Ijson = new irisJSON();
                        Ijson.rybh = quanjubianliang.rybh;
                        Ijson.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        Ijson.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        Ijson.hmywdm = (Convert.ToInt32(rs["HMYWDM"]) - 1).ToString();
                        Ijson.hmqsqkdm = rs["HMQSQKDM"].ToString();

                        Ijson.cjtphs = "";
                        if (DBNull.Value != rs["HMSJ"] && ((byte[])rs["HMSJ"]).Length > 0)
                        {

                            Ijson.hmsj = "iris_l.bmp";

                            Ijson.xxzldf = gj.getsj();

                        }
                        else
                        {
                            Ijson.hmsj = "";
                            Ijson.xxzldf = "";
                        }

                        Ijson.sizeHmsj = "";
                        Ijson.cjsblx = "IKUSB-E30";
                        Ijson.cjsb = "IKUSB-E30_"+rs["SBBH"].ToString();

                        JSONdata = JSONdata + JsonConvert.SerializeObject(Ijson) + ",";
                    }
                    if (Convert.ToInt32(rs["HMYWDM"]) - 1 == 1)
                    {

                        irisJSON Ijson1 = new irisJSON();
                        Ijson1.rybh = quanjubianliang.rybh;
                        Ijson1.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        Ijson1.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        Ijson1.hmywdm = (Convert.ToInt32(rs["HMYWDM"]) - 1).ToString();
                        Ijson1.hmqsqkdm = rs["HMQSQKDM"].ToString();

                        Ijson1.cjtphs = "";
                        if (DBNull.Value != rs["HMSJ"] && ((byte[])rs["HMSJ"]).Length > 0)
                        {

                            Ijson1.hmsj = "iris_r.bmp";

                            Ijson1.xxzldf = gj.getsj();

                        }
                        else
                        {
                            Ijson1.hmsj = "";
                            Ijson1.xxzldf = "";
                        }

                        Ijson1.sizeHmsj = "";
                        Ijson1.cjsblx = "IKUSB-E30";
                        Ijson1.cjsb = "IKUSB-E30_" + rs["SBBH"].ToString();

                        JSONdata = JSONdata + JsonConvert.SerializeObject(Ijson1);
                    }

                }

                JSONdata = JSONdata + "],";
            }
        }
        public struct footJson
        {
            public string dataIndex;
            public string hghw;
            
            public string jyc;
            public string jyzk;
            public string jygk;
            public string qzhw;
            public string rybh;
            public string sizeZjsj;
            public string xxzldf;
            public string xyc;
            public string zjbwdm;
            public string zjlxdm;
            public string zjsj;
            //public string xm;
            //public string gmsfhm;


            //public string cjlxdm;
            //public string xdzldm;
            //public string zyjxy;




            //public string zyjy;




            //public string cjsblx;
            //public string cjsb;

        }
        private void PackfootJson(ref string JSONdata)
        {
           
            if (Convert.ToInt32(gj.getback("select count(*) from zuji where RYJCXXCJBH='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personFoots\":[";
                string sql = "select * from zuji where RYJCXXCJBH='" + quanjubianliang.rybh + "'";
                int counts = Convert.ToInt32(gj.getback("select count(*) from zuji where RYJCXXCJBH='" + quanjubianliang.rybh + "'"));
                int index = 0;
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    index++;
                    if (Convert.ToInt32(rs["ZJBWDM"])== 0)
                    {
                        footJson fjson = new footJson();
                        fjson.rybh = quanjubianliang.rybh;
                        //fjson.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        //fjson.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        fjson.zjbwdm = (Convert.ToInt32(rs["ZJBWDM"])).ToString();
                        fjson.zjlxdm = "1";
                        fjson.dataIndex = "1";
                        if (DBNull.Value != rs["ZJSJ"] && ((byte[])rs["ZJSJ"]).Length > 0)
                        {

                            fjson.zjsj= "foot_l.jpg";

                            fjson.xxzldf = gj.getsj();

                        }
                        else
                        {
                            fjson.zjsj = "";
                            fjson.xxzldf = "";
                        }
                        //fjson.xdzldm="0";
                        //fjson.zyjxy = "";
                        fjson.qzhw = "99";
                        fjson.hghw = "99";
                        fjson.xyc = "0";
                        fjson.jyc = "0";
                        //fjson.zyjy = "";
                        fjson.jyzk = "0";
                        fjson.jygk = "0";
                        fjson.sizeZjsj="0";
                        //fjson.cjsblx="";
                        //fjson.cjsb="";
                        if (counts == index)
                        {
                            JSONdata = JSONdata + JsonConvert.SerializeObject(fjson) ;
                        }
                        else
                        {

                            JSONdata = JSONdata + JsonConvert.SerializeObject(fjson)+ ",";
                        }
                    }
                    if (Convert.ToInt32(rs["ZJBWDM"]) == 1)
                    {
                        footJson fjson1 = new footJson();
                        fjson1.rybh = quanjubianliang.rybh;
                        //fjson1.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        //fjson1.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                        fjson1.zjbwdm = (Convert.ToInt32(rs["ZJBWDM"])).ToString();
                        fjson1.zjlxdm = "1";
                        fjson1.dataIndex = "1";
                        if (DBNull.Value != rs["ZJSJ"] && ((byte[])rs["ZJSJ"]).Length > 0)
                        {

                            fjson1.zjsj = "foot_r.jpg";

                            fjson1.xxzldf = gj.getsj();

                        }
                        else
                        {
                            fjson1.zjsj = "";
                            fjson1.xxzldf = "";
                        }
                        fjson1.qzhw = "99";
                        fjson1.hghw = "99";
                        fjson1.xyc = "0";
                        fjson1.jyc = "0";
                        //fjson.zyjy = "";
                        fjson1.jyzk = "0";
                        fjson1.jygk = "0";
                        fjson1.sizeZjsj = "0";
                        //fjson.cjsblx="";
                        //fjson.cjsb="";


                        if (counts == index)
                        {
                            JSONdata = JSONdata + JsonConvert.SerializeObject(fjson1) ;
                        }
                        else
                        {

                            JSONdata = JSONdata + JsonConvert.SerializeObject(fjson1)+ ",";
                        }
                    }


                }

                JSONdata = JSONdata + "],";
            }
        }
        public struct DNAJson
        {
            public string rybh;
            public string xm;
            public string gmsfhm;
            public string rydnabh;
            public string yblxdm;
            public string cjzkbsdm;
        }
        private void PackDNAJson(ref string JSONdata)
        {
            DNAJson Djson = new DNAJson();
            Djson.rybh = quanjubianliang.rybh;
            Djson.xm = gj.getback("select XM from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
            Djson.gmsfhm = gj.getback("select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
            Djson.rydnabh = quanjubianliang.rybh.Replace("R","D");
            Djson.yblxdm = "01";
            Djson.cjzkbsdm = "1";
            JSONdata = JSONdata + "\"personDNA\":";
            JSONdata = JSONdata + JsonConvert.SerializeObject(Djson);
            JSONdata = JSONdata + ",";
            
        }
        private void PackfingerJson(ref string JSONdata)
        {

            if (Convert.ToInt32(gj.getback("select count(*) from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personFps\":[";
                string sqlfinger = "select * from LEDEN_COLLECT_FINGER  where RYJCXXCJBH='" + quanjubianliang.rybh + "'  and ZW_TXYSFFMS='1419'  order by cast(zwzwdm as '9999')";
                foreach (DataRow rs in gj.gettable(sqlfinger).Rows)
                {

                    JSONdata = JSONdata + "{";
                    JSONdata = JSONdata + "\"rybh\":" + "\"" + quanjubianliang.rybh + "\",";//*人员编号
                    JSONdata = JSONdata + "\"dataType\":" + "\"1\",";
                    if (rs["ZWZWDM"].ToString().Length == 1)
                    {
                        JSONdata = JSONdata + "\"zwdm\":" + "\"" +"0"+ rs["ZWZWDM"].ToString() + "\",";


                    }
                    else
                    {
                        JSONdata = JSONdata + "\"zwdm\":" + "\"" + rs["ZWZWDM"].ToString() + "\",";


                    }
                    JSONdata = JSONdata + "\"qsqkdm\":" + "\"" + rs["ZZHWQSQKDM"].ToString() + "\",";
                    JSONdata = JSONdata + "\"txzl\":" + "\"" + rs["ZW_TXZL"].ToString() + "\",";//
                    JSONdata = JSONdata + "\"txspfxcd\":" + "\"640\",";
                    JSONdata = JSONdata + "\"txczfxcd\":" + "\"640\",";
                    JSONdata = JSONdata + "\"txfbl\":" + "\"500\",";
                    string zwtxmc = "";
                    if(rs["ZWZWDM"].ToString().Length==0)
                    {
                        zwtxmc = "finger_0" + rs["ZWZWDM"].ToString() + ".wsq";
                    }
                    else
                    {
                        zwtxmc = "finger_" + rs["ZWZWDM"].ToString() + ".wsq";

                    }
                    JSONdata = JSONdata + "\"txsj\":" + "\"" + zwtxmc + "\",";//*采集单位代码
                    JSONdata = JSONdata + "\"sizeTxsj\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"forceFlag\":" + "\"0\",";//*数据来源
                    JSONdata = JSONdata + "\"forceCode\":" + "\"0\",";//*数据来源
                    JSONdata = JSONdata + "\"cjsblx\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"cjsb\":" + "\"" + "" + "\",";//*数据来源
                    JSONdata = JSONdata + "\"cjsj\":" + "\"" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"";//*采集时间

                    if (Convert.ToUInt32(rs["ZWZWDM"].ToString()) == 20)
                    {
                        if (Convert.ToInt32(gj.getback("select count(*) from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + quanjubianliang.rybh + "'")) > 0)
                        {
                            JSONdata = JSONdata + "},";//*
                        }
                        else

                        {
                            JSONdata = JSONdata + "}";//*
                        }

                            
                    }
                    else
                    {
                        JSONdata = JSONdata + "},";//*
                    }


                }

                string sqlplam = "select * from LEDEN_COLLECT_PALM  where RYJCXXCJBH='" + quanjubianliang.rybh + "' and ZHW_TXYSFSMS='1419'  order by cast(ZHWZHWDM as '9999')";
                foreach (DataRow rs in gj.gettable(sqlplam).Rows)
                {

                    JSONdata = JSONdata + "{";
                    JSONdata = JSONdata + "\"rybh\":" + "\"" + quanjubianliang.rybh + "\",";//*人员编号
                    JSONdata = JSONdata + "\"dataType\":" + "\"1\",";//*案件类别代码1
                    JSONdata = JSONdata + "\"zwdm\":" + "\"" + rs["ZHWZHWDM"].ToString() + "\",";//*案件类别代码1
                    JSONdata = JSONdata + "\"qsqkdm\":" + "\"" + rs["ZHW_ZZHWQSQKDM"].ToString() + "\",";//*物品名称
                    JSONdata = JSONdata + "\"txzl\":" + "\"" + rs["ZHW_TXZL"].ToString() + "\",";//*人员编号
                    JSONdata = JSONdata + "\"txspfxcd\":" + "\"2304\",";//*人员编号
                    JSONdata = JSONdata + "\"txczfxcd\":" + "\"2304\",";//*采集人姓名
                    JSONdata = JSONdata + "\"txfbl\":" + "\"500\",";//*采集人姓名
                    string zwtxmc = "";
                    if (rs["ZHWZHWDM"].ToString().Length == 0)
                    {
                        zwtxmc = "finger_0" + rs["ZHWZHWDM"].ToString() + ".wsq";
                    }
                    else
                    {
                        zwtxmc = "finger_" + rs["ZHWZHWDM"].ToString() + ".wsq";

                    }
                    JSONdata = JSONdata + "\"txsj\":" + "\"" + zwtxmc + "\",";//*采集单位代码
                    JSONdata = JSONdata + "\"sizeTxsj\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"forceFlag\":" + "\"0\",";//*数据来源
                    JSONdata = JSONdata + "\"forceCode\":" + "\"" + "" + "\",";//*采集时间
                    JSONdata = JSONdata + "\"cjsblx\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"cjsb\":" + "\"" + "" + "\",";//*数据来源
                    JSONdata = JSONdata + "\"cjsj\":" + "\"" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\"";//*采集时间

                    if (Convert.ToUInt32(rs["ZHWZHWDM"].ToString()) == 34)
                    {
                        
                            JSONdata = JSONdata + "}";//*
                       


                    }
                    else
                    {
                        JSONdata = JSONdata + "},";//*
                    }


                }
                JSONdata = JSONdata + "],";
            }

        }
        private void PackEffectsJson(ref string JSONdata)
        {

            if (Convert.ToInt32(gj.getback("select count(*) from wuzheng where rycjbh='" + quanjubianliang.rybh + "'")) > 0)
            {
                JSONdata = JSONdata + "\"personEffects\":[";
                string sqlEffects = "select * from wuzheng where rycjbh='" + quanjubianliang.rybh + "'";
                int counts = Convert.ToInt32(gj.getback("select count(*) from wuzheng where rycjbh='" + quanjubianliang.rybh + "'"));
                int index = 0;
                foreach (DataRow rs in gj.gettable(sqlEffects).Rows)
                {

                    JSONdata = JSONdata + "{";
                    JSONdata = JSONdata + "\"rybh\":" + "\"" + quanjubianliang.rybh + "\",";//*人员编号
                 
                    JSONdata = JSONdata + "\"wpmc\":" + "\"" + rs["mingcheng"].ToString() + "\",";//*案件类别代码1
                    JSONdata = JSONdata + "\"sawpdm\":" + "\"" + "" + "\",";//*物品名称
                    JSONdata = JSONdata + "\"wpbzh\":" + "\"" + ""+ "\",";//*人员编号
                    JSONdata = JSONdata + "\"wptzms\":" + "\"\",";//*人员编号
                    JSONdata = JSONdata + "\"jz\":" + "\"\",";//*采集人姓名
                    JSONdata = JSONdata + "\"ysdm\":" + "\"\",";//*采集人姓名
                    string zwtxmc = "";
                    if (DBNull.Value != rs["tupian"] && ((byte[])rs["tupian"]).Length > 0)
                    {
                        zwtxmc = "good_" + (index+1).ToString() + ".jpeg";
                    }
                   
                    JSONdata = JSONdata + "\"sswpzp\":" + "\"" + zwtxmc + "\",";//*采集单位代码
                    JSONdata = JSONdata + "\"sizeSswpzp\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"dataIndex\":" + "\"\",";//*数据来源
                    JSONdata = JSONdata + "\"cjsblx\":" + "\"" + "" + "\",";//*采集时间
                    JSONdata = JSONdata + "\"cjsb\":" + "\"" + "" + "\",";//*采集单位名称
                    JSONdata = JSONdata + "\"remark\":" + "\"" + "" + "\"";//*数据来源
                    index++;
                    if (counts == index)
                    {

                        JSONdata = JSONdata + "}";//*
                    }
                    else
                    {
                        JSONdata = JSONdata + "},";//*
                    }


                }

                JSONdata = JSONdata + "],";

            }
        }
   

        private void button1_Click(object sender, EventArgs e)
        {try
            {
                weitefile_pho();
                weitefile_Fig1();
                weitefile_goods();
                weitefile_tz();
                MessageBox.Show("生成完成");
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
  
    }
}
