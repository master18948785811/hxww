using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 华夏网为.DNA
{
    public partial class DNA : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public DNA()
        {
            InitializeComponent();
        }

        private void DNA_Load(object sender, EventArgs e)
        {

            if (gj.getbool("rycjbh",quanjubianliang.rybh, "DNA" ) > 0)
            {
                this.textBox1.Text = gj.getback("select DNANumber from DNA where rycjbh='" + quanjubianliang.rybh.ToString() + "'");
                this.textBox1.Focus();
            }

            string sql = "select * from renyuanjibenxinxi where rycjbh='" + quanjubianliang.rybh + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                this.textBox1.Text = quanjubianliang.rybh.Replace("R", "D");
                this.textBox2.Text = rs["xm"].ToString();
                this.textBox3.Text = "";
                if (rs["xbdm"].ToString() != "")
                {
                    try
                    {
                        if (Convert.ToInt32(rs["xbdm"].ToString()) == 1)
                        {
                            this.textBox7.Text = "男";
                        }
                        else if (Convert.ToInt32(rs["xbdm"].ToString()) == 2)
                        {
                            this.textBox7.Text = "女";
                        }
                        else
                        {
                            this.textBox7.Text = "未知";
                        }
                    }
                    catch
                    {
                        this.textBox7.Text = rs["xbdm"].ToString();
                    }
                }

                this.textBox6.Text = rs["csrq"].ToString();
                this.textBox9.Text = gj.getback("select  mingcheng from zd_hujidixingzhengquhua where bianhao ='" + rs["hjdz_xzqhdm"].ToString() + "'");
                this.textBox8.Text = rs["gmsfzhm"].ToString();
                this.textBox11.Text = rs["hjdz_dzmc"].ToString();
                this.textBox15.Text = gj.getback("select PersonalName from yonghu where id='" + quanjubianliang.cjrbh + "'");
                this.textBox17.Text = gj.getback("select PersonalName from yonghu where id='" + quanjubianliang.cjrbh + "'");
                this.textBox19.Text = gj.getback("select UnitName from yonghu where id='" + quanjubianliang.cjrbh + "'");
                this.textBox14.Text = System.DateTime.Now.ToShortDateString();
                this.textBox21.Text = System.DateTime.Now.ToShortDateString();
                
            }
        }

        private void DNA_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gj.getbool("rycjbh", quanjubianliang.rybh, "DNA") > 0)
            {
                string sqlDNA = "UPDATE DNA SET DNANumber='" + this.textBox1.Text.ToString() + "' WHERE rycjbh='" + quanjubianliang.rybh.ToString() + "'";

                gj.sqliteexcu(sqlDNA);
            }
            else
            {
                if ("" == this.textBox1.Text.ToString())
                {
                    return;
                }
                string sqlDNA = "insert into DNA(rycjbh,DNANumber)values('" + quanjubianliang.rybh + "','" + this.textBox1.Text.ToString() + "')";
                gj.sqliteexcu(sqlDNA);
            }
        }
    }
}
