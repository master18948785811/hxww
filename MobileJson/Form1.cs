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
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Runtime.Serialization.Json;

namespace MobileJson
{
    public partial class Form1 : Form
    {
        public struct Bcp
        {
            [JsonProperty("phone")]
            public JObject Phone { get; set; }
            [JsonProperty("phoneBook")]
            public JArray PhoneBook { get; set; }
            [JsonProperty("phoneRecords")]
            public JArray PhoneRecords { get; set; }
            [JsonProperty("phoneSms")]
            public JArray PhoneSms { get; set; }
            [JsonProperty("phoneVid")]
            public JArray PhoneVid { get; set; }

        }
        public struct Phone
        {
            public string rybh { get; set; }
            public string xm { get; set; }
            public string gmsfhm { get; set; }
            public string cjmbbh { get; set; }
            public string dxbh { get; set; }
            public string wpmc { get; set; }
            public string imeiEsnMeid { get; set; }
            public string macdz { get; set; }
            public string lymacdz { get; set; }
            public string xh { get; set; }
            public string tzms { get; set; }
            public string sjhm { get; set; }
        }

        public struct PhoneBook
        {
            public string cjmbbh { get; set; }
            public string zzxlmc { get; set; }
            public string txlzdlxdm { get; set; }
            public string lxdhlx { get; set; }
            public string gxrlxfs { get; set; }
            public string sczt { get; set; }
            public string scsj { get; set; }
        }

        public struct PhoneRecords
        {
            public string cjmbbh { get; set; }
            public string yddh { get; set; }
            public string gxrlxfs { get; set; }
            public string gxrxm { get; set; }
            public string thzt { get; set; }
            public string bdsjbs { get; set; }
            public string kssj { get; set; }
            public string jssj { get; set; }
            public string sc { get; set; }
            public string smxsz { get; set; }
            public string ljzt { get; set; }
            public string sczt { get; set; }
            public string scsj { get; set; }
        }

        public struct PhoneSms
        {
            public string cjmbbh { get; set; }
            public string yddh { get; set; }
            public string gxrlxfs { get; set; }
            public string gxrxm { get; set; }
            public string thzt { get; set; }
            public string bdsjbs { get; set; }
            public string dxsfsj { get; set; }
            public string txnr { get; set; }
            public string ckzt { get; set; }
            public string dxccwz { get; set; }
            public string smxsz { get; set; }
            public string ljzt { get; set; }
            public string sczt { get; set; }
            public string scsj { get; set; }
        }

        public struct PhoneVid
        {
            public string cjmbbh { get; set; }
            public string zhlx { get; set; }
            public string yhid { get; set; }
            public string zh { get; set; }

        }

        public Form1()
        {
            InitializeComponent();
        }

        public int phoneNum { get; private set; }
        private void button1_Click(object sender, EventArgs e)
        {
            Selectbcp();
            Click(phoneNum);
        }

        private void Click(int phoneNum)
        {
            Bcp[] bcp = new Bcp[phoneNum];
            for (int i = 0; i < bcp.Length; i++)
            {
                bcp[i].Phone = (JObject)bcpToJson("phone");
                bcp[i].PhoneBook = (JArray)bcpToJson("phoneBook");
                bcp[i].PhoneRecords = (JArray)bcpToJson("phoneRecords");
                bcp[i].PhoneSms = (JArray)bcpToJson("phoneSms");
                bcp[i].PhoneVid = (JArray)bcpToJson("phoneVid");
            }
            string fileName = path + "111.json";
            string jsonString = JsonConvert.SerializeObject(bcp, Newtonsoft.Json.Formatting.Indented);
            //测试解析
            File.WriteAllText(fileName, jsonString);

            //var jb = JArray.Parse(jsonString);
            //JToken data = jb[0]["phone"]["xm"];
            //JToken data1 = jb[0]["phoneBook"][0]["cjmbbh"];
            //Trace.WriteLine(data);
            //Trace.WriteLine(data1);
        }

