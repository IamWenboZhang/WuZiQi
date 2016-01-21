namespace HJZBYSJ.View
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.btnSingle = new System.Windows.Forms.Button();
            this.btnOnline = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnAgainstComputer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSingle
            // 
            this.btnSingle.Location = new System.Drawing.Point(105, 64);
            this.btnSingle.Name = "btnSingle";
            this.btnSingle.Size = new System.Drawing.Size(75, 23);
            this.btnSingle.TabIndex = 0;
            this.btnSingle.Text = "双人单机";
            this.btnSingle.UseVisualStyleBackColor = true;
            this.btnSingle.Click += new System.EventHandler(this.btnSingle_Click);
            // 
            // btnOnline
            // 
            this.btnOnline.Location = new System.Drawing.Point(105, 152);
            this.btnOnline.Name = "btnOnline";
            this.btnOnline.Size = new System.Drawing.Size(75, 23);
            this.btnOnline.TabIndex = 1;
            this.btnOnline.Text = "网络对战";
            this.btnOnline.UseVisualStyleBackColor = true;
            this.btnOnline.Click += new System.EventHandler(this.btnOnline_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(105, 196);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出游戏";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(105, 108);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 3;
            this.btnLoadFile.Text = "载入存档";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnAgainstComputer
            // 
            this.btnAgainstComputer.Location = new System.Drawing.Point(105, 20);
            this.btnAgainstComputer.Name = "btnAgainstComputer";
            this.btnAgainstComputer.Size = new System.Drawing.Size(75, 23);
            this.btnAgainstComputer.TabIndex = 4;
            this.btnAgainstComputer.Text = "人机对战";
            this.btnAgainstComputer.UseVisualStyleBackColor = true;
            this.btnAgainstComputer.Click += new System.EventHandler(this.btnAgainstComputer_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnAgainstComputer);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOnline);
            this.Controls.Add(this.btnSingle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C#五子棋";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSingle;
        private System.Windows.Forms.Button btnOnline;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnAgainstComputer;
    }
}