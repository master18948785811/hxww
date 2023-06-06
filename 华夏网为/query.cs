using Microsoft.Office.Interop.Word;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为
{
    public partial class query : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        main form;
        string value = "";
        string id = "";
        int temp = 0;
        bool col = false;   //检测是否直接关闭false 直接关闭 true 双击某个信息关闭
        string getrybh = "";              //指纹卡数据对象数组
        Object[] FingersCard = new Object[41];
        //指纹卡所需数据数组
        string[] FingersMes = new string[39];

        //确认卡数据对象数组
        object[] querenCard = new object[20];
        //确认卡所需数据数组
        string[] querenMes = new string[20];
        object[] GMSFZHM = new object[18];//公民身份证号码数据对象数组
        public query(main a)
        {
            InitializeComponent();
            form = a;
        }
        private void query_Load(object sender, EventArgs e)
        {
            InitFingersCard();
            //dateTimePicker1.Value=Convert.ToDateTime("2013/1/19");
            dateTimePicker1.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            //获取合计条数
            //string sqlcount = "SELECT COUNT(*) FROM renyuanjibenxinxi where  shifoushanchu =1";
            dataGridView1.Visible = true;

            ShowMessage();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if ("" == textBox1.Text && "" == textBox2.Text)
            //{
            //    MessageBox.Show("查询条件不能为空，请填入查询条件");
            //    return;
            //}
            dataGridView1.Visible = true;
        
            ShowMessage();
            //button1.Visible = false;

        }
        private void ShowMessage()
        {

            string sql = "select * from  renyuanjibenxinxi where 1=1 and shifoushanchu=1   ";
            if ("" != textBox1.Text)
            {
                sql = sql + " and gmsfzhm = '"+textBox1.Text.Trim()+"'";
            }
            else if ("" != textBox2.Text)
            {
                sql = sql + " and  xm ='" + textBox2.Text + "'";
            }
          if(checkBox1.Checked)
            { 
            sql = sql + " and  caijishijian between '" + dateTimePicker1.Text + "' and '" + dateTimePicker2.Text + "' ";
            }
            sql = sql + "order by caijishijian desc";
            this.dataGridView1.Rows.Clear();
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(dataGridView1);
                dr.Cells[0].Value = rs["rycjbh"].ToString();
                dr.Cells[1].Value = rs["gmsfzhm"].ToString();
                dr.Cells[2].Value = rs["xm"].ToString();
              
                if (Convert.ToInt32(rs["xbdm"].ToString()) == 1)
                {
                    dr.Cells[3].Value = "男";
                }
                else if (Convert.ToInt32(rs["xbdm"].ToString()) == 2)
                {
                    dr.Cells[3].Value = "女";
                }
                else
                {
                    dr.Cells[3].Value = "未知";
                }
                dr.Cells[4].Value = rs["csrq"].ToString();
                dr.Cells[6].Value = gj.getback("select mingcheng from zd_guoji where bianhao='" + rs["gjdm"].ToString() + "'"); ;
                dr.Cells[5].Value = gj.getback("select mingcheng from zd_minzu where bianhao='" + rs["mzdm"].ToString() + "'");
                dr.Cells[7].Value = rs["caijishijian"].ToString();
                if (rs["shifoushangchuan"].ToString() == "1")
                {
                    dr.Cells[8].Value = "已上传";

                }
                else
                {
                    dr.Cells[8].Value = "未上传";
                }

                this.dataGridView1.Rows.Add(dr);
                temp++;
            }
        }
        private void query_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!col)
            {
                quanjubianliang.i_query = 1;
            }
        }
        //单击选中
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
           
                    id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
              
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column11")
            //{

            //    try
            //    {
            //        string strSerializeJSON = "userName=huaxiawangwei&passWord=huaxiawangwei2020&rybh=" + dataGridView1.CurrentRow.Cells[0].Value.ToString();

            //        string result = null;        //接受服务器信息
            //        result = Appcode.SD_WebSvcCaller.QueryPostWebService("http://10.48.21.54:58888/htmisWebService/person/getDataState", strSerializeJSON);
            //        JObject jb = JObject.Parse(result);
            //        string status = (string)jb["status1"];
            //        switch (status)
            //        {
            //            case "-4": MessageBox.Show("用户名或密码错误"); break;
            //            case "-3": MessageBox.Show("该用户权限不足"); break;
            //            case "-2": MessageBox.Show(result); break;
            //            case "-1": MessageBox.Show("未找到该数据"); break;
            //            case "0": MessageBox.Show("数据正在等待解析"); break;
            //            case "1": MessageBox.Show("数据包已解析"); break;
            //            default: break;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column6")
            {
              
               
                Appcode.sdyuantuFPT5 getpack = new Appcode.sdyuantuFPT5();
                getpack.packFPT(Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value), "1419");
                //上传至FTP
                XmlDocument fhxml = new XmlDocument();
                fhxml.Load(@"zwftp.xml");
                string hostname = fhxml.SelectSingleNode("//fpt//hostname").InnerText;
                FileInfo fileinfo = new FileInfo(gj.getdburl1() + "FPT\\WSQ\\"+dataGridView1.CurrentRow.Cells[0].Value+".fptx");//zip路径
                string targetDir = fhxml.SelectSingleNode("//fpt//targetDir").InnerText;
                string username = fhxml.SelectSingleNode("//fpt//username").InnerText;
                string password = fhxml.SelectSingleNode("//fpt//password").InnerText;
                string ports = fhxml.SelectSingleNode("//fpt//ports").InnerText;
                HXWW_FTP.ftpcountect FTP1 = new HXWW_FTP.ftpcountect();
                string outmessage;
                if (FTP1.CheckFtp(hostname.Split(':')[0].ToString(), username, password, out outmessage, Convert.ToInt32(ports)))
                {
                    HXWW_FTP.ftpcreat FFFF = new HXWW_FTP.ftpcreat();
                    FFFF.UploadFile(fileinfo, targetDir, hostname, username, password);
                    MessageBox.Show("生成指纹包成功");
                }
                else
                {
                    MessageBox.Show(outmessage);
                
                    return;
                }
               
                


            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column14")
            {
                
                    this.Hide();
                    value = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                    if ("" != value)
                    {
                        quanjubianliang.rybh= value;
                       
                    }

                    col = true;
                    if (col)
                    {
                        if ("" != value)
                        {
                            
                        quanjubianliang.i_query = 0;
                            
                        
                            form.changeb("jibenxinxi");
                            this.Close();
                        }
                    }

               
            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column9")
            {

                DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                switch (RSS)
                {
                    case DialogResult.Yes:
                        for (int i = this.dataGridView1.SelectedRows.Count; i > 0; i--)
                        {
                            id = this.dataGridView1.SelectedRows[i - 1].Cells[0].Value.ToString();
                            this.dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i - 1].Index);

                            //使用获得的ID删除数据库的数据
                            string sql = "update  renyuanjibenxinxi set shifoushanchu =0 where rycjbh='" + id + "'";
                      
                            gj.sqliteexcu(sql);
                        }
                        break;
                    case DialogResult.No:
                        break;
                }


            }
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column6")
            {




            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.Enabled = false;

            if (id == "")
            {
                MessageBox.Show("人员编号不存在，请检查是否有人员编号");
                this.button2.Enabled = true;
                return;
            }
            else
            {
                weitefile_Fig(id);
                    string filePath = System.IO.Directory.GetCurrentDirectory() + "\\SDZWK.docx";
                    string fliePathSrc = System.IO.Directory.GetCurrentDirectory() + "\\zhiwenka\\" + id + ".docx";
                    if (!System.IO.File.Exists(filePath))
                    {
                    MessageBox.Show(filePath);
                    MessageBox.Show(fliePathSrc);
                    return;

                }
                       
                    FileCopy(filePath, fliePathSrc);
                    ReadFingersMes();
                    WriteBookMark(fliePathSrc);
               
               
            }
            this.button2.Enabled = true;

        }
        private void weitefile_Fig( string uuid)
        {
            try
            {
                string sql = "select * from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + uuid + "'and ZW_TXYSFFMS='0000'";
                string path = "";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (DBNull.Value != rs["ZW_TXSJ"] && ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZW_TXSJ"];
                        switch (rs["ZWZWDM"].ToString())
                        {
                            case "1":
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                            case "10":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_RP_" + rs["ZWZWDM"].ToString() + ".Bmp";
                                break;
                            case "11":
                            case "12":
                            case "13":
                            case "14":
                            case "15":
                            case "16":
                            case "17":
                            case "18":
                            case "19":
                            case "20":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_FP_" + (Convert.ToInt32(rs["ZWZWDM"].ToString()) - 10).ToString() + ".Bmp";
                                break;
                            default: break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }
                sql = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + uuid + "'and ZHW_TXYSFSMS='0000'";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {
                    if (null != rs["ZHW_TXSJ"] || ((byte[])rs["ZW_TXSJ"]).Length > 0)
                    {
                        byte[] fileBytes = (byte[])rs["ZHW_TXSJ"];
                        switch (rs["ZHWZHWDM"].ToString())
                        {
                            case "31":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_PM_R.bmp";
                                break;
                            case "32":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_PM_L.bmp";
                                break;
                            case "33":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_PW_R.bmp";
                                break;
                            case "34":
                                path = System.Windows.Forms.Application.StartupPath.ToString() + "//ZIP//" + uuid + "//" + uuid + "_PW_L.bmp";
                                break;
                        }
                        if (System.IO.File.Exists(path)) //如果已经存在
                        {
                            System.IO.File.Delete(path);
                        }
                        FileStream fs = new FileStream(path, FileMode.Create);
                        fs.Write(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        //实现文件复制  
        void FileCopy(string path1, string path2)
        {
            try
            {
                int bufferSize = 10240;

                Stream source = new FileStream(path1, FileMode.Open, FileAccess.Read);
                Stream target = new FileStream(path2, FileMode.Create, FileAccess.Write);

                byte[] buffer = new byte[bufferSize];
                int bytesRead;
                do
                {
                    bytesRead = source.Read(buffer, 0, bufferSize);
                    target.Write(buffer, 0, bytesRead);
                }
                while (bytesRead > 0);
                source.Dispose();
                target.Dispose();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        //将数据写入文件书签
        void WriteBookMark(string FilePath)
        {
            object Nothing = System.Reflection.Missing.Value;
            //创建一个名为wordApp的组件对象
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            //word文档位置
            Object filename = FilePath;
            //定义该插入图片是否为外部链接
            Object linkToFile = true;
            //定义插入图片是否随word文档一起保存
            Object saveWithDocument = true;
            Object confirmConversions = Type.Missing;
            Object readOnly = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;
            for (int i = 1; i <= wordApp.Documents.Count; i++)
            {
                String str = wordApp.Documents[i].FullName.ToString();
                if (str == filename.ToString())
                {
                    MessageBox.Show("请勿重复打开该文档");
                    return;
                }
            }
            //打开word文档
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing);


            try
            {
                //标签
                for (int i = 0; i < 10; i++)
                {
                    Object bookMark = FingersCard[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                        doc.Bookmarks.get_Item(ref bookMark).Range.Text = FingersMes[i];
                    }

                }

                for (int i = 0; i < 18; i++)
                {

                    Object bookMark = GMSFZHM[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                        doc.Bookmarks.get_Item(ref bookMark).Range.Text = FingersMes[6].ToString().Substring(i, 1);
                    }

                }
                for (int i = 37; i <38; i++)
                {

                    Object bookMark = FingersCard[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                        doc.Bookmarks.get_Item(ref bookMark).Range.Text = FingersMes[11];
                    }

                }
                for (int i = 39; i < 40; i++)
                {

                    Object bookMark = FingersCard[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                        doc.Bookmarks.get_Item(ref bookMark).Range.Text = FingersMes[12];
                    }

                }
                for (int i = 40; i < 41; i++)
                {

                    Object bookMark = FingersCard[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                        doc.Bookmarks.get_Item(ref bookMark).Range.Text = FingersMes[13];
                    }

                }
                string zmzpath = "";
                for (int i = 38; i < 39; i++)
                {

                    Object bookMark = FingersCard[i];
                    zmzpath = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_PH_F"  + ".jpg";
                    if (!System.IO.File.Exists(zmzpath)) //如果不存在
                    {
                        
                    }
                    else
                    {
                        if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                        {
                            //查找书签
                            doc.Bookmarks.get_Item(ref bookMark).Select();
                            //设置图片位置
                            wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                            //在书签的位置添加图片
                            InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(zmzpath, ref linkToFile, ref saveWithDocument, ref Nothing);
                            //设置图片大小
                            inlineShape.Width = 90;
                            inlineShape.Height = 120;
                        }
                    }

                }
                //图片
                string replacePic = "";              //路径
                for (int i = 0; i < 10; i++)        //滚指纹
                {
                    Object bookMark = FingersCard[i + 10];//从指纹开始算下标
                    replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_RP_" + (i + 1).ToString() + ".bmp";
                    if (!System.IO.File.Exists(replacePic)) //如果不存在
                    {
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                    }
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        //查找书签
                        doc.Bookmarks.get_Item(ref bookMark).Select();
                        //设置图片位置
                        wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        //在书签的位置添加图片
                        InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                        //设置图片大小
                        //inlineShape.Width = 80;
                        //inlineShape.Height = 80;
                    }
                }

                for (int i = 0; i < 10; i++)        //平面指纹
                {
                    Object bookMark = FingersCard[i + 27];//从指纹开始算下标
                    replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_FP_" + (i + 1).ToString() + ".bmp";
                    if (!System.IO.File.Exists(replacePic)) //如果不存在
                    {
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                    }
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        //查找书签
                        doc.Bookmarks.get_Item(ref bookMark).Select();
                        //设置图片位置
                        wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        //在书签的位置添加图片
                        InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                        //设置图片大小
                        //inlineShape.Width = 100;
                        //inlineShape.Height = 100;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        Object bookMark = FingersCard[i + 20];//从掌纹开始算下标
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_PW_R" + ".bmp";
                        if (!System.IO.File.Exists(replacePic)) //如果不存在
                        {
                            replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                        }
                        else
                        {
                            if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                            {
                                //查找书签
                                doc.Bookmarks.get_Item(ref bookMark).Select();
                                //设置图片位置
                                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //在书签的位置添加图片
                                InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                                //设置图片大小
                                inlineShape.Width = 255;
                                inlineShape.Height = 255;
                            }
                        }
                    }
                    if (i == 1)
                    {
                        Object bookMark = FingersCard[i + 20];//从掌纹开始算下标
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_PW_L" + ".bmp";
                        if (!System.IO.File.Exists(replacePic)) //如果不存在
                        {
                            replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                        }
                        else
                        {
                            if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                            {
                                //查找书签
                                doc.Bookmarks.get_Item(ref bookMark).Select();
                                //设置图片位置
                                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //在书签的位置添加图片
                                InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                                //设置图片大小
                                inlineShape.Width = 255;
                                inlineShape.Height = 255;

                            }
                        }
                    }
                    if (i == 2)
                    {
                        Object bookMark = FingersCard[i + 20];//从掌纹开始算下标
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_PM_L" + ".bmp";
                        if (!System.IO.File.Exists(replacePic)) //如果不存在
                        {
                            replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                        }
                        else
                        {
                            if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                            {
                                //查找书签
                                doc.Bookmarks.get_Item(ref bookMark).Select();
                                //设置图片位置
                                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //在书签的位置添加图片
                                InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                                //设置图片大小
                                inlineShape.Width = 255;
                                inlineShape.Height = 255;
                            }
                        }
                    }
                    if (i == 3)
                    {
                        Object bookMark = FingersCard[i + 20];//从掌纹开始算下标
                        replacePic = System.IO.Directory.GetCurrentDirectory() + "\\zip\\" + id + "\\" + id + "_PM_R" + ".bmp";
                        if (!System.IO.File.Exists(replacePic)) //如果不存在
                        {
                            replacePic = System.IO.Directory.GetCurrentDirectory() + "\\images\\dz.bmp";
                        }
                        else
                        {
                            if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                            {
                                //查找书签
                                doc.Bookmarks.get_Item(ref bookMark).Select();
                                //设置图片位置
                                wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                                //在书签的位置添加图片
                                InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                                //设置图片大小
                                inlineShape.Width = 255;
                                inlineShape.Height = 255;
                            }
                        }
                    }

                }
                
                doc.Save();
                wordApp.Visible = true;
                //doc.Close(ref Nothing, ref Nothing, ref Nothing);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开Word文档出错:" + ex.ToString());
            }
        }
        //初始化word标签数组
        void InitFingersCard()
        {
            FingersCard[0] = "RYBH";
            FingersCard[1] = "ZWKH";
            FingersCard[2] = "XM";
            FingersCard[3] = "BM";
            FingersCard[4] = "XB";
            FingersCard[5] = "CSRQ";
            FingersCard[6] = "GMSFZH";
            FingersCard[7] = "HJD";
            FingersCard[8] = "XZD";
            FingersCard[9] = "NYRQ";

            FingersCard[10] = "YMZ";
            FingersCard[11] = "YSZ";
            FingersCard[12] = "YZZ";
            FingersCard[13] = "YHZ";
            FingersCard[14] = "YXZ";
            FingersCard[15] = "ZMZ";
            FingersCard[16] = "ZSZ";
            FingersCard[17] = "ZZZ";
            FingersCard[18] = "ZHZ";
            FingersCard[19] = "ZXZ";

            FingersCard[20] = "ZSCZW";
            FingersCard[21] = "YSCZW";
            FingersCard[22] = "ZSZW";
            FingersCard[23] = "YSZW";
            FingersCard[24] = "ZP1";
            FingersCard[25] = "ZP2";
            FingersCard[26] = "ZP3";

            FingersCard[27] = "YS1";
            FingersCard[28] = "YS2";
            FingersCard[29] = "YS3";
            FingersCard[30] = "YS4";
            FingersCard[31] = "YS5";
            FingersCard[32] = "ZS1";
            FingersCard[33] = "ZS2";
            FingersCard[34] = "ZS3";
            FingersCard[35] = "ZS4";
            FingersCard[36] = "ZS5";
            FingersCard[37] = "BCJYY";
            FingersCard[38] = "ZMZP";
            FingersCard[39] = "NYDW";
            FingersCard[40] = "RYLB";


            GMSFZHM[0] = "GMSFZH1";
            GMSFZHM[1] = "GMSFZH2";
            GMSFZHM[2] = "GMSFZH3";
            GMSFZHM[3] = "GMSFZH4";
            GMSFZHM[4] = "GMSFZH5";
            GMSFZHM[5] = "GMSFZH6";
            GMSFZHM[6] = "GMSFZH7";
            GMSFZHM[7] = "GMSFZH8";
            GMSFZHM[8] = "GMSFZH9";
            GMSFZHM[9] = "GMSFZH10";
            GMSFZHM[10] = "GMSFZH11";
            GMSFZHM[11] = "GMSFZH12";
            GMSFZHM[12] = "GMSFZH13";
            GMSFZHM[13] = "GMSFZH14";
            GMSFZHM[14] = "GMSFZH15";
            GMSFZHM[15] = "GMSFZH16";
            GMSFZHM[16] = "GMSFZH17";
            GMSFZHM[17] = "GMSFZH18";
        }
        //读取指纹卡所要显示的信息
        void ReadFingersMes()
        {
            /*指纹信息*/
            string sql = "select * from renyuanjibenxinxi where rycjbh='" + id + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                FingersMes[0] = rs["rycjbh"].ToString();        //人员编号s 
                FingersMes[1] = rs["rycjbh"].ToString().Substring(1, rs["rycjbh"].ToString().Length-1);         //卡号

                /*基本信息*/

                FingersMes[2] = rs["xm"].ToString();     //姓名
                FingersMes[3] = "";    //别名
                if ("1" == rs["xbdm"].ToString())          //性别
                {
                    FingersMes[4] = "男";
                }
                else if ("2" == rs["xbdm"].ToString())
                {
                    FingersMes[4] = "女";
                }
                FingersMes[5] = rs["csrq"].ToString();      //出生日期
                FingersMes[6] = rs["gmsfzhm"].ToString();
                FingersMes[7] = rs["hjdz_dzmc"].ToString();       //户籍地
                FingersMes[8] = rs["xzz_dzmc"].ToString();      //现住地
                FingersMes[9] = System.DateTime.Now.ToString("yyyyMMdd");      //现住地
                FingersMes[10] = "";
                FingersMes[11] = gj.getback("select  mingcheng from zd_caijiyuanyin where bianhao ='" + rs["caijiyuanyin"].ToString() + "'");
                FingersMes[12] = gj.getback("select  UnitName from yonghu where id ='" + rs["caijirendaima"].ToString() + "'");
                FingersMes[13] = gj.getback("select  mingcheng from zd_renyuanleibie where bianhao ='" + rs["renyuanleibie"].ToString() + "'");
            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.button3.Enabled = false;
            try
            {
                if (id == "")
                {
                    MessageBox.Show("请选中需要打印数据!");
                    this.button3.Enabled = true;
                    return;
                }


                

                  
                    prints1(id);

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            this.button3.Enabled = true;
        }
        public void prints1(String UID)
        {
            KillProcess("WINDORD");
            KillProcess("WPS");
            KillProcess("Microsoft Office Word");
            KillProcess("Microsoft Office Word (32位)");



            string filePath = System.Windows.Forms.Application.StartupPath.ToString() + "\\queren.docx";
            string fliePathSrc = System.Windows.Forms.Application.StartupPath.ToString() + "\\queren\\" + UID + ".docx";
            if (!System.IO.File.Exists(filePath))
                return;
            InitFingersCard1();
            FileCopy(filePath, fliePathSrc);
            ReadFingersMes1(UID);
            WriteBookMark1(fliePathSrc);
        }
        private static void KillProcess(string processName)//杀死与Excel相关的进程
        {
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();//得到所有打开的进程
            try
            {
                foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
                {
                    if (!thisproc.CloseMainWindow())
                    {
                        thisproc.Kill();
                    }
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }
        public void InitFingersCard1()
        {
            querenCard[0] = "RYBH";
            querenCard[1] = "CJDW";
            querenCard[2] = "XM";
            querenCard[3] = "XB";
            querenCard[4] = "CSRQ";
            querenCard[5] = "CJRQ";
            querenCard[6] = "HJXXDZ";
            querenCard[7] = "JZDXXDZ";
            querenCard[8] = "ZW";
            querenCard[9] = "RX";
            querenCard[10] = "SW";
            querenCard[11] = "HM";
            querenCard[12] = "DNA";
            querenCard[13] = "ZJ";
            querenCard[14] = "BJ";
            querenCard[15] = "SJ";
            querenCard[16] = "TBBJ";
            querenCard[17] = "SSWP";
            querenCard[18] = "QRQZ";
            querenCard[19] = "QRRQ";

        }
        public void ReadFingersMes1(String UID)
        {
            /*指纹信息*/
            try
            {
                string sql = "select * from renyuanjibenxinxi where rycjbh='" + UID + "'";
                foreach (DataRow rs in gj.gettable(sql).Rows)
                {

                    querenMes[0] = UID;        //人员编号s 
                    querenMes[1] = gj.getback("select UnitName from yonghu where id='" + rs["caijirendaima"].ToString() +"'");                   //卡号
                    /*基本信息*/
                    querenMes[2] = rs["XM"].ToString();     //姓名
                    if ("1" == rs["XBDM"].ToString())          //性别
                    {
                        querenMes[3] = "男";
                    }
                    else if ("2" == rs["XBDM"].ToString())
                    {
                        querenMes[3] = "女";
                    }
                    querenMes[4] = rs["CSRQ"].ToString();      //出生日期
                    querenMes[5] = Convert.ToDateTime(rs["caijishijian"]).ToString("yyyyMMdd");

                    querenMes[6] = rs["hjdz"].ToString();       //户籍地
                    querenMes[7] = rs["xzz_dzmc"].ToString();      //现住地
                    querenMes[19] = System.DateTime.Now.ToString("yyyy-MM-dd");     
                }
                if (gj.getbool("rycjbh", UID, "sanmianzhao") > 0)
                {
                    querenMes[9] = "1";
                }
                else
                {
                    querenMes[9] = "0";
                }
                if (gj.getbool("RYJCXXCJBH", UID, "LEDEN_COLLECT_FINGER") > 0)
                {
                    querenMes[8] = "1";
                }
                else
                {
                    querenMes[8] = "0";
                }
                if (gj.getbool("RYJCXXCJBH", UID, "LEDEN_COLLECT_VOICEPRINT") > 0)
                {
                    querenMes[10] = "1";
                }
                else
                {
                    querenMes[10] = "0";
                }
                if (gj.getbool("rycjbh", UID, "DNA") > 0)
                {
                    querenMes[11] = "1";
                }
                else
                {
                    querenMes[11] = "0";
                }
                if (gj.getbool("RYJCXXCJBH", UID, "zuji") > 0)
                {
                    querenMes[13] = "1";
                }
                else
                {
                    querenMes[13] = "0";
                }
               
               querenMes[14] = "0";
                querenMes[15] = "0";
                if (gj.getbool("rycjbh", UID, "teshutezheng") > 0)
                {
                    querenMes[16] = "1";
                }
                else
                {
                    querenMes[16] = "0";
                }
                if (gj.getbool("rycjbh", UID, "wuzheng") > 0)
                {
                    querenMes[17] = "1";
                }
                else
                {
                    querenMes[17] = "0";
                }

            }
            catch
            {
                MessageBox.Show("获取信息人员基本信息失败！");
            }

        }
        public void WriteBookMark1(string FilePath)
        {
            object Nothing = System.Reflection.Missing.Value;
            //创建一个名为wordApp的组件对象
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            //word文档位置
            Object filename = FilePath;
            //定义该插入图片是否为外部链接
            Object linkToFile = true;
            //定义插入图片是否随word文档一起保存
            Object saveWithDocument = true;
            Object confirmConversions = Type.Missing;
            Object readOnly = Type.Missing;
            Object addToRecentFiles = Type.Missing;
            Object passwordDocument = Type.Missing;
            Object passwordTemplate = Type.Missing;
            Object revert = Type.Missing;
            Object writePasswordDocument = Type.Missing;
            Object writePasswordTemplate = Type.Missing;
            Object format = Type.Missing;
            Object encoding = Type.Missing;
            Object visible = Type.Missing;
            Object openConflictDocument = Type.Missing;
            Object openAndRepair = Type.Missing;
            Object documentDirection = Type.Missing;
            Object noEncodingDialog = Type.Missing;
            for (int i = 1; i <= wordApp.Documents.Count; i++)
            {
                String str = wordApp.Documents[i].FullName.ToString();
                if (str == filename.ToString())
                {
                    MessageBox.Show("请勿重复打开该文档");
                    return;
                }
            }
            // 打开word文档
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing,
               ref Nothing, ref Nothing, ref Nothing, ref Nothing);


            try
            {
                //标签
                for (int i = 0; i < 20; i++)
                {

                    Object bookMark = querenCard[i];
                    if (doc.Bookmarks.Exists(Convert.ToString(bookMark)) == true)
                    {
                        if (i > 7 && i < 18)
                        {
                            wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                            if (Convert.ToInt32(querenMes[i]) == 1)
                            { doc.Bookmarks.get_Item(ref bookMark).Range.Text = "√"; }


                        }
                        else
                        {
                            wordApp.ActiveDocument.Bookmarks.get_Item(ref bookMark).Select();
                            doc.Bookmarks.get_Item(ref bookMark).Range.Text = querenMes[i];

                        }

                    }

                }

                doc.Save();
                wordApp.Visible = true;
                //doc.Close(ref Nothing, ref Nothing, ref Nothing);
                //KillProcess("WINWORD");
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开Word文档出错:" + ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.button2.Enabled = false;

                if (id == "")
                {
                    MessageBox.Show("人员编号不存在，请检查是否有人员编号");
                    this.button2.Enabled = true;
                    return;
                }
                else
                {
                    string unitcode = gj.getback("select address from yonghu where id='" + quanjubianliang.cjrbh + "'");
                    string sysid = gj.getback("select sysid from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
                    string password = gj.getback("select passWord from shebaizhuce where sbid='" + quanjubianliang.ythsbid + "'");
      
                    string result = Appcode.SD_WebSvcCaller.QueryGetWebService("http://" + quanjubianliang.webserviceip + "/pics/api/data/getRybh", unitcode, sysid, password);

                    if (null != result)
                    {
                        JObject jo = JObject.Parse(result);
                        if ("1" == jo["flag"].ToString())//生成的编号成功
                        {
                            string xid = jo["data"].ToString();
                            string xg1 = "Update renyuanjibenxinxi set rycjbh='"+xid+"' where rycjbh='"+id+"'";
                            string xg2 = "Update sanmianzhao set rycjbh='"+xid+"' where rycjbh='"+id+"'";
                            string xg3 = "Update xingti set rycjbh='"+xid+"' where rycjbh='"+id+"'";
                            string xg4 = "Update LEDEN_COLLECT_FINGER set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            string xg5 = "Update LEDEN_COLLECT_FOURFINGER set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            string xg6 = "Update LEDEN_COLLECT_PALM set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            string xg7 = "Update LEDEN_COLLECT_VOICEPRINT set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            string xg8 = "Update hongmu set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            string xg9 = "Update teshutezheng set rycjbh='"+xid+"' where rycjbh='"+id+"'";
                            string xg10 = "Update wuzheng set rycjbh='"+xid+"' where rycjbh='"+id+"'";
                            string xg11= "Update zuji set RYJCXXCJBH='"+xid+"' where RYJCXXCJBH='"+id+"'";
                            gj.getback(xg11);
                            gj.getback(xg10);
                            gj.getback(xg9);
                            gj.getback(xg8);
                            gj.getback(xg7);
                            gj.getback(xg6);
                            gj.getback(xg5);
                            gj.getback(xg4);
                            gj.getback(xg3);
                            gj.getback(xg2);
                            gj.getback(xg1);
                            MessageBox.Show("修改完成请重新查询");


                        }
                        else
                        {
                            MessageBox.Show("生成编号出错" + result);

                        }
                    }


                }
                this.button2.Enabled = true;
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
