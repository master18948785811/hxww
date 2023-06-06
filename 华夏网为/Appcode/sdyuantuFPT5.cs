using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;

namespace 华夏网为.Appcode
{
    class sdyuantuFPT5
    {
        Appcode.Class2 gj = new Appcode.Class2();

        int Flag = 1;
        int plams = 31;
        int four = 21;
        int picture = 1;
        string fileName;
        string sfys = "0000"; //是否压缩0:Wsq;1:bmp
        string s_NUMBER = "";//打包编号
        string cjrbh = "";

        /// 打包fpt时所用编号
        /// </summary>
        /// <param name="num"></param>
        public void packFPT(string num, string Compression)
        {
            try
            {
                if (null == num || "" == num)
                {
                    MessageBox.Show("编号不存在，请确认");
                    return;
                }
                if ("0000" != Compression && "1419" != Compression)
                {
                    MessageBox.Show("指纹压缩方式不存在，请确认");
                    return;
                }
                s_NUMBER = num;
                cjrbh = gj.getback("select caijirendaima from renyuanjibenxinxi where rycjbh='" + s_NUMBER + "'");
                XmlDocument fhxml1 = new XmlDocument();
                //fhxml1.Load(@"zwtpsfys.xml");
                //sfys = Convert.ToInt32(gj.IniReadValue("FINGERPRINT", "wsq",  System.Windows.Forms.Application.StartupPath.ToString() + "//config.ini"));
                sfys = Compression;
                GeneratedFile();
                GreateFPT();
                //MessageBox.Show("导出FPT文件成功!");
            }
            catch (Exception)
            {
                MessageBox.Show("导出FPT文件失败,请重新导出!");
                throw;
            }
        }
        private void GreateFPT()
        {

            inserttext("<?xml version=\"1.0\" encoding=\"utf-8\" ?><package>");
            gethead();
            getFPackage();
            inserttext("</package>");

        }
        private void gethead()
        {
            string alldata = "";
            alldata = alldata + "<packageHead>";
            alldata = alldata + " <version>FPT0500</version>";
            alldata = alldata + " <createTime>" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "</createTime>";
            alldata = alldata + " <originSystem>CJ</originSystem>";
            alldata = alldata + " <fsdw_gajgjgdm>" + gj.getback("select Address from yonghu where id='" + cjrbh + "'") + "</fsdw_gajgjgdm>";
            alldata = alldata + " <fsdw_gajgmc>" + gj.getback("select UnitName from yonghu where id='" + cjrbh + "'") + "</fsdw_gajgmc>";
            alldata = alldata + " <fsdw_xtlx>1419</fsdw_xtlx>";
            alldata = alldata + " <fsr_xm>" + gj.getback("select PersonalName from yonghu where id='" + cjrbh + "'") + "</fsr_xm>";
            alldata = alldata + " <fsr_gmsfhm>" + gj.getback("select IDcardnumber from yonghu where id='" + cjrbh + "'") + "</fsr_gmsfhm>";
            alldata = alldata + " <fsr_lxdh>" + gj.getback("select Telnumber from yonghu where id='" + cjrbh + "'") + "</fsr_lxdh>";
            alldata = alldata + "</packageHead>";
            inserttext(alldata);
        }
        private void getFPackage()
        {
            inserttext("<fingerprintPackage>");
            getDMsg();
            getCMsg();
            inserttext("<fingers>");
            getfigers();
            inserttext("</fingers>");
            inserttext("<palms>");
            getpalms();
            inserttext("</palms>");
            inserttext("<fourprints>");
            getfourprints();
            inserttext("</fourprints>");
            inserttext("<faceImages>");
            getpicture();
            inserttext("</faceImages>");
            inserttext("</fingerprintPackage>");
           
           
        }
        private void getDMsg()
        {
            string sqlinf = "select * from renyuanjibenxinxi where rycjbh='" + s_NUMBER + "'";
            string alldata = "";
            foreach (DataRow rs in gj.gettable(sqlinf).Rows)
            {

                alldata = alldata + "<descriptiveMsg>";
                alldata = alldata + " <ysxt_asjxgrybh>" + rs["rycjbh"] + "</ysxt_asjxgrybh>";
                alldata = alldata + "<jzrybh></jzrybh>";
                alldata = alldata + "<asjxgrybh></asjxgrybh>";
                alldata = alldata + "<zzhwkbh>" + rs["rycjbh"].ToString().Substring(1, rs["rycjbh"].ToString().Length - 1) + "</zzhwkbh>";
                alldata = alldata + "<collectingReasonSet><cjxxyydm>" + rs["caijirendaima"].ToString() + "</cjxxyydm></collectingReasonSet>";
                alldata = alldata + "<xm>" + rs["xm"].ToString() + "</xm>";
                alldata = alldata + "<bmch></bmch>";
                alldata = alldata + "<xbdm>" + rs["xbdm"].ToString() + "</xbdm>";
                alldata = alldata + "<csrq>" + rs["csrq"].ToString().Replace("-", "") + "</csrq>";
                alldata = alldata + "<gjdm>156</gjdm>";
                alldata = alldata + "<mzdm>" + rs["mzdm"].ToString() + "</mzdm>";
                alldata = alldata + "<cyzjdm>111</cyzjdm>";
                alldata = alldata + "<zjhm>" + rs["zjhm"].ToString() + "</zjhm>";
                alldata = alldata + "<hjdz_xzqhdm>" + rs["hjdz_xzqhdm"].ToString() + "</hjdz_xzqhdm>";
                alldata = alldata + "<hjdz_dzmc>" + rs["hjdz_dzmc"].ToString() + "</hjdz_dzmc>";
                alldata = alldata + "<xzz_xzqhdm>" + rs["xzz_xzqhdm"].ToString() + "</xzz_xzqhdm>";
                alldata = alldata + "<xzz_dzmc>" + rs["xzz_dzmc"].ToString() + "</xzz_dzmc>";
                alldata = alldata + "<bz></bz>";

                alldata = alldata + "</descriptiveMsg>";
            }
            inserttext(alldata);

        }
        private void getCMsg()
        {
            string alldata = "";
            alldata = alldata + "<collectInfoMsg>";
            alldata = alldata + "<zwbdxtlxms>1419</zwbdxtlxms>";
            alldata = alldata + "<nydw_gajgjgdm>" + gj.getback("select Address from yonghu where id='" + cjrbh + "'") + "</nydw_gajgjgdm>";
            alldata = alldata + "<nydw_gajgmc>" + gj.getback("select UnitName from yonghu where id='" + cjrbh + "'") + "</nydw_gajgmc>";
            alldata = alldata + "<nyry_xm>" + gj.getback("select PersonalName from yonghu where id='" + cjrbh + "'") + "</nyry_xm>";
            alldata = alldata + "<nyry_gmsfhm>" + gj.getback("select IDcardnumber from yonghu where id='" + cjrbh + "'") + "</nyry_gmsfhm>";
            alldata = alldata + "<nyry_lxdh>" + gj.getback("select Telnumber from yonghu where id='" + cjrbh + "'") + "</nyry_lxdh>";
            alldata = alldata + "<nysj>" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + "</nysj>";
            alldata = alldata + "</collectInfoMsg>";
            inserttext(alldata);

        }
        private void getfigers()
        {
            string alldata="";
            bool mark = false;         //标记是否获取到指纹数据,没有获取到说明是断指
           
            string FingerState = "0";            //指纹状态0 正常 1 残缺 2 系统设置不采集 3 受伤未采集 9 其他缺失情况 
            string pf = "";//质量评分
            byte[] fileBytes_bmp = new byte[409600]; //指纹缓冲区
            string sqlfig = "select * from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + s_NUMBER + "'and ZWZWDM='" + Flag.ToString() + "'and ZW_TXYSFFMS='" + sfys + "'";
            foreach (DataRow rs in gj.gettable(sqlfig).Rows)
            {
               
                if (rs["ZZHWQSQKDM"].ToString()=="0")
                {
                    mark = true;
                }
                    pf = rs[10].ToString();
                    alldata = alldata + "<fingerMsg>";
                    if (Flag < 10)
                        alldata = alldata + "<zwzwdm>0" + Flag.ToString() + "</zwzwdm>";     //01~09指位*
                    else
                        alldata = alldata + "<zwzwdm>" + Flag.ToString() + "</zwzwdm>";                 //10~20指位*

                    alldata = alldata + "<zzhwqsqkdm>" + rs["ZZHWQSQKDM"] + "</zzhwqsqkdm>";   //指纹其它状态
                    alldata = alldata + "<zw_txspfxcd>640</zw_txspfxcd>";
                    alldata = alldata + "<zw_txczfxcd>640</zw_txczfxcd>";
                    alldata = alldata + "<zw_txfbl>500</zw_txfbl>";
                    if (mark)
                    {
                        if (sfys == "0000"&& sfys==rs["ZW_TXYSFFMS"].ToString())
                        {
                            byte[] fileBytes_bmp1 = (byte[])rs["ZW_TXSJ"];
                            alldata = alldata + "<zw_txysffms>" + rs["ZW_TXYSFFMS"].ToString() + "</zw_txysffms>";
                            alldata = alldata + "<zw_txzl>"+rs["ZW_TXZL"].ToString() +"</zw_txzl>";
                            alldata = alldata + " <zw_txsj>" + Convert.ToBase64String(fileBytes_bmp1) + "</zw_txsj>";
                        }
                        else if (sfys == "1419" && sfys==rs["ZW_TXYSFFMS"].ToString())
                        {
                            byte[] fileBytes_wsq = (byte[])rs["ZW_TXSJ"];
                            alldata = alldata + "<zw_txysffms>" + rs["ZW_TXYSFFMS"].ToString() + "</zw_txysffms>";
                            alldata = alldata + "<zw_txzl>" + rs["ZW_TXZL"].ToString() + "</zw_txzl>";
                            alldata = alldata + " <zw_txsj>" + Convert.ToBase64String(fileBytes_wsq) + "</zw_txsj>";
                        }
                    } 
                    else
                    {
                        alldata = alldata + "<zw_txzl>0</zw_txzl>";
                        alldata = alldata + "<zw_txsj></zw_txsj>";

                    }
                    alldata = alldata + "</fingerMsg>";
                    inserttext(alldata);
                
            }
           
            if (Flag < 20)
            {
                Flag++;
                getfigers();

            }
        }
        private void getpalms()
        {
            string alldata = "";
            bool mark = false;         //标记是否获取到指纹数据,没有获取到说明是断指
            int gd = 0;
            byte[] fileBytes_bmp = new byte[5308416];
            switch (plams)
            {
                case 31:
                    gd = 31;
                    break;
                case 32:
                    gd = 32;
                    break;
                case 33:
                    gd = 33;
                    break;
                case 34:
                    gd = 34;
                    break;

            }
            string FingerState = "0";            //指纹状态0 正常 1 残缺 2 系统设置不采集 3 受伤未采集 9 其他缺失情况 

            string sqlfig = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + s_NUMBER + "'and ZHWZHWDM='" + gd.ToString() + "'and ZHW_TXYSFSMS='" + sfys + "'";
            foreach (DataRow rs in gj.gettable(sqlfig).Rows)
            {
                //获取指纹状态
                FingerState = rs["ZHW_ZZHWQSQKDM"].ToString();
                if (DBNull.Value != rs["ZHW_TXSJ"] && ((byte[])rs["ZHW_TXSJ"]).Length>1)
                {
                    mark = true;
                    alldata = alldata + "<palmMsg>";
                    alldata = alldata + "<zhwzhwdm>" + plams.ToString() + "</zhwzhwdm>";
                    alldata = alldata + "<zhw_zzhwqsqkdm>" + FingerState + "</zhw_zzhwqsqkdm>";   //指纹其它状态
                    alldata = alldata + "<zhw_txspfxcd>2304</zhw_txspfxcd>";
                    alldata = alldata + "<zhw_txczfxcd>2304</zhw_txczfxcd>";
                    alldata = alldata + "<zhw_txfbl>500</zhw_txfbl>";

                    if (mark)
                    {
                        if (sfys == "0000" && sfys == rs["ZHW_TXYSFSMS"].ToString())
                        {
                            //去头
                            //Array.Copy((byte[])rs["ZHW_TXSJ"], 1078, fileBytes_bmp, 0, 5308416);
                            //不去头
                            byte[] fileBytes_bmp1 = (byte[])rs["ZHW_TXSJ"];
                            alldata = alldata + "<zhw_txysffms>" + rs["ZHW_TXYSFSMS"].ToString() + "</zhw_txysffms>";
                            alldata = alldata + "<zhw_txzl>" + rs["ZHW_TXZL"].ToString() + "</zhw_txzl>";
                            alldata = alldata + " <zhw_txsj>" + Convert.ToBase64String(fileBytes_bmp1) + "</zhw_txsj>";
                        }
                        else if (sfys == "1419" && sfys == rs["ZHW_TXYSFSMS"].ToString())
                        {
                            byte[] fileBytes_wsq = (byte[])rs["ZHW_TXSJ"];
                            alldata = alldata + "<zhw_txysffms>" + rs["ZHW_TXYSFSMS"].ToString() + "</zhw_txysffms>";
                            alldata = alldata + "<zhw_txzl>" + rs["ZHW_TXZL"].ToString() + "</zhw_txzl>";
                            //alldata = alldata + " <zhw_txsj>" + Convert.ToBase64String(System.IO.File.ReadAllBytes(@"Palm\" + ystmc)) + "</zhw_txsj>";
                            alldata = alldata + " <zhw_txsj>" + Convert.ToBase64String(fileBytes_wsq) + "</zhw_txsj>";
                        }
                    }
                    alldata = alldata + "</palmMsg>";
                    inserttext(alldata);
                }
            }
           
            if (plams < 34)
            {
                plams++;
                getpalms();

            }

        }
        private void getfourprints()
        {
            string alldata = "";
            bool mark = false;         //标记是否获取到指纹数据,没有获取到说明是断指
            string FingerState = "0";            //指纹状态0 正常 1 残缺 2 系统设置不采集 3 受伤未采集 9 其他缺失情况 
            byte[] fileBytes_bmp = new byte[5308416];
            string sqlfig = "select * from LEDEN_COLLECT_FOURFINGER where RYJCXXCJBH='" + s_NUMBER + "'and SLZ_ZWZWDM='" + four.ToString() + "'and SLZ_TXYSFSMS='"+sfys+"'";
            foreach (DataRow rs in gj.gettable(sqlfig).Rows)
            {
                //获取指纹状态
                FingerState = rs["SLZ_ZZHWQSQKDM"].ToString();
                if (DBNull.Value != rs["SLZ_TXSJ"] && ((byte[])rs["SLZ_TXSJ"]).Length>1)
                {
                    mark = true;
                    alldata = alldata + "<fourprintMsg>";
                    alldata = alldata + "<slz_zwzwdm>" + four.ToString() + "</slz_zwzwdm>";
                    alldata = alldata + "<slz_zzhwqsqkdm>" + FingerState + "</slz_zzhwqsqkdm>";   //指纹其它状态
                    alldata = alldata + "<slz_txspfxcd>2304</slz_txspfxcd>";
                    alldata = alldata + "<slz_txczfxcd>2304</slz_txczfxcd>";
                    alldata = alldata + "<slz_txfbl>500</slz_txfbl>";
                    if (mark)
                    {
                        if (sfys == "0000" && sfys == rs["SLZ_TXYSFSMS"].ToString())
                        {
                            //去头
                            //Array.Copy((byte[])rs["SLZ_TXSJ"], 1078, fileBytes_bmp, 0, 5308416);
                            //不去头
                            byte[] fileBytes_bmp1 = (byte[])rs["SLZ_TXSJ"];
                            alldata = alldata + "<slz_txysffms>"+rs["SLZ_TXYSFSMS"].ToString()+"</slz_txysffms>";
                            alldata = alldata + "<slz_txzl>"+rs["SLZ_TXZL"].ToString() +"</slz_txzl>";
                            alldata = alldata + " <slz_txsj>" + Convert.ToBase64String(fileBytes_bmp1) + "</slz_txsj>";
                        }
                        else if (sfys == "1419" && sfys == rs["SLZ_TXYSFSMS"].ToString())
                        {
                            byte[] fileBytes_wsq = (byte[])rs["SLZ_TXSJ"];
                            alldata = alldata + "<slz_txysffms>" + rs["SLZ_TXYSFSMS"].ToString() + "</slz_txysffms>";
                            alldata = alldata + "<slz_txzl>" + rs["SLZ_TXZL"].ToString() + "</slz_txzl>";
                            //alldata = alldata + " <slz_txsj>" + Convert.ToBase64String(System.IO.File.ReadAllBytes(@"Palm\" + ystmc)) + "</slz_txsj>";
                            alldata = alldata + " <slz_txsj>" + Convert.ToBase64String(fileBytes_wsq) + "</slz_txsj>";
                        }
                    }
                    alldata = alldata + "</fourprintMsg>";
                    inserttext(alldata);
                }
            }
           
            if (four < 22)
            {
                four++;
                getfourprints();

            }

        }
        private void getpicture()
        {
            string alldata = "";

            switch (picture)
            {
                case 1:
                    if (System.IO.File.Exists(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_F.JPG")) //如果不存在
                    {
                        Bitmap bs = new Bitmap(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_F.JPG");
                        KiSaveAsJPEG(bs, System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait1" + s_NUMBER+ ".jpeg", 70);
                        bs.Dispose();
                        alldata = alldata + "<faceImage>";
                        alldata = alldata + "<rxzplxdm>1</rxzplxdm>";
                        alldata = alldata + "<rx_dzwjgs>JPEG</rx_dzwjgs>";
                        alldata = alldata + " <rx_txsj>" + Convert.ToBase64String(System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait1" + s_NUMBER+ ".jpeg")) + "</rx_txsj>";
                        alldata = alldata + "</faceImage>";
                    }
                    break;
                case 2:

                    if (System.IO.File.Exists(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_R.JPG")) //如果不存在
                    {
                        Bitmap bs = new Bitmap(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_R.JPG");
                        KiSaveAsJPEG(bs, System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait2" + s_NUMBER+ ".jpeg", 70);
                        bs.Dispose();
                        alldata = alldata + "<faceImage>";
                        alldata = alldata + "<rxzplxdm>2</rxzplxdm>";
                        alldata = alldata + "<rx_dzwjgs>JPEG</rx_dzwjgs>";
                        alldata = alldata + " <rx_txsj>" + Convert.ToBase64String(System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait2" + s_NUMBER+ ".jpeg")) + "</rx_txsj>";
                        alldata = alldata + "</faceImage>";
                    }
                    break;
                case 4:

                    if (System.IO.File.Exists(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_L.JPG")) //如果不存在
                    {
                        Bitmap bs = new Bitmap(@"ZIP//" + s_NUMBER+ "//" + s_NUMBER+ "_PH_L.JPG");
                        KiSaveAsJPEG(bs, System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait3" + s_NUMBER+ ".jpeg", 70);
                        bs.Dispose();
                        alldata = alldata + "<faceImage>";
                        alldata = alldata + "<rxzplxdm>4</rxzplxdm>";
                        alldata = alldata + "<rx_dzwjgs>JPEG</rx_dzwjgs>";
                        alldata = alldata + " <rx_txsj>" + Convert.ToBase64String(System.IO.File.ReadAllBytes(System.IO.Directory.GetCurrentDirectory() + "\\PortraitData\\portrait3" + s_NUMBER+ ".jpeg")) + "</rx_txsj>";
                        alldata = alldata + "</faceImage>";
                    }
                    break;
                default: break;

            }
            inserttext(alldata);
            if (picture < 4)
            {
                picture++;
                getpicture();

            }
        
        }
        private void getzdy()
        {

            string alldata = "<customDataPackage>";
            alldata = alldata + " <zwbdxtlxms>1419</zwbdxtlxms>";
            alldata = alldata + "<zdyxx>";
            string  zz= "<primary_personid>"+s_NUMBER.ToString().Substring(1, 22)+"</primary_personid><tpcardid>"+s_NUMBER.ToString().Substring(1,22)+"</tpcardid><primary_caseid></primary_caseid><taskid></taskid>";
            byte[] b = System.Text.Encoding.Default.GetBytes(zz);
            //转成 Base64 形式的 System.String  
            alldata = alldata +  Convert.ToBase64String(b)+ "bz1IIEdsS5BMo0BOcOyMR";
            alldata = alldata + "</zdyxx></customDataPackage> ";
            inserttext(alldata);

        }
        private void GeneratedFile()
        {
            try
            {
                string folderPath =  System.Windows.Forms.Application.StartupPath.ToString() + "\\FPT";
                if (false == System.IO.Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string folderPath_bmp =  System.Windows.Forms.Application.StartupPath.ToString() + "\\FPT\\BMP";
                if (false == System.IO.Directory.Exists(folderPath_bmp))
                {
                    Directory.CreateDirectory(folderPath_bmp);
                }
                string folderPath_wsq =  System.Windows.Forms.Application.StartupPath.ToString() + "\\FPT\\WSQ";
                if (false == System.IO.Directory.Exists(folderPath_wsq))
                {
                    Directory.CreateDirectory(folderPath_wsq);
                }
                if (sfys == "0000")
                {
                    fileName =  System.Windows.Forms.Application.StartupPath.ToString() + "\\FPT\\BMP\\" + s_NUMBER + ".fptx";
                    if (System.IO.File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
                    {
                        file.Close();
                    }
                }
                else 
                {
                    if (sfys == "1419")
                    {
                        fileName =  System.Windows.Forms.Application.StartupPath.ToString() + "\\FPT\\WSQ\\" + s_NUMBER + ".fptx";
                        if (System.IO.File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
                        {
                            file.Close();
                        }
                    }
                }
              
            }
            catch (Exception)
            {
                MessageBox.Show("FPT生成失败，请重新生成!");
                throw;
            }
            
        }

       
        private byte[] xztp(byte[] bitmap1)  //旋转图片
        {
            byte[] mm = new byte[409600];

            for (int i = 0; i < 640; i++)
            {
                for (int j = 0; j < 640; j++)
                {
                    if (i == 0)
                    {
                        mm[408960 + j] = bitmap1[j];

                    }
                    else
                    {
                        mm[408960 - (640 * i) + j] = bitmap1[640 * i + j];

                    }

                }

            }

            return mm;
        }
        public static bool KiSaveAsJPEG(Bitmap bmp, string FileName, int Qty)//压缩图片
        {
            try
            {
                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qty);
                ps.Param[0] = p;
                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        private void inserttext(string neirong)
        {


            FileStream fs = new FileStream(fileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(neirong);
            sw.Close();
            fs.Close();


        }
        private string  getysdwz(string flag)
        {
            switch (flag)
            {
                case "1":
                    return s_NUMBER + "_RP_1" + ".wsq";
                case "2":
                    return s_NUMBER + "_RP_2" + ".wsq";
                case "3":
                    return s_NUMBER + "_RP_3" + ".wsq";
                case "4": ;
                    return s_NUMBER + "_RP_4" + ".wsq";
                case "5":
                    return s_NUMBER + "_RP_5" + ".wsq";

                case "6":
                    return s_NUMBER + "_RP_6" + ".wsq";
                case "7":
                    return s_NUMBER + "_RP_7" + ".wsq";
                case "8":
                    return s_NUMBER + "_RP_8" + ".wsq";
                case "9":
                    return s_NUMBER + "_RP_9" + ".wsq";
                case "10":
                    return s_NUMBER + "_RP_10" + ".wsq";
                case "11":
                    return s_NUMBER + "_FP_1" + ".wsq";
                case "12":
                    return s_NUMBER + "_FP_2" + ".wsq";
                case "13":
                    return s_NUMBER + "_FP_3" + ".wsq";
                case "14":
                    return s_NUMBER + "_FP_4" + ".wsq";
                case "15":
                    return s_NUMBER + "_FP_5" + ".wsq";

                case "16":
                    return s_NUMBER + "_FP_6" + ".wsq";
                case "17":
                    return s_NUMBER + "_FP_7" + ".wsq";
                case "18":
                    return s_NUMBER + "_FP_8" + ".wsq";
                case "19":
                    return s_NUMBER + "_FP_9" + ".wsq";
                case "20":
                    return s_NUMBER + "_FP_10" + ".wsq";
                case "21":
                    return s_NUMBER + "_FO_R" + ".wsq";
                case "22":
                    return s_NUMBER + "_PO_R" + ".wsq";
                case "31":
                    return s_NUMBER + "_PM_R" + ".wsq";
                case "32":
                    return s_NUMBER + "_PM_L" + ".wsq";
                case "33":
                    return s_NUMBER + "_PW_R" + ".wsq";
                case "34":
                    return s_NUMBER + "_PW_L" + ".wsq";
                   
            }

            return "";

        
        }
        //联腾需要指纹分数字段
        private string AddQUStr() 
        {
            string re = "";
            string sql = "select * from LEDEN_COLLECT_FINGER where RYJCXXCJBH='" + s_NUMBER + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows) 
            {
                re += MakeUpStr(rs["ZW_TXZL"].ToString());//指纹分数
            }
            sql = "select * from LEDEN_COLLECT_FOURFINGER where RYJCXXCJBH='" + s_NUMBER + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                re += MakeUpStr(rs["SLZ_TXZL"].ToString());//四联指分数
            }
            sql = "select * from LEDEN_COLLECT_PALM where RYJCXXCJBH='" + s_NUMBER + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                re += MakeUpStr(rs["ZHW_TXZL"].ToString());//掌纹分数
            }
            sql = "select * from LEDEN_COLLECT_FULLPALM where RYJCXXCJBH='" + s_NUMBER + "'";
            foreach (DataRow rs in gj.gettable(sql).Rows)
            {
                re += MakeUpStr(rs["QZ_TXZL"].ToString());//全掌分数
            }
            return re;
        }
        //补齐字符串
        private string MakeUpStr(string str) 
        {
            string re = "";
            int s_length=str.Length;
            
            if (s_length<2)
            {
                re = "00" + str;
            }
            else if (s_length < 3)
            {
                re = "0" + str;
            }
            return re;
        }
    }
}