        /// <summary>
        /// 解析bcp索引文件
        /// </summary>
        /// <param name="xmlpath">索引目录</param>
        /// <returns></returns>
        private Dictionary<string, List<string>> redxml(string xmlpath)
        {
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

            IEnumerable<XElement> datasetNode = from el in xe.Elements("DATASET").Elements("DATA").Elements("DATASET").Elements("DATA")
                                                select el;


            Dictionary<string, List<string>> hashMap = new Dictionary<string, List<string>>();
            foreach (var eleNode in datasetNode)
            {

                //Console.WriteLine(eleNode.Attributes("name").First().Value);
                //Console.WriteLine(eleNode.Attributes("rmk").First().Value);
                //读取DATA->ITEM
                IEnumerable<XElement> item = from el in eleNode.Elements("ITEM")
                                             select el;
                foreach (var eleNode1 in item)
                {
                    //Console.WriteLine(eleNode1.Attributes("key").First().Value.ToString());
                    //Console.WriteLine(eleNode1.Attributes("val").First().Value.ToString());
                    //Console.WriteLine(eleNode1.Attributes("rmk").First().Value.ToString());
                    //if ("A010004" == eleNode1.Attributes("key").First().Value.ToString())
                    //{
                    //    list1.Add(eleNode1.Attributes("val").First().Value.ToString());
                    //}
                }
                //读取DATA->DATASET
                IEnumerable<XElement> datasetNode1 = from el in eleNode.Elements("DATASET")
                                                     select el;
                List<string> list1 = new List<string>();
                //List<string> list2 = new List<string>();
                List<string> list3 = new List<string>();
                //Dictionary<string, string> hashMap1 = new Dictionary<string, string>();
                foreach (var eleNode2 in datasetNode1)
                {
                    //Console.WriteLine(eleNode2.Attributes("name").First().Value.ToString());
                    //Console.WriteLine(eleNode2.Attributes("rmk").First().Value.ToString());

                    //读取DATA->DATASET->DATA->DATASET->DATA
                    IEnumerable<XElement> datasetNode2 = from el in eleNode2.Elements("DATA")
                                                         select el;
                    IEnumerable<XElement> datasetNode3 = from el in eleNode2.Elements("DATA").Elements("ITEM")
                                                         select el;
                    switch (eleNode2.Attributes("name").First().Value)
                    {
                        case "WA_COMMON_010014":
                            foreach (var eleNode3 in datasetNode2)
                            {
                                foreach (var eleNode4 in datasetNode3)
                                {
                                    if ("H010020" == eleNode4.Attributes("key").First().Value.ToString())
                                    {
                                        //Console.WriteLine(eleNode3.Attributes("key").First().Value);
                                        //Console.WriteLine(eleNode3.Attributes("val").First().Value);
                                        //Console.WriteLine(eleNode3.Attributes("rmk").First().Value);
                                        list1.Add(eleNode4.Attributes("val").First().Value.ToString());
                                    }
                                    //if ("I010034" == eleNode4.Attributes("key").First().Value.ToString())
                                    //{
                                    //    list2.Add(eleNode4.Attributes("val").First().Value.ToString());
                                    //}
                                }

                            }
                            break;
                        case "WA_COMMON_010015":
                            foreach (var eleNode3 in datasetNode3)
                            {
                                //Console.WriteLine(eleNode3.Attributes("key").First().Value);
                                //Console.WriteLine(eleNode3.Attributes("eng").First().Value);
                                //Console.WriteLine(eleNode3.Attributes("chn").First().Value);
                                list3.Add(eleNode3.Attributes("eng").First().Value.ToString());
                            }
                            break;
                    }
                    //Map

                    for (int i = 0; i < list1.Count; i++)
                    {
                        //if (!hashMap1.ContainsKey(list1[i]))
                        //    hashMap1.Add(list1[i], list2[i]);
                        if (!hashMap.ContainsKey(list1[i]))
                            hashMap.Add(list1[i], list3);

                    }

                }
            }
            return hashMap;
        }

        string path = System.Windows.Forms.Application.StartupPath + "mobile\\" + "137-570839051-371402-371402-1660375302-00001\\";

