using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 华夏网为.sanmianzhao
{
    public partial class ShowImage1 : Form
    {
        string gpnames = "";
        public ShowImage1(string pnames)
        {
            gpnames = pnames;
            InitializeComponent();
        }
        private void ShowImage_Load(object sender, EventArgs e)
        {
            string filePath = @"ZIP//" + quanjubianliang.rybh+ "//" +  quanjubianliang.rybh +gpnames+ ".jpg";
            if (!System.IO.File.Exists(filePath)) //如果不存在
            {
                this.Close();
                return;
            }
            Image image = Image.FromFile(filePath);
            Image cloneImage = new Bitmap(image);
            image.Dispose();
            pictureBox1.Image = cloneImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//图片自适应控件大小
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image.Dispose();
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Close();
            pictureBox1.Image.Dispose();
        }

        private void ShowImage_FormClosing(object sender, FormClosingEventArgs e)
        {
            pictureBox1.Image.Dispose();
        }
    }
}
