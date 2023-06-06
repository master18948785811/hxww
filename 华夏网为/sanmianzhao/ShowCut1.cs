using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;

namespace 华夏网为.sanmianzhao
{
    public partial class ShowCut1 : Form
    {
        int m_ViewX, m_ViewY;                  //截图框初始位置
        int m_ViewW, m_ViewH;
        int m_Bolder;
        bool m_Photograph;                     //是否采集截图
        int times = 1;                         //控制截图时间
        public ShowCut1()
        {
            InitializeComponent();
        }
        private void ShowCut_Load(object sender, EventArgs e)
        {
            m_ViewX = 0;
            m_ViewY = 0;
            m_Bolder = 0;
            m_ViewW = 240 + m_Bolder;
            m_ViewH = 320 + m_Bolder;
            m_Photograph = false;

            Image image = Image.FromFile(@"test1.jpg");
            Image cloneImage = new Bitmap(image);
            image.Dispose();
            pictureBox1.Image = cloneImage;
            // ImagePath(@"test1.jpg", pictureBox1);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
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

        //鼠标移动事件
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            m_ViewX = e.X;
            m_ViewY = e.Y;
            if (m_ViewX < pictureBox1.Location.X)
            {
                m_ViewX = 0;
            }
            else if (m_ViewX + m_ViewW > pictureBox1.Width)
            {
                m_ViewX = pictureBox1.Width - m_ViewW - 1;
            }

            if (m_ViewY < pictureBox1.Location.Y)
            {
                m_ViewY = 0;
            }
            else if (m_ViewY + m_ViewH > pictureBox1.Height)
            {
                m_ViewY = pictureBox1.Height - m_ViewH - 1;
            }
            pictureBox1.Invalidate();
        }

        //截图
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Mouse.ShowCursor(0);//隐藏鼠标指针
            pictureBox1.Invalidate();
            times = 0;
        }

