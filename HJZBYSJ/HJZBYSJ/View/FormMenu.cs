using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using HJZBYSJ.DataBase;

namespace HJZBYSJ.View
{
    public partial class FormMenu : Form
    {
        public Game LoadGame = new Game();

        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            FormGameFile frmGameFile = new FormGameFile();
            if (frmGameFile.ShowDialog() == DialogResult.OK)
            {
                if (GameUtil.GetAt(frmGameFile.SelectedGameID, ref LoadGame))
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void btnAgainstComputer_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

    }
}
