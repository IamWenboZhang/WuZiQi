using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using HJZBYSJ.View;

namespace HJZBYSJ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DoInFormMenu();
        }

        //FormMenu的显示与操作
        private static void DoInFormMenu()
        {
            FormMenu frmMenu = new FormMenu();
            DialogResult result = frmMenu.ShowDialog();
            if (result == DialogResult.OK)
            {
                Application.Run(new FormGame());
            }
            else if (result == DialogResult.Ignore)
            {
                DoInFormLoad();
            }
            else
            {
                Application.ExitThread();
                Application.Exit();
            }
        }
        //FormLoad的显示与操作
        private static void DoInFormLoad()
        {
            FormLoad frmLoad = new FormLoad();
            DialogResult result = frmLoad.ShowDialog();
            if (result == DialogResult.OK)
            {
                DoInFormRoom();
            }
            else
            {
                DoInFormMenu();
            }
        }
        //FormRoom的显示与操作
        private static void DoInFormRoom()
        {
            FormRoom frmRoom = new FormRoom();
            DialogResult result = frmRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                FormGame frmGame = new FormGame();
                frmGame.GameTypeIsDouble = true;
                Application.Run(frmGame);    
            }
            else
            {
                DoInFormLoad();
            }
        }
    }
}