        //绘画截图框
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (times==2)
            {
                UInt32 u_count = sanmianozhao.m_PortraitDirection;//获取图片位置编号
                string m_Path = "";
                switch (u_count)
                {
                    case 1:
                        m_Path = System.IO.Directory.GetCurrentDirectory() + "//ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_F.jpg";
                        break;
                    case 2:
                        m_Path = System.IO.Directory.GetCurrentDirectory() + "//ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_L.jpg";
                        break;
                    case 3:
                        m_Path = System.IO.Directory.GetCurrentDirectory() + "//ZIP//" + quanjubianliang.rybh+ "//" + quanjubianliang.rybh+ "_PH_R.jpg";
                        break;

                    default:break;

                }
                
                if (System.IO.File.Exists(m_Path)) //如果已经存在
                {
                    System.IO.File.Delete(m_Path);
                }
                //截图
                CaptureImage(@"test1.jpg", pictureBox1.PointToClient(MousePosition).X, pictureBox1.PointToClient(MousePosition).Y, m_Path, 480, 640);
              
                m_Photograph = true;

                this.Close();

            }
            if ( times == 1)
            {
                Pen pen1 = new Pen(Color.Red, m_Bolder);
                Graphics g = e.Graphics; //利用该事件的参数e创建画面对象
                //矩形外框
                Rectangle rect = new Rectangle(m_ViewX, m_ViewY, m_ViewW, m_ViewH);//是创建画矩形的区域
                g.DrawRectangle(pen1, rect);//g对象提供了画图形的方法，我们只需调用即可
                //g.FillRectangle(Brushes.Red, r);//填充颜色
                //横线1
                e.Graphics.DrawLine(pen1, m_ViewX, m_ViewY + m_ViewH * 1 / 4 + 10, m_ViewX + m_ViewW, m_ViewY + m_ViewH * 1 / 4 + 10);
                //横线2
                e.Graphics.DrawLine(pen1, m_ViewX, m_ViewY + m_ViewH * 1 / 2, m_ViewX + m_ViewW, m_ViewY + m_ViewH * 1 / 2);
                //横线3
                e.Graphics.DrawLine(pen1, m_ViewX, m_ViewY + m_ViewH * 3 / 4, m_ViewX + m_ViewW, m_ViewY + m_ViewH * 3 / 4);
                //竖线1
                e.Graphics.DrawLine(pen1, m_ViewX + m_ViewW * 1 / 4, m_ViewY, m_ViewX + m_ViewW * 1 / 4, m_ViewY + m_ViewH * 3 / 4);
                //竖线2
                e.Graphics.DrawLine(pen1, m_ViewX + m_ViewW * 1 / 2, m_ViewY, m_ViewX + m_ViewW * 1 / 2, m_ViewY + m_ViewH * 3 / 4);
                //竖线3
                e.Graphics.DrawLine(pen1, m_ViewX + m_ViewW * 3 / 4, m_ViewY, m_ViewX + m_ViewW * 3 / 4, m_ViewY + m_ViewH * 3 / 4);
            }
            else 
            {
                times = 2;
            }
           
        }

        //关闭
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image.Dispose();
        }

        //画胸牌
        private void DrawBreastplate(Image image, string Name)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                //获取身份证ID
               
                int border = 4;
                Pen pen1 = new Pen(Color.Black, border);          //创建画笔
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Far;       // 垂直底部
                format.Alignment = StringAlignment.Center;        // 水平居中
                Rectangle rect = new Rectangle(120, 560, m_ViewW, m_ViewH / 4);
                g.FillRectangle(Brushes.White, rect);             //填充背景颜色
                g.DrawRectangle(pen1, rect);                      //画边框颜色

                if (sanmianozhao.m_Breastplate&& sanmianozhao.m_BreastplateID)
                {
                    Appcode.Class2 gj = new Appcode.Class2();
                    string sql = "select gmsfzhm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
                    g.DrawString(Name + "\r\n" + gj.getback(sql), new Font("宋体", 18), Brushes.Black, rect, format);
                }else
                    g.DrawString(Name , new Font("宋体", 24), Brushes.Black, rect, format);
                
                g.Flush();
            }
        }

        public bool Get()
        {
            return m_Photograph;
        }

        public void Set()
        {
            m_Photograph = false;
        }

        //截取图片指定位置数据
        #region 从大图中截取一部分图片
        /// <summary>
        /// 从大图中截取一部分图片
        /// </summary>
        /// <param name="fromImagePath">来源图片地址</param>        
        /// <param name="offsetX">从偏移X坐标位置开始截取</param>
        /// <param name="offsetY">从偏移Y坐标位置开始截取</param>
        /// <param name="toImagePath">保存图片地址</param>
        /// <param name="width">保存图片的宽度</param>
        /// <param name="height">保存图片的高度</param>
        /// <returns></returns>
        public void CaptureImage(string fromImagePath, int offsetX, int offsetY, string toImagePath, int width, int height)
        {
            offsetX *= 2;
            offsetY *= 2;
            //原图片文件
            Image fromImage = Image.FromFile(fromImagePath);
            //创建新图位图
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            //创建作图区域
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区
            graphic.DrawImage(fromImage, 0, 0, new Rectangle(offsetX, offsetY, width, height), GraphicsUnit.Pixel);
            //从作图区生成新图
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());

            if (sanmianozhao.m_Breastplate /*&& 1 == u_count*/)//画胸牌
            {
                Appcode.Class2 gj = new Appcode.Class2();
                string name = gj.getback("select xm from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'");
                //name = "张三";
                DrawBreastplate(bitmap, name);
            }
            //设置dpi
            bitmap.SetResolution(300, 300);
            //保存图片
            bitmap.Save(toImagePath, ImageFormat.Jpeg);
            //释放资源   
            fromImage.Dispose();
            saveImage.Dispose();
            graphic.Dispose();
            bitmap.Dispose();

            /**********************************************/

            //Rectangle gec = new Rectangle(new Point(offsetX, offsetY), new Size(480, 640));
            //Image a = Image.FromFile(fromImagePath);
            //Bitmap bitmap = new Bitmap(a);
            //Bitmap cloneBitmap = bitmap.Clone(gec, PixelFormat.Format24bppRgb);
            //if (Pho1.m_Breastplate /*&& 1 == u_count*/)//画胸牌
            //{
            //        Appcode.Class2 gj = new Appcode.Class2();
            //        string name = gj.getback("select ChineseName from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh+ "'");
            //        //name = "张三";
            //        DrawBreastplate(cloneBitmap, name);
            // }
            // cloneBitmap.Save(toImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            // cloneBitmap.Dispose();
            // bitmap.Dispose();
            // a.Dispose();
          
               
        }
        #endregion

        //隐藏鼠标指针
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //Mouse.ShowCursor(0);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //Mouse.ShowCursor(1);
        }

        /****************************************************/
        
    }
}
