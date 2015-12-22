using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HJZBYSJ.View
{
    public partial class FormLoad : Form
    {
        public string ServerIP = "";
        public string NickName = "";

        public FormLoad()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.textBoxServerIP.Text != "" && this.textBoxNickName.Text != "")
            {
                ServerIP = this.textBoxServerIP.Text;
                NickName = this.textBoxNickName.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("服务器IP和用户名不能为空！");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {

        }
    }
}
