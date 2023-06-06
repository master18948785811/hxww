using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 华夏网为.yonghuguangli
{
    public partial class denglu : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public string key = "0";
        public denglu()
        {
            InitializeComponent();
            this.userAccount.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            yonghuguangli.zuceyonghu form = new yonghuguangli.zuceyonghu();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.userAccount.Text.Trim() == "")
            {
                MessageBox.Show("用户名称不能为空");
            }
            else
            {
                string strsql = "select count(*) from yonghu where username='" + this.userAccount.Text.ToString().Trim() + "' and password='" + this.userPassword.Text.ToString().Trim() + "'";

                if (Convert.ToInt32(gj.getback(strsql)) > 0)
                {
                    //检测是不是超级用户
                    string sql = "select id from yonghu where username='" + this.userAccount.Text.ToString().Trim() + "' and password='" + this.userPassword.Text.ToString().Trim() + "'";
                    quanjubianliang.cjrbh = gj.getback(sql); 
                   
                    this.Close();
                }
                else
                {
                    MessageBox.Show("用户名称和密码不匹配");
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            key = "1";
            this.Close();
           
        }
    }
}
