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
    public partial class zuceyonghu : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public struct Person
        {
            public string Name;
            public string Pwd;
            public string PersonalName;
            public string UnitName;
            public string PoliceNumber;
            public string Address;
            public string idcard;
            public string telephone;
        }
        Person per = new Person();
        public zuceyonghu()
        {
            InitializeComponent();
            string sql = "select * from zd_danweidaima order by code";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                this.UnitName.Items.Add(rs[2].ToString());
                this.UnitName.AutoCompleteCustomSource.Add(rs[2].ToString());
                this.Province.Items.Add(rs[1].ToString());
                this.Province.AutoCompleteCustomSource.Add(rs[1].ToString());
            }
        }

        //保存
        private void button1_Click(object sender, EventArgs e)
        {
            if ("" == ID.Text)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            else if ("" == password.Text)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            else if (password.Text != password2.Text)
            {
                MessageBox.Show("两次密码不相同，请重新输入");
                password.Text = "";
                password2.Text = "";
                return;
            }
            if ("" == PersonalName.Text)
            {
                MessageBox.Show("采集人姓名不能为空");
                return;
            }
            if ("" == Province.Text)
            {
                MessageBox.Show("单位代码不能为空");
                return;
            }
            if ("" == PoliceNumber.Text)
            {
                MessageBox.Show("警号不能为空");
                return;
            }
            //检测ID是否存在
            string str = "select * from yonghu where username='" + ID.Text + "'";
            if ("" != gj.getback(str))
            {
                MessageBox.Show("此ID已存在，请重新输入新的ID");
                ID.Text = "";
                ID.Invalidate();
                return;
            }
            //插入数据库
            string sql = "insert into yonghu(username,password,PersonalName,UnitName,PoliceNumber,IDcardnumber,Telnumber,Address)values('"
                + ID.Text + "','"                   //账号
                + password.Text + "','"             //密码
                + PersonalName.Text + "','"         //采集人姓名
                + UnitName.Text + "','"             //单位名称(汉字)
                + PoliceNumber.Text + "','"         //警号
                + IDcardnumber.Text + "','"         //身份证号码
                + Telnumber.Text + "','"            //联系电话
                + Province.Text + "')";             //单位代码
            gj.sqliteexcu(sql);
            this.Close();
        }

        //取消
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("取消添加用户");
            per.Name = "";
            this.Close();
        }

        public Person Get()
        {
            return per;
        }

        private void UnitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select code from zd_danweidaima where name='" + UnitName.Text.Trim() + "'";
            Province.Text = gj.getback(sql);
        }

       
    }
}
