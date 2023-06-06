using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 华夏网为.zuji
{
    public partial class zuji : Form
    {
        byte[] fileBytesleft;
        byte[] fileBytesright;
        Appcode.Class2 gj = new Appcode.Class2();
        public zuji()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            if (axCapture1.PickFootImage())
            {


                fileBytesleft = Convert.FromBase64String(axCapture1.FootImage);
                this.pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytesleft));
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(bit, pictureBox1.ClientRectangle);
                bit.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "_FT_L.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                bit.Dispose();
                if (Convert.ToInt32(gj.getback("select count(*) from zuji where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='0'")) == 0)
                {
                   
                    string sqltrait1 = "insert into zuji (RYJCXXCJBH, ZJBWDM,ZJLXDM ,XDHWDM  ,XYDM ,XXZLDF )values('" + quanjubianliang.rybh.ToString() + "','0','1','','','60')";
                    gj.sqliteexcu(sqltrait1);
                    string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='0'";
                    gj.savePicture1(fileBytesleft, sql1);
                }
                else
                {
                    string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='0'";
                    gj.savePicture1(fileBytesleft, sql1);
                }
               

            }
            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.button1.Enabled = false;
            this.button2.Enabled = false;
            if (axCapture1.PickFootImage())
            {
           
                fileBytesright = Convert.FromBase64String(axCapture1.FootImage);
                this.pictureBox2.Image = Image.FromStream(new MemoryStream(fileBytesright));
                this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                Bitmap bit = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                pictureBox2.DrawToBitmap(bit, pictureBox2.ClientRectangle);
                bit.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "_FT_R.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                bit.Dispose();
                if (Convert.ToInt32(gj.getback("select count(*)  from zuji where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='1'")) == 0)
                {
                    string sqltrait1 = "insert into zuji (RYJCXXCJBH, ZJBWDM,ZJLXDM ,XDHWDM  ,XYDM ,XXZLDF )values('" + quanjubianliang.rybh.ToString() + "','1','1','','','60')";
                    gj.sqliteexcu(sqltrait1);
                    string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='1'";
                    gj.savePicture1(fileBytesright, sql1);
                }
                else
                {
                    string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='1'";
                    gj.savePicture1(fileBytesright, sql1);
                }
            }
            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }

        private void zuji_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void writefoot()
        {
            

            if (fileBytesleft != null)
            {
                string sqltrait1 = "insert into zuji (RYJCXXCJBH, ZJBWDM,ZJLXDM ,XDHWDM  ,XYDM ,XXZLDF )values('"+ quanjubianliang.rybh.ToString() + "','0','1','','','60')";
                gj.sqliteexcu(sqltrait1);
                string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='0'";
                gj.savePicture1(fileBytesleft, sql1);
            }
            if (fileBytesright != null)
            {
                string sqltrait2 = "insert into zuji (RYJCXXCJBH, ZJBWDM,ZJLXDM ,XDHWDM  ,XYDM ,XXZLDF )values('" + quanjubianliang.rybh.ToString() + "','1','1','','','60')";
                gj.sqliteexcu(sqltrait2);
                string sql2 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='1'";
                gj.savePicture1(fileBytesright, sql2);
            }
        }
        private void readvalue()
        {

            string sqlplane = "select * from zuji where  RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
            foreach (DataRow rs in gj.gettable(sqlplane).Rows)
            {
          
              
                if ("" != rs["ZJSJ"].ToString())
                {
                 
                    if (rs["ZJBWDM"].ToString() == "0")
                    {
                        byte[] fileBytes = (byte[])rs["ZJSJ"];
                        this.pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytes));
                        this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                    if (rs["ZJBWDM"].ToString() == "1")
                    {
                        byte[] fileBytes = (byte[])rs["ZJSJ"];
                        this.pictureBox2.Image = Image.FromStream(new MemoryStream(fileBytes));
                        this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
                    }
                }
              


            }


        }
        private void updatefoot()
        {

            if (fileBytesleft!= null)
            {
                string sql1 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='0'";
                gj.savePicture1(fileBytesleft, sql1);
            }
            if (fileBytesright!= null)
            {
                string sql2 = "update zuji set ZJSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "' AND ZJBWDM='1'";
                gj.savePicture1(fileBytesright, sql2);
            }
        }

        private void zuji_Load(object sender, EventArgs e)
        {
            //判断是否有案件编号
            if (gj.getbool("RYJCXXCJBH", quanjubianliang.rybh.ToString(), "zuji") > 0)
            {
                readvalue();//读取数据库数据，并显示到界面
            }
        }
    }
}
