using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MrOwlLibrary.NetWork.TCP;
using System.Threading;
using System.Net;
using MrOwlLibrary.NetWork;
using HJZBYSJ_Server.Model;

namespace HJZBYSJ_Server
{
    public partial class Form1 : Form
    {
        MrOwlTCPListener mrowlTCPListener = new MrOwlTCPListener();

        IPAddress ipadd;

        List<Player> UserList = new List<Player>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!MrOwlNetWork.GetLocalIP(out ipadd))
            {
                MessageBox.Show("获取本地IP错误！","错误！");
            }
            this.textBoxIP.Text = ipadd.ToString();
            this.textBoxPort.Text = "4566";
            mrowlTCPListener = new MrOwlTCPListener(ipadd, "4566");
            mrowlTCPListener.funcChuLi = DealMsg;
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            Thread listenerTheard = new Thread(new ThreadStart(mrowlTCPListener.StartListen));
            listenerTheard.IsBackground = true;
            listenerTheard.Start();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void DealMsg(string msg)
        {
            MessagePackage delPkg = new MessagePackage(msg);
            switch (delPkg.Command)
            {
                case "LianJie":
                    Player tmpUser = new Player(ChessPieceType.None);
                    tmpUser.IP = delPkg.SenderIP;
                    tmpUser.NickName = delPkg.SenderName;
                    tmpUser.UserSocket = mrowlTCPListener.clietSockets[mrowlTCPListener.clietSockets.Count - 1];
                    UserList.Add(tmpUser);
                    this.Invoke(new MethodInvoker(delegate()
                        {
                            for(int i = 0 ;i<UserList.Count;i++)
                            {
                                listBoxUser.Items.Add(UserList[i].NickName);
                            }
                           
                        }));
                    break;
                case "TuiChu": break;
                case "UserList":break;
                case "LuoZi":break;
                case "CreatRoom": break;
                case "GetInRoom": break;
                case "RoomList": break;
                case "ExitRoom": break;
            }
        }
    }
}
