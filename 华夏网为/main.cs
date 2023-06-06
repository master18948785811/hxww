using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为
{
    public partial class main : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public main()
        {
            InitializeComponent();
           
        }

        private void main_Load(object sender, EventArgs e)
        {
            
           
            string str2 = gj.MD5Encrypt(gj.GetMAC(), 32);
            string str1 = File.ReadAllText(@"secret.lsn").Trim();
            getconfing();
            getsb();

            if (!str1.Equals(str2))
            {
                MessageBox.Show("软件授权已过期请联系相厂家。");
                Application.Exit();
            }
            else
            {
                //本地登陆,添加用户
                if (quanjubianliang.states == 1)
                {
                    string str = "";
                    if (quanjubianliang.djms == 0)
                    {
                        str = "select count(*) from shebaizhuce where mac='" + gj.GetMAC() + "'  and IP='" + gj.GetIP() + "'";
                    }
                    else
                    {
                        str = "select count(*) from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "' ";
                    }
                    if (Convert.ToInt32(gj.getback(str)) == 0)
                    {
                        yonghuguangli.shebeizhuce zc = new yonghuguangli.shebeizhuce();
                        zc.ShowDialog();
                    }
                }
                yonghuguangli.denglu log = new yonghuguangli.denglu();
                log.ShowDialog();
                if (log.key == "0")
                {
                    this.DesktopBounds = Screen.GetWorkingArea(this);

                    this.CurrentTime.Text = "当前日期：" + System.DateTime.Now.ToString("yyyy-MM-dd");
                    this.UserName.Text = "使用人：" + gj.getback("select PersonalName from yonghu where id=" + quanjubianliang.cjrbh.ToString() + "");
                    this.WarningSign.Text = "警号：" + gj.getback("select PoliceNumber from yonghu where id=" + quanjubianliang.cjrbh.ToString() + "");
                    this.Unit.Text = "单位：" + gj.getback("select UnitName from yonghu where id=" + quanjubianliang.cjrbh.ToString() + "");
                    getmenus();
                }
                else
                {

                    Application.Exit();
                }


            }
        }
        private void getmenus()
        {

            PictureBox pic;
            int left1 = 0;
            int km = 0;
            string sql = "select * from daohang WHERE shifuoxianshi='1' order by id";
            DataTable db = gj.gettable(sql);
            foreach (DataRow rs in db.Rows)
            {
                if (Convert.ToInt32(rs["shifuoxianshi"].ToString()) == 1)
                {
                    if (km >0)
                    {
                        pic = new PictureBox();
                        pic.BackgroundImage = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\" + rs["tupingmingcheng"].ToString() + ".png");
                        pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        pic.BackColor = Color.Transparent;
                        pic.Location = new Point(0, left1);
                        left1 = left1 + 60;
                        pic.Size = new System.Drawing.Size(360, 60);
                        pic.Name = rs["mingcheng"].ToString();
                        pic.Click += new EventHandler(pic_Click);
                        pic.Cursor = System.Windows.Forms.Cursors.Hand;
                        this.menus.Controls.Add(pic);
                    }

                    else
                    {
                        pic = new PictureBox();
                        pic.BackgroundImage = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\" + rs["tupingmingcheng"].ToString() + ".png");
                        pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        pic.BackColor = Color.Transparent;
                        pic.Location = new Point(0, left1);
                        left1 = left1 + 60;
                        pic.Size = new System.Drawing.Size(360, 60);
                        pic.Name = rs["mingcheng"].ToString();
                        pic.Click += new EventHandler(pic_Click);
                        pic.Cursor = System.Windows.Forms.Cursors.Hand;
                        this.menus.Controls.Add(pic);

                   
                        jinbenxinxi.jinbenxinxi form = new jinbenxinxi.jinbenxinxi();
                        form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                        form.TopLevel = false;
                        form.Width = this.loads.Width;
                        form.Height = this.loads.Height;//指示子窗体非顶级窗体
                        this.loads.Controls.Clear();//指示子窗体非顶级窗体
                        this.loads.Controls.Add(form);//将子窗体载入panel
                        form.Show();
                    }

                }
                km = km + 1;
            }

        }
        public void pic_Click(object sender, EventArgs e)
        {
            PictureBox b1 = (PictureBox)sender;

            changeb(b1.Name.ToString());
        }
        public void changeb(string names)
        {
            bool stop = true;
            System.Environment.CurrentDirectory = System.Windows.Forms.Application.StartupPath.ToString();
          
            foreach (Control ctrl in menus.Controls)
            {
                if (ctrl is PictureBox)
                {
                   

                    if (ctrl.Name.ToString() == "jibenxinxi")
                    {


                        if (ctrl.Name == names)
                        {

                                if (1 == quanjubianliang.i_query)//如果是同一编号切换采集界面，则调用上一界面的close
                                    stop = this.csl();
                                else if (0 == quanjubianliang.i_query)//若是获取不同编号查询界面的内容，则不调用当前界面的close，防止修改数据库
                                quanjubianliang.i_query = 1;
                                if (!stop)
                                {
                                    return;
                                }
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\jibenxinxi_01.png");
                            jinbenxinxi.jinbenxinxi form = new jinbenxinxi.jinbenxinxi();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();


                        }
                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\jibenxinxi.png");

                        }
                    }
                    if (ctrl.Name == "renxiang")
                    {

                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\renxiang_01.png");
                            sanmianzhao.sanmianozhao form = new sanmianzhao.sanmianozhao();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\renxiang.png");

                        }
                    }
                    if (ctrl.Name == "wuzheng")
                    {
                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\wuzheng_01.png");
                            wuzheng.wuzheng form = new wuzheng.wuzheng();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\wuzheng.png");

                        }
                    }
                    if (ctrl.Name == "zhiwen")
                    {
                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\zhiwen_01.png");
                            zhizhangwen.zhizhangwen form = new zhizhangwen.zhizhangwen();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\zhiwen.png");
                        }
                    }
                    if (ctrl.Name == "yinhangka")
                    {
                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\yinhangka_01.png");
                            yinhangka.yinhangka form = new yinhangka.yinhangka();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\yinhangka.png");
                        }
                    }
                    if (ctrl.Name.Trim() == "shouji")
                    {
                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\shouji_01.png");
                           shoujicaiji.shoujicaiji form = new shoujicaiji.shoujicaiji();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\shouji.png");
                        }
                    }
                    if (ctrl.Name == "timao")
                    {
                        if (ctrl.Name == names)
                        {
                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }

                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\timao_01.png");
                            teshutizheng.teshutizheng form = new teshutizheng.teshutizheng();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\timao.png");
                        }




                    }
                    if (ctrl.Name == "DNA")
                    {
                        if (ctrl.Name == names)
                        {
                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }

                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\DNA_01.png");
                            DNA.DNA form = new DNA.DNA();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\DNA.png");
                        }



                    }
                    if (ctrl.Name == "zuji")
                    {
                        if (ctrl.Name == names)
                        {
                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }

                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\zuji_01.png");
                            zuji.zuji form = new zuji.zuji();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\zuji.png");
                        }



                    }
                    if (ctrl.Name == "shengwen")
                    {
                        if (ctrl.Name == names)
                        {
                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }

                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\shengwen_01.png");
                            shengwen.shenwen form = new shengwen.shenwen();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\shengwen.png");
                        }



                    }
                    if (ctrl.Name == "hongmo")
                    {
                        if (ctrl.Name == names)
                        {
                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }

                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\hongmo_01.png");
                            hongmu.hongmu form = new hongmu.hongmu();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\hongmo.png");
                        }



                    }
                    if (ctrl.Name == "xinxi")
                    {
                        if (ctrl.Name == names)
                        {

                            stop = this.csl();
                            if (!stop)
                            {
                                return;
                            }
                             ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\xinxi_01.png");
                             xinxizonglan.xinxizonglan form = new xinxizonglan.xinxizonglan();
                            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
                            form.TopLevel = false;//指示子窗体非顶级窗体
                            form.Width = this.loads.Width;
                            form.Height = this.loads.Height;
                            this.loads.Controls.Clear();
                            this.loads.Controls.Add(form);//将子窗体载入panel
                            form.Show();
                        }



                        else
                        {
                            ((PictureBox)ctrl).Image = Image.FromFile(System.Windows.Forms.Application.StartupPath.ToString() + "\\images\\xinxi.png");
                        }



                    }


                }
                

            }
         


        }
        public bool csl()
        {
            foreach (Control ctrl in loads.Controls)
            {
                if (ctrl is Form)
                {
                    ((Form)ctrl).Close();
                    if (0 == quanjubianliang.interrupt)
                    {
                       
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        private void tuichudenglu_Click(object sender, EventArgs e)
        {
            //this.csl();

            //this.Close();
            System.Environment.Exit(0);
        }

        private void jiluchaxun_Click(object sender, EventArgs e)
        {
            query form2 = new query(this);
            form2.ShowDialog();
        }

        private void congxincaiji_Click(object sender, EventArgs e)
        {
            quanjubianliang.rybh = "";
            quanjubianliang.i_query = 1;
            changeb("jibenxinxi");
        }
        private void getconfing()
        {

            XmlDocument camname = new XmlDocument();
            camname.Load(@"config/confing.xml");
            quanjubianliang.states=Convert.ToInt32(camname.SelectSingleNode("//confing//states").InnerText.ToString());
           
        }
        private void getsb()
        {

            XmlDocument camname = new XmlDocument();
            camname.Load(@"yth.xml");
            quanjubianliang.ythsbid = camname.SelectSingleNode("//root//sbid").InnerText.ToString();
            quanjubianliang.djms = Convert.ToInt32(camname.SelectSingleNode("//root//djms").InnerText.ToString());
            quanjubianliang.webserviceip = camname.SelectSingleNode("//root//ip").InnerText.ToString();
      

        }
    }
}
