using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Security.Cryptography;

namespace 华夏网为.Appcode
{
    class Class2
    {
        public SQLiteConnection getconn()
        {
           
            return new SQLiteConnection("Data Source=" + getdburl());
        }
        //调用不返回（删除等操作...）
        public string getdburl()
        {
            string[] gtdb = System.Windows.Forms.Application.ExecutablePath.Split('\\');
            string getdb = "";

            for (int i = 0; i < gtdb.Length - 1; i++)
            {
                getdb = getdb + gtdb[i].ToString() + "\\";

            }
            getdb = getdb + "hxww.db";
            return getdb;
        }
        //
        public int sqliteexcu(string strsql)
        {
            SQLiteConnection conn=this.getconn();
            if (conn.State.ToString().Trim() == "Closed")
            {
                conn.Open();
            }
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = strsql;
            int affectedRows = cmd.ExecuteNonQuery();

            conn.Close();
            return affectedRows;
      
        }
        //返回一个字段
        public string  getback(string strsql)
        {
            SQLiteConnection conn = this.getconn();
            if (conn.State.ToString().Trim() == "Closed")
            {
                conn.Open();
            }
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = strsql;
            
            
            object value = cmd.ExecuteScalar();
            conn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }
        //返回一个表名
        public DataTable gettable(string strsql)
        {
            SQLiteConnection conn = this.getconn();
            if (conn.State.ToString().Trim() == "Closed")
            {
                conn.Open();
            }
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = strsql;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);
            conn.Close();
            return data; 

        }
        //更新数据库
        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {

            String vals = "";
            Boolean returnCode = true;
            if (data.Count >= 1)
            {

                foreach (KeyValuePair<String, String> val in data)
                {

                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());

                }

                vals = vals.Substring(0, vals.Length - 1);

            }

            try
            {
                SQLiteConnection conn = this.getconn();
                if (conn.State.ToString().Trim() == "Closed")
                {
                    conn.Open();
                }
                SQLiteCommand cmd;
                cmd = conn.CreateCommand();

                cmd.CommandText=(String.Format("update {0} set {1} where {2};", tableName, vals, where));
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            catch
            {

                returnCode = false;

            }

            return returnCode;

        }
        
        //返回一个随机数
        public string getid1()
        {

            string mm = "";

            System.Random rand = new Random();
            mm = rand.Next(0, 888888).ToString();
            return mm;

        }

        public string getsj()
        {

            Random rd = new Random();
            int i = rd.Next(60, 100);
            return i.ToString();
        }

