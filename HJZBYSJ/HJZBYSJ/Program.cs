using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MrOwlLibrary.NetWork.TCP;
using HJZBYSJ.View;
using HJZBYSJ.Model;
using System.IO;
using MrOwlLibrary.Config;
using MrOwlLibrary.DataBase;
using HJZBYSJ.DataBase;
using System.Threading;

namespace HJZBYSJ
{
    static class Program
    {
        private static MrOwlTCPClient ThisGameMrowlTcpClient = new MrOwlTCPClient();
        private static Player ThisGamePlayer;
        private static Thread listenThread = new Thread(new ThreadStart(ThisGameMrowlTcpClient.GetMessage));
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        { 
            // 判断配置文件是否存在
            if (File.Exists(Application.StartupPath + "\\HJZBYSJ.ini"))
            {
                // 从配置文件中读取连接字符串的参数值
                MrOwlIniFile iniFile = new MrOwlIniFile(Application.StartupPath + "\\HJZBYSJ.ini");
                string str = "";
                iniFile.GetValueOfKey("Connection", "Server", ref MrOwlDB_SQLserver.server);
                iniFile.GetValueOfKey("Connection", "Database", ref MrOwlDB_SQLserver.database);
                iniFile.GetValueOfKey("Connection", "User", ref MrOwlDB_SQLserver.user);
                iniFile.GetValueOfKey("Connection", "Password", ref MrOwlDB_SQLserver.password);
            }
            else
            {
                MrOwlDB_SQLserver.server = "";
                MrOwlDB_SQLserver.database = "";
                MrOwlDB_SQLserver.user = "";
                MrOwlDB_SQLserver.password = "";
                MessageBox.Show("未找到配置文件.");
            }
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
                Game tmpGame = new Game(1, false, ChessPieceType.Black, GameModel.DoubleOffLine);
                FormGame frmGame = new FormGame (tmpGame);
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
            listenThread.IsBackground = true;
            listenThread.Start();
            DialogResult result = frmLoad.ShowDialog();
            if (result == DialogResult.OK)
            {
                ThisGameMrowlTcpClient = frmLoad.mrowlTcpClient;
                ThisGamePlayer = frmLoad.ThisPlayer;
                DoInFormRoom();
            }
            else
            {
                MessagePackage sendMsgPkg = new MessagePackage("TuiChu", ThisGamePlayer.NickName,
                ThisGamePlayer.IP, ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                ThisGameMrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
                ThisGameMrowlTcpClient.Close();
                frmLoad.Dispose();
                Application.ExitThread();
                Application.Exit();
            }
        }

        //FormRoom的显示与操作
        private static void DoInFormRoom()
        {
            FormRoom frmRoom = new FormRoom();
            frmRoom.mrowlTcpClient = ThisGameMrowlTcpClient;
            frmRoom.ThisPlayer = ThisGamePlayer;           
            DialogResult result = frmRoom.ShowDialog();
            if (result == DialogResult.OK)
            {
                FormGame frmGame = new FormGame();
                frmGame.mrowlTCPClient = ThisGameMrowlTcpClient;
                frmGame.thisPlayer = ThisGamePlayer;
                frmGame.ThisGame.thisGameModel = GameModel.Online;
                Application.Run(frmGame);    
            }
            else
            {
                MessagePackage sendMsgPkg = new MessagePackage("TuiChu", frmRoom.ThisPlayer.NickName,
                frmRoom.ThisPlayer.IP, frmRoom.ThisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                frmRoom.mrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
                frmRoom.mrowlTcpClient.Close();
                frmRoom.Dispose();
                Application.ExitThread();
                Application.Exit();
            }
        }
    }
}
