namespace 华夏网为.hongmu
{
    partial class hongmu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hongmu));
            this.axBiometrics1 = new AxBiometricsLib.AxBiometrics();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.axBiometrics1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axBiometrics1
            // 
            this.axBiometrics1.Enabled = true;
            this.axBiometrics1.Location = new System.Drawing.Point(3, 3);
            this.axBiometrics1.Name = "axBiometrics1";
            this.axBiometrics1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axBiometrics1.OcxState")));
            this.axBiometrics1.Size = new System.Drawing.Size(986, 624);
            this.axBiometrics1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.axBiometrics1);
            this.panel2.Location = new System.Drawing.Point(200, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1071, 630);
            this.panel2.TabIndex = 1;
            // 
            // hongmu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 772);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "hongmu";
            this.Text = "hongmu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.hongmu_FormClosing);
            this.Load += new System.EventHandler(this.hongmu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axBiometrics1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxBiometricsLib.AxBiometrics axBiometrics1;
        private System.Windows.Forms.Panel panel2;
    }
}