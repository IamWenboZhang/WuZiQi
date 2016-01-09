namespace HJZBYSJ.View
{
    partial class FormGame
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
            this.pictureBoxGameSence = new System.Windows.Forms.PictureBox();
            this.groupBoxWhite = new System.Windows.Forms.GroupBox();
            this.labelUserIPWhite = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUserNameWhite = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxBlack = new System.Windows.Forms.GroupBox();
            this.labelUserIPBlack = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelUserNameBlack = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStep = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelCurrentColor = new System.Windows.Forms.Label();
            this.btnRevert = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBoxMsg = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameSence)).BeginInit();
            this.groupBoxWhite.SuspendLayout();
            this.groupBoxBlack.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxGameSence
            // 
            this.pictureBoxGameSence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGameSence.Location = new System.Drawing.Point(158, 6);
            this.pictureBoxGameSence.Name = "pictureBoxGameSence";
            this.pictureBoxGameSence.Size = new System.Drawing.Size(450, 450);
            this.pictureBoxGameSence.TabIndex = 0;
            this.pictureBoxGameSence.TabStop = false;
            this.pictureBoxGameSence.Click += new System.EventHandler(this.pictureBoxGameSence_Click);
            // 
            // groupBoxWhite
            // 
            this.groupBoxWhite.Controls.Add(this.labelUserIPWhite);
            this.groupBoxWhite.Controls.Add(this.label3);
            this.groupBoxWhite.Controls.Add(this.labelUserNameWhite);
            this.groupBoxWhite.Controls.Add(this.label1);
            this.groupBoxWhite.Location = new System.Drawing.Point(0, 6);
            this.groupBoxWhite.Name = "groupBoxWhite";
            this.groupBoxWhite.Size = new System.Drawing.Size(152, 81);
            this.groupBoxWhite.TabIndex = 1;
            this.groupBoxWhite.TabStop = false;
            this.groupBoxWhite.Text = "白方";
            // 
            // labelUserIPWhite
            // 
            this.labelUserIPWhite.AutoSize = true;
            this.labelUserIPWhite.Location = new System.Drawing.Point(35, 46);
            this.labelUserIPWhite.Name = "labelUserIPWhite";
            this.labelUserIPWhite.Size = new System.Drawing.Size(95, 12);
            this.labelUserIPWhite.TabIndex = 3;
            this.labelUserIPWhite.Text = "110.110.110.110";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "IP:";
            // 
            // labelUserNameWhite
            // 
            this.labelUserNameWhite.AutoSize = true;
            this.labelUserNameWhite.Location = new System.Drawing.Point(47, 26);
            this.labelUserNameWhite.Name = "labelUserNameWhite";
            this.labelUserNameWhite.Size = new System.Drawing.Size(77, 12);
            this.labelUserNameWhite.TabIndex = 1;
            this.labelUserNameWhite.Text = "一二三四五六";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "昵称:";
            // 
            // groupBoxBlack
            // 
            this.groupBoxBlack.Controls.Add(this.labelUserIPBlack);
            this.groupBoxBlack.Controls.Add(this.label4);
            this.groupBoxBlack.Controls.Add(this.label6);
            this.groupBoxBlack.Controls.Add(this.labelUserNameBlack);
            this.groupBoxBlack.Location = new System.Drawing.Point(0, 381);
            this.groupBoxBlack.Name = "groupBoxBlack";
            this.groupBoxBlack.Size = new System.Drawing.Size(152, 75);
            this.groupBoxBlack.TabIndex = 2;
            this.groupBoxBlack.TabStop = false;
            this.groupBoxBlack.Text = "黑方";
            // 
            // labelUserIPBlack
            // 
            this.labelUserIPBlack.AutoSize = true;
            this.labelUserIPBlack.Location = new System.Drawing.Point(35, 53);
            this.labelUserIPBlack.Name = "labelUserIPBlack";
            this.labelUserIPBlack.Size = new System.Drawing.Size(95, 12);
            this.labelUserIPBlack.TabIndex = 7;
            this.labelUserIPBlack.Text = "888.888.888.888";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "昵称:";
            // 
            // labelUserNameBlack
            // 
            this.labelUserNameBlack.AutoSize = true;
            this.labelUserNameBlack.Location = new System.Drawing.Point(53, 28);
            this.labelUserNameBlack.Name = "labelUserNameBlack";
            this.labelUserNameBlack.Size = new System.Drawing.Size(77, 12);
            this.labelUserNameBlack.TabIndex = 5;
            this.labelUserNameBlack.Text = "一二三四五六";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前回合数：";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(89, 104);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(11, 12);
            this.labelStep.TabIndex = 4;
            this.labelStep.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "当前落子方：";
            // 
            // labelCurrentColor
            // 
            this.labelCurrentColor.AutoSize = true;
            this.labelCurrentColor.Location = new System.Drawing.Point(89, 131);
            this.labelCurrentColor.Name = "labelCurrentColor";
            this.labelCurrentColor.Size = new System.Drawing.Size(29, 12);
            this.labelCurrentColor.TabIndex = 6;
            this.labelCurrentColor.Text = "黑方";
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(8, 312);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(133, 23);
            this.btnRevert.TabIndex = 7;
            this.btnRevert.Text = "重新开始";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(8, 341);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(133, 23);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "退出房间";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(8, 283);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(133, 23);
            this.btnLoad.TabIndex = 9;
            this.btnLoad.Text = "载入存档";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 254);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存对战";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBoxMsg
            // 
            this.textBoxMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMsg.Location = new System.Drawing.Point(0, 155);
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ReadOnly = true;
            this.textBoxMsg.Size = new System.Drawing.Size(152, 93);
            this.textBoxMsg.TabIndex = 11;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 462);
            this.Controls.Add(this.textBoxMsg);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRevert);
            this.Controls.Add(this.labelCurrentColor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxBlack);
            this.Controls.Add(this.groupBoxWhite);
            this.Controls.Add(this.pictureBoxGameSence);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGame_FormClosing);
            this.Load += new System.EventHandler(this.FormGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameSence)).EndInit();
            this.groupBoxWhite.ResumeLayout(false);
            this.groupBoxWhite.PerformLayout();
            this.groupBoxBlack.ResumeLayout(false);
            this.groupBoxBlack.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameSence;
        private System.Windows.Forms.GroupBox groupBoxWhite;
        private System.Windows.Forms.Label labelUserIPWhite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelUserNameWhite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxBlack;
        private System.Windows.Forms.Label labelUserIPBlack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelUserNameBlack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCurrentColor;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBoxMsg;
    }
}