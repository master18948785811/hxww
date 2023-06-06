using Microsoft.Office.Interop.Word;
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
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace 华夏网为
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Appcode.Class2 gj = new Appcode.Class2();
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(System.Windows.Forms.Application.StartupPath.ToString() + "//upload/zip//" + quanjubianliang.rybh + "//fps");
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps");
            }
            string sql = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + quanjubianliang.rybh + "'and ZHW_TXYSFSMS='0000'";
            quanjubianliang.rybh = "R3709210000002020100001";
            string path = "";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                if (null != rs["ZHW_TXSJ"] || ((byte[])rs["ZW_TXSJ"]).Length > 0)
                {
                    byte[] fileBytes = (byte[])rs["ZHW_TXSJ"];
                    switch (rs["ZHWZHWDM"].ToString())
                    {
                        case "31":

                            path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps//finger_31.wsq";
                            break;
                        case "32":
                            path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps//finger_32.wsq";

                            break;
                        case "33":
                            path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps//finger_33.wsq";
                            break;
                        case "34":
                            path = System.Windows.Forms.Application.StartupPath.ToString() + "//upload//" + quanjubianliang.rybh + "//fps//finger_34.wsq";
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

        public struct Phone
        {
            public string rybh;
            public string xm;
            public string gmsfhm;
            public string cjmbbh;
            public string dxbh;
            public string wpmc;
            public string imeiEsnMeid;
            public string macdz;
            public string lymacdz;
            public string xh;
            public string tzms;
            public string sjhm;
        }

        public struct PhoneBook
        {
            public string cjmbbh;
            public string zzxlmc;
            public string txlzdlxdm;
            public string lxdhlx;
            public string gxrlxfs;
            public string sczt;
            public string scsj;
        }

        public struct PhoneRecords
        {
            public string cjmbbh;
            public string yddh;
            public string gxrlxfs;
            public string gxrxm;
            public string thzt;
            public string bdsjbs;
            public string kssj;
            public string jssj;
            public string sc;
            public string smxsz;
            public string ljzt;
            public string sczt;
            public string scsj;
        }

        public struct PhoneSms
        {
            public string cjmbbh;
            public string yddh;
            public string gxrlxfs;
            public string gxrxm;
            public string thzt;
            public string bdsjbs;
            public string dxsfsj;
            public string txnr;
            public string ckzt;
            public string dxccwz;
            public string smxsz;
            public string ljzt;
            public string sczt;
            public string scsj;
        }

        public struct PhoneVid
        {
            public string cjmbbh;
            public string zhlx;
            public string yhid;
            public string zh;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//R3709210000002020100001//phone"))
            //{
            //    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath.ToString() + "//upload//R3709210000002020100001//phone");
            //}

            //if (Appcode.zips.UnZip(System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001.zip", System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001", ""))
            //{
            //    MessageBox.Show(System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001" + "//" + "137-570839051-370611-370611-1660007719-00001" + "-WA_MFORENSICS_010200-0.bcp");
            //    if (System.IO.File.Exists(System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001"+"//"+ "137-570839051-370611-370611-1660007719-00001" + "-WA_MFORENSICS_010200-0.bcp"))
            //    {
            //        MessageBox.Show(gj.redfile(System.Windows.Forms.Application.StartupPath.ToString() + "//mobile//137-570839051-370611-370611-1660007719-00001" + "//" + "137-570839051-370611-370611-1660007719-00001" + "-WA_MFORENSICS_010200-0.bcp"));

            //    }

            //}
            CreateJson();
        }
        private void redxml(string xmlpath)
        {
            //int count = 0;
            XElement xe = XElement.Load(xmlpath);
            //读取第一个DATASET
            IEnumerable<XElement> dataset = from el in xe.Elements("DATASET")
                                            select el;
            foreach (var ele in dataset)
            {
                //Console.WriteLine(ele.Attributes("name").First().Value);
                //Console.WriteLine(ele.Attributes("ver").First().Value);
                //Console.WriteLine(ele.Attributes("rmk").First().Value);
            }
            //读取第二个DATASET
           
            IEnumerable<XElement> datasetNode = from el in xe.Elements("DATASET").Elements("DATA").Elements("DATASET")
                                                select el;
          
            List<List<List<string>>> list = new List<List<List<string>>>();
            List<List<string>> list0 = new List<List<string>>();
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            foreach (var eleNode in datasetNode)
            {
               
                //Console.WriteLine(eleNode.Attributes("name").First().Value);
                //Console.WriteLine(eleNode.Attributes("rmk").First().Value);
                //读取DATA->ITEM
                IEnumerable<XElement> item = from el in eleNode.Elements("DATA").Elements("ITEM")
                                             select el;
                foreach (var eleNode1 in item)
                {
                    //Console.WriteLine(eleNode1.Attributes("key").First().Value.ToString());
                    //Console.WriteLine(eleNode1.Attributes("val").First().Value.ToString());
                    //Console.WriteLine(eleNode1.Attributes("rmk").First().Value.ToString());
                    if ("A010004" == eleNode1.Attributes("key").First().Value.ToString()) 
                    {
                        list1.Add(eleNode1.Attributes("val").First().Value.ToString());
                    }
                 
                    //读取DATA->DATASET
                    //IEnumerable<XElement> datasetNode1 = from el in eleNode.Elements("DATA").First().Elements("DATASET")
                    //                                     select el;
                    IEnumerable<XElement> datasetNode1 = from el in eleNode.Elements("DATA").Elements("DATASET")
                                                         select el;
                    foreach (var eleNode2 in datasetNode1)
                    {
                        //Console.WriteLine(eleNode2.Attributes("name").First().Value.ToString());
                        //Console.WriteLine(eleNode2.Attributes("rmk").First().Value.ToString());

                        //读取DATA->DATASET->DATA->DATASET->DATA
                        //IEnumerable<XElement> datasetNode2 = from el in eleNode2.Elements("DATA").First().Elements("ITEM")
                        //                                     select el;
                        IEnumerable<XElement> datasetNode2 = from el in eleNode2.Elements("DATA").Elements("ITEM")
                                                             select el;
                        foreach (var eleNode3 in datasetNode2)
                        {
                            switch (eleNode2.Attributes("name").First().Value)
                            {
                                case "WA_COMMON_010014":
                                    if("H010020"==eleNode3.Attributes("key").First().Value.ToString())
                                    {
                                        //Console.WriteLine(eleNode3.Attributes("key").First().Value);
                                        //Console.WriteLine(eleNode3.Attributes("val").First().Value);
                                        //Console.WriteLine(eleNode3.Attributes("rmk").First().Value);
                                        list2.Add(eleNode3.Attributes("val").First().Value.ToString());
                                    }
                                    break;
                                case "WA_COMMON_010015":
                                    //Console.WriteLine(eleNode3.Attributes("key").First().Value);
                                    //Console.WriteLine(eleNode3.Attributes("eng").First().Value);
                                    //Console.WriteLine(eleNode3.Attributes("chn").First().Value);
                                    list3.Add(eleNode3.Attributes("eng").First().Value.ToString());
                                    break;
                            }
                        }
                    }
                }
            }
            list0.Add(list2);
            list0.Add(list3);
            list.Add(list0);
            return;
        }
       
        private string CreateJson()
        {
            string getxmldata = "";
            string str = "E:\\VS2019\\xinjiemian\\xinjiemian\\xinjiemian\\华夏网为\\华夏网为\\bin\\Debug\\mobile\\137-570839051-370611-370611-1660007719-00001\\GAB_ZIP_INDEX.xml";
            redxml(str);

            //Phone phone = new Phone();
            //phone.rybh = quanjubianliang.rybh;
            //phone.xm

            //PhoneBook phoneBook = new PhoneBook();
            //PhoneRecords phoneRecords = new PhoneRecords();
            //PhoneSms phoneSms = new PhoneSms();
            //PhoneVid phoneVid = new PhoneVid();

            //string strSerializeJSON = JsonConvert.SerializeObject(bjson)

            return getxmldata;
        }
    }
}
