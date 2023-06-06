using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 华夏网为
{
    class quanjubianliang
    {
        public static string rybh="";//人员编号
        public static int sfzcsh = 0;//身份证阅读器是否初始化成功
        public static string cjrbh = "";//采集人编号,登录人员编号
        public static string sfzyxqkssj="";//身份证有效期开始时间
        public static string sfzyxqjssj = "";//身份证有效期j结束时间
        public static int sxtsfdk = 0;//特殊体质摄像头是否打开中
        public static int sxwzdk = 0;//wuzheng摄像头是否打开中
        public static int interrupt = 0;//必填项
        public static int i_query = 1;    //检测是否查看用户,0 不是查看切换 1查看切换，用于确定是否调用icc 的closing
        public static int states = 0;
        public static String ythsbid = "";//一体化设备编号（唯一）
        public static int djms = 0;//和标采的对接模式0旧1新
        public static string webserviceip = "";

    }
}
