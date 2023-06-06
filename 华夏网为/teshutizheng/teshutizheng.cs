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
using AForge.Video.DirectShow;

namespace 华夏网为.teshutizheng
{
    public partial class teshutizheng : Form
    {
        FilterInfoCollection videoDevices;
        AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        Appcode.Class2 gj = new Appcode.Class2();
        byte[] by = null; //判断是否进行拍照
        public teshutizheng()
        {
            InitializeComponent();
        }

        private void teshutizheng_Load(object sender, EventArgs e)
        {
            videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            videoSourcePlayer.Height = Player.Height;
            videoSourcePlayer.Width = Player.Width;
            Player.Controls.Add(videoSourcePlayer);
            //videoSourcePlayer.SendToBack();
            //遍历获取可操作的所有摄像头的名称
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            openCamera();
            this.bianhao.Text = quanjubianliang.rybh;
            ShowInfo();
            bianhao.Text = getWPBH();//重新获取编号
            //读取数据库数据，并显示到界面
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "teshutezheng") > 0)
            {
               
                readvalue();
            }
        }
        //生成随机物品编号
        private string getWPBH()
        {
            string year = DateTime.Now.Year.ToString();             // 获取年份 
            string month = DateTime.Now.Month.ToString();  // 获取月份   
            if (month.Length == 1)
            {
                month = "0" + month;

            }
            //string countmounth = gj.getback("select count(*) from LEDEN_COLLECT_PERSON where CJSJ between datetime('now','start of month','+1 second') and datetime('now','start of month','+1 month','-1 second') AND CJDWDM='" + helpuser.USER_UNIT_CODE + "' ");
            int seed = Guid.NewGuid().GetHashCode();//随机种子
            Random rd = new Random(seed);
            int cou = rd.Next(1000, 9999);
            string countmounth = cou.ToString();
            Int32 clength = countmounth.Length;
            for (int i = 0; i < 4 - clength; i++)
            {
                countmounth = "0" + countmounth;
            }

            return "T" + quanjubianliang.rybh + year + month + countmounth;
        }
        //打开摄像头相机
        private void openCamera()
        {
            try
            {
                XmlDocument camname = new XmlDocument();
                camname.Load(@"config/gaopaiyi.xml");
                string cname = camname.SelectSingleNode("//camname//names1").InnerText.ToString();

                if (videoDevices.Count > 0)
                {
                    for (int i = 0; i < videoDevices.Count; i++)
                    {
                        if (videoDevices[i].Name == cname)
                        {
                            VideoCaptureDevice source = new VideoCaptureDevice(videoDevices[i].MonikerString);
                            //设置设备的分辨率
                            for (int j = 0; j < source.VideoCapabilities.Length; j++)
                            {
                                if (source.VideoCapabilities[j].FrameSize.Width == 1920 && source.VideoCapabilities[j].FrameSize.Height == 1080)
                                {
                                    source.VideoResolution = source.VideoCapabilities[j];
                                }
                            }
                            videoSourcePlayer.SignalToStop();
                            videoSourcePlayer.WaitForStop();
                            videoSourcePlayer.VideoSource = source;
                            videoSourcePlayer.Start();

                            quanjubianliang.sxtsfdk = 1;

                            btnTakingPictures.Enabled = true;

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTakingPictures_Click(object sender, EventArgs e)
        {
            // 捕获的图像
            Bitmap b = videoSourcePlayer.GetCurrentVideoFrame();

            Bitmap c = GetBitmap(b);
            c.Save(@"tstz.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
           

            b.Dispose();
            c.Dispose();
            ImagePath(@"tstz.jpg",this.picShowImg );
        }
        public Bitmap GetBitmap(Bitmap old)
        {
            string hw = GetImageSize(old.Width, old.Height);
            string[] aryhw = hw.Split(';');
            int twidth = Convert.ToInt32(aryhw[0]);
            int theight = Convert.ToInt32(aryhw[1]);
            //新建一个bmp图片                                          
            System.Drawing.Bitmap timage = new System.Drawing.Bitmap(640, 480);
            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(timage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFFFFF"));

            Size size = new Size(twidth, theight);
            Rectangle rect = new Rectangle(new Point((640 - twidth) / 2, (480 - theight) / 2), size);
            g.DrawImage(old, rect, new Rectangle(0, 0, old.Width, old.Height), GraphicsUnit.Pixel);

            return timage;
        }
        /// <summary>
        /// 修改图片尺寸
        /// </summary>
        /// <param name="LoadImgW">宽</param>
        /// <param name="LoadImgH">高</param>
        /// <returns></returns>
        public string GetImageSize(int LoadImgW, int LoadImgH)
        {
            int xh = 0;
            int xw = 0;
            //容器高与宽
            int oldW = 640;
            int oldH = 640;
            //图片的高宽与容器的相同
            if (LoadImgH == oldH && LoadImgW == (oldW))
            {//1.正常显示 
                xh = LoadImgH;
                xw = LoadImgW;
            }
            if (LoadImgH == oldH && LoadImgW > (oldW))
            {//2、原高==容高，原宽>容宽 以原宽为基础 
                xw = (oldW);
                xh = LoadImgH * xw / LoadImgW;
            }
            if (LoadImgH == oldH && LoadImgW < (oldW))
            {//3、原高==容高，原宽<容宽  正常显示    
                xw = LoadImgW;
                xh = LoadImgH;
            }
            if (LoadImgH > oldH && LoadImgW == (oldW))
            {//4、原高>容高，原宽==容宽 以原高为基础    
                xh = oldH;
                xw = LoadImgW * xh / LoadImgH;
            }
            if (LoadImgH > oldH && LoadImgW > (oldW))
            {//5、原高>容高，原宽>容宽            
                if ((LoadImgH / oldH) > (LoadImgW / (oldW)))
                {//原高大的多，以原高为基础 
                    xh = oldH;
                    xw = LoadImgW * xh / LoadImgH;
                }
                else
                {//以原宽为基础 
                    xw = (oldW);
                    xh = LoadImgH * xw / LoadImgW;
                }
            }
            if (LoadImgH > oldH && LoadImgW < (oldW))
            {//6、原高>容高，原宽<容宽 以原高为基础         
                xh = oldH;
                xw = LoadImgW * xh / LoadImgH;
            }
            if (LoadImgH < oldH && LoadImgW == (oldW))
            {//7、原高<容高，原宽=容宽 正常显示        
                xh = LoadImgH;
                xw = LoadImgW;
            }
            if (LoadImgH < oldH && LoadImgW > (oldW))
            {//8、原高<容高，原宽>容宽 以原宽为基础     
                xw = (oldW);
                xh = LoadImgH * xw / LoadImgW;
            }
            if (LoadImgH < oldH && LoadImgW < (oldW))
            {//9、原高<容高，原宽<容宽//正常显示     
                xh = LoadImgH;
                xw = LoadImgW;
            }
            return xw + ";" + xh;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.picShowImg.Image == null)
                {
                    MessageBox.Show("请先拍照");
                    return;
                }
                    if (tezhengmingcheng.SelectedIndex == -1)
                {
                    MessageBox.Show("特征名称不能不空!请选择");
                    return;
                }
                if (tezhengbuwei.SelectedIndex == -1)
                {
                    MessageBox.Show("体貌部位不能不空!请选择");
                    return;
                }
                if (tezhengfangwei.SelectedIndex == -1)
                {
                    MessageBox.Show("体貌方位不能不空!请选择");
                    return;
                }
                if (tezhengshuliang.SelectedIndex == -1)
                {
                    MessageBox.Show("数量不能不空!请选择");
                    return;
                }
                if (tezhengyanse.SelectedIndex == -1)
                {
                    MessageBox.Show("颜色不能不空!请选择");
                    return;
                }

               
              
                string bianhao = this.bianhao.Text;
                string tzmc = tezhengmingcheng.Text.ToString();//特征名称
                string bw = tezhengbuwei.Text.ToString();//部位
                string fw = tezhengfangwei.Text.ToString();//方位
                string sl = tezhengshuliang.Text.ToString();//数量
                string ys = tezhengyanse.Text.ToString();//颜色
                string size = tezhengchicun.Text;
                string Remark = beizhu.Text;
                
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.Cells[0].Value = bianhao;
                dr.Cells[1].Value = tzmc;
                dr.Cells[2].Value = bw;
                dr.Cells[3].Value = fw;
                dr.Cells[4].Value = sl;
                dr.Cells[5].Value =ys ;
                dr.Cells[6].Value = size;
                dr.Cells[7].Value = Remark;
                this.dataGridView1.Rows.Add(dr);
                string Path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + bianhao+ ".jpg";
                this.picShowImg.Image.Save(Path);
                   
                   
                    string sql = string.Format(@"INSERT INTO teshutezheng (rycjbh,tezhengshuliang,tezhengmingcheng,tezhengyanse,tezhengbuwei,tezhengfangwei,tezhengdaxiao,bianhao,beizhu)VALUES('"
                                                + quanjubianliang.rybh + "','"
                                                + gj.getback("select bianhao from zd_tezhengshuliang where mingcheng ='" + sl + "'") + "','"
                                                + gj.getback("select bianhao from zd_tezhengmingcheng where mingcheng='" + tzmc + "'") + "','"
                                                + gj.getback("select bianhao from zd_tezhengyanse where mingcheng='" + ys + "'") + "','"
                                                + gj.getback("select bianhao from zd_tezhengbuwei where mingcheng='" + bw + "'") + "','"
                                                + gj.getback("select bianhao from zd_tezhengfangwei where mingcheng='" + fw + "'") + "','"
                                                + size + "','"
                                                + bianhao + "','"
                                                + Remark + "') ");
                    int res = gj.sqliteexcu(sql);
                    string sql2 = string.Format("update teshutezheng set tezhengtupian=@data where bianhao='{0}'", bianhao);
                    gj.savePicture1(gj.SaveImage(Path), sql2);
                    if (res > 0)
                    {
                        MessageBox.Show("保存成功!");
                    
                        //flowLayoutPanel1.Controls.Clear();
                        //List.Clear();
                    }
                    else
                    {
                        MessageBox.Show("保存失败!");
                    }
                this.picShowImg.Image = null;
                this.bianhao.Text = getWPBH();//重新获取编号
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //根据点击的行不同获取数据库中不同的图像数据
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Selected)
                {
                    string WPBH = this.dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string sql = "select * from teshutezheng where bianhao='" + WPBH + "'";
                   
                    foreach (DataRow rs in gj.gettable(sql).Rows)
                    {
                        if ("" != rs["tezhengtupian"].ToString() || null != rs["tezhengtupian"])
                        {
                            byte[] img = (byte[])rs["tezhengtupian"];
                            picShowImg.Image = Image.FromStream(new MemoryStream(img));
                            picShowImg.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                        }
                    }
                }
            }
        }
        //删除
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dataGridView1.Columns[e.ColumnIndex].Name == "删除")
                {
                    int a = dataGridView1.CurrentRow.Index;
                    string WPBH = dataGridView1.Rows[a].Cells[0].Value.ToString();
                    string sql = "DELETE FROM teshutezheng WHERE bianhao='" + WPBH + "'";
                   
                    //同时删除原图
                    if (System.IO.File.Exists(@"ZIP//" + quanjubianliang.rybh + "//" + WPBH + ".jpg"))
                    {
                        System.IO.File.Delete(@"ZIP//" + quanjubianliang.rybh + "//" + WPBH + ".jpg");
                    }
                   
                    this.dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        //读取特征点
        private void readvalue()
        {
         
            try
            {
                

                string sqltrait1 = "select * from teshutezheng where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sqltrait1).Rows)
                {
                    if ("" != rs[7].ToString())//查看标记位是否存在
                    {
                        //MessageBox.Show(rs[7].ToString());
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(dataGridView1);
                        dr.Cells[0].Value = rs["bianhao"];
                        dr.Cells[1].Value = gj.getback("select mingcheng from zd_tezhengmingcheng where bianhao='" + rs["tezhengmingcheng"] + "'");
                        dr.Cells[2].Value = gj.getback("select mingcheng from zd_tezhengbuwei where bianhao='" + rs["tezhengbuwei"] + "'");
                        dr.Cells[3].Value = gj.getback("select mingcheng from zd_tezhengfangwei where bianhao='" + rs["tezhengfangwei"] + "'");
                        dr.Cells[4].Value = gj.getback("select mingcheng from zd_tezhengshuliang where bianhao='" + rs["tezhengshuliang"] + "'");
                        dr.Cells[5].Value = gj.getback("select mingcheng from zd_tezhengyanse where bianhao='" + rs["tezhengyanse"] + "'");
                        dr.Cells[6].Value =  rs["tezhengdaxiao"];
                        dr.Cells[8].Value = rs["beizhu"];

                        this.dataGridView1.Rows.Add(dr);

                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// 判断byte是否为空
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static Boolean IsNull(byte[] bs)
        {
            if (bs == null || bs.Length == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 绑定数据
        /// 
        /// </summary>
        private void ShowInfo()
        {
            string sql = string.Format(@"SELECT * FROM zd_tezhengbuwei");
            string sql1 = string.Format(@"SELECT * FROM zd_tezhengfangwei");
            string sql2 = string.Format(@"SELECT * FROM zd_tezhengmingcheng");
            string sql3 = string.Format(@"SELECT * FROM zd_tezhengshuliang");
            string sql4 = string.Format(@"SELECT * FROM zd_tezhengyanse");
            //方位
            DataTable dt = gj.gettable(sql);
            tezhengbuwei.DataSource = dt;
            tezhengbuwei.DisplayMember = "mingcheng";
            tezhengbuwei.ValueMember = "bianhao";
            tezhengbuwei.SelectedValue = "-1";
            tezhengbuwei.Text = "请选择";


            //部位
            DataTable dt1 = gj.gettable(sql1);
            tezhengfangwei.DataSource = dt1;
            tezhengfangwei.DisplayMember = "mingcheng";
            tezhengfangwei.ValueMember = "bianhao";
            tezhengfangwei.SelectedValue = "-1";
            tezhengfangwei.Text = "请选择";
//特征名称
            
            DataTable dt2 = gj.gettable(sql2);
            tezhengmingcheng.DataSource = dt2;
            tezhengmingcheng.DisplayMember = "mingcheng";
            tezhengmingcheng.ValueMember = "bianhao";
            tezhengmingcheng.SelectedValue = "-1";
            tezhengmingcheng.Text = "请选择";
//数量
            
            DataTable dt3 = gj.gettable(sql3);
            tezhengshuliang.DataSource = dt3;
            tezhengshuliang.DisplayMember = "mingcheng";
            tezhengshuliang.ValueMember = "bianhao";
            tezhengshuliang.SelectedValue = "-1";
            tezhengshuliang.Text = "请选择";

            //颜色
            DataTable dt4 = gj.gettable(sql4);
            tezhengyanse.DataSource = dt4;
            tezhengyanse.DisplayMember = "mingcheng";
            tezhengyanse.ValueMember = "bianhao";
            tezhengyanse.SelectedValue = "-1";
            tezhengyanse.Text = "请选择";
         

        }
        //获取图片路径(并显示在采集框里)
        private void ImagePath(string filePath, System.Windows.Forms.PictureBox showbox)
        {
            if (!System.IO.File.Exists(filePath)) //如果不存在
            {
                //MessageBox.Show(filePath + "不存在！");
                return;
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);

            //文件流关闭,文件解除锁定
            fileStream.Close();

            showbox.Image = Image.FromStream(new MemoryStream(fileBytes));
            showbox.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
        }

        private void teshutizheng_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.Stop();//停止获取摄像头资源
            }
        }
    }
}
