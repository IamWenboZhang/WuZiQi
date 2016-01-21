namespace HJZBYSJ.View
{
    partial class FormRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRoom));
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxRoom = new System.Windows.Forms.GroupBox();
            this.listBoxRooms = new System.Windows.Forms.ListBox();
            this.groupBoxUsers = new System.Windows.Forms.GroupBox();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.btnCreat = new System.Windows.Forms.Button();
            this.groupBoxRoom.SuspendLayout();
            this.groupBoxUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(362, 252);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 23);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "进入房间";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(362, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "返回";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBoxRoom
            // 
            this.groupBoxRoom.Controls.Add(this.listBoxRooms);
            this.groupBoxRoom.Location = new System.Drawing.Point(12, 12);
            this.groupBoxRoom.Name = "groupBoxRoom";
            this.groupBoxRoom.Size = new System.Drawing.Size(335, 292);
            this.groupBoxRoom.TabIndex = 4;
            this.groupBoxRoom.TabStop = false;
            this.groupBoxRoom.Text = "房间列表";
            // 
            // listBoxRooms
            // 
            this.listBoxRooms.FormattingEnabled = true;
            this.listBoxRooms.ItemHeight = 12;
            this.listBoxRooms.Location = new System.Drawing.Point(6, 18);
            this.listBoxRooms.Name = "listBoxRooms";
            this.listBoxRooms.Size = new System.Drawing.Size(323, 268);
            this.listBoxRooms.TabIndex = 0;
            // 
            // groupBoxUsers
            // 
            this.groupBoxUsers.Controls.Add(this.listBoxUsers);
            this.groupBoxUsers.Location = new System.Drawing.Point(362, 12);
            this.groupBoxUsers.Name = "groupBoxUsers";
            this.groupBoxUsers.Size = new System.Drawing.Size(120, 205);
            this.groupBoxUsers.TabIndex = 5;
            this.groupBoxUsers.TabStop = false;
            this.groupBoxUsers.Text = "用户列表";
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.ItemHeight = 12;
            this.listBoxUsers.Location = new System.Drawing.Point(6, 20);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(108, 172);
            this.listBoxUsers.TabIndex = 0;
            // 
            // btnCreat
            // 
            this.btnCreat.Location = new System.Drawing.Point(362, 223);
            this.btnCreat.Name = "btnCreat";
            this.btnCreat.Size = new System.Drawing.Size(120, 23);
            this.btnCreat.TabIndex = 6;
            this.btnCreat.Text = "创建房间";
            this.btnCreat.UseVisualStyleBackColor = true;
            this.btnCreat.Click += new System.EventHandler(this.btnCreat_Click);
            // 
            // FormRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 311);
            this.Controls.Add(this.btnCreat);
            this.Controls.Add(this.groupBoxUsers);
            this.Controls.Add(this.groupBoxRoom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRoom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "游戏大厅";
            this.Load += new System.EventHandler(this.FormRoom_Load);
            this.groupBoxRoom.ResumeLayout(false);
            this.groupBoxUsers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxRoom;
        private System.Windows.Forms.ListBox listBoxRooms;
        private System.Windows.Forms.GroupBox groupBoxUsers;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button btnCreat;
    }
}