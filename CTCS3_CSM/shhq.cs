using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CTCS3_CSM
{
    public partial class shhq : Form
    {
        public shhq()
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
        public string ntime;//作为模拟时间

        private void shhq_Load(object sender, EventArgs e)
        {

        }
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
    }
}
