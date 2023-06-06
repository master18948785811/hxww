using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.wuzheng
{
    public partial class wuzheng : Form
    {
        FilterInfoCollection videoDevices;
        AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        Appcode.Class2 gj = new Appcode.Class2();
        byte[] by = null; //判断是否进行拍照
        public wuzheng()
        {
            InitializeComponent();
        }

        private void wuzheng_Load(object sender, EventArgs e)
        {
            videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            videoSourcePlayer.Height = Player.Height;
            videoSourcePlayer.Width = Player.Width;
            Player.Controls.Add(videoSourcePlayer);
            //videoSourcePlayer.SendToBack();
            //遍历获取可操作的所有摄像头的名称
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            openCamera();
            ShowInfo();
            bianhao.Text = getWPBH();//重新获取编号
            //读取数据库数据，并显示到界面
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "wuzheng") > 0)
            {

                readvalue();
            }
        }
        /// <summary>
        /// 绑定数据
        /// 
        /// </summary>
        private void ShowInfo()
        {
          
          
            string sql2 = string.Format(@"SELECT * FROM zd_wuzhengleibie");
            string sql3 = string.Format(@"SELECT * FROM zd_tezhengshuliang");
            string sql4 = string.Format(@"SELECT * FROM zd_tezhengyanse");
        

            //部位
            DataTable dt1 = gj.gettable(sql2);
            leibie.DataSource = dt1;
            leibie.DisplayMember = "mingcheng";
            leibie.ValueMember = "bianhao";
            leibie.SelectedValue = "-1";
            leibie.Text = "请选择";
          
            //数量

            DataTable dt3 = gj.gettable(sql3);
            shuliang.DataSource = dt3;
            shuliang.DisplayMember = "mingcheng";
            shuliang.ValueMember = "bianhao";
            shuliang.SelectedValue = "-1";
            shuliang.Text = "请选择";

            //颜色
            DataTable dt4 = gj.gettable(sql4);
            yanse.DataSource = dt4;
            yanse.DisplayMember = "mingcheng";
            yanse.ValueMember = "bianhao";
            yanse.SelectedValue = "-1";
            yanse.Text = "请选择";


        }
        //打开摄像头相机
        private void openCamera()
        {
            try
            {
                XmlDocument camname = new XmlDocument();
                camname.Load(@"config/gaopaiyi.xml");
                string cname = camname.SelectSingleNode("//camname//names2").InnerText.ToString();

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

                            quanjubianliang.sxwzdk = 1;

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
            c.Save(@"wuzheng.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            c.Dispose();
            b.Dispose();
            ImagePath(@"wuzheng.jpg", this.picShowImg);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.picShowImg.Image == null)
            {
                MessageBox.Show("请先进行拍照!");

                return;
            }
            try
            {
                if (mingcheng.Text == "")
                {
                    MessageBox.Show("请填写名称!");
                    return;
                }
                if (yanse.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择物品颜色!");
                    return;
                }
                if (leibie.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择物品类型!");
                    return;
                }
                if (shuliang.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择物品数量!");
                    return;
                }
                string PersonID = quanjubianliang.rybh;//人员编号
                string GoodsID = bianhao.Text; //编号
                string GoodsName = mingcheng.Text;//名称
                string GoodsIdentification = biaoshi.Text;//标识
                string GoodsType = leibie.Text;//随身物品类形代码
                string GoodsFeatures = tezheng.Text;  //特征
                string GoodsCost = jiazhi.Text;//价值
                string GoodsColor = yanse.Text;//颜色代码
                string GoodsNum = shuliang.Text;//数量代码
                string GoodsWeight = zhongliang.Text;//重量
                string GoodsBuyTime = goumaishijian.Text;//购买时间
        
                string GoodsRemarke = beizhu.Text;//备注
                if (!IsDate(GoodsBuyTime))
                {
                    MessageBox.Show("时间格式不正确，请以2000-01-01为例");
                    return;
                }
                string Path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + GoodsID + ".jpg";
                this.picShowImg.Image.Save(Path);
                this.picShowImg.Image.Dispose();
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dgvList);
                dr.Cells[0].Value = GoodsID;
                dr.Cells[1].Value = GoodsName;
                dr.Cells[2].Value = GoodsIdentification;
                dr.Cells[3].Value = GoodsType;
                dr.Cells[4].Value = GoodsFeatures;
                dr.Cells[5].Value = GoodsCost;
                dr.Cells[6].Value = GoodsColor;
                dr.Cells[7].Value = GoodsNum;
                dr.Cells[8].Value = GoodsWeight;
                dr.Cells[9].Value = GoodsBuyTime;
                dr.Cells[10].Value = GoodsRemarke;
               
                this.dgvList.Rows.Add(dr);

                string sql = string.Format(@"INSERT INTO wuzheng(bianhao,rycjbh,mingcheng,leibie,biaoshi,tezheng,jiazhi,yanse,shuliang,zhongliang,goumaishijian,beizhu)VALUES('"
                                              + GoodsID + "','"
                                              + PersonID + "'" + ",'"
                                              + GoodsName + "','"
                                              + gj.getback("select bianhao from zd_wuzhengleibie where mingcheng='" + GoodsType + "'") + "','"
                                              + GoodsIdentification + "','"
                                              + GoodsFeatures + "','"
                                              + GoodsCost + "'" + ",'"
                                              + gj.getback("select  bianhao from zd_tezhengyanse where  mingcheng='" + GoodsColor + "'")+ "','"
                                              + gj.getback("select  bianhao from zd_tezhengshuliang where  mingcheng='" + GoodsNum + "'") + "','"
                                              + GoodsWeight + "','"
                                              + GoodsBuyTime + "','"
                                           
                                              + GoodsRemarke + "')");
               
                gj.sqliteexcu(sql);
                
              
                //存对应物品类型表
                string sql4 = string.Format("UPDATE wuzheng SET tupian=@data WHERE bianhao='{0}'", GoodsID);
                gj.savePicture1(gj.SaveImage(Path), sql4);
                this.picShowImg.Image = null;
                bianhao.Text = getWPBH();//重新获取编号
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 是否为日期型字符串
        /// </summary>
        /// <param name="StrSource">日期字符串(2008-05-08)</param>
        /// <returns></returns>
        public static bool IsDate(string StrSource)
        {
            return Regex.IsMatch(StrSource, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
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

            return "W" + quanjubianliang.rybh + year + month + countmounth;
        }
        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //根据点击的行不同获取数据库中不同的图像数据
            for (int i = 0; i < this.dgvList.Rows.Count; i++)
            {
                if (this.dgvList.Rows[i].Selected)
                {
                    string WPBH = this.dgvList.Rows[i].Cells[0].Value.ToString();
                    string sql = "select * from wuzheng where bianhao='" + WPBH + "'";
                  
                    foreach (DataRow rs in gj.gettable(sql).Rows)
                    {
                        if ("" != rs["tupian"].ToString() || null != rs["tupian"])
                        {
                            byte[] img = (byte[])rs["tupian"];
                            picShowImg.Image = Image.FromStream(new MemoryStream(img));
                            picShowImg.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                        }
                    }
                }
            }
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dgvList.Columns[e.ColumnIndex].Name == "删除")//删除
                {
                    if (MessageBox.Show("确定要删除该数据？", "警告!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int a = dgvList.CurrentRow.Index;
                        string WPBH = dgvList.Rows[a].Cells[0].Value.ToString();
                        string sql = "DELETE FROM wuzheng WHERE bianhao='" + WPBH + "'";
                       

                        int res = gj.sqliteexcu(sql);
                     
                        if (res > 0)
                        {
                            MessageBox.Show("删除成功!");
                            string Path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + quanjubianliang.rybh + "//" + WPBH + ".jpg";
                            if (System.IO.File.Exists(Path))
                            {
                                System.IO.File.Delete(Path);
                            }
                            //BindGoodsAll();
                            for (int i = this.dgvList.SelectedRows.Count; i > 0; i--)
                            {
                                this.dgvList.Rows.RemoveAt(dgvList.SelectedRows[i - 1].Index);
                            }

                        }
                        else
                        {
                            MessageBox.Show("删除失败!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //读取特征点
        private void readvalue()
        {

            try
            {


                string sqltrait1 = "select * from wuzheng where rycjbh='" + quanjubianliang.rybh + "'";
        
                foreach (DataRow rs in gj.gettable(sqltrait1).Rows)
                {
                    if ("" != rs["tupian"].ToString())//查看标记位是否存在
                    {
                   
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(dgvList);
                        dr.Cells[0].Value = rs["bianhao"];
                        dr.Cells[1].Value = rs["mingcheng"]; 
                        dr.Cells[2].Value = rs["biaoshi"];
                        dr.Cells[3].Value = gj.getback("select mingcheng from zd_tezhengshuliang where bianhao='" + rs["shuliang"] + "'");
                        dr.Cells[4].Value = rs["tezheng"]; ;
                        dr.Cells[5].Value = rs["jiazhi"]; ;
                        dr.Cells[6].Value = gj.getback("select mingcheng from zd_tezhengyanse where bianhao='" + rs["yanse"] + "'");
                        dr.Cells[7].Value = gj.getback("select mingcheng from zd_wuzhengleibie where bianhao='" + rs["leibie"] + "'");
                        dr.Cells[8].Value = gj.getback("select mingcheng from zd_tezhengshuliang where bianhao='" + rs["shuliang"] + "'");
                        dr.Cells[9].Value = rs["zhongliang"];
                        dr.Cells[10].Value = rs["goumaishijian"];
                        dr.Cells[11].Value = rs["beizhu"];

                        this.dgvList.Rows.Add(dr);

                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void wuzheng_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.Stop();//停止获取摄像头资源
            }
        }

    }
}
