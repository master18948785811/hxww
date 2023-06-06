using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 华夏网为.jinbenxinxi
{
    public partial class jinbenxinxi : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        [StructLayout(LayoutKind.Sequential, Size = 16, CharSet = CharSet.Ansi)]
        public struct IDCARD_ALL
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public char name;     //姓名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public char sex;      //性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public char people;    //民族，护照识别时此项为空
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char birthday;   //出生日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
            public char address;  //地址，在识别护照时导出的是国籍简码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public char number;  //地址，在识别护照时导出的是国籍简码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public char signdate;   //签发日期，在识别护照时导出的是有效期至 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char validtermOfStart;  //有效起始日期，在识别护照时为空
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char validtermOfEnd;  //有效截止日期，在识别护照时为空
        }
        int iRetUSB = 0, iRetCOM = 0;
        bool m_IDcard = false;   //默认没有身份证
        public jinbenxinxi()
        {
            InitializeComponent();
        }
        private void jinbenxinxi_Load(object sender, EventArgs e)
        {
            try
            {
              
                if(quanjubianliang.i_query==0)
                {
                    quanjubianliang.i_query = 1;
                }
                getshuju();
                //读取数据库数据，并显示到界面
                if (gj.getbool("rycjbh", quanjubianliang.rybh, "renyuanjibenxinxi") > 0 && quanjubianliang.rybh.Length>0)
                {
                    readvalue();
                }
                else
                {
                    // 生成本次采集数据编号
                    if (quanjubianliang.states == 1)
                    {
                        try
                        {
                            if (quanjubianliang.djms == 0)
                            {
                                Redata data = new Redata();
                                data.Address = gj.getback("select Address from shebaizhuce where IP='" + gj.GetIP() + "' and MAC='" + gj.GetMAC() + "'");
                                string strSerializeJSON = JsonConvert.SerializeObject(data);
                                strSerializeJSON = "unitCode=" + data.Address;
                                //通过webservice发送给服务器
                                string result = Appcode.SD_WebSvcCaller.QueryPostWebService("http://10.48.21.54:58888/htmisWebService/person/postCreateId", strSerializeJSON); ;        //接受服务器信息
                                if (null != result)
                                {
                                    JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                                    if ("1" == jo["status"].ToString())//生成的编号成功
                                    {
                                        this.renyuanbianhao.Text = quanjubianliang.rybh = jo["value"]["rybh"].ToString();
                                        string DNANumber = jo["value"]["dna_n"].ToString();
                                        //将DNA编号存入数据库
                                        string sqlDNA = "insert into DNA(rycjbh,DNANumber)values('" + quanjubianliang.rybh + "','" + DNANumber + "')";
                                        gj.sqliteexcu(sqlDNA);
                                    }
                                    else if ("error" == jo["status"].ToString())
                                    {
                                        MessageBox.Show("服务器异常，请联系管理员");
                                        return;
                                    }
                                    else
                                    {
                                        MessageBox.Show("生成编号出错" + result);
                                        return;
                                    }
                                }

                            }
                            else
                            {
                              
                                string unitcode = gj.getback("select address from yonghu where id='" + quanjubianliang.cjrbh + "'");
                                string sysid = gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
                                string password= gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
                                string result = Appcode.SD_WebSvcCaller.QueryGetWebService("http://"+quanjubianliang.webserviceip +"/pics/api/data/getRybh", unitcode,sysid,password);
                           
                                if (null != result)
                                {
                                    JObject jo = JObject.Parse(result);
                                    if ("1" == jo["flag"].ToString())//生成的编号成功
                                    {
                                        this.renyuanbianhao.Text = quanjubianliang.rybh = jo["data"].ToString();
                                        string DNANumber = jo["dnaNo"].ToString();
                                        //将DNA编号存入数据库
                                        string sqlDNA = "insert into DNA(rycjbh,DNANumber)values('" + quanjubianliang.rybh + "','" + DNANumber + "')";
                                        gj.sqliteexcu(sqlDNA);
                                    }
                                    else
                                    {
                                        MessageBox.Show("生成编号出错" + result);
                                        Application.Exit();
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("获取人员编号失败" + ex.ToString());
                            Application.Exit();
                        }
                    }
                    else
                    {

                          this.renyuanbianhao.Text = quanjubianliang.rybh = getRYJCXXCJBH();

                    }
                    //创建人员编号文件夹
                    bool exist = System.IO.Directory.Exists(@"ZIP//" + quanjubianliang.rybh);

                    if (!exist)
                    {
                        System.IO.Directory.CreateDirectory(@"ZIP//" + quanjubianliang.rybh);
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("链接服务器异常,错误信息:"+ ex.ToString());
               
            }
        }
        //获取各个种类
        private void getshuju()
        {
            string sql = "select * from zd_guoji order by bianhao";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                this.guoji.Items.Add(rs[2].ToString());
            }
            this.guoji.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.guoji.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           this.guoji.Text= "中华人民共和国";


            string sql2 = "select * from zd_xingbie order by bianhao";
            foreach (DataRow rs in gj.gettable(sql2).Rows)
            {
                this.xingbie.Items.Add(rs[2].ToString());
                this.xingbie.AutoCompleteCustomSource.Add(rs[2].ToString());
            }
            this.xingbie.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.xingbie.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.xingbie.Text = "男";



            string sql3 = "select * from zd_minzu order by bianhao";
            foreach (DataRow rs in gj.gettable(sql3).Rows)
            {
                this.minzu.Items.Add(rs[2].ToString());
                this.minzu.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.minzu.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.minzu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.minzu.Text = "汉族";


            string sql4 = "select * from zd_hujidixingzhengquhua order by bianhao";
            foreach (DataRow rs in gj.gettable(sql4).Rows)
            {
                this.hujidixingzhengquhua.Items.Add(rs[2].ToString());
                this.hujidixingzhengquhua.AutoCompleteCustomSource.Add(rs[2].ToString());

                this.xianjuzhudixingzhengquhua.Items.Add(rs[2].ToString());
                this.xianjuzhudixingzhengquhua.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.hujidixingzhengquhua.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.hujidixingzhengquhua.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            this.xianjuzhudixingzhengquhua.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.xianjuzhudixingzhengquhua.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            string sql5 = "select * from zd_renyuanleibie order by bianhao";
            foreach (DataRow rs in gj.gettable(sql5).Rows)
            {
                this.renyuanleibie.Items.Add(rs[2].ToString());
                this.renyuanleibie.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.renyuanleibie.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.renyuanleibie.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.renyuanleibie.Text = "其他人员";

            string sql6= "select * from zd_zhiyeleibie order by bianhao";
            foreach (DataRow rs in gj.gettable(sql6).Rows)
            {
                this.zhiyeleibie.Items.Add(rs[2].ToString());
                this.zhiyeleibie.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.zhiyeleibie.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.zhiyeleibie.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.zhiyeleibie.Text = "不便分类的其他从业人员";

            string sql7 = "select * from zd_caijiyuanyin order by bianhao";
            foreach (DataRow rs in gj.gettable(sql7).Rows)
            {
                this.caijiyuanyin.Items.Add(rs[2].ToString());
                this.caijiyuanyin.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.caijiyuanyin.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.caijiyuanyin.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.caijiyuanyin.Text = "其他";


            string sql8 = "select * from zd_qitazhengjianzhonglei order by bianhao";
            foreach (DataRow rs in gj.gettable(sql8).Rows)
            {
                this.qitazhengjianzhonglei.Items.Add(rs[2].ToString());
                this.qitazhengjianzhonglei.AutoCompleteCustomSource.Add(rs[2].ToString());
            }

            this.qitazhengjianzhonglei.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.qitazhengjianzhonglei.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.qitazhengjianzhonglei.Text = "其他";
        }
        //读取数据库内容显示在界面
        private void readvalue()
        {
            try
            {
                string sql = "select * from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    renyuanbianhao.Text = quanjubianliang.rybh;
                    xingming.Text = rs["xm"].ToString();
                    guoji.Text = gj.getback("select mingcheng from zd_guoji where bianhao='" + rs["gjdm"].ToString() + "'");
                    waiwenxingming.Text = rs["waiwenxingming"].ToString();
                    if ("" != rs["shenfengzshengzhaopian"].ToString())
                    {
                        byte[] fileBytes = (byte[])rs["shenfengzshengzhaopian"];
                        showbox.Image = Image.FromStream(new System.IO.MemoryStream(fileBytes));
                        showbox.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                   
                    shenfenzhenghao.Text = rs["gmsfzhm"].ToString();
                    xingbie.Text= gj.getback("select mingcheng from zd_xingbie where bianhao='" + rs["xbdm"].ToString() + "'");
                    minzu.Text = gj.getback("select mingcheng from zd_minzu where bianhao='" + rs["mzdm"].ToString() + "'");
                    chushengriqi.Text = rs["csrq"].ToString();

                    hujidizhi.Text =  rs["hjdz"].ToString() ;
                    hujidixingzhengquhua.Text = gj.getback("select  mingcheng from zd_hujidixingzhengquhua where bianhao ='" + rs["hjdz_xzqhdm"].ToString() + "'");
                    hujidixiangxidizhi.Text = rs["hjdz_dzmc"].ToString();
                    xianjuzhudixingzhengquhua.Text = gj.getback("select  mingcheng from zd_hujidixingzhengquhua where bianhao ='" + rs["xzz_xzqhdm"].ToString() + "'");
                    xianjuzhudixiangxidizhi.Text = rs["xzz_dzmc"].ToString();

                    gongzuodanwei.Text = rs["fwcs"].ToString();
                    shoujihaoma.Text = rs["yddh"].ToString();
                    renyuanleibie.Text = gj.getback("select  mingcheng from zd_renyuanleibie where bianhao ='" + rs["renyuanleibie"].ToString() + "'");
                    zhiyeleibie.Text = gj.getback("select  mingcheng from zd_zhiyeleibie where bianhao ='" + rs["zhiyeleibie"].ToString() + "'");
                    caijiyuanyin.Text = gj.getback("select  mingcheng from zd_caijiyuanyin where bianhao ='" + rs["caijiyuanyin"].ToString() + "'");

                    qitazhengjianzhonglei.Text = gj.getback("select  mingcheng from zd_qitazhengjianzhonglei where bianhao ='" + rs["cyzjdm"].ToString() + "'");
                    qitazhengjianhaoma.Text = rs["zjhm"].ToString();
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //保存采集内容,写入数据库
        private void writevalue()
        {
            try
            {
                if ("" == quanjubianliang.rybh)
                {
                    return;
                }
                else
                {
                    string sql = "insert into renyuanjibenxinxi(rycjbh ,qtxtbh , xm ,waiwenxingming, gjdm ,gmsfzhm,xbdm , mzdm, csrq , hjdz,hjdz_xzqhdm , hjdz_dzmc ,xzz_xzqhdm , xzz_dzmc,fwcs ,yddh, renyuanleibie , zhiyeleibie,caijiyuanyin ,cyzjdm  , zjhm,caijirendaima  )values('"
                                    + quanjubianliang.rybh + "','"
                                     + ""+ "','"
                                    + xingming.Text + "','"
                                    + waiwenxingming.Text + "','"
                                    + gj.getback("select bianhao from zd_guoji where  mingcheng='"+guoji.Text+"'") + "','"
                                    + shenfenzhenghao.Text + "','"
                                    + gj.getback("select bianhao from zd_xingbie where   mingcheng='"+xingbie.Text+"'") + "','"
                                    + gj.getback("select bianhao from zd_minzu where  mingcheng='"+minzu.Text+"'") + "','"
                                    + chushengriqi.Text + "','"
                                    + hujidizhi.Text + "','"
                                    + gj.getback("select bianhao from zd_hujidixingzhengquhua where  mingcheng='" + hujidixingzhengquhua.Text + "'") + "','"
                                    + hujidixiangxidizhi.Text + "','"
                                    + gj.getback("select bianhao from zd_hujidixingzhengquhua where  mingcheng='" + xianjuzhudixingzhengquhua.Text + "'") + "','"
                                    + xianjuzhudixiangxidizhi.Text + "','"
                                    + gongzuodanwei.Text + "','"
                                    + shoujihaoma.Text + "','"
                                      + gj.getback("select bianhao from zd_renyuanleibie where  mingcheng='" + renyuanleibie.Text + "'") + "','"
                                       + gj.getback("select bianhao from zd_zhiyeleibie where  mingcheng='" + zhiyeleibie.Text + "'") + "','"
                                     + gj.getback("select bianhao from zd_caijiyuanyin where  mingcheng='" + caijiyuanyin.Text + "'") + "','"
                                     + gj.getback("select bianhao from zd_qitazhengjianzhonglei where  mingcheng='" + qitazhengjianzhonglei.Text + "'") + "','"
                                    + qitazhengjianhaoma.Text + "','"+quanjubianliang.cjrbh+"')";
                    
                    gj.sqliteexcu(sql);
                    if (m_IDcard)//判断是否有证件照
                    {
                        gj.savePicture(@"zp.bmp", "update renyuanjibenxinxi set shenfengzshengzhaopian=@data where rycjbh='" + quanjubianliang.rybh + "'");
                    }
                    //MessageBox.Show("基本信息保存成功");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //更新采集内容,写入数据库
        private void updatevalue()
        {
            try
            {
                if ("" == quanjubianliang.rybh)
                {
                    return;
                }
                else
                {

                    string sql = "update renyuanjibenxinxi set xm='" + xingming.Text
                                 + "',gjdm='" + gj.getback("select bianhao from zd_guoji where mingcheng='" + guoji.Text + "'")
                                 + "',waiwenxingming='" + waiwenxingming.Text
                                 + "',gmsfzhm='" + shenfenzhenghao.Text
                                 + "',xbdm='" + gj.getback("select bianhao from zd_xingbie where mingcheng='" + xingbie.Text + "'")
                                 + "',mzdm='" + gj.getback("select bianhao from zd_minzu where  mingcheng='" + minzu.Text + "'")
                                 + "',csrq='" + chushengriqi.Text
                                 + "',hjdz='" + hujidizhi.Text
                                 + "',hjdz_xzqhdm='" + gj.getback("select bianhao from zd_hujidixingzhengquhua where  mingcheng='" + hujidixingzhengquhua.Text + "'")
                                 + "',hjdz_dzmc='" + hujidixiangxidizhi.Text
                                 + "',xzz_xzqhdm='" + gj.getback("select bianhao from zd_hujidixingzhengquhua where  mingcheng='" + xianjuzhudixingzhengquhua.Text + "'")
                                 + "',xzz_dzmc='" + xianjuzhudixiangxidizhi.Text
                                 + "',fwcs='" + gongzuodanwei.Text
                                 + "',yddh='" + shoujihaoma.Text
                                 + "',renyuanleibie='" + gj.getback("select bianhao from zd_renyuanleibie where  mingcheng='" + renyuanleibie.Text + "'")
                                 + "',zhiyeleibie='" + gj.getback("select bianhao from zd_zhiyeleibie where  mingcheng='" + zhiyeleibie.Text + "'")
                                 + "',caijiyuanyin='" + gj.getback("select bianhao from zd_caijiyuanyin where  mingcheng='" + caijiyuanyin.Text + "'")
                                 + "',cyzjdm='" + gj.getback("select bianhao from zd_qitazhengjianzhonglei where  mingcheng='" + qitazhengjianzhonglei.Text + "'")
                                 + "',zjhm='" + qitazhengjianhaoma.Text
                                 + "',caijirendaima='" + quanjubianliang.cjrbh

                                 + "' where rycjbh='" + quanjubianliang.rybh + "'";
                    gj.sqliteexcu(sql);
                    if (m_IDcard)//判断是否有证件照
                    {
                        gj.savePicture(@"zp.bmp", "update renyuanjibenxinxi set shenfengzshengzhaopian=@data where rycjbh='" + quanjubianliang.rybh + "'");
                    }
                    //MessageBox.Show("更新成功");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int iPort;
                for (iPort = 1001; iPort <= 1016; iPort++)
                {
                    iRetUSB = CVRSDK.CVR_InitComm(iPort);
                    if (iRetUSB == 1)
                    {
                        break;
                    }
                }
                if (iRetUSB != 1)
                {
                    for (iPort = 1; iPort <= 4; iPort++)
                    {
                        iRetCOM = CVRSDK.CVR_InitComm(iPort);
                        if (iRetCOM == 1)
                        {
                            break;
                        }
                    }
                }
                if ((iRetCOM == 1) || (iRetUSB == 1))
                {
                    //MessageBox.Show("身份证阅读器初始化成功！");
                    quanjubianliang.sfzcsh = 1;
                }
                else
                {
                    MessageBox.Show("身份证阅读器初始化失败！");
                    return;
                }
                if ((iRetCOM == 1) || (iRetUSB == 1))
                {

                    int authenticate = CVRSDK.CVR_Authenticate();
                    if (authenticate == 1)
                    {
                        int readContent = CVRSDK.CVR_Read_Content(4);
                        if (readContent == 1)
                        {
                            m_IDcard = true;//读取身份证成功
                            FillData();

                        }
                        else
                        {
                            MessageBox.Show("读卡操作失败！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("未放卡或卡片放置不正确");
                    }
                }
                else
                {
                    MessageBox.Show("初始化失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void jinbenxinxi_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!checkBox1.Checked)
            {
                if (!DetectionOptions())
                {
                    e.Cancel = true;//就不退了
                    quanjubianliang.interrupt = -1;
                    return;
                }
                else
                {

                }
            }
            else
            {
                e.Cancel = false;//退了
                quanjubianliang.interrupt = 0;

            }

            try
                {
                    CVRSDK.CVR_CloseComm();
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "renyuanjibenxinxi") > 0)
            {
                //if (m_flag)//点击了修改
                updatevalue();//更新数据库
            }
            else
            {
                writevalue();//写入数据库
            }


        }
        //检测必填项
        private bool DetectionOptions()
        {
            if(quanjubianliang.i_query==1)
            { 

            if ("" == renyuanbianhao.Text)
            {
                MessageBox.Show("人员编号不能为空");
                renyuanbianhao.Focus();
                return false;
            }
            else if ("" == xingming.Text)
            {
                MessageBox.Show("姓名不能为空");
                xingming.Focus();
                return false;
            }
            else if ("" == shenfenzhenghao.Text)
            {
                MessageBox.Show("身份证不能为空");
                shenfenzhenghao.Focus();
                return false;
            }
            else if ("" == hujidixingzhengquhua.Text || "" == xianjuzhudixingzhengquhua.Text || "" == hujidizhi.Text )
            {
                MessageBox.Show("户籍地不能为空");
                return false;
            }
            else if ("" == hujidixiangxidizhi.Text || "" == xianjuzhudixiangxidizhi.Text)
            {
                MessageBox.Show("行政区划不能为空");
                return false;
            }
            else if ("" == zhiye.Text)
            {
                MessageBox.Show("职业不能为空");
                zhiye.Focus();
                return false;
            }
            else if ("" == renyuanleibie.Text)
            {
                MessageBox.Show("人员类别不能为空");
                renyuanleibie.Focus();
                return false;
            }
            else if ("" == caijiyuanyin.Text)
            {
                MessageBox.Show("采集原因不能为空");
                caijiyuanyin.Focus();
                return false;
            }
            else if ("" == gongzuodanwei.Text)
            {
                MessageBox.Show("工作单位不能为空");
                gongzuodanwei.Focus();
                return false;
            }
            else if ("" == shoujihaoma.Text)
            {
                MessageBox.Show("手机号码不能为空");
                shoujihaoma.Focus();
                return false;
            }
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "renyuanjibenxinxi") > 0)
            {
                //if (m_flag)//点击了修改
                updatevalue();//更新数据库
            }
            else
            {
                writevalue();//写入数据库
            }
        }

        public void FillData()
        {
            try
            {
                //showbox.ImageLocation = Application.StartupPath + "\\zp.bmp";
                showbox.ImageLocation = @"zp.bmp";
                showbox.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                if (System.IO.File.Exists(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_IDCARD.JPG"))
                {
                    System.IO.File.Delete(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_IDCARD.JPG");
                }
                File.Copy(@"zp.bmp", @"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_IDCARD.JPG");//将身份证图片存入ZIP
                byte[] name = new byte[30];
                int length = 1;
                CVRSDK.GetPeopleName(ref name[0], ref length);
                 xingming.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();
                //MessageBox.Show();
                byte[] number = new byte[30];
                length = 1;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);

                byte[] people = new byte[30];
                length = 1;
                CVRSDK.GetPeopleNation(ref people[0], ref length);
                byte[] validtermOfStart = new byte[30];
                length = 1;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);
                quanjubianliang.sfzyxqkssj= System.Text.Encoding.Default.GetString(validtermOfStart).Replace(".", "").Replace("\0", "");

                byte[] birthday = new byte[30];
                length = 1;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);
                byte[] address = new byte[60];
                length = 1;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);
                byte[] validtermOfEnd = new byte[30];
                length = 1;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);
                quanjubianliang.sfzyxqjssj = System.Text.Encoding.Default.GetString(validtermOfEnd).Replace(".", "").Replace("\0", "");

                byte[] signdate = new byte[30];
                length = 1;
                CVRSDK.GetDepartment(ref signdate[0], ref length);
                byte[] sex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] samid = new byte[32];
                CVRSDK.CVR_GetSAMID(ref samid[0]);

                shenfenzhenghao.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                hujidizhi.Text=   hujidixiangxidizhi.Text = xianjuzhudixiangxidizhi.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                xianjuzhudixingzhengquhua.Text= hujidixingzhengquhua.Text = gj.getback("select mingcheng from zd_hujidixingzhengquhua where bianhao ='" + shenfenzhenghao.Text.Substring(0, 6) + "'");
                xingbie.Text= System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
               

                chushengriqi.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                //Office.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();


                string lblNation = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                minzu.Text = lblNation + "族";
                //label11.Text = "安全模块号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(samid).Replace("\0", "").Trim();
                //lblValidDate.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //生成随机采集编号
        private string getRYJCXXCJBH()
        {
            int a = 0;
            string personid = "";
            do
            {
                string cjdwdm = gj.getback("select Address from yonghu where id='" + quanjubianliang.cjrbh + "'");
                a = a + 1;
                string year = DateTime.Now.Year.ToString();             // 获取年份 
                string month = DateTime.Now.Month.ToString();  // 获取月份   
                if (month.Length == 1)
                {
                    month = "0" + month;

                }
                string countmounth = (Convert.ToInt32(gj.getback("select count(*) from renyuanjibenxinxi where caijishijian between datetime('now','start of month','+1 second') and datetime('now','start of month','+1 month','-1 second')  ")) + a).ToString();
                Int32 clength = countmounth.Length;
                for (int i = 0; i < 4 - clength; i++)
                {
                    countmounth = "0" + countmounth;
                }
                personid = "R" + cjdwdm + year + month + countmounth;
            }
            while (Convert.ToInt32(gj.getback("select count(*) from renyuanjibenxinxi where rycjbh='" + personid + "'")) > 0);

            return personid;


        }
        private void GMSFHM_Validated(object sender, EventArgs e)
        {
            //有身份证验证
            if (!checkBox1.Checked)
            {
                TextBox value = (TextBox)sender;
                if ("" == value.Text.ToString())
                {
                    MessageBox.Show("身份证不能为空,请输入");
                    return;
                }
                else if (!(CheckIDCard15(value.Text.ToString()) || CheckIDCard18(value.Text.ToString())))
                {
                    MessageBox.Show("身份证不正确,请重新输入");
                    value.Focus();
                    value.Text = "";
                    return;
                }
                if (" " != value.Text)
                {
                    //获取身份证号中的出生日期
                    if (18 == value.Text.Length)
                    {
                        chushengriqi.Text = value.Text.Substring(6, 8);
                    }
                    else
                    {
                        chushengriqi.Text = "19" + value.Text.Substring(6, 6);
                    }
                    chushengriqi.Text = chushengriqi.Text.Insert(4, "-");
                    chushengriqi.Text = chushengriqi.Text.Insert(7, "-");
                    xianjuzhudixingzhengquhua.Text = hujidixingzhengquhua.Text = gj.getback("select mingcheng from zd_hujidixingzhengquhua where bianhao ='" + shenfenzhenghao.Text.Substring(0, 6) + "'");
                    //获取身份证号中的行政区划
                    //JGSSXDM.Text = HJDSSXDM.Text = gj.getback("select name from DICT_XZQH where code ='" + GMSFHM.Text.Substring(0, 6) + "'");
                }
            }
        }
        /******************失去身份证焦点***************/
        //18位身份证号
        private static bool CheckIDCard18(string idNumber)
        {
            if (idNumber.Length < 18)
                return false;
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准
        }
        //15位身份证号 例如:130503670401001
        private static bool CheckIDCard15(string idNumber)
        {
            if (idNumber.Length < 15)
                return false;
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

    }
}
