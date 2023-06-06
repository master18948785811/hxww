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
    public partial class Form2 : Form
    {
        #region bcp打包Json结构体
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
        #endregion
        public Form2()
        {
            InitializeComponent();
        }

        #region bcp打包Json使用对象
        public int phoneNum { get; private set; }//采集手机部数
        string path = System.Windows.Forms.Application.StartupPath + "mobile\\" + "137-570839051-371402-371402-1660375302-00001\\";

        Phone[] phone = null;
        List<PhoneBook>[] listphoneBook = null;
        List<PhoneRecords>[] listphoneRecords = null;
        List<PhoneSms>[] listphoneSms = null;
        List<PhoneVid>[] listphoneVid = null;
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            phoneNum = GetphoneNum();
            Selectbcp();
            createJson(phoneNum);
        }
        /// <summary>
        /// 生成Json
        /// </summary>
        /// <param name="phoneNum">采集手机部数</param>
        private void createJson(int phoneNum)
        {
            Bcp[] bcp = new Bcp[phoneNum];
            for (int i = 0; i < bcp.Length; i++)
            {
                bcp[i].Phone = JObject.Parse(JsonConvert.SerializeObject(phone[i], Newtonsoft.Json.Formatting.Indented));
                bcp[i].PhoneBook = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneBook[i], Newtonsoft.Json.Formatting.Indented));
                bcp[i].PhoneRecords = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneRecords[i], Newtonsoft.Json.Formatting.Indented));
                bcp[i].PhoneSms = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneSms[i], Newtonsoft.Json.Formatting.Indented));
                bcp[i].PhoneVid = (JArray)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(listphoneVid[i], Newtonsoft.Json.Formatting.Indented));
            }
            string fileName = path + "111.json";
            string jsonString = JsonConvert.SerializeObject(bcp, Newtonsoft.Json.Formatting.Indented);
            //测试解析
            File.WriteAllText(fileName, jsonString);

            //var jb = JArray.Parse(jsonString);
            //JToken data = jb[0]["phone"]["xm"];
            //JToken data1 = jb[1]["phone"]["xm"];
            //JToken data2 = jb[0]["phoneBook"][0]["cjmbbh"];
            //JToken data3 = jb[1]["phoneBook"][1]["cjmbbh"];

        }

        /// <summary>
        /// 解析bcp索引文件
        /// </summary>
        /// <param name="xmlpath">索引目录</param>
        /// <returns></returns>
        private Dictionary<string, List<string>> redxml(string xmlpath)
        {
            XElement xe = XElement.Load(xmlpath);
            #region 解析索引文件
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
                    #endregion
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

        /// <summary>
        /// 通过索引获取采集手机部数
        /// </summary>
        /// <returns>手机部数</returns>
        private int GetphoneNum()
        {
            Dictionary<string, List<string>> hashMap = redxml(path + "GAB_ZIP_INDEX.xml");
            int num = 0;
            foreach (var item in hashMap)
            {
                Regex r = new Regex("WA_MFORENSICS_010100");
                // 定义一个Regex对象实例
                Match m = r.Match(item.Key); // 在字符串中匹配
                if (m.Success)
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>
        /// 遍历bcp数据包
        /// </summary>
        private void Selectbcp()
        {
            Dictionary<string, List<string>> hashMap = redxml(path + "GAB_ZIP_INDEX.xml");
            #region 初始化打包json缓存数据
            phone = new Phone[phoneNum];
            listphoneBook = new List<PhoneBook>[phoneNum];
            listphoneRecords = new List<PhoneRecords>[phoneNum];
            listphoneSms = new List<PhoneSms>[phoneNum];
            listphoneVid = new List<PhoneVid>[phoneNum];
            for (int i = 0; i < phoneNum; i++)
            {
                listphoneBook[i] = new List<PhoneBook>();
                listphoneRecords[i] = new List<PhoneRecords>();
                listphoneSms[i] = new List<PhoneSms>();
                listphoneVid[i] = new List<PhoneVid>();
            }
            #endregion
            foreach (var item in hashMap)
            {
                //phone
                GetbcpData("WA_MFORENSICS_010100", item, path);
                GetbcpData("WA_MFORENSICS_010200", item, path);
                GetbcpData("WA_MFORENSICS_010300", item, path);
                //phoneBook
                GetbcpData("WA_MFORENSICS_010500", item, path);
                //phoneRecords
                GetbcpData("WA_MFORENSICS_010600", item, path);
                //phoneSms
                GetbcpData("WA_MFORENSICS_010700", item, path);
                //phoneVid
                GetbcpData("WA_MFORENSICS_020100", item, path);

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
        private void GetbcpData(string regex, KeyValuePair<string, List<string>> item, string Path, int Num = 5000)
        {
            string path = Path + item.Key;
            if (!System.IO.File.Exists(path))
                return;
            Regex r = new Regex(regex);
            // 定义一个Regex对象实例
            Match m = r.Match(item.Key); // 在字符串中匹配
            if (m.Success)
            {
                #region 读取bcp文件
                StreamReader sR = File.OpenText(path);
                string str = "";
                string nextLine;
                //int countLine = 0;
                while ((nextLine = sR.ReadLine()) != null)
                {
                    //Trace.WriteLine(nextLine);
                    str += nextLine;
                    str += "\n";
                }
                sR.Close();
                string[] words = str.Split(new char[2] { '\t', '\n' }, StringSplitOptions.TrimEntries);
                #endregion

                #region 解析bcp
                int num = Num > (words.Length / item.Value.Count) ? (words.Length / item.Value.Count) : Num;
                string[,] bcpBuff = new string[words.Length / item.Value.Count, item.Value.Count];
                int word_count = 0;
                for (int i = 0; i < words.Length / item.Value.Count; i++)
                {
                    if (i == num)
                        break;
                    ArrayList arr_list = new ArrayList();//缓存对应字段下标
                    for (int j = 0; j < item.Value.Count; j++)
                    {
                        bcpBuff[i, j] = words[word_count];
                        arr_list.Add(item.Value[j]);
                        word_count++;
                    }
                    switch (regex)
                    {
                        case "WA_MFORENSICS_010100":
                            phone[i].rybh = "";
                            phone[i].xm = bcpBuff[i, arr_list.IndexOf("NAME")]?.ToString();
                            phone[i].gmsfhm = bcpBuff[i, arr_list.IndexOf("CERTIFICATE_CODE")]?.ToString();
                            phone[i].cjmbbh = bcpBuff[i, arr_list.IndexOf("COLLECT_TARGET_ID")]?.ToString();
                            phone[i].dxbh = bcpBuff[i, arr_list.IndexOf("MATERIALS_SERIAL")]?.ToString();
                            phone[i].wpmc = bcpBuff[i, arr_list.IndexOf("MATERIALS_NAME")]?.ToString();
                            break;
                        case "WA_MFORENSICS_010200":
                            phone[i].imeiEsnMeid = bcpBuff[i, arr_list.IndexOf("IMEI_ESN_MEID")]?.ToString();
                            phone[i].macdz = bcpBuff[i, arr_list.IndexOf("MAC")]?.ToString();
                            phone[i].lymacdz = bcpBuff[i, arr_list.IndexOf("BLUETOOTH_MAC")]?.ToString();
                            phone[i].xh = bcpBuff[i, arr_list.IndexOf("MODEL")]?.ToString();
                            phone[i].tzms = bcpBuff[i, arr_list.IndexOf("CHARACTERISTIC_DESC")]?.ToString();
                            break;
                        case "WA_MFORENSICS_010300":
                            phone[i].sjhm = bcpBuff[i, arr_list.IndexOf("MSISDN")]?.ToString();
                            break;
                        case "WA_MFORENSICS_010500":
                            PhoneBook phoneBook = new PhoneBook();
                            phoneBook.cjmbbh = bcpBuff[i, arr_list.IndexOf("COLLECT_TARGET_ID")];
                            phoneBook.zzxlmc = bcpBuff[i, arr_list.IndexOf("SEQUENCE_NAME")];
                            phoneBook.txlzdlxdm = bcpBuff[i, arr_list.IndexOf("PHONE_VALUE_TYPE")];
                            phoneBook.lxdhlx = bcpBuff[i, arr_list.IndexOf("PHONE_NUMBER_TYPE")];
                            phoneBook.gxrlxfs = bcpBuff[i, arr_list.IndexOf("RELATIONSHIP_ACCOUNT")];
                            phoneBook.sczt = bcpBuff[i, arr_list.IndexOf("DELETE_STATUS")];
                            phoneBook.scsj = bcpBuff[i, arr_list.IndexOf("DELETE_TIME")];
                            for (int n = 0; n < phone.Length; n++)
                            {
                                if (phone[n].cjmbbh == phoneBook.cjmbbh)
                                {
                                    listphoneBook[n].Add(phoneBook);
                                }
                            }
                            break;
                        case "WA_MFORENSICS_010600":
                            PhoneRecords phoneRecords = new PhoneRecords();
                            phoneRecords.cjmbbh = bcpBuff[i, arr_list.IndexOf("COLLECT_TARGET_ID")];
                            phoneRecords.yddh = bcpBuff[i, arr_list.IndexOf("MSISDN")];
                            phoneRecords.gxrlxfs = bcpBuff[i, arr_list.IndexOf("RELATIONSHIP_ACCOUNT")];
                            phoneRecords.gxrxm = bcpBuff[i, arr_list.IndexOf("RELATIONSHIP_NAME")];
                            phoneRecords.thzt = bcpBuff[i, arr_list.IndexOf("CALL_STATUS")];
                            phoneRecords.bdsjbs = bcpBuff[i, arr_list.IndexOf("LOCAL_ACTION")];
                            phoneRecords.kssj = bcpBuff[i, arr_list.IndexOf("START_TIME")];
                            phoneRecords.jssj = bcpBuff[i, arr_list.IndexOf("END_TIME")];
                            phoneRecords.sc = bcpBuff[i, arr_list.IndexOf("DUAL_TIME")];
                            phoneRecords.smxsz = bcpBuff[i, arr_list.IndexOf("PRIVACYCONFIG")];
                            phoneRecords.sczt = bcpBuff[i, arr_list.IndexOf("DELETE_STATUS")];
                            phoneRecords.scsj = bcpBuff[i, arr_list.IndexOf("DELETE_TIME")];
                            phoneRecords.ljzt = bcpBuff[i, arr_list.IndexOf("INTERCEPT_STATE")];
                            for (int n = 0; n < phone.Length; n++)
                            {
                                if (phone[n].cjmbbh == phoneRecords.cjmbbh)
                                {
                                    listphoneRecords[n].Add(phoneRecords);
                                }
                            }
                            break;
                        case "WA_MFORENSICS_010700":
                            PhoneSms phoneSms = new PhoneSms();
                            phoneSms.cjmbbh = bcpBuff[i, arr_list.IndexOf("COLLECT_TARGET_ID")];//采集目标编号
                            phoneSms.yddh = bcpBuff[i, arr_list.IndexOf("MSISDN")];//本机号码
                            phoneSms.gxrlxfs = bcpBuff[i, arr_list.IndexOf("RELATIONSHIP_ACCOUNT")];//关系人联系方式
                            phoneSms.gxrxm = bcpBuff[i, arr_list.IndexOf("RELATIONSHIP_NAME")];//关系人姓名
                            //phoneSms.thzt = bcpBuff[i, arr_list.IndexOf("CALL_STATUS")]?.ToString();//通话状态
                            phoneSms.bdsjbs = bcpBuff[i, arr_list.IndexOf("LOCAL_ACTION")];//本地数据标识
                            phoneSms.dxsfsj = bcpBuff[i, arr_list.IndexOf("MAIL_SEND_TIME")];//短信发送时间
                            phoneSms.txnr = bcpBuff[i, arr_list.IndexOf("CONTENT")];//通信内容
                            phoneSms.ckzt = bcpBuff[i, arr_list.IndexOf("MAIL_VIEW_STATUS")];//查看状态
                            phoneSms.dxccwz = bcpBuff[i, arr_list.IndexOf("MAIL_SAVE_FOLDER")];//短信存储位置
                            phoneSms.smxsz = bcpBuff[i, arr_list.IndexOf("PRIVACYCONFIG")];//私密性设置
                            phoneSms.sczt = bcpBuff[i, arr_list.IndexOf("DELETE_STATUS")];//删除状态
                            phoneSms.scsj = bcpBuff[i, arr_list.IndexOf("DELETE_TIME")];//删除时间
                            phoneSms.ljzt = bcpBuff[i, arr_list.IndexOf("INTERCEPT_STATE")];//拦截状态
                            for (int n = 0; n < phone.Length; n++)
                            {
                                if (phone[n].cjmbbh == phoneSms.cjmbbh)
                                {
                                    listphoneSms[n].Add(phoneSms);
                                }
                            }
                            break;
                        case "WA_MFORENSICS_020100":
                            PhoneVid phoneVid = new PhoneVid();
                            phoneVid.cjmbbh = bcpBuff[i, arr_list.IndexOf("COLLECT_TARGET_ID")];
                            phoneVid.zhlx = bcpBuff[i, arr_list.IndexOf("CONTACT_ACCOUNT_TYPE")];
                            phoneVid.yhid = bcpBuff[i, arr_list.IndexOf("ACCOUNT_ID")];
                            phoneVid.zh = bcpBuff[i, arr_list.IndexOf("ACCOUNT")];
                            for (int n = 0; n < phone.Length; n++)
                            {
                                if (phone[n].cjmbbh == phoneVid.cjmbbh)
                                {
                                    listphoneVid[n].Add(phoneVid);
                                }
                            }
                            break;
                    }
                }
                #endregion
            }
        }


    }
}
