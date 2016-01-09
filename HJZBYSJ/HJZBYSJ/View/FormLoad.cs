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
        public MrOwlTCPClient mrowlTcpClient = new MrOwlTCPClient();
        public Player ThisPlayer;
        public string ServerIP = "";
        public string NickName = "";
        Thread listenThread;

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
                    mrowlTcpClient = new MrOwlTCPClient(ipadd, "4566");
                    mrowlTcpClient.FuncChuLiMessage = DealMsg;
                }
                if (mrowlTcpClient.ConnectSever(ServerIP, "4566"))
                {
                    ThisPlayer = new Player(ipadd.ToString(), NickName, ChessPieceType.None);
                    listenThread = new Thread(new ThreadStart(mrowlTcpClient.GetMessage));
                    listenThread.IsBackground = true;
                    listenThread.Start();
                    MessagePackage sendPkg = new MessagePackage("LianJie", "用户登录：" + NickName, ThisPlayer.IP, ThisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
                    mrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormLoad_Load(object sender, EventArgs e)
        {
        }

        private void DealMsg(string msg)
        {
            MessagePackage dealMsgPkg = new MessagePackage(msg);
            switch (dealMsgPkg.Command)
            {     
                case "LianJieResponse": 
                    this.DialogResult = DialogResult.OK;
                    this.listenThread.Abort();
                    this.Close();
                    break;
            }
        }
    }
}