        //存入图像数据
        public int savePicture(string dbPath,string sql)
        {
            if (!System.IO.File.Exists(dbPath))//图片不存在 
                return 0;
            using (SQLiteConnection cnn = this.getconn()) 
            {
                if (cnn.State.ToString().Trim() == "Closed")
                {
                    cnn.Open();
                }
                using (SQLiteCommand cmd = cnn.CreateCommand())  
                {
                    cmd.CommandText = sql;
                    SQLiteParameter para = new SQLiteParameter("@data", DbType.Binary);
                    string file = dbPath;
                    FileStream fs = new FileStream(file, FileMode.Open);
                    StreamUtil su = new StreamUtil();
                    byte[] buffer = StreamUtil.ReadFully(fs);

                    fs.Close();

                    para.Value = buffer;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }

        //检测案件编号（主键）是否存在
        public int getbool(string key ,string casid ,string tablename)
        {
            string sqlstr="select count(*) from "+tablename+ " where "+key+"='" + casid+"'";
            int m = Convert.ToInt32(getback(sqlstr));
            return m;

        }

        //保存足迹用
        public string getfooturl()
        {
            string[] gtdb = System.Windows.Forms.Application.ExecutablePath.Split('\\');
            string getdb = "";

            for (int i = 0; i < gtdb.Length - 1; i++)
            {
                getdb = getdb + gtdb[i].ToString() + "\\";

            }
            getdb = getdb + "foot";
            return getdb;
        }

        //保存足迹图片(存入二进制数据)
        public void savePicture1(byte[] buffer, string sql)
        {
            using (SQLiteConnection cnn = this.getconn())
            {
                if (cnn.State.ToString().Trim() == "Closed")
                {
                    cnn.Open();
                }
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SQLiteParameter para = new SQLiteParameter("@data", DbType.Binary);
                    para.Value = buffer;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //文件转行byte[]
        public  byte[] ReadFileToByte(string fileName)
        {

            FileStream pFileStream = null;


            byte[] pReadByte = new byte[0];


            try
            {

                pFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

                BinaryReader r = new BinaryReader(pFileStream);

                r.BaseStream.Seek(0, SeekOrigin.Begin);    //将文件指针设置到文件开

                pReadByte = r.ReadBytes((int)r.BaseStream.Length);

                return pReadByte;



            }


            catch
            {

                return pReadByte;

            }

            finally
            {

                if (pFileStream != null)

                    pFileStream.Close();

            }

        }

        //获取执行文件目录
        public string getdburl1()
        {
            string[] gtdb = System.Windows.Forms.Application.ExecutablePath.Split('\\');
            string getdb = "";

            for (int i = 0; i < gtdb.Length - 1; i++)
            {
                getdb = getdb + gtdb[i].ToString() + "\\";

            }
            return getdb;
        }

        //存入二进制数据
        public int savegyte(byte[] imagedata, string sql)
        {
            using (SQLiteConnection cnn = this.getconn())
            {
                if (cnn.State.ToString().Trim() == "Closed")
                {
                    cnn.Open();
                }
                using (SQLiteCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    SQLiteParameter para = new SQLiteParameter("@data", DbType.Binary);
                    para.Value = imagedata;
                    cmd.Parameters.Add(para);
                    cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }
        public  string CreateImageFromBytes(string fileName, byte[] buffer)
        {
            string file = fileName;
            Image image = BytesToImage(buffer);
            ImageFormat format = image.RawFormat;
            if (format.Equals(ImageFormat.Jpeg))
            {
                file += ".jpeg";
            }
            else if (format.Equals(ImageFormat.Png))
            {
                file += ".png";
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                file += ".bmp";
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                file += ".gif";
            }
            else if (format.Equals(ImageFormat.Icon))
            {
                file += ".icon";
            }
            System.IO.FileInfo info = new System.IO.FileInfo(file);
            System.IO.Directory.CreateDirectory(info.Directory.FullName);
            File.WriteAllBytes(file, buffer);
            return file;
        }
        public  Image BytesToImage(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
        public string GetIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                
                return "";
            }
        }

        public string GetMAC()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch (Exception ex)
            {
                
                return "noknow";
            }
        }
        /// <summary>
        /// 将图片以二进制流
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] SaveImage(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read); //将图片以文件流的形式进行保存
            BinaryReader br = new BinaryReader(fs);
            byte[] imgBytesIn = br.ReadBytes((int)fs.Length); //将流读入到字节数组中
            br.Dispose();
            fs.Dispose();
            return imgBytesIn;
            
        }
        public string MD5Encrypt(string password, int bit)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes;
            hashedDataBytes = md5Hasher.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(password));
            StringBuilder tmp = new StringBuilder();
            foreach (byte i in hashedDataBytes)
            {
                tmp.Append(i.ToString("x2"));
            }
            if (bit == 16)
                return tmp.ToString().Substring(8, 16);
            else
            if (bit == 32) return tmp.ToString();//默认情况
            else return string.Empty;
        }
        public void Writefile1(string filepath, string content)
        {

            StreamWriter sr = new StreamWriter(filepath, false);
            sr.Write(content);
            sr.Close();


        }
        public string redfile(string path)
        {
            string getbacredfile = "" ;
            using (StreamReader sr=new StreamReader(path))
            {
                string onechar = "";
                while((onechar=sr.ReadLine())!=null)
                {

                    getbacredfile = getbacredfile + onechar;
                }

            }
            return getbacredfile;

        }


    }
}
