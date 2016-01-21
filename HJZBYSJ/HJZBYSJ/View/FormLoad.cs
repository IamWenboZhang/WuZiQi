using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using HJZBYSJ.Model;
using System.Net;
using MrOwlLibrary.NetWork;
using MrOwlLibrary.NetWork.TCP;
using System.Threading;

namespace HJZBYSJ.View
{
    public partial class FormLoad : Form
    {
        //public MrOwlTCPClient mrowlTcpClient = new MrOwlTCPClient();
        //public Player ThisPlayer;
        public string ServerIP = "";
        public string NickName = "";
        public Thread listenThread;

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
                IPAddress ipadd;
                if (MrOwlNetWork.GetLocalIP(out ipadd))
                {
                    Program.ThisGameMrowlTcpClient = new MrOwlTCPClient(ipadd, "4566");
                    Program.ThisGameMrowlTcpClient.FuncChuLiMessage = DealMsg;
                }
                if (Program.ThisGameMrowlTcpClient.ConnectSever(ServerIP, "4566"))
                {
                    Program.ThisGamePlayer = new Player(ipadd.ToString(), NickName, ChessPieceType.None);
                    listenThread = new Thread(new ThreadStart(Program.ThisGameMrowlTcpClient.GetMessage));
                    listenThread.IsBackground = true;
                    listenThread.Start();
                    MessagePackage sendPkg = new MessagePackage("LianJie", "用户登录：" + NickName, Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
                    Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
                }
            }
            else
            {
                MessageBox.Show("服务器IP和用户名不能为空！");
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Program.frmMenu.Show();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void DealMsg(string msg)
        {
            MessagePackage dealMsgPkg = new MessagePackage(msg);
            switch (dealMsgPkg.Command)
            {     
                case "LianJieResponse": 
                    //this.DialogResult = DialogResult.OK;
                    //this.listenThread.Abort();
                    if (dealMsgPkg.Data == "Success")
                    {
                        NavigateToFormRoom();
                    }
                    else
                    {
                        MessageBox.Show("用户名重复！");
                    }
                    break;       
            }
        }

        private void NavigateToFormRoom()
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }));
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
            IPAddress ipadd1;
            if (MrOwlNetWork.GetLocalIP(out ipadd1))
            {
                this.textBoxServerIP.Text = ipadd1.ToString();
            }
            this.textBoxNickName.Focus();
        }
    }
}
