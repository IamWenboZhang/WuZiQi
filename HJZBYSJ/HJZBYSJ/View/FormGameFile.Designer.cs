namespace HJZBYSJ.View
{
    partial class FormGameFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameFile));
            this.gridGameView = new System.Windows.Forms.DataGridView();
            this.GameID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GameName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridGameView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridGameView
            // 
            this.gridGameView.AllowUserToAddRows = false;
            this.gridGameView.AllowUserToDeleteRows = false;
            this.gridGameView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGameView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GameID,
            this.GameName});
            this.gridGameView.Location = new System.Drawing.Point(12, 12);
            this.gridGameView.Name = "gridGameView";
            this.gridGameView.ReadOnly = true;
            this.gridGameView.RowTemplate.Height = 23;
            this.gridGameView.Size = new System.Drawing.Size(294, 208);
            this.gridGameView.TabIndex = 0;
            // 
            // GameID
            // 
            this.GameID.HeaderText = "游戏存档ID";
            this.GameID.Name = "GameID";
            this.GameID.ReadOnly = true;
            this.GameID.Visible = false;
            // 
            // GameName
            // 
            this.GameName.HeaderText = "游戏名称";
            this.GameName.Name = "GameName";
            this.GameName.ReadOnly = true;
            this.GameName.Width = 250;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(231, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(150, 226);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "载入";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(69, 226);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // FormGameFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 256);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gridGameView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "游戏存档";
            this.Load += new System.EventHandler(this.FormGameFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridGameView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridGameView;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GameName;

    }
}