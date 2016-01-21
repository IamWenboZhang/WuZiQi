namespace HJZBYSJ_Server
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.btnStar = new System.Windows.Forms.Button();
            this.groupBoxRoom = new System.Windows.Forms.GroupBox();
            this.listBoxRoom = new System.Windows.Forms.ListBox();
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.listBoxUser = new System.Windows.Forms.ListBox();
            this.groupBoxRoom.SuspendLayout();
            this.groupBoxUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "本机IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(233, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "本机端口号：";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(68, 12);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.ReadOnly = true;
            this.textBoxIP.Size = new System.Drawing.Size(130, 21);
            this.textBoxIP.TabIndex = 2;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(316, 12);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.ReadOnly = true;
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 3;
            // 
            // btnStar
            // 
            this.btnStar.Location = new System.Drawing.Point(466, 10);
            this.btnStar.Name = "btnStar";
            this.btnStar.Size = new System.Drawing.Size(150, 23);
            this.btnStar.TabIndex = 4;
            this.btnStar.Text = "启动服务";
            this.btnStar.UseVisualStyleBackColor = true;
            this.btnStar.Click += new System.EventHandler(this.btnStar_Click);
            // 
            // groupBoxRoom
            // 
            this.groupBoxRoom.Controls.Add(this.listBoxRoom);
            this.groupBoxRoom.Location = new System.Drawing.Point(13, 39);
            this.groupBoxRoom.Name = "groupBoxRoom";
            this.groupBoxRoom.Size = new System.Drawing.Size(403, 277);
            this.groupBoxRoom.TabIndex = 8;
            this.groupBoxRoom.TabStop = false;
            this.groupBoxRoom.Text = "房间列表";
            // 
            // listBoxRoom
            // 
            this.listBoxRoom.FormattingEnabled = true;
            this.listBoxRoom.ItemHeight = 12;
            this.listBoxRoom.Location = new System.Drawing.Point(6, 22);
            this.listBoxRoom.Name = "listBoxRoom";
            this.listBoxRoom.Size = new System.Drawing.Size(391, 244);
            this.listBoxRoom.TabIndex = 0;
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.listBoxUser);
            this.groupBoxUser.Location = new System.Drawing.Point(466, 40);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(156, 276);
            this.groupBoxUser.TabIndex = 9;
            this.groupBoxUser.TabStop = false;
            this.groupBoxUser.Text = "用户列表";
            // 
            // listBoxUser
            // 
            this.listBoxUser.FormattingEnabled = true;
            this.listBoxUser.ItemHeight = 12;
            this.listBoxUser.Location = new System.Drawing.Point(6, 21);
            this.listBoxUser.Name = "listBoxUser";
            this.listBoxUser.Size = new System.Drawing.Size(144, 244);
            this.listBoxUser.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 326);
            this.Controls.Add(this.groupBoxUser);
            this.Controls.Add(this.groupBoxRoom);
            this.Controls.Add(this.btnStar);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "五子棋服务器端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxRoom.ResumeLayout(false);
            this.groupBoxUser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button btnStar;
        private System.Windows.Forms.GroupBox groupBoxRoom;
        private System.Windows.Forms.ListBox listBoxRoom;
        private System.Windows.Forms.GroupBox groupBoxUser;
        private System.Windows.Forms.ListBox listBoxUser;
    }
}

