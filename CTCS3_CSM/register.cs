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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
            message.Text = "";
        }

        private void register_check_Click(object sender, EventArgs e)
        {
            //判断输入
            if (textBox3.Text == "")
            {
                message.Text = "用户名不能为空！！！";
                return;
            }
            else if (textBox1.Text == "" || textBox2.Text == "")
            {
                message.Text = "密码不能为空！！！";
                return;
            }
            if (textBox2.Text != textBox1.Text)
            {
                message.Text = "两次输入密码不一致!!!";
                return;
            }


            //数据库打开及连接
            MySqlConnection conn = null;//创建MySqlConnection对象，连接MySql数据库
            conn = new MySqlConnection("server=localhost;User Id=root;password=123456;Database=csm");//数据库名字，用户名，密码
            conn.Open();//调用open对象，打开对象
            if (conn.State.ToString() != "Open")//判断conn的状态是否打开
            {
                message.Text = "数据库连接失败！！！";
            }
            ////创建SQL语句  插入
            string sqlStr = "insert into login (usr_name,pwd) values ('" + textBox3.Text + "','" + textBox2.Text + "')";
            //DataSet ds = new DataSet();
            MySqlCommand cmd = new MySqlCommand(sqlStr, conn);
            MySqlDataReader sdr = cmd.ExecuteReader();
            message.Text = "注册成功，请返回登录界面！！！";
        
        }

        private void quit_Click(object sender, EventArgs e)
        {
             this.Close();
        }

        private void register_Load(object sender, EventArgs e)
        {

        }
    }
}
