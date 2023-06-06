using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AForge.Video.DirectShow;
namespace 华夏网为.sanmianzhao
{
    public partial class sanmianozhao : Form
    {

        Appcode.Class2 gj = new Appcode.Class2();
        public static SerialPort myport = null;                  //串口句柄

        public static bool m_Breastplate = true;                 //胸牌
        public static bool m_BreastplateID = true;               //胸牌显示身份证
        public static UInt32 m_PortraitDirection = 1;            //人像方位
        bool m_SelectShoot = false;                            //选择拍摄
        FilterInfoCollection videoDevices;
        AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        public sanmianozhao()
        {
            InitializeComponent();
        }
        //打开形体
        private void OpenShape(object sender, EventArgs e)
        {
            XmlDocument camname = new XmlDocument();
            camname.Load(@"config/com.xml");

            myport = new SerialPort();
        

            myport.PortName = camname.SelectSingleNode("//coms//COM").InnerText.ToString();
            myport.BaudRate = int.Parse(camname.SelectSingleNode("//coms//BaudRate").InnerText.ToString());
            myport.DataBits = int.Parse(camname.SelectSingleNode("//coms//DataBits").InnerText.ToString());
            myport.Parity = System.IO.Ports.Parity.None;
            myport.DataReceived += new SerialDataReceivedEventHandler(port1_DataReceived);
            //打开串口的方法
            try
            {
                myport.Open();
                if (myport.IsOpen)
                {
                    this.gethew.Enabled = false;
                        SendCommand("1");
                }
                else
                {
                    MessageBox.Show("打开串口失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开串口失败!");
            }

        }
        public void SendCommand(string CommandString)
        {

            byte[] WriteBuffer = new byte[1];
            WriteBuffer[0] = 0x01;
            myport.Write(WriteBuffer, 0, 1);

        }
        private void port1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(1000);
            int temp = 0;
            try
            {
                
                    // string currentline = "";
                    string[] line = new string[10];
                    int data = 0;
                    //循环接收串口中的数据
                    while (myport.BytesToRead > 0)
                    {
                        data = myport.ReadByte();
                        line[temp] = data.ToString("x2");
                        temp++;
                    }
                //在这里对接收到的数据进行显示
                //如果不在窗体加载的事件里写上：Form.CheckForIllegalCrossThreadCalls = false; 就会报错）
                //将获取信息显示在界面上

                    this.Stature.Text = round(System.Convert.ToInt32("0x" + line[5] + line[6], 16).ToString().Substring(0, 4), 3);

                if (System.Convert.ToInt32("0x" + line[7] + line[8], 16).ToString().Length > 3)
                    {
                        this.Weight.Text = round(System.Convert.ToInt32("0x" + line[7] + line[8], 16).ToString().Substring(0, 4), 3);
                }
                    else
                    {
                        this.Weight.Text = round(System.Convert.ToInt32("0x" + line[7] + line[8], 16).ToString().Substring(0, 3), 2);
                }
                    this.FootLength.Text = round(System.Convert.ToInt32("0x" + line[3] + line[4], 16).ToString().Substring(0, 3), 2);

                //MessageBox.Show("接收数据完成");
                myport.Close();
                    gethew.Enabled = true;





            }
            catch (Exception ex)
            {
                MessageBox.Show("请先站人在采集！"+ex.ToString());
                gethew.Enabled = true;
                //MessageBox.Show("接收数据完成");
                myport.Close();
            }
            finally
            {
                myport.Close();
            }
        }

        private string getsj()
        {

            Random rnd = new Random();
            int iNum1 = rnd.Next(10);
            return iNum1.ToString();
        }
        //形体四舍五入
        private string round(string str, int count)
        {
            string value = "";
            string value1 = "";
            value = str.Substring(0, count);
            value1 = str.Substring(count, 1);
            if (int.Parse(value1) >= 5)
                str = (int.Parse(value) + 1).ToString();
            else
                str = int.Parse(value).ToString();
            return str;
        }

