using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MrOwlLibrary.NetWork.TCP;
using HJZBYSJ.View;

namespace HJZBYSJ
{
    static class Program
    {
        private static MrOwlTCPClient mrowlTCPClient;
        private static string ServerIP = "";
        private static string NickName = "";

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
                FormGame frmGame = new FormGame ();
                frmGame.thisGameModel = GameModel.DoubleOffLine;
                Application.Run(frmGame);
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
                ServerIP = frmLoad.ServerIP;
                NickName = frmLoad.NickName;
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
            frmRoom.ServerIP = ServerIP;
            frmRoom.NickName = NickName;           
            DialogResult result = frmRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                FormGame frmGame = new FormGame();
                frmGame.mrowlTCPClient = frmRoom.mrowlTcpClient;
                frmGame.thisPlayer = frmRoom.ThisPlayer;
                frmGame.thisGameModel = GameModel.Online;
                Application.Run(frmGame);    
            }
            else
            {
                DoInFormLoad();
            }
        }
    }
}
