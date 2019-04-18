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
    /// <summary>
    /// 登录窗口的编写
    /// </summary>
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();//窗口初始化
            message.Text = "";
        }

        private void login_Load(object sender, EventArgs e)
        {
            cbox_select.SelectedIndex = 0;                        //默认选择combox的第一项admin
        }

        private void log_in_Click(object sender, EventArgs e)
        {
            //数据库打开及连接
            MySqlConnection conn = null;//创建MySqlConnection对象，连接MySql数据库
            conn = new MySqlConnection("server=localhost;User Id=root;password=123456;Database=csm");//数据库名字，用户名，密码
            conn.Open();//调用open对象，打开对象
            if (conn.State.ToString() != "Open")//判断conn的状态是否打开
            {
                message.Text = "数据库连接失败！！！";
            }
            //用户名、密码查询
            if (textBox1.Text == "")
            {
                message.Text = "用户名不能为空";
            }
            if (textBox2.Text == "")
            {
                message.Text = "密码不能为空";
            }
            //创建SQL语句
            //查找数据库
            string sqlStr = "select usr_name,pwd from login where usr_name = '" + textBox1.Text + "'";
            DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            MySqlDataReader sdr = cmd.ExecuteReader();
            if (!sdr.Read())
            {
                message.Text = "用户名不存在！请重新输入";
            }
            else if (sdr["pwd"].ToString().Trim() == textBox2.Text)
            {
                message.Text = "登录成功！！！！";
                CSM form_main = new CSM();
                form_main.ShowDialog();
                this.Close();
                Invalidate();
            }
            else
            {
                message.Text = "密码错误";
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();              //退出窗口
        }

        private void register_Click(object sender, EventArgs e)
        {
            register form_register = new register();
            //this.Close();
            form_register.ShowDialog();
            Invalidate();
        }
    }
}
