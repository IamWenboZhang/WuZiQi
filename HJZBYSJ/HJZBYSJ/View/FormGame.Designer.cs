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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
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
            this.btnSave = new System.Windows.Forms.Button();
            this.listBoxMsg = new System.Windows.Forms.ListBox();
            this.btnReturn = new System.Windows.Forms.Button();
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
            this.groupBoxWhite.Location = new System.Drawing.Point(0, 371);
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
            this.labelUserIPWhite.Size = new System.Drawing.Size(0, 12);
            this.labelUserIPWhite.TabIndex = 3;
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
            this.labelUserNameWhite.Size = new System.Drawing.Size(0, 12);
            this.labelUserNameWhite.TabIndex = 1;
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
            this.groupBoxBlack.Location = new System.Drawing.Point(0, 6);
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
            this.labelUserIPBlack.Size = new System.Drawing.Size(0, 12);
            this.labelUserIPBlack.TabIndex = 7;
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
            this.labelUserNameBlack.Size = new System.Drawing.Size(0, 12);
            this.labelUserNameBlack.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "当前回合数：";
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStep.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelStep.Location = new System.Drawing.Point(89, 93);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(19, 19);
            this.labelStep.TabIndex = 4;
            this.labelStep.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "当前落子方：";
            // 
            // labelCurrentColor
            // 
            this.labelCurrentColor.AutoSize = true;
            this.labelCurrentColor.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCurrentColor.Location = new System.Drawing.Point(89, 141);
            this.labelCurrentColor.Name = "labelCurrentColor";
            this.labelCurrentColor.Size = new System.Drawing.Size(40, 16);
            this.labelCurrentColor.TabIndex = 6;
            this.labelCurrentColor.Text = "黑方";
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(8, 295);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(133, 23);
            this.btnRevert.TabIndex = 7;
            this.btnRevert.Text = "重新开始";
            this.btnRevert.UseVisualStyleBackColor = true;
            this.btnRevert.Click += new System.EventHandler(this.btnRevert_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 266);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(133, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存对战";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // listBoxMsg
            // 
            this.listBoxMsg.FormattingEnabled = true;
            this.listBoxMsg.ItemHeight = 12;
            this.listBoxMsg.Location = new System.Drawing.Point(8, 160);
            this.listBoxMsg.Name = "listBoxMsg";
            this.listBoxMsg.Size = new System.Drawing.Size(133, 100);
            this.listBoxMsg.TabIndex = 11;
            // 
            // btnReturn
            // 
            this.btnReturn.Enabled = false;
            this.btnReturn.Location = new System.Drawing.Point(8, 324);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(133, 23);
            this.btnReturn.TabIndex = 12;
            this.btnReturn.Text = "返回大厅";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 462);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.listBoxMsg);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRevert);
            this.Controls.Add(this.labelCurrentColor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxBlack);
            this.Controls.Add(this.groupBoxWhite);
            this.Controls.Add(this.pictureBoxGameSence);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BetaGo";
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox listBoxMsg;
        private System.Windows.Forms.Button btnReturn;
    }
}