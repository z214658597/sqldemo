namespace CTCS3_CSM
{
    partial class CSM
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label19 = new System.Windows.Forms.Label();
            this.NJNZ = new System.Windows.Forms.Button();
            this.SHHQ = new System.Windows.Forms.Button();
            this.BJNZ = new System.Windows.Forms.Button();
            this.tmDate = new System.Windows.Forms.Timer(this.components);
            this.nowTime = new System.Windows.Forms.Label();
            this.danniu1 = new ControlLib.Danniu();
            this.danniu2 = new ControlLib.Danniu();
            this.danniu3 = new ControlLib.Danniu();
            this.timer_connChecker = new System.Windows.Forms.Timer(this.components);
            this.txt_显示 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 42F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label19.Location = new System.Drawing.Point(376, 41);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(1228, 84);
            this.label19.TabIndex = 1284;
            this.label19.Text = "CTCS-3级京沪高铁微机监测系统";
            // 
            // NJNZ
            // 
            this.NJNZ.BackColor = System.Drawing.Color.White;
            this.NJNZ.Location = new System.Drawing.Point(144, 260);
            this.NJNZ.Margin = new System.Windows.Forms.Padding(4);
            this.NJNZ.Name = "NJNZ";
            this.NJNZ.Size = new System.Drawing.Size(181, 48);
            this.NJNZ.TabIndex = 1765;
            this.NJNZ.Text = "南京南监测已断开";
            this.NJNZ.UseVisualStyleBackColor = false;
            this.NJNZ.Click += new System.EventHandler(this.NJNZ_Click);
            // 
            // SHHQ
            // 
            this.SHHQ.BackColor = System.Drawing.Color.White;
            this.SHHQ.Location = new System.Drawing.Point(144, 340);
            this.SHHQ.Margin = new System.Windows.Forms.Padding(4);
            this.SHHQ.Name = "SHHQ";
            this.SHHQ.Size = new System.Drawing.Size(181, 45);
            this.SHHQ.TabIndex = 1764;
            this.SHHQ.Text = "上海虹桥监测已断开";
            this.SHHQ.UseVisualStyleBackColor = false;
            this.SHHQ.Click += new System.EventHandler(this.SHHQ_Click);
            // 
            // BJNZ
            // 
            this.BJNZ.BackColor = System.Drawing.Color.White;
            this.BJNZ.Location = new System.Drawing.Point(144, 181);
            this.BJNZ.Margin = new System.Windows.Forms.Padding(4);
            this.BJNZ.Name = "BJNZ";
            this.BJNZ.Size = new System.Drawing.Size(181, 48);
            this.BJNZ.TabIndex = 1763;
            this.BJNZ.Text = "北京南监测已断开";
            this.BJNZ.UseVisualStyleBackColor = false;
            this.BJNZ.Click += new System.EventHandler(this.BJNZ_Click);
            // 
            // tmDate
            // 
            this.tmDate.Enabled = true;
            this.tmDate.Interval = 1000;
            this.tmDate.Tick += new System.EventHandler(this.tmDate_Tick);
            // 
            // nowTime
            // 
            this.nowTime.AutoSize = true;
            this.nowTime.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nowTime.ForeColor = System.Drawing.Color.Red;
            this.nowTime.Location = new System.Drawing.Point(1551, 155);
            this.nowTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nowTime.Name = "nowTime";
            this.nowTime.Size = new System.Drawing.Size(110, 24);
            this.nowTime.TabIndex = 1766;
            this.nowTime.Text = "当前时间";
            // 
            // danniu1
            // 
            this.danniu1.Location = new System.Drawing.Point(95, 195);
            this.danniu1.Margin = new System.Windows.Forms.Padding(6);
            this.danniu1.Name = "danniu1";
            this.danniu1.Size = new System.Drawing.Size(24, 24);
            this.danniu1.TabIndex = 1779;
            this.danniu1.显示状态 = ControlLib.Danniu.Xianshi.红;
            // 
            // danniu2
            // 
            this.danniu2.Location = new System.Drawing.Point(95, 274);
            this.danniu2.Margin = new System.Windows.Forms.Padding(6);
            this.danniu2.Name = "danniu2";
            this.danniu2.Size = new System.Drawing.Size(24, 24);
            this.danniu2.TabIndex = 1780;
            this.danniu2.显示状态 = ControlLib.Danniu.Xianshi.红;
            // 
            // danniu3
            // 
            this.danniu3.Location = new System.Drawing.Point(95, 352);
            this.danniu3.Margin = new System.Windows.Forms.Padding(6);
            this.danniu3.Name = "danniu3";
            this.danniu3.Size = new System.Drawing.Size(24, 24);
            this.danniu3.TabIndex = 1781;
            this.danniu3.显示状态 = ControlLib.Danniu.Xianshi.红;
            // 
            // timer_connChecker
            // 
            this.timer_connChecker.Enabled = true;
            this.timer_connChecker.Interval = 5000;
            this.timer_connChecker.Tick += new System.EventHandler(this.timer_connChecker_Tick);
            // 
            // txt_显示
            // 
            this.txt_显示.Location = new System.Drawing.Point(299, 652);
            this.txt_显示.Margin = new System.Windows.Forms.Padding(4);
            this.txt_显示.Multiline = true;
            this.txt_显示.Name = "txt_显示";
            this.txt_显示.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_显示.Size = new System.Drawing.Size(1238, 226);
            this.txt_显示.TabIndex = 1782;
            // 
            // CSM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1957, 982);
            this.Controls.Add(this.txt_显示);
            this.Controls.Add(this.danniu3);
            this.Controls.Add(this.danniu2);
            this.Controls.Add(this.danniu1);
            this.Controls.Add(this.nowTime);
            this.Controls.Add(this.NJNZ);
            this.Controls.Add(this.SHHQ);
            this.Controls.Add(this.BJNZ);
            this.Controls.Add(this.label19);
            this.Name = "CSM";
            this.Text = "CTCS-3级京沪高铁微机监测系统";
            this.Load += new System.EventHandler(this.CSM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button NJNZ;
        private System.Windows.Forms.Button SHHQ;
        private System.Windows.Forms.Button BJNZ;
        private System.Windows.Forms.Timer tmDate;
        private System.Windows.Forms.Label nowTime;
        public ControlLib.Danniu danniu1;
        public ControlLib.Danniu danniu2;
        public ControlLib.Danniu danniu3;
        private System.Windows.Forms.Timer timer_connChecker;
        private System.Windows.Forms.TextBox txt_显示;


    }
}