        private void sanmianozhao_Load(object sender, EventArgs e)
        {
            videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            videoSourcePlayer.Height = Player.Height;
            videoSourcePlayer.Width = Player.Width;
            Player.Controls.Add(videoSourcePlayer);
            //videoSourcePlayer.SendToBack();
            //遍历获取可操作的所有摄像头的名称
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            openCamera();
            //读取数据库数据，并显示到界面
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "sanmianzhao") > 0)
            {
                readvalue();
            }
        }
        //打开摄像头相机
        private void openCamera()
        {
            try
            {
                XmlDocument camname = new XmlDocument();
                camname.Load(@"config/camname.xml");
                string cname = camname.SelectSingleNode("//camname//names").InnerText.ToString();

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
                         
                            GetPortrait.Enabled = true;
                          
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
        
        private void GetPortrait_Click(object sender, EventArgs e)
        {
            if (!videoSourcePlayer.IsRunning)
            {
                MessageBox.Show("请打开摄像头");
                return;
            }
            //显示胸牌
            if (this.checkBox1.Checked)
                m_Breastplate = true;
            else
                m_Breastplate = false;
            //显示身份证
            if (this.checkBox2.Checked)
                m_BreastplateID = true;
            else
                m_BreastplateID = false;

            PhoXJ();
            //切换到截图框
            ShowCut1 form2 = new ShowCut1();
            form2.ShowDialog();
            //string m_Path = System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait" + m_PortraitDirection + quanjubianliang.rybh + ".jpg";
            //CutImage(m_Path);
            //获取人像方位图
            if (form2.Get())
            {
                switch (m_PortraitDirection)
                {
                    case 1:
                        ImagePath(@"ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_F.JPG", FrontageImage);
                        break;
                    case 2:
                        ImagePath(@"ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_L.JPG", LeftImage);
                        break;
                    case 3:
                        ImagePath(@"ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_R.JPG", RightImage);
                        break;
                    default: break;
                }
                if (!m_SelectShoot) //若是非选择拍摄则自动按顺序拍摄
                {
                    m_PortraitDirection++;
                    if (m_PortraitDirection > 3)
                    { m_PortraitDirection = 1; }
                }
                //form2.Set();
            }
            //SpecialMark.Image = b;     //拍摄的照片

            Cursor.Current = Cursors.Default;
        }
        //相机拍照
        private void PhoXJ()
        {
            // 捕获的图像
            Bitmap b = KiResizeImage(videoSourcePlayer.GetCurrentVideoFrame(), 1920, 1080);
         
           
                b.Save(@"test1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
           
            b.Dispose();
        }
        /// <summary>  
        /// 修改图片的分辨率 
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <returns>处理以后的Bitmap</returns>  
        public static Bitmap KiResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }
        //获取图片路径(并显示在采集框里)
        private void ImagePath(string filePath, System.Windows.Forms.PictureBox showbox)
        {
            if (!System.IO.File.Exists(filePath)) //如果不存在
            {
                //MessageBox.Show(filePath+"不存在！");
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
        //选择拍摄人像方位
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.FrontageImage.CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                m_SelectShoot = true;
                m_PortraitDirection = 1;
                LeftImage.Invalidate();
                RightImage.Invalidate();
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(1, 1, FrontageImage.Width - 4, FrontageImage.Height - 4));
            }
            else
            {

                ShowImage1 ss = new ShowImage1("_PH_F");
                ss.ShowDialog();
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.LeftImage.CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                m_PortraitDirection = 2;
                FrontageImage.Invalidate();
                RightImage.Invalidate();
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(1, 1, FrontageImage.Width - 4, FrontageImage.Height - 4));
            }
            else
            {

                ShowImage1 ss = new ShowImage1("_PH_L");
                ss.ShowDialog();
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.RightImage.CreateGraphics();
            if (e.Button == MouseButtons.Left)
            {
                m_PortraitDirection = 3;
                FrontageImage.Invalidate();
                LeftImage.Invalidate();
                g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(1, 1, FrontageImage.Width - 4, FrontageImage.Height - 4));
            }
            else
            {

                ShowImage1 ss = new ShowImage1("_PH_R");
                ss.ShowDialog();
            }
        }

        /********************************读写数据库*************************************/
        //将人像数据写入数据库
        private void image_sql(string imagePath, System.Windows.Forms.PictureBox showbox)
        {
            if (!System.IO.File.Exists(imagePath))
            {
                //MessageBox.Show("图片路径不存在！"); 
                return;
            }
            string sql = "";
            if (showbox == FrontageImage)
            {
                sql = "update sanmianzhao set zhengmiaozhao=@data where rycjbh='" + quanjubianliang.rybh + "'";
            }
            else if (showbox == LeftImage)
            {
                sql = "update sanmianzhao set zuocemiaozhao=@data where rycjbh='" + quanjubianliang.rybh + "'";
            }
            else if (showbox == RightImage)
            {
                sql = "update sanmianzhao set youcemianzhao=@data where rycjbh='" + quanjubianliang.rybh + "'";
            }
            gj.savePicture(imagePath, sql);
            // MessageBox.Show("保存人像成功");
        }
        //读取人像和标记数据
        private void readvalue()
        {
            try
            {
                //人像
                string sqlpho = "select * from sanmianzhao where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sqlpho).Rows)
                {
                    if ("" != rs["zhengmiaozhao"].ToString())
                    {
                        byte[] fileBytes = (byte[])rs["zhengmiaozhao"];
                        FrontageImage.Image = Image.FromStream(new MemoryStream(fileBytes));
                        FrontageImage.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                    if ("" != rs["zuocemiaozhao"].ToString())
                    {
                        byte[] fileBytes = (byte[])rs["zuocemiaozhao"];
                        LeftImage.Image = Image.FromStream(new MemoryStream(fileBytes));
                        LeftImage.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                    if ("" != rs["youcemianzhao"].ToString())
                    {
                        byte[] fileBytes = (byte[])rs["youcemianzhao"];
                        RightImage.Image = Image.FromStream(new MemoryStream(fileBytes));
                        RightImage.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                  
                }
                string sqlxingti = "select * from xingti where rycjbh='" + quanjubianliang.rybh + "'";
                foreach (DataRow rs in gj.gettable(sqlxingti).Rows)
                {
                    
                    Stature.Text = rs["shengao"].ToString();
                    Weight.Text = rs["tizhong"].ToString();
                    FootLength.Text = rs["zuchang"].ToString();
                }
              
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //保存采集内容,写入人像数据库
        private void writePhovalue()
        {
            try
            {
                if ("" == quanjubianliang.rybh)
                {
                    return;
                }
                else
                {
                    //人像
                    string sql1 = "insert into xingti(rycjbh,shengao,tizhong,zuchang)values('"
                    + quanjubianliang.rybh + "','"
                    + Stature.Text + "','"
                    + Weight.Text + "','"
                    + FootLength.Text + "')";
                    gj.sqliteexcu(sql1);
                    string sql2 = "insert into sanmianzhao(rycjbh)values('" + quanjubianliang.rybh + "')";
                    gj.sqliteexcu(sql2);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_F.JPG", FrontageImage);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_L.JPG", LeftImage);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_R.JPG", RightImage);
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //更新采集内容,写入人像数据
        private void updatePhovalue()
        {
            try
            {
                if ("" == quanjubianliang.rybh)
                {
                    return;
                }
                else
                {
                    //人像
                    string sql = "update xingti set shengao='"
                        + Stature.Text + "',tizhong='"
                        + Weight.Text + "',zuchang='"
                        + FootLength.Text + "' where rycjbh='"
                        + quanjubianliang.rybh + "'";
                    gj.sqliteexcu(sql);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_F.JPG", FrontageImage);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_L.JPG", LeftImage);
                    image_sql(@"ZIP//" + quanjubianliang.rybh + "//" + quanjubianliang.rybh + "_PH_R.JPG", RightImage);
                    //MessageBox.Show("人像保存成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //检测必填项
        private bool DetectionOptions()
        {
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "sanmianzhao") > 0)
            {
               return true; 
            }
            else
            {   MessageBox.Show("人员编号不能为空");
               
                return false;}
            
          
        }

        private void sanmianozhao_FormClosing(object sender, FormClosingEventArgs e)
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
                    e.Cancel = false;//退了
                    quanjubianliang.interrupt = 0;
                }
            }
            else
            {
                e.Cancel = false;//退了
                quanjubianliang.interrupt = 0;

            }
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "sanmianzhao") > 0)
            {
                updatePhovalue();//更新数据库
            }
            else
            {
                writePhovalue();//写入人像数据
            }
            if (myport != null)
            {
                myport.Close();
                myport = null;
             
            }
            if (videoSourcePlayer.IsRunning)
            {
                videoSourcePlayer.Stop();//停止获取摄像头资源
            }
        }
    }
}
