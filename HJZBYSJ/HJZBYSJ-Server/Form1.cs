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
using System.Net.Sockets;

namespace HJZBYSJ_Server
{
    public partial class Form1 : Form
    {
        MrOwlTCPListener mrowlTCPListener = new MrOwlTCPListener();

        IPAddress ipadd;

        List<Player> UserList = new List<Player>();
        List<Room> RoomList = new List<Room>();

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
            this.btnStar.Enabled = false;
            MessageBox.Show("正在监听:IP:" + this.mrowlTCPListener.localIP + "端口:" + this.mrowlTCPListener.localPort, "监听已经启动");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private string GetUserListString()
        {
            string result = "";
            for (int i = 0; i < UserList.Count; i++)
            {
                result += UserList[i].NickName + "|";
            }
            return result;
        }

        private string GetRoomListString()
        {
            string result = "";
            for (int i = 0; i < RoomList.Count; i++)
            {
                result += RoomList[i].RoomName + "}";
            }
            return result;
        }

        //刷新用户列表的状态并通知连接的客户端
        private void RefreshUserList()
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                this.listBoxUser.Items.Clear();
                for (int i = 0; i < UserList.Count; i++)
                {
                    this.listBoxUser.Items.Add(UserList[i].NickName);
                    SendUserList(UserList[i].UserSocket);
                }
            }));
        }

        //刷新房间列表的状态并通知连接的客户端
        private void RefreshRoomList()
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                this.listBoxRoom.Items.Clear();
                for (int i = 0; i < RoomList.Count; i++)
                {
                    this.listBoxRoom.Items.Add(RoomList[i].RoomName);                    
                }
                for (int i = 0; i < UserList.Count; i++)
                {
                    SendRoomList(UserList[i].UserSocket);
                }
            }));
        }

        //向所有连接的客户端发送用户列表
        private void SendUserList(Socket userSocket)
        {
            MessagePackage sendMsgPkg = new MessagePackage("UserList", GetUserListString()
                                , mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            mrowlTCPListener.SendToClient(userSocket, sendMsgPkg.MsgPkgToString());
        }

        //向所有连接的客户端发送房间列表
        private void SendRoomList(Socket userSocket)
        {
            MessagePackage exitMsgPkg = new MessagePackage("RoomList", GetRoomListString()
                              , mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            mrowlTCPListener.SendToClient(userSocket, exitMsgPkg.MsgPkgToString());
        }

        private bool SearchUserByIP(string ip,out Player player)
        {
            bool isSuccess = false;
            player = new Player(ChessPieceType.None);
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].IP == ip)
                {
                    player = UserList[i];
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        //消息处理方法 
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
                    RefreshUserList();
                    MessagePackage lianjieresponsePkg = new MessagePackage("LianJieResponse", "Success",
                        this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                    this.mrowlTCPListener.SendToClient(tmpUser.UserSocket, lianjieresponsePkg.MsgPkgToString());
                    break;
                case "TuiChu":
                    for (int i = 0; i < UserList.Count; i++)
                    {
                        if (UserList[i].NickName.CompareTo(delPkg.SenderName) == 0)
                        {
                            UserList[i].UserSocket.Close();
                            mrowlTCPListener.keepConnect = false;
                            UserList.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < RoomList.Count; i++)
                    {
                        if (RoomList[i].RoomMaster.NickName.CompareTo(delPkg.SenderName) == 0)
                        {
                            RoomList.RemoveAt(i);
                        }
                        if (RoomList[i].RoomMember.NickName.CompareTo(delPkg.SenderName) == 0)
                        {
                            RoomList[i].RoomMemberNum = RoomList[i].RoomMemberNum - 1;
                        }
                    }
                    RefreshUserList();
                    RefreshRoomList();
                    break;
                case "UserList":break;
                case "LuoZi":break;
                case "CreatRoom":                  
                    Player roomMaster = new Player (ChessPieceType.None);
                    if (SearchUserByIP(delPkg.SenderIP,out roomMaster))
                    {
                        roomMaster.Color = ChessPieceType.Black;
                        Room tmpRoom = new Room(roomMaster, this.RoomList.Count);
                        this.RoomList.Add(tmpRoom);
                        RefreshRoomList();
                        MessagePackage responsePkg = new MessagePackage("CreatRoomResponse", "Success",
                        this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                        this.mrowlTCPListener.SendToClient(roomMaster.UserSocket, responsePkg.MsgPkgToString());
                    }                 
                    break;
                case "CreatRoomResponse": break;
                case "GetInRoom":
                    Player roomMemeber = new Player(ChessPieceType.None);
                    if (SearchUserByIP(delPkg.SenderIP, out roomMemeber))
                    {
                        roomMemeber.Color = ChessPieceType.White;
                        int index = int.Parse(delPkg.Data);
                        if (index < RoomList.Count && RoomList[index].RoomMemberNum == 1)
                        {
                            RoomList[index].AddMember(roomMemeber);
                            MessagePackage responsePkg = new MessagePackage("GetInRoomResponse", "Success_" + roomMemeber.NickName + "_" + roomMemeber.IP,
                            this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                            this.mrowlTCPListener.SendToClient(RoomList[index].RoomMaster.UserSocket, responsePkg.MsgPkgToString());
                            this.mrowlTCPListener.SendToClient(RoomList[index].RoomMember.UserSocket, responsePkg.MsgPkgToString());
                        }
                    }
                    break;
                case "GetInRoomResponse": break;
                case "RoomList": break;
                case "ExitRoom": break;
            }
        }
    }
}
