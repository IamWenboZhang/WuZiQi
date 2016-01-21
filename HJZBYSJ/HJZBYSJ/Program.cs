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
        public static MrOwlTCPClient ThisGameMrowlTcpClient;
        public static Player ThisGamePlayer = new Player(ChessPieceType.None);
        public static FormMenu frmMenu ;
        public static FormLoad frmLoad;
        public static FormRoom frmRoom;
        public static FormGame frmGame;
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
            frmMenu =  new FormMenu();
            Application.Run(frmMenu);
        }      
    }
}
