using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using ControlLib;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CTCS3_CSM
{
    public partial class njnz : Form
    {
        public njnz()
        {
            InitializeComponent();
        }
        #region 根据窗体大小调整控件大小位置
        private float P, Q;
        //获得控件的长度、宽度、位置、字体大小的数据
        private void setTag(Control cons)//Control类，定义控件的基类
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;//获取或设置包含有关控件的数据的对象
                if (con.Controls.Count > 0)
                    setTag(con);//递归算法
            }
        }

        private void setControls(float newx, float newy, Control cons)//实现控件以及字体的缩放
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);//递归
                }
            }
        }

        private void MyForm_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / P;//当前宽度与变化前宽度之比
            float newy = this.Height / Q;//当前高度与变化前宽度之比
            setControls(newx, newy, this);
            this.Text = this.Width.ToString() + "*" + this.Height.ToString();  //窗体标题显示长度和宽度
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Resize += new EventHandler(MyForm_Resize);
            P = this.Width;
            Q = this.Height;
            setTag(this);
        }
        #endregion
        #region 定义变量区

        #region 轨道电路变量
        Hashtable trackstatehash = new Hashtable();
        String[] trackname = { "NJN_X1JG", "NJN_X2JG", "NJN_X3JG", "NJN_S3LQ", "NJN_S2LQ", "NJN_S1LQ", "daocha_1_1", "daocha_1_3", "NJN_IIIG",
                            "NJN_IG","NJN_IIG","NJN_4G","daocha_1_2", "daocha_1_4","NJN_X1LQ","NJN_X2LQ","NJN_X1LQ","NJN_S3JG","NJN_S2JG","NJN_S1JG"};

        #endregion

        #region 信号机变量
        String Signal_H;
        String Signal_L;
        String Signal_U1;
        String Signal_U2;
        String Signal_B;
        String[] signalname = { "xinhaoji_X", "xinhaoji_XF", "xinhaoji_S3", "xinhaoji_S1", "xinhaoji_S2", "xinhaoji_S4", "xinhaoji_X3", "xinhaoji_X1", "xinhaoji_X2", "xinhaoji_X4", "xinhaoji_SF", "Nxinhaoji_S" };
        Hashtable Signalstate = new Hashtable();

        #endregion

        #region 操作界面按钮变量
        //下行方向

        /* String XnormalStation;//总定位按钮
         String XreverseStation;//总反位按钮
         String XsingleLock;//单锁按钮
         String XsingleUnlock;//单解按钮
         String XLock;//岔封按钮
         String XUnlock;//岔解按钮
         String Xclose;//钮封按钮
         String Xopen;//钮解按钮
         String XrouteCancle;//进路取消按钮
         String XtrkUnlock;//区故解按钮
         String XallUnlock;//引导总锁闭
         String XhandUnlock;//总人解按钮
         String XclearAlarm;//清报警按钮
         String XsignalName;//线号机名称显示按钮
         String XswitchName;//信号机名称显示按钮
         String XtrackName;//信号机名称显示按钮
         //上行方向
         String SnormalStation;//总定位按钮
         String sreverseStation;//总反位按钮
         String SsingleLock;//单锁按钮
         String SsingleUnlock;//单解按钮
         String SLock;//岔封按钮
         String SUnlock;//岔解按钮
         String Sclose;//钮封按钮
         String Sopen;//钮解按钮
         String SrouteCancle;//进路取消按钮
         //String StrackUnlock;//区故解按钮
         String SallUnlock;//引导总锁闭
         String ShandUnlock;//总人解按钮

         String Shelp;//帮助按钮
         String Sguide;//引导接车按钮
         String Svoice;//语音提示
         String Sslopunclock;//坡道解锁
         String SpowerUnlock;//上电解锁*/

        #endregion

        #region 道岔状态采集
        String switch_1;
        String switch_3;
        String switch_2;
        String switch_4;
        #endregion
        public string ntime;//作为模拟时间
        #endregion


        #region 屏幕显示当前时间
        private void tmDate_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;  //当前时间
            string date = dt.ToLongDateString();
            string time = dt.ToLongTimeString();
            ntime = DateTime.Now.ToString("hhmmss");
            nowTime.Text = "当前时间\n" + date + "\n" + time;
        }
        #endregion

        private void njnz_Load(object sender, EventArgs e)
        {
            Handle_MessageFromBJN_LS();
        }
        //处理北京南联锁发来的微机监测信息
        private void Handle_MessageFromBJN_LS()
        {
            string receive = CTCS3_CSM.CSM.strSRecMsg;
            if (receive.Length != 0)
            {
                if (receive.Substring(0, 3) == "NJN")
                {
                    if (receive.Substring(10, 1) == "1")
                    {
                        NJN_X1JG.flag_zt = 1;
                        NJN_X1JG.Drawpic();
                    }
                    if (receive.Substring(11, 1) == "1")
                    {
                        NJN_X2JG.flag_zt = 1;
                        NJN_X2JG.Drawpic();
                    }
                    if (receive.Substring(12, 1) == "1")
                    {
                        NJN_X3JG.flag_zt = 1;
                        NJN_X3JG.Drawpic();
                    }
                    if (receive.Substring(13, 1) == "1")
                    {
                        NJN_S3LQ.flag_zt = 1;
                        NJN_S3LQ.Drawpic();
                    }
                    if (receive.Substring(14, 1) == "1")
                    {
                        NJN_S3LQ.flag_zt = 1;
                        NJN_S3LQ.Drawpic();
                    }
                    if (receive.Substring(15, 1) == "1")
                    {
                        NJN_S2LQ.flag_zt = 1;
                        NJN_S2LQ.Drawpic();
                    }
                    if (receive.Substring(16, 1) == "1")
                    {
                        NJN_S1LQ.flag_zt = 1;
                        NJN_S1LQ.Drawpic();
                    }
                    if (receive.Substring(17, 1) == "1")
                    {
                        daocha_1_1.锁闭状态 = Daocha_1.STATE.占用;
                    }
                    if (receive.Substring(18, 1) == "1")
                    {
                        daocha_1_3.锁闭状态 = Daocha_1.STATE.占用;
                    }

                }
            }
        }
    }
}
