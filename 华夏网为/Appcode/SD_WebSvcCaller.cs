using System;
using System.Web;
using System.Xml;
using System.Collections;
using System.Net;
using System.Text;
using System.IO;
using System.Xml.Serialization;
namespace 华夏网为.Appcode
    {
    class SD_WebSvcCaller
        {
        //<webServices>
        //  <protocols>
        //    <add name="HttpGet"/>
        //    <add name="HttpPost"/>
        //  </protocols>
        //</webServices>

        private static Hashtable _xmlNamespaces = new Hashtable();//缓存xmlNamespace，避免重复调用GetNamespace


        /// <summary>
        /// 需要WebService支持Post调用
        /// </summary>
        public static String QueryPostWebService(String URL, String Pars)
            {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json;charset=UTF-8";
            //request.Accept = "text/xml,application/xhtml+xml,*/*";
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadStrResponse(request.GetResponse());
            }
        public static String QueryPostWebService1(String URL, byte[] Pars, string sysId, string sysPass)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.Headers.Add("sysId", sysId);
            request.Headers.Add("sysPass", sysPass);
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json;charset=UTF-8";
            //request.Accept = "text/xml,application/xhtml+xml,*/*";
            SetWebRequest(request);
            byte[] data = Pars;
            WriteRequestData(request, data);
            return ReadStrResponse(request.GetResponse());
        }
        public static String QueryPostWebService2(String URL, string Pars, string sysId, string sysPass)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.Headers.Add("sysId", sysId);
            request.Headers.Add("sysPass", sysPass);
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentType = "application/json;charset=UTF-8";
            //request.Accept = "text/xml,application/xhtml+xml,*/*";
            SetWebRequest(request);
            byte[] data = EncodePars(Pars);
            WriteRequestData(request, data);
            return ReadStrResponse(request.GetResponse());
        }
        /// <summary>
        /// 需要WebService支持Get调用
        /// </summary>
        public static String QueryGetWebService(String URL, String Pars,string sysId, string sysPass)
            {
            
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL +"/" + Pars);
            request.Method = "GET";
            request.Headers.Add("sysId", sysId);
            request.Headers.Add("sysPass", sysPass);
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadStrResponse(request.GetResponse());
            }
        /// <summary>
        /// 通用WebService调用(Soap),参数Pars为String类型的参数名、参数值
        /// </summary>

        private static void SetWebRequest(HttpWebRequest request)
            {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 30000;
            }

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
            {
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
            }

        private static byte[] EncodePars(String Pars)
            {
               
               return Encoding.UTF8.GetBytes(Pars);
            }

        private static String ParsToString(Hashtable Pars)
            {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
                {
                if (sb.Length > 0)
                    {
                    sb.Append("&");
                    }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
                }
            return sb.ToString();
            }

        private static String ReadStrResponse(WebResponse response)
            {
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            String retStr = sr.ReadToEnd();
            sr.Close();
            return retStr;
            }

        private static void AddDelaration(XmlDocument doc)
            {
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.InsertBefore(decl, doc.DocumentElement);
            }
        }
    }
