using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.shengwen
{
    public partial class shenwen : Form
    {
        int uploadback = 100000;
        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //FrmMian为我窗体的类名
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
        Appcode.Class2 gj = new Appcode.Class2();
        string times = "", times_now = "";//音频时长、有效时长
        string fSnr = "";//信噪比
        string fAvgEnergy = "";//平均能量
        string nScore = "";//得分
        string vessons = "";//版本号
        Thread td = null;
        string oldfile = "", fileName = "", wavname="";
        public shenwen()
        {
            InitializeComponent();
        }

        private void shenwen_Load(object sender, EventArgs e)
        {
            ClearMemory();
            getload();
            ShowInfo();

            if (System.IO.File.Exists( "zip\\" + quanjubianliang.rybh.ToString() + "\\" + quanjubianliang.rybh.ToString() + ".vid"))
            {
                this.label3.Text = "已采集过，可以播放";
                this.btn_file.Enabled = true;

            }
            else
            {
                this.label3.Text = "";
            }

            XmlDocument sb = new XmlDocument();
            sb.Load(@"sbid.xml");
            vessons = sb.SelectSingleNode("root//sbid").InnerText;
            this.label_unitNum.Text = vessons;

            axiAudioX21.wsEndPoint = sb.SelectSingleNode("root//WsEndPoint").InnerText;
            if (sb.SelectSingleNode("root//sfsc").InnerText == "1")
            {

                this.button1.Visible = true;
            }
        }
        private void getload()
        {
            axiAudioX21.SetIvdSampleRate(16000);
            axiAudioX21.SetAsioViewChannelIndex(4);
            axiAudioX21.RecSamplesPerSec = 16000;
            axiAudioX21.SampleWidth = 16;
            axiAudioX21.IVCOutPath = System.IO.Directory.GetCurrentDirectory()+"\\IVC_OUT";
            axiAudioX21.CalcQualityInterval = Convert.ToInt32(10);

        }
        private void ShowInfo()
        {
            string sql = string.Format(@"SELECT * FROM zd_LYYZH");
            string sql1 = string.Format(@"SELECT * FROM zd_FYFS");
            string sql2 = string.Format(@"SELECT * FROM zd_SHWXD");
            string sql3 = string.Format(@"SELECT * FROM zd_HYFY");
            //语种
            DataTable dt = gj.gettable(sql);
            LYYZDM.DataSource = dt;
            LYYZDM.DisplayMember = "Name";
            LYYZDM.ValueMember = "Code";

            LYYZDM.Text = "汉语";


            //发音方式
            DataTable dt1 = gj.gettable(sql1);
            FYFSDM.DataSource = dt1;
            FYFSDM.DisplayMember = "Name";
            FYFSDM.ValueMember = "Code";

            FYFSDM.Text = "念读";

            //信道
            DataTable dt2 = gj.gettable(sql2);
            XDDM.DataSource = dt2;
            XDDM.DisplayMember = "Name";
            XDDM.ValueMember = "Code";

            XDDM.Text = "高保真";

            //汉语方言
            DataTable dt3 = gj.gettable(sql3);
            HYFYDM.DataSource = dt3;
            HYFYDM.DisplayMember = "Name";
            HYFYDM.ValueMember = "Code";

            HYFYDM.Text = "普通话";
        }
        /// <summary>
        /// 录音过称中时长判断
        /// </summary>
        private void Label_times_now()
        {
            while (true)
            {
                times = (axiAudioX21.GetSeconds()).ToString();//s)
                times_now = (axiAudioX21.GetEffectiveTime(1) / 1000).ToString();//ms
                this.Invoke(new MethodInvoker(delegate () { this.lab_times.Text = times.ToString(); }));
                this.Invoke(new MethodInvoker(delegate () { this.lab_NewTimes.Text = times_now.ToString(); }));
                Application.DoEvents();
                if (Convert.ToInt32(times_now) >= 150)
                {
                    this.btn_stop.Enabled = true;
                    DialogResult dr = MessageBox.Show("已经达到有效录音，是否结束采集", "有效时长达标", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        this.Invoke(new EventHandler(btn_stop_Click));
                    }
                }
            }
        }
        private string GetErrorMessage(int errorCode)
        {
            var error_message = "";
            switch (errorCode)
            {
                case 1: error_message = "完成"; break;
                case 0: error_message = "执行成功"; break;
                case -1: error_message = "执行失败"; break;
                case -2: error_message = "执行出现异常"; break;
                case -3: error_message = "启动失败"; break;
                case -4: error_message = "初始化失败"; break;
                case -5: error_message = "不支持的采样类型"; break;
                case -6: error_message = "错误的文件名"; break;
                case -7: error_message = "不支持的文件类型"; break;
                case -8: error_message = "无法降采样"; break;
                case -9: error_message = "不需要降采样"; break;
                case -10: error_message = "VSPP引擎初始化失败"; break;
                case -11: error_message = "VSPP引擎实例创建失败"; break;
                case -12: error_message = "VSPP引擎实例销毁失败"; break;
                case -13: error_message = "VSPP引擎销毁失败"; break;
                case -14: error_message = "VSPP引擎未初始化"; break;
                case -15: error_message = "WAV文件大小超过VSPP引擎处理能力"; break;
                case -16: error_message = "VSPP处理失败"; break;
                case -17: error_message = "写PLP文件失败"; break;
                case -18: error_message = "读取PLP data失败"; break;
                case -19: error_message = "没有发现声纹采集设备"; break;
                case -20: error_message = "输入的声道数超过ASIO可用声道数上限"; break;
                case -21: error_message = "录音文件写入失败"; break;
                case -22: error_message = "声纹质量检查不符合"; break;
                case -23: error_message = "未开始"; break;
                case -24: error_message = "重复启动"; break;
                case -25: error_message = "没有文件"; break;
                case -26: error_message = "非目录文件"; break;
                case -27: error_message = "文件没有找到"; break;
                case -28: error_message = "无效的IVD的文件"; break;
                case -29: error_message = "设备驱动没有配置"; break;
                case -30: error_message = "IVD压缩出错"; break;
                case -31: error_message = "IVD解压缩出错"; break;
                case -32: error_message = "IVD文件写入错误"; break;
                case -33: error_message = "IVD文件声道号指定不正确"; break;
                case -34: error_message = "IVD声纹注册失败"; break;
                case -35: error_message = "第三方采集终端身份验证不通过"; break;
                case -36: error_message = "临时文件夹没有足够的剩余空间,至少需要200M的剩余空间,临时文件夹位置请查看iAudiox.ini TempPath配置项"; break;
                case -37: error_message = "无法连接分布式存储服务器"; break;
                case -38: error_message = "上传分布式服务器失败"; break;
                case -39: error_message = "分布式服务器文件下载失败"; break;
                case -40: error_message = "连接声纹库WebSerivce失败或服务器发生错误"; break;
                case -41: error_message = "分布式服务器存储空间不足"; break;
                case -42: error_message = "无法取得分布式服务器分组信息"; break;
                case -43: error_message = "无法连接分布式主服务器"; break;
                case -44: error_message = "不存在的分布式文件存储路径"; break;
                default: error_message = "未知错误，错误码：" + errorCode; break;
            }
            return error_message;
        }
        private bool sjsc(string mes, string mess)
        {
            DialogResult dr = MessageBox.Show(mes, mess, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void filesbase(string fileNames)
        {
            try
            {
                wavname = fileNames.Substring(0, fileNames.Length - 4) + "_8000.wav";

                FileInfo file = new FileInfo(fileNames);
                if (file.Exists)
                {
                    string vidfile = @"zip\\" + quanjubianliang.rybh.ToString() + "\\" + quanjubianliang.rybh.ToString() + ".vid";
                    file.CopyTo(vidfile, true);

                }
                if (System.IO.File.Exists(wavname))
                {

                    string vidfile =   @"zip\\" + quanjubianliang.rybh.ToString() + "\\" + quanjubianliang.rybh.ToString() + ".wav";

                    System.IO.File.Copy(wavname, vidfile, true);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void AxiAudioX21_CompressIVDCompleteEvent(object sender, AxIAUDIOX2Lib._DiAudioX2Events_CompressIVDCompleteEventEvent e)
        {
            try
            {
                this.btn_start.Enabled = true;
                this.btn_file.Enabled = true;
                fileName = e.sFileName;
                oldfile = fileName;
                filesbase(fileName);
                ////wavname = fileName.Substring(0, fileName.Length - 4) + "_8000.wav";
                //wavname = this.getdburl3() + "\\" + quanjubianliang.rybh.ToString() + "\\sound\\" + quanjubianliang.rybh.ToString() + "_8000.wav";
                string status = axiAudioX21.GetSpeechQualityStr(1);
                JObject swzl = JObject.Parse(status);
                fAvgEnergy = swzl["AvgEnergy"].ToString();
                fSnr = swzl["Snr"].ToString();
                nScore = "60";

                if (gj.getbool("RYJCXXCJBH",quanjubianliang.rybh.ToString(),"LEDEN_COLLECT_VOICEPRINT") > 0)
                {
                    UpdateVoiceValue();
                }
                else
                {
                    WriteVoiceValue();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void axidStop()
        {

            axiAudioX21.StopRecord();
            axiAudioX21.CompressIVDCompleteEvent += AxiAudioX21_CompressIVDCompleteEvent;

        }
        public byte[] File2Bytes(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return new byte[0];
            }

            FileInfo fi = new FileInfo(path);
            byte[] buff = new byte[fi.Length];

            FileStream fs = fi.OpenRead();
            fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return buff;


        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (axiAudioX21.GetStatus() != 1)
            {

                axiAudioX21.Recorder();
                //MessageBox.Show(axiAudioX21.GetCurrentIvcError().ToString());
                if (axiAudioX21.GetCurrentIvcError() == 0)
                {

                    this.btn_file.Enabled = false;
                    this.btn_start.Enabled = false;
                    MessageBox.Show("开始成功，请对着麦克风说话！");
                    td = new Thread(Label_times_now);
                    td.Start();
                }
                else
                {
                    MessageBox.Show(GetErrorMessage(axiAudioX21.GetCurrentIvcError()) + "，检查是否连接设备后请重试！");

                }

            }
            else
            {


                MessageBox.Show("正在录音中");
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (axiAudioX21.GetEffectiveTime(1) / 1000 < 150)
            {
                if (sjsc("有效时长小于150秒，是否确定", "采集时长不足") == true)
                {
                    this.btn_start.Enabled = false;
                    this.btn_stop.Enabled = false;
                    this.btn_file.Enabled = false;
                    td.Abort();
                    axidStop();
                }
            }
            else
            {
                this.btn_start.Enabled = false;
                this.btn_stop.Enabled = false;
                this.btn_file.Enabled = false;

                td.Abort();
                axidStop();
            }
        }

        private void btn_file_Click(object sender, EventArgs e)
        {
            string urls = @"zip\\" + quanjubianliang.rybh.ToString() + "\\" + quanjubianliang.rybh.ToString() + ".vid";
            if (System.IO.File.Exists(urls))
            {
                int sd = axiAudioX21.OpenFile(urls);
                int i = axiAudioX21.Play();
            }
            else
            {
                MessageBox.Show("未检测到文件");
            }
        }

        private void shenwen_FormClosing(object sender, FormClosingEventArgs e)
        {
            axiAudioX21.StopPlay();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label_unitNum_Click(object sender, EventArgs e)
        {
            swzc ss = new swzc();
            ss.ShowDialog();
        }

        //读取声纹数据
        private void ReadVoiceValue()
        {
            string sql = "select * from LEDEN_COLLECT_VOICEPRINT where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                LYYZDM.Text = gj.getback("select name from zd_LYYZH where code='" + rs["LYYZDM"].ToString() + "'");
                FYFSDM.Text = gj.getback("select name from zd_FYFS where code='" + rs["FYFSDM"].ToString() + "'");
                XDDM.Text = gj.getback("select name from zd_SHWXD where code='" + rs["XDDM"].ToString() + "'");
                HYFYDM.Text = gj.getback("select name from zd_HYFY where code='" + rs["HYFYDM"].ToString() + "'");
            }
        }
        //更新声纹数据
        private void UpdateVoiceValue()
        {
            string sql = "";
            if ("" == quanjubianliang.rybh.ToString())
            {
                return;
            }
            else
            {
                sql = "update LEDEN_COLLECT_VOICEPRINT set QBYP_SC='" + times +
                        "',YXYP_SC='" + times_now +
                        "',XZB='" + fSnr +
                        "',NLZ='" + fAvgEnergy +
                        "',XXZLDF='" + nScore +
                        "',LYYZDM='" + gj.getback("select code from zd_LYYZH where name='" + LYYZDM.Text + "'") +
                        "',LYSB='" + vessons +
                        "',FYFSDM='" + gj.getback("select code from zd_FYFS where name='" + FYFSDM.Text + "'") +
                        "',XDDM='" + gj.getback("select code from zd_SHWXD where name='" + XDDM.Text + "'") +
                        "',HYFYDM='" + gj.getback("select code from zd_HYFY where name='" + HYFYDM.Text + "'") +
                             "',ANNEX='" + fileName +
                        "',DELETAG='0' where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
                gj.sqliteexcu(sql);
     
                sql = "update LEDEN_COLLECT_VOICEPRINT set YPSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
                gj.savePicture1(File2Bytes(wavname), sql);
                sql = "update LEDEN_COLLECT_VOICEPRINT set SWSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
                gj.savePicture1(File2Bytes(fileName), sql);
            }
        }
        //保存声纹数据
        private void WriteVoiceValue()
        {
            string sql = "";
            if ("" == quanjubianliang.rybh.ToString())
            {
                return;
            }
            else
            {
                string uuid = "";
                uuid = System.Guid.NewGuid().ToString();
                sql = "insert into LEDEN_COLLECT_VOICEPRINT(PK_ID,RYJCXXCJBH,QBYP_SC,YXYP_SC,XZB,NLZ,XXZLDF,LYYZDM,LYSB,FYFSDM,XDDM,HYFYDM,ANNEX,DELETAG)values('"
                      + uuid + "','"
                      + quanjubianliang.rybh.ToString() + "','"
                      + times + "','"
                      + times_now + "','"
                      + fSnr + "','"
                      + fAvgEnergy + "','"
                      + nScore + "','"
                      + gj.getback("select code from zd_LYYZH where name='" + LYYZDM.Text + "'") + "','"
                      + vessons + "','"
                      + gj.getback("select code from zd_FYFS where name='" + FYFSDM.Text + "'") + "','"
                      + gj.getback("select code from zd_SHWXD where name='" + XDDM.Text + "'") + "','"
                      + gj.getback("select code from zd_HYFY where name='" + HYFYDM.Text + "'") + "','"
                      + fileName + "','"
                      + "0" + "')";
                
                gj.sqliteexcu(sql);
                sql = "update LEDEN_COLLECT_VOICEPRINT set YPSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
                gj.savePicture1(File2Bytes(wavname), sql);
                sql = "update LEDEN_COLLECT_VOICEPRINT set SWSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";
                gj.savePicture1(File2Bytes(fileName), sql);
            }

        }
    }
}
