using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace 华夏网为.hongmu
{
    public partial class hongmu : Form
    {
        Appcode.Class2 gj = new Appcode.Class2();
        public struct s_IRIS
        {
            public string UUID;
            public string HMYWDM;
            public byte[] HMSJ;
            public string HMQSQKDM;
            public string XXZLDF;
            public string HMCJHS;
            public string QZCJBZ;
            public string HMCJSBXHDM;
            public string SBBH;
            public string SBCSDM;
        }
        s_IRIS[] ir = new s_IRIS[2];
        public hongmu()
        {
            InitializeComponent();
        }

        private void hongmu_Load(object sender, EventArgs e)
        {

            Thread newthread = new Thread(new ThreadStart(open));
            newthread.Start();
            if (getinitxml(axBiometrics1.initIrisOCX()))
            {  
            }
            else
            {
                MessageBox.Show("初始化失败");
            }
           
        }
        void open()
        {
            Thread.Sleep(1000);
            if (gj.getbool("RYJCXXCJBH",quanjubianliang.rybh.ToString(),  "hongmu") > 0)
            {
                readvalue();
            }
        }
        //读取虹膜数据
        private void readvalue()
        {
            string irisXml = "";
            irisXml += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            irisXml += "<root>";
            irisXml += "<IRISLIST>";
            string sqlIris = "select * from hongmu where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'";

            foreach (DataRow rs in gj.gettable(sqlIris).Rows)
            {
                //if(((byte[])rs["HMSJ"]).Length>0)//检测是否有虹膜数据a
                {
                    irisXml += "<IRIS>";
                    irisXml += "<HMYWDM>" + rs["HMYWDM"].ToString().ToString() + "</HMYWDM>";
                    irisXml += "<HMSJ>" + Convert.ToBase64String((byte[])rs["HMSJ"]) + "</HMSJ>";
                    irisXml += "<HMQSQKDM>" + rs["HMQSQKDM"].ToString() + "</HMQSQKDM>";
                    irisXml += "<XXZLDF>" + rs["XXZLDF"].ToString() + "</XXZLDF>";
                    irisXml += "<HMCJHS>" + "0" + "</HMCJHS>";
                    irisXml += "<QZCJBZ>" + "0" + "</QZCJBZ>";
                    irisXml += "<HMCJSBXHDM>" + "0" + "</HMCJSBXHDM>";
                    irisXml += "<SBBH>" + "0" + "</SBBH>";
                    irisXml += "<SBCSDM>" + "0" + "</SBCSDM>";
                    irisXml += "</IRIS>";
                }
            }
            irisXml += "</IRISLIST>";
            irisXml += "</root>";
            //传入数据(用于展示)
            string re = axBiometrics1.setIrisInfoList(irisXml);
        }
        //保存虹膜数据
        private void writeIRISvalue()
        {
            string sql = "";
            if ("" == quanjubianliang.rybh.ToString())
            {
                return;
            }
            else
            {
                string uuid = "";
                for (int i = 0; i < 2; i++)
                {
                    uuid = System.Guid.NewGuid().ToString();
                    sql = "insert into hongmu(RYJCXXCJBH,HMYWDM,HMQSQKDM,XXZLDF,QZCJBZ,HMCJSBXHDM,HMCJHS,SBBH,SBCSDM,DELETAG)values('"

                          + quanjubianliang.rybh.ToString() + "','"
                          + ir[i].HMYWDM + "','"
                          + ir[i].HMQSQKDM + "','"
                          + ir[i].XXZLDF + "','"
                          + ir[i].QZCJBZ + "','"
                          + ir[i].HMCJSBXHDM + "','"
                          + ir[i].HMCJHS + "','"
                          + ir[i].SBBH + "','"
                          + ir[i].SBCSDM + "','"
                          + "0" + "')";
                    gj.sqliteexcu(sql);
                    sql = "update hongmu set HMSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'and HMYWDM='" + ir[i].HMYWDM + "'";
                    gj.savePicture1(ir[i].HMSJ, sql);
                    if (ir[i].HMSJ.ToString().Length > 1)
                    {
                        MemoryStream ms = new MemoryStream((byte[])ir[i].HMSJ);
                        Image image = System.Drawing.Image.FromStream(ms);
                        if (ir[i].HMYWDM.ToString() == "1")
                        {
                            image.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "iris_left.bmp");
                        }
                        else if (ir[i].HMYWDM.ToString() == "2")
                        {
                            image.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "iris_right.bmp");

                        }
                    }
                }

            }

        }
        //更新虹膜数据
        private void updateIRISvalue()
        {
            string sql = "";
            if ("" == quanjubianliang.rybh.ToString())
            {
                return;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    sql = "update hongmu set HMYWDM='" + ir[i].HMYWDM +
                            "',HMQSQKDM='" + ir[i].HMQSQKDM +
                            "',XXZLDF='" + ir[i].XXZLDF +
                            "',QZCJBZ='" + ir[i].QZCJBZ +
                            "',HMCJSBXHDM='" + ir[i].HMCJSBXHDM +
                            "',HMCJHS='" + ir[i].HMCJHS +
                            "',SBBH='" + ir[i].SBBH +
                            "',SBCSDM='" + ir[i].SBCSDM +
                            "',DELETAG='0' where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'and HMYWDM='" + ir[i].HMYWDM + "'";
                    gj.sqliteexcu(sql);
                    //if (null != ir[i].HMSJ && ir[i].HMSJ.Length > 1)//有虹膜数据的更新
                    {
                        sql = "update hongmu set HMSJ=@data where RYJCXXCJBH='" + quanjubianliang.rybh.ToString() + "'and HMYWDM='" + ir[i].HMYWDM + "'";
                        gj.savePicture1(ir[i].HMSJ, sql);
                    }
                    if (ir[i].HMSJ.ToString().Length > 1)
                    {
                        MemoryStream ms = new MemoryStream((byte[])ir[i].HMSJ);
                        Image image = System.Drawing.Image.FromStream(ms);
                        if (ir[i].HMYWDM.ToString() == "1")
                        {
                            image.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "iris_left.bmp");
                        }
                        else if (ir[i].HMYWDM.ToString() == "2")
                        {
                            image.Save(@"ZIP//" + quanjubianliang.rybh.ToString() + "//" + quanjubianliang.rybh.ToString() + "iris_right.bmp");

                        }
                    }
                }
            }
        }
        private bool GetirisXML(string xml)
        {
            if ("" == xml)
                return false;
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(xml);//加载xml
            XmlNode FirstNode = xx.SelectSingleNode("root");
            XmlNode Node1 = FirstNode.FirstChild;
            XmlNode Node2 = FirstNode.FirstChild.NextSibling;
            XmlElement xe = null;
            foreach (XmlNode xxNode in Node1)
            {
                xe = (XmlElement)xxNode;
                if (xe.Name == "flag")
                    Console.Write(xxNode.InnerText);
                else if (xe.Name == "message")
                    Console.Write(xxNode.InnerText);
            }
            XmlNode Node3 = Node2.FirstChild;
            for (int i = 0; i < 2; i++)
            {
                if (null == Node3)
                    break;
                foreach (XmlNode xxNode in Node3)
                {
                    xe = (XmlElement)xxNode;
                    switch (xe.Name)
                    {
                        case "HMYWDM":
                            ir[i].HMYWDM = xxNode.InnerText;

                            break;
                        case "HMSJ":
                            ir[i].HMSJ = Convert.FromBase64String(xxNode.InnerText);
                            break;
                        case "HMQSQKDM":
                            ir[i].HMQSQKDM = xxNode.InnerText;
                            break;
                        case "XXZLDF":
                            ir[i].XXZLDF = xxNode.InnerText;
                            break;
                        case "HMCJHS":
                            ir[i].HMCJHS = xxNode.InnerText;
                            break;
                        case "QZCJBZ":
                            ir[i].QZCJBZ = xxNode.InnerText;
                            break;
                        case "HMCJSBXHDM":
                            ir[i].HMCJSBXHDM = xxNode.InnerText;
                            break;
                        case "SBBH":
                            ir[i].SBBH = xxNode.InnerText;
                            break;
                        case "SBCSDM":
                            ir[i].SBCSDM = xxNode.InnerText;
                            break;
                    }
                }
                Node3 = Node3.NextSibling;
            }
            return true;
        }

        private void hongmu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ("1" == axBiometrics1.gatherIrisFinished())
            {
                if (GetirisXML(axBiometrics1.getIrisInfoList()))//获取虹膜数据
                {
                    if (gj.getbool("RYJCXXCJBH",quanjubianliang.rybh.ToString(), "hongmu" ) > 0)
                    {
                        updateIRISvalue();

                    }
                    else
                    {
                        writeIRISvalue();

                    }
                }
            }
        }

        private bool getinitxml(string xmls)
        {
            bool k = false;
            XmlDocument xx = new XmlDocument();
            xx.LoadXml(xmls);//加载xml
            if (xx.SelectSingleNode("root/head/flag").InnerText == "SUCCESS")
            {
                k = true;
            }
            return k;
        }
    }
}
