namespace 华夏网为.yonghuguangli
{
    partial class zuceyonghu
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
            this.IDcardnumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Telnumber = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.UnitName = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.Province = new System.Windows.Forms.ComboBox();
            this.PersonalName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.password2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.PoliceNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IDcardnumber
            // 
            this.IDcardnumber.Location = new System.Drawing.Point(112, 264);
            this.IDcardnumber.Margin = new System.Windows.Forms.Padding(2);
            this.IDcardnumber.Name = "IDcardnumber";
            this.IDcardnumber.Size = new System.Drawing.Size(345, 21);
            this.IDcardnumber.TabIndex = 47;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 272);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 46;
            this.label9.Text = "*身份证号码：";
            // 
            // Telnumber
            // 
            this.Telnumber.Location = new System.Drawing.Point(112, 229);
            this.Telnumber.Margin = new System.Windows.Forms.Padding(2);
            this.Telnumber.Name = "Telnumber";
            this.Telnumber.Size = new System.Drawing.Size(345, 21);
            this.Telnumber.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 237);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 44;
            this.label8.Text = "*联系电话:";
            // 
            // UnitName
            // 
            this.UnitName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.UnitName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.UnitName.FormattingEnabled = true;
            this.UnitName.Location = new System.Drawing.Point(112, 161);
            this.UnitName.Margin = new System.Windows.Forms.Padding(2);
            this.UnitName.Name = "UnitName";
            this.UnitName.Size = new System.Drawing.Size(345, 20);
            this.UnitName.TabIndex = 43;
            this.UnitName.SelectedIndexChanged += new System.EventHandler(this.UnitName_SelectedIndexChanged);
            this.UnitName.Click += new System.EventHandler(this.UnitName_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(431, 320);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 42;
            // 
            // Province
            // 
            this.Province.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Province.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.Province.FormattingEnabled = true;
            this.Province.Location = new System.Drawing.Point(112, 197);
            this.Province.Name = "Province";
            this.Province.Size = new System.Drawing.Size(345, 20);
            this.Province.TabIndex = 41;
            // 
            // PersonalName
            // 
            this.PersonalName.Location = new System.Drawing.Point(112, 125);
            this.PersonalName.Name = "PersonalName";
            this.PersonalName.Size = new System.Drawing.Size(345, 21);
            this.PersonalName.TabIndex = 40;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "*采集人姓名：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 38;
            this.label6.Text = "*单位代码：";
            // 
            // password2
            // 
            this.password2.Location = new System.Drawing.Point(112, 87);
            this.password2.Name = "password2";
            this.password2.PasswordChar = '●';
            this.password2.Size = new System.Drawing.Size(345, 21);
            this.password2.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "*再次输入密码：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(356, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 35;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 34;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PoliceNumber
            // 
            this.PoliceNumber.Location = new System.Drawing.Point(112, 297);
            this.PoliceNumber.Name = "PoliceNumber";
            this.PoliceNumber.Size = new System.Drawing.Size(345, 21);
            this.PoliceNumber.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 32;
            this.label4.Text = "*警号：";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(112, 49);
            this.password.Name = "password";
            this.password.PasswordChar = '●';
            this.password.Size = new System.Drawing.Size(345, 21);
            this.password.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "*用户密码：";
            // 
            // ID
            // 
            this.ID.Location = new System.Drawing.Point(112, 12);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(345, 21);
            this.ID.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "*单位名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "*用户账号：";
            // 
            // zuceyonghu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 379);
            this.Controls.Add(this.IDcardnumber);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Telnumber);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.UnitName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Province);
            this.Controls.Add(this.PersonalName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.password2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PoliceNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "zuceyonghu";
            this.Text = "zuceyonghu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IDcardnumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Telnumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox UnitName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox Province;
        private System.Windows.Forms.TextBox PersonalName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox password2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox PoliceNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}