        Phone phone = new Phone();
        List<PhoneBook> listphoneBook = new List<PhoneBook>();
        List<PhoneRecords> listphoneRecords = new List<PhoneRecords>();
        List<PhoneSms> listphoneSms = new List<PhoneSms>();
        List<PhoneVid> listphoneVid = new List<PhoneVid>();
        /// <summary>
        /// 遍历bcp数据包
        /// </summary>
        private void Selectbcp()
        {
            string str = System.Windows.Forms.Application.StartupPath + "mobile\\137-570839051-371402-371402-1660375302-00001\\GAB_ZIP_INDEX.xml";
            Dictionary<string, List<string>> hashMap = redxml(str);
            int count = 0;
            //phone = new Phone[item.Value.Count];
            foreach (var item in hashMap)
            {
                //phone
                GetbcpData("WA_MFORENSICS_010100", item, path, count);
                GetbcpData("WA_MFORENSICS_010200", item, path, count);
                GetbcpData("WA_MFORENSICS_010300", item, path, count);
                //phoneBook
                GetbcpData("WA_MFORENSICS_010500", item, path, count);
                //phoneRecords
                GetbcpData("WA_MFORENSICS_010600", item, path, count);
                //phoneSms
                GetbcpData("WA_MFORENSICS_010700", item, path, count);
                //phoneVid
                GetbcpData("WA_MFORENSICS_020100", item, path, count);
                count++;
                //Console.WriteLine("key:{0} value:{1}", item.Key, item.Value);
                //Trace.WriteLine(c);
            }
        }
        /// <summary>
        /// 获取bcp数据
        /// </summary>
        /// <param name="regex">val</param>
        /// <param name="item">bcp集合</param>
        /// <param name="Path">bcp文件目录</param>
        /// <returns></returns>
        private void GetbcpData(string regex, KeyValuePair<string, List<string>> item, string Path, int ReadNum)
        {
            string path = Path + item.Key;
            if (!System.IO.File.Exists(path))
                return;
            Regex r = new Regex(regex);
            // 定义一个Regex对象实例
            Match m = r.Match(item.Key); // 在字符串中匹配
            if (m.Success)
            {
                StreamReader sR = File.OpenText(path);
                string str = "";
                string nextLine;
                int countLine = 0;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    //Trace.WriteLine(nextLine);
                    str += nextLine;
                    str += "\n";
                    if (regex == "WA_MFORENSICS_010100")
                    {
                        countLine++;
                        phoneNum = countLine;
                    }
                }
                sR.Close();
                string[] words = str.Split(new char[2] { '\t', '\n' }, StringSplitOptions.TrimEntries);

                object[] result = new object[item.Value.Count];
                forBcp(regex, words, item.Value.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regex"></param>
        /// <param name="sen"></param>
        /// <param name="list"></param>
        /// <param name="words"></param>
        /// <param name="itemCount"></param>
        /// <param name="Num"></param>
        private void forBcp(string regex, string[] words, int itemCount, int Num = 200)
        {
            int num = Num > (words.Length / itemCount) ? (words.Length / itemCount) : Num;
            string[,] str1 = new string[words.Length / itemCount, itemCount];
            int count = 0;
            for (int i = 0; i < words.Length / itemCount; i++)
            {
                if (i == num)
                    break;
                for (int j = 0; j < itemCount; j++)
                {
                    str1[i, j] = words[count];
                    count++;
                }
                string? a = null;
                switch (regex)
                {
                    case "WA_MFORENSICS_010100":
                        phone.rybh = "";
                        phone.xm = a ?? str1[i, 3];
                        phone.gmsfhm = a ?? str1[i, 5];
                        phone.cjmbbh = a ?? str1[i, 0];
                        phone.dxbh = a ?? str1[i, 25];
                        phone.wpmc = a ?? str1[i, 21];
                        break;
                    case "WA_MFORENSICS_010200":
                        phone.imeiEsnMeid = a ?? str1[i, 2];
                        phone.macdz = a ?? str1[i, 3];
                        phone.lymacdz = a ?? str1[i, 4];
                        phone.xh = a ?? str1[i, 6];
                        phone.tzms = a ?? str1[i, 7];
                        break;
                    case "WA_MFORENSICS_010300":
                        phone.sjhm = a ?? str1[i, 1];
                        break;
                    case "WA_MFORENSICS_010500":
                        PhoneBook phoneBook = new PhoneBook();
                        phoneBook.cjmbbh = str1[i, 0];
                        phoneBook.zzxlmc = str1[i, 1];
                        phoneBook.txlzdlxdm = str1[i, 2];
                        phoneBook.lxdhlx = str1[i, 3];
                        phoneBook.gxrlxfs = str1[i, 4];
                        phoneBook.sczt = str1[i, 5];
                        phoneBook.scsj = str1[i, 6];
                        listphoneBook.Add(phoneBook);
                        break;
                    case "WA_MFORENSICS_010600":
                        PhoneRecords phoneRecords = new PhoneRecords();
                        phoneRecords.cjmbbh = str1[i, 0];
                        phoneRecords.yddh = str1[i, 1];
                        phoneRecords.gxrlxfs = str1[i, 2];
                        phoneRecords.gxrxm = str1[i, 3];
                        phoneRecords.thzt = str1[i, 4];
                        phoneRecords.bdsjbs = str1[i, 5];
                        phoneRecords.kssj = str1[i, 6];
                        phoneRecords.jssj = str1[i, 7];
                        phoneRecords.sc = str1[i, 8];
                        phoneRecords.smxsz = str1[i, 9];
                        phoneRecords.sczt = str1[i, 10];
                        phoneRecords.scsj = str1[i, 11];
                        phoneRecords.ljzt = str1[i, 12];
                        listphoneRecords.Add(phoneRecords);
                        break;
                    case "WA_MFORENSICS_010700":
                        PhoneSms phoneSms = new PhoneSms();
                        phoneSms.cjmbbh = str1[i, 0];//采集目标编号
                        phoneSms.yddh = str1[i, 1];//本机号码
                        phoneSms.gxrlxfs = str1[i, 2];//关系人联系方式
                        phoneSms.gxrxm = str1[i, 3];//关系人姓名
                        //phoneSms.thzt = str1[i, 4];//通话状态
                        phoneSms.bdsjbs = str1[i, 4];//本地数据标识
                        phoneSms.dxsfsj = str1[i, 5];//短信发送时间
                        phoneSms.txnr = str1[i, 6];//通信内容
                        phoneSms.ckzt = str1[i, 7];//查看状态
                        phoneSms.dxccwz = str1[i, 8];//短信存储位置
                        phoneSms.smxsz = str1[i, 9];//私密性设置
                        phoneSms.sczt = str1[i, 10];//删除状态
                        phoneSms.scsj = str1[i, 11];//删除时间
                        phoneSms.ljzt = str1[i, 12];//拦截状态
                        listphoneSms.Add(phoneSms);
                        break;
                    case "WA_MFORENSICS_020100":
                        PhoneVid phoneVid = new PhoneVid();
                        phoneVid.cjmbbh = str1[i, 0];
                        phoneVid.zhlx = str1[i, 1];
                        phoneVid.yhid = str1[i, 2];
                        phoneVid.zh = str1[i, 3];
                        listphoneVid.Add(phoneVid);
                        break;
                }
            }
        }
        //打包bcp到Json
        private Object bcpToJson(string nodeName)
        {
            Object result = null;
            switch (nodeName)
            {
                case "phone":
                    result = JObject.Parse(JsonConvert.SerializeObject(phone, Newtonsoft.Json.Formatting.Indented));
                    break;
                case "phoneBook":
                    result = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneBook, Newtonsoft.Json.Formatting.Indented));
                    break;
                case "phoneRecords":
                    result = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneRecords, Newtonsoft.Json.Formatting.Indented));
                    break;
                case "phoneSms":
                    result = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneSms, Newtonsoft.Json.Formatting.Indented));
                    break;
                case "phoneVid":
                    result = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneVid, Newtonsoft.Json.Formatting.Indented));
                    break;
            }
            return result;
        }
    }
}
