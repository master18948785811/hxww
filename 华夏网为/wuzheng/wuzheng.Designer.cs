namespace 华夏网为.wuzheng
{
    partial class wuzheng
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.goumaishijian = new System.Windows.Forms.DateTimePicker();
            this.shuliang = new System.Windows.Forms.ComboBox();
            this.yanse = new System.Windows.Forms.ComboBox();
            this.leibie = new System.Windows.Forms.ComboBox();
            this.biaoshi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.beizhu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.zhongliang = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.jiazhi = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tezheng = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.bianhao = new System.Windows.Forms.TextBox();
            this.mingcheng = new System.Windows.Forms.TextBox();
            this.Player = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnTakingPictures = new System.Windows.Forms.Button();
            this.picShowImg = new System.Windows.Forms.PictureBox();
            this.WPBH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.XDWP_WPMC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPBZH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OBJECT_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPTZMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPJZRMBY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPYSDM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPSL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPZL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WPGZSJ_RQSJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANNEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.删除 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(78)))), ((int)(((byte)(208)))));
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(996, 498);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 33);
            this.button1.TabIndex = 178;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.goumaishijian);
            this.groupBox1.Controls.Add(this.shuliang);
            this.groupBox1.Controls.Add(this.yanse);
            this.groupBox1.Controls.Add(this.leibie);
            this.groupBox1.Controls.Add(this.biaoshi);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.beizhu);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.zhongliang);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.jiazhi);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tezheng);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.bianhao);
            this.groupBox1.Controls.Add(this.mingcheng);
            this.groupBox1.Location = new System.Drawing.Point(632, 266);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(819, 226);
            this.groupBox1.TabIndex = 177;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "物品基本信息";
            // 
            // goumaishijian
            // 
            this.goumaishijian.CustomFormat = "yyyy-MM-dd";
            this.goumaishijian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.goumaishijian.Location = new System.Drawing.Point(643, 74);
            this.goumaishijian.Name = "goumaishijian";
            this.goumaishijian.Size = new System.Drawing.Size(132, 21);
            this.goumaishijian.TabIndex = 191;
            // 
            // shuliang
            // 
            this.shuliang.FormattingEnabled = true;
            this.shuliang.Location = new System.Drawing.Point(375, 186);
            this.shuliang.Name = "shuliang";
            this.shuliang.Size = new System.Drawing.Size(132, 20);
            this.shuliang.TabIndex = 190;
            // 
            // yanse
            // 
            this.yanse.FormattingEnabled = true;
            this.yanse.Location = new System.Drawing.Point(107, 187);
            this.yanse.Margin = new System.Windows.Forms.Padding(4);
            this.yanse.Name = "yanse";
            this.yanse.Size = new System.Drawing.Size(132, 20);
            this.yanse.TabIndex = 188;
            // 
            // leibie
            // 
            this.leibie.FormattingEnabled = true;
            this.leibie.Location = new System.Drawing.Point(375, 26);
            this.leibie.Name = "leibie";
            this.leibie.Size = new System.Drawing.Size(132, 20);
            this.leibie.TabIndex = 189;
            // 
            // biaoshi
            // 
            this.biaoshi.Location = new System.Drawing.Point(375, 78);
            this.biaoshi.Margin = new System.Windows.Forms.Padding(4);
            this.biaoshi.Name = "biaoshi";
            this.biaoshi.Size = new System.Drawing.Size(132, 21);
            this.biaoshi.TabIndex = 175;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 169;
            this.label1.Text = "*名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 84);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 170;
            this.label2.Text = "编号";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(285, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 173;
            this.label4.Text = "类别";
            // 
            // beizhu
            // 
            this.beizhu.Location = new System.Drawing.Point(643, 132);
            this.beizhu.Margin = new System.Windows.Forms.Padding(4);
            this.beizhu.Name = "beizhu";
            this.beizhu.Size = new System.Drawing.Size(132, 21);
            this.beizhu.TabIndex = 187;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(285, 192);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 180;
            this.label7.Text = "数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 84);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 174;
            this.label3.Text = "标识";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 177;
            this.label5.Text = "颜色";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 138);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 176;
            this.label6.Text = "特征";
            // 
            // zhongliang
            // 
            this.zhongliang.Location = new System.Drawing.Point(643, 25);
            this.zhongliang.Margin = new System.Windows.Forms.Padding(4);
            this.zhongliang.Name = "zhongliang";
            this.zhongliang.Size = new System.Drawing.Size(132, 21);
            this.zhongliang.TabIndex = 183;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 138);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 179;
            this.label8.Text = "价值";
            // 
            // jiazhi
            // 
            this.jiazhi.Location = new System.Drawing.Point(375, 132);
            this.jiazhi.Margin = new System.Windows.Forms.Padding(4);
            this.jiazhi.Name = "jiazhi";
            this.jiazhi.Size = new System.Drawing.Size(132, 21);
            this.jiazhi.TabIndex = 181;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(555, 30);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 182;
            this.label9.Text = "重量";
            // 
            // tezheng
            // 
            this.tezheng.Location = new System.Drawing.Point(107, 133);
            this.tezheng.Margin = new System.Windows.Forms.Padding(4);
            this.tezheng.Name = "tezheng";
            this.tezheng.Size = new System.Drawing.Size(132, 21);
            this.tezheng.TabIndex = 178;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(539, 84);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 184;
            this.label10.Text = "购买时间";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(555, 138);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 186;
            this.label11.Text = "备注";
            // 
            // bianhao
            // 
            this.bianhao.Location = new System.Drawing.Point(107, 79);
            this.bianhao.Margin = new System.Windows.Forms.Padding(4);
            this.bianhao.Name = "bianhao";
            this.bianhao.ReadOnly = true;
            this.bianhao.Size = new System.Drawing.Size(132, 21);
            this.bianhao.TabIndex = 172;
            // 
            // mingcheng
            // 
            this.mingcheng.Location = new System.Drawing.Point(107, 25);
            this.mingcheng.Margin = new System.Windows.Forms.Padding(4);
            this.mingcheng.Name = "mingcheng";
            this.mingcheng.Size = new System.Drawing.Size(132, 21);
            this.mingcheng.TabIndex = 171;
            // 
            // Player
            // 
            this.Player.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Player.Location = new System.Drawing.Point(26, 12);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(580, 435);
            this.Player.TabIndex = 176;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvList);
            this.panel2.Font = new System.Drawing.Font("宋体", 9.5F);
            this.panel2.Location = new System.Drawing.Point(163, 537);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1244, 318);
            this.panel2.TabIndex = 175;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WPBH,
            this.XDWP_WPMC,
            this.WPBZH,
            this.OBJECT_TYPE,
            this.WPTZMS,
            this.WPJZRMBY,
            this.WPYSDM,
            this.WPSL,
            this.WPZL,
            this.WPGZSJ_RQSJ,
            this.ANNEX,
            this.删除});
            this.dgvList.Location = new System.Drawing.Point(0, 3);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1238, 272);
            this.dgvList.TabIndex = 102;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            // 
            // btnTakingPictures
            // 
            this.btnTakingPictures.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(78)))), ((int)(((byte)(208)))));
            this.btnTakingPictures.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btnTakingPictures.ForeColor = System.Drawing.Color.White;
            this.btnTakingPictures.Location = new System.Drawing.Point(221, 473);
            this.btnTakingPictures.Name = "btnTakingPictures";
            this.btnTakingPictures.Size = new System.Drawing.Size(105, 33);
            this.btnTakingPictures.TabIndex = 172;
            this.btnTakingPictures.Text = "拍照";
            this.btnTakingPictures.UseVisualStyleBackColor = false;
            this.btnTakingPictures.Click += new System.EventHandler(this.btnTakingPictures_Click);
            // 
            // picShowImg
            // 
            this.picShowImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picShowImg.Location = new System.Drawing.Point(890, 8);
            this.picShowImg.Name = "picShowImg";
            this.picShowImg.Size = new System.Drawing.Size(334, 249);
            this.picShowImg.TabIndex = 173;
            this.picShowImg.TabStop = false;
            // 
            // WPBH
            // 
            this.WPBH.DataPropertyName = "WPBH";
            this.WPBH.HeaderText = "物品编号";
            this.WPBH.Name = "WPBH";
            this.WPBH.ReadOnly = true;
            // 
            // XDWP_WPMC
            // 
            this.XDWP_WPMC.DataPropertyName = "XDWP_WPMC";
            this.XDWP_WPMC.HeaderText = "物品名称";
            this.XDWP_WPMC.Name = "XDWP_WPMC";
            this.XDWP_WPMC.ReadOnly = true;
            // 
            // WPBZH
            // 
            this.WPBZH.DataPropertyName = "WPBZH";
            this.WPBZH.HeaderText = "物品标识号";
            this.WPBZH.Name = "WPBZH";
            this.WPBZH.ReadOnly = true;
            // 
            // OBJECT_TYPE
            // 
            this.OBJECT_TYPE.DataPropertyName = "OBJECT_TYPE";
            this.OBJECT_TYPE.HeaderText = "物品类别";
            this.OBJECT_TYPE.Name = "OBJECT_TYPE";
            this.OBJECT_TYPE.ReadOnly = true;
            // 
            // WPTZMS
            // 
            this.WPTZMS.DataPropertyName = "WPTZMS";
            this.WPTZMS.HeaderText = "物品特征";
            this.WPTZMS.Name = "WPTZMS";
            this.WPTZMS.ReadOnly = true;
            // 
            // WPJZRMBY
            // 
            this.WPJZRMBY.DataPropertyName = "WPJZRMBY";
            this.WPJZRMBY.HeaderText = "价值";
            this.WPJZRMBY.Name = "WPJZRMBY";
            this.WPJZRMBY.ReadOnly = true;
            // 
            // WPYSDM
            // 
            this.WPYSDM.DataPropertyName = "WPYSDM";
            this.WPYSDM.HeaderText = "颜色";
            this.WPYSDM.Name = "WPYSDM";
            this.WPYSDM.ReadOnly = true;
            // 
            // WPSL
            // 
            this.WPSL.DataPropertyName = "WPSL";
            this.WPSL.HeaderText = "数量";
            this.WPSL.Name = "WPSL";
            this.WPSL.ReadOnly = true;
            // 
            // WPZL
            // 
            this.WPZL.DataPropertyName = "WPZL";
            this.WPZL.HeaderText = "重量";
            this.WPZL.Name = "WPZL";
            this.WPZL.ReadOnly = true;
            // 
            // WPGZSJ_RQSJ
            // 
            this.WPGZSJ_RQSJ.DataPropertyName = "WPGZSJ_RQSJ";
            this.WPGZSJ_RQSJ.HeaderText = "购置时间";
            this.WPGZSJ_RQSJ.Name = "WPGZSJ_RQSJ";
            this.WPGZSJ_RQSJ.ReadOnly = true;
            // 
            // ANNEX
            // 
            this.ANNEX.DataPropertyName = "ANNEX";
            this.ANNEX.HeaderText = "备注";
            this.ANNEX.Name = "ANNEX";
            this.ANNEX.ReadOnly = true;
            // 
            // 删除
            // 
            this.删除.HeaderText = "操作";
            this.删除.Name = "删除";
            this.删除.ReadOnly = true;
            this.删除.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.删除.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.删除.Text = "删除";
            this.删除.UseColumnTextForButtonValue = true;
            // 
            // wuzheng
            // 
            this.AccessibleDescription = "ssssss";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 811);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Player);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnTakingPictures);
            this.Controls.Add(this.picShowImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wuzheng";
            this.Text = "wuzheng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wuzheng_FormClosing);
            this.Load += new System.EventHandler(this.wuzheng_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShowImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker goumaishijian;
        private System.Windows.Forms.ComboBox shuliang;
        private System.Windows.Forms.ComboBox yanse;
        private System.Windows.Forms.ComboBox leibie;
        private System.Windows.Forms.TextBox biaoshi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox beizhu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox zhongliang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox jiazhi;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tezheng;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox bianhao;
        private System.Windows.Forms.TextBox mingcheng;
        private System.Windows.Forms.Panel Player;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnTakingPictures;
        private System.Windows.Forms.PictureBox picShowImg;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPBH;
        private System.Windows.Forms.DataGridViewTextBoxColumn XDWP_WPMC;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPBZH;
        private System.Windows.Forms.DataGridViewTextBoxColumn OBJECT_TYPE;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPTZMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPJZRMBY;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPYSDM;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPSL;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPZL;
        private System.Windows.Forms.DataGridViewTextBoxColumn WPGZSJ_RQSJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn ANNEX;
        private System.Windows.Forms.DataGridViewButtonColumn 删除;
    }
}