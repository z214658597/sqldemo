using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
    public partial class CSM : Form
    {
        public CSM()
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
        #region 参数定义
        public string ntime;//作为模拟时间
        private UdpClient udpcRecv;//用于UDP接收的网络服务类
        public static Thread threadwatch = null;//负责监听客户端的线程
        public string clientIP;//访问客户端的IP
        public static string strSRecMsg = "";//接收到的信息
        #endregion
        #region 进入三个站场子界面
        private void BJNZ_Click(object sender, EventArgs e)
        {
            bjnz form_bjnz = new bjnz();
            //this.Close();
            form_bjnz.ShowDialog();
            Invalidate();
        }

        private void NJNZ_Click(object sender, EventArgs e)
        {
            njnz form_njnz = new njnz();
            //this.Close();
            form_njnz.ShowDialog();
            Invalidate();
        }

        private void SHHQ_Click(object sender, EventArgs e)
        {
            shhq form_shhq = new shhq();
            //this.Close();
            form_shhq.ShowDialog();
            Invalidate();
        }
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
        #region 网络连接
        private void CSM_Load(object sender, EventArgs e)
        {
            Thread start = new Thread(Startup_CSM);
            start.IsBackground = true;
            start.Start();
        }
        //设置定时器，定时接收并处理信息
        private void timer_connChecker_Tick(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// 微机监测作为服务器，设置端口和ip
        /// </summary>
        private void Startup_CSM()
        {
            //UDP服务器绑定本机IP地址
            IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("192.168.1.23"), 7788); // 本机IP和监听端口号
            udpcRecv = new UdpClient(localIpep);
            threadwatch = new Thread(UDPconnection);
            threadwatch.Start();
            threadwatch.IsBackground = true;
            //UDP服务器绑定本机IP地址
        }
        //不断监听所有客户端
        private void UDPconnection()
        {          
            //与服务器端建立连接通信
            while (true)
            {
                //获取客户端的IP和端口号
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);////用来保存发送方的ip和端口号
                clientIP = remoteIpep.Address.ToString(); //获取发送源的IP
                switch (clientIP)
                {
                    case "192.168.1.201":
                        NJNZ.Text = "北京南正在监测中"; //表示北京南联锁连接成功
                        danniu1.显示状态 = Danniu.Xianshi.绿;
                        break;
                    case "192.168.1.35":
                        NJNZ.Text = "南京南正在监测中";  //表示南京南联锁连接成功
                        danniu2.显示状态 = Danniu.Xianshi.绿;
                        break;
                    case "192.168.1.210":
                        NJNZ.Text = "上海虹桥正在监测中"; ;  //表示上海虹桥联锁连接成功
                        danniu3.显示状态 = Danniu.Xianshi.绿;
                        break;
                }
                //获取访问客户端发来的信息
                byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                string strSRecMsg = Encoding.ASCII.GetString(bytRecv, 0, bytRecv.Length);
                ReceiveMessage();//处理信息
            }
        }
        #endregion

        #region 处理客户端发来的消息
        //接收所有客户端发来的消息
        private void ReceiveMessage()
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);////用来保存发送方的ip和端口号
            while (true)
            {               
                try
                {
                        //获取访问客户端的IP
                        byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                        string strSRecMsg = Encoding.UTF8.GetString(bytRecv, 0, bytRecv.Length);
                        clientIP = remoteIpep.Address.ToString(); //获取发送源的IP

                        if (clientIP == "192.168.1.201")
                        {
                            Handle_MessageFromBJN_LS();//处理北京南联锁发来的微机监测信息
                        }
                        if (clientIP == "192.168.1.215")
                        {
                            Handle_MessageFromNJN_LS();//处理南京南联锁发来的微机监测信息
                        }
                        if (clientIP == "192.168.1.210")
                        {
                            Handle_MessageFromSHHQ_LS();//处理上海虹桥联锁发来的微机监测信息
                        }                  
                }
                catch (Exception error)
                {

                }
            }
        }
        //处理北京南联锁发来的微机监测信息
        private void Handle_MessageFromBJN_LS()
        {
            if (strSRecMsg.Length != 0)
            {
                if (strSRecMsg.Substring(0, 3) == "BJN")
                {
                    if (strSRecMsg.Substring(10, 1) == "1")
                    {

                    }
                }
            }
        }
        //处理南京南联锁发来的微机监测信息
        private void Handle_MessageFromNJN_LS()
        {
            if (strSRecMsg.Length != 0)
            {
                if (strSRecMsg.Substring(0, 3) == "NJN")
                {
                    if (strSRecMsg.Substring(10, 1) == "1")
                    {

                    }
                }
            }
        }
        //处理上海虹桥联锁发来的微机监测信息
        private void Handle_MessageFromSHHQ_LS()
        {
            if (strSRecMsg.Length != 0)
            {
                if (strSRecMsg.Substring(0, 4) == "SHHQ")
                {
                    if (strSRecMsg.Substring(10, 1) == "1")
                    {

                    }
                }
            }
        }
        #endregion


    }
}
