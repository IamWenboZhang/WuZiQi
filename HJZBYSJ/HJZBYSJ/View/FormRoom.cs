using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MrOwlLibrary.NetWork;
using MrOwlLibrary.NetWork.TCP;
using System.Net;
using System.Threading;
using HJZBYSJ.Model;

namespace HJZBYSJ.View
{
    public partial class FormRoom : Form
    {
        public MrOwlTCPClient mrowlTcpClient = new MrOwlTCPClient();
        public Player ThisPlayer;

        public string ServerIP = "";
        public string NickName = "";

        public FormRoom()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            MessagePackage sendMsgPkg = new MessagePackage("GetInRoom", this.listBoxRooms.SelectedItem.ToString()
                , ThisPlayer.IP, ThisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            mrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormRoom_Load(object sender, EventArgs e)
        {
            IPAddress ipadd;
            if (MrOwlNetWork.GetLocalIP(out ipadd))
            {
                mrowlTcpClient = new MrOwlTCPClient(ipadd, "4566");
                mrowlTcpClient.FuncChuLiMessage = DealMsg;
            }
            if (mrowlTcpClient.ConnectSever(ServerIP, "4566"))
            {
                ThisPlayer = new Player(ipadd.ToString(), NickName, ChessPieceType.None);
                Thread listenThread = new Thread(new ThreadStart(mrowlTcpClient.GetMessage));
                listenThread.IsBackground = true;
                listenThread.Start();
                MessagePackage sendPkg = new MessagePackage("LianJie", "用户登录：" + NickName, ThisPlayer.IP, ThisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
                mrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
            }
        }

        public void DealMsg(string msg)
        {
            MessagePackage dealMsgPkg = new MessagePackage(msg);
            switch (dealMsgPkg.Command)
            {
                case "LianJie":
                    Player tmpUser = new Player(ChessPieceType.None);
                    tmpUser.IP = dealMsgPkg.SenderIP;
                    tmpUser.NickName = dealMsgPkg.SenderName;
                    break;
                case "TuiChu": break;
                case "UserList":
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        string[] tmpUserlist = dealMsgPkg.Data.Split(new char[]{'|'});
                        for (int i = 0; i < tmpUserlist.Length; i++)
                        {
                            this.listBoxUsers.Items.Add(tmpUserlist[i]);
                        }
                    }));                    
                    break;
                case "LuoZi":
                    ChessPiece piece = MessagePackage.LuoZiMsgToChessPiece(dealMsgPkg.Data);
                    break;
                case "CreatRoom": break;
                case "GetInRoom": break;
                case "RoomList":
                    this.Invoke(new MethodInvoker(delegate()
                        {
                            string[] tmpRoomlist = dealMsgPkg.Data.Split(new char[] { '|' });
                            for (int i = 0; i < tmpRoomlist.Length; i++)
                            {
                                this.listBoxRooms.Items.Add(tmpRoomlist[i]);
                            }
                        }));
                    break;
                case "ExitRoom": break;
            }
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            MessagePackage sendMsgPkg = new MessagePackage("CreatRoom", ThisPlayer.NickName + "的房间"
                , ThisPlayer.IP, ThisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            mrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
    }
}
