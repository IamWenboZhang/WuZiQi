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
            //mrowlTCPListener = new MrOwlTCPListener(ipadd, "4566");
            //mrowlTCPListener.funcChuLi = DealMsg;
        }

        private void btnStar_Click(object sender, EventArgs e)
        {
            IPAddress tmp = IPAddress.Parse(this.textBoxIP.Text);
            mrowlTCPListener = new MrOwlTCPListener(tmp, "4566");
            mrowlTCPListener.funcMsgChuLi = DealMsg;
            Thread listenerTheard = new Thread(new ThreadStart(mrowlTCPListener.StartListen));
            listenerTheard.IsBackground = true;
            listenerTheard.Start();
            this.btnStar.Enabled = false;
            MessageBox.Show("正在监听:IP:" + this.mrowlTCPListener.localIP + "端口:" + this.mrowlTCPListener.localPort, "监听已经启动");
        }        

        //查找用户列表里是否有重复的名字
        private bool CheckisRepeat(string name)
        {
            bool result = false;
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].NickName.CompareTo(name) == 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        //将用户列表里的所有用户名序列化为字符串
        private string GetUserListString()
        {
            string result = "";
            for (int i = 0; i < UserList.Count; i++)
            {
                result += UserList[i].NickName + "|";
            }
            return result;
        }

        //将房间列表里的所有房间名序列化为字符串
        private string GetRoomListString()
        {
            string result = "";
            for (int i = 0; i < RoomList.Count; i++)
            {
                result += RoomList[i].RoomName + "|";
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
                    RoomList[i].RoomID = i;
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

        //根据消息发送者的IP和昵称在RoomList里面寻找对应的房间
        private bool SearchRoomByIP(string ip, string name, out Room room)
        {
            room = new Room();
            bool isSuccess = false;
            for (int i = 0; i < RoomList.Count; i++)
            {
                if((RoomList[i].RoomMaster.IP==ip && RoomList[i].RoomMaster.NickName == name)
                    || (RoomList[i].RoomMember.IP == ip && RoomList[i].RoomMember.NickName == name))
                {
                    room = RoomList[i];
                    isSuccess = true;
                    break;
                }
            }
            return isSuccess;
        }

        //根据消息发送者的IP和昵称在UserList里面寻找对应的玩家
        private bool SearchUserByIP(string ip,string name,out Player player)
        {
            bool isSuccess = false;
            player = new Player(ChessPieceType.None);
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].IP == ip && UserList[i].NickName == name)
                {
                    player = UserList[i];
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        //消息处理方法 
        private void DealMsg(byte[] bytemsg)
        {
            string msg = Encoding.Default.GetString(bytemsg);         
            MessagePackage delPkg = new MessagePackage(msg);
            switch (delPkg.Command)
            {
                case "LianJie":
                    Player tmpUser = new Player(ChessPieceType.None);
                    tmpUser.IP = delPkg.SenderIP;
                    tmpUser.NickName = delPkg.SenderName;
                    tmpUser.UserSocket = mrowlTCPListener.clientSockets[mrowlTCPListener.clientSockets.Count - 1];
                    if (!CheckisRepeat(delPkg.SenderName))
                    {
                        UserList.Add(tmpUser);
                        RefreshUserList();
                        SendLianJieResponse(tmpUser.UserSocket, true);
                    }
                    else
                    {
                        SendLianJieResponse(tmpUser.UserSocket, false);
                    }
                    break;
                case "TuiChu":
                    Player quitUser = new Player (ChessPieceType.None);
                    Room quitRoom = new Room();
                    if (SearchUserByIP(delPkg.SenderIP, delPkg.SenderName, out quitUser))
                    {
                        quitUser.UserSocket.Close();                        
                        UserList.Remove(quitUser);
                        if (SearchRoomByIP(delPkg.SenderIP, delPkg.SenderName, out quitRoom))
                        {
                            if (quitRoom.RoomMemberNum == 2)
                            {
                                if (quitUser.NickName == quitRoom.RoomMaster.NickName && quitUser.IP == quitRoom.RoomMaster.IP)
                                {
                                    SendExitRoomResponse(quitRoom, true);
                                }
                                else if (quitUser.NickName == quitRoom.RoomMember.NickName && quitUser.IP == quitRoom.RoomMember.IP)
                                {
                                    SendExitRoomResponse(quitRoom, false);
                                }
                            }
                            RoomList.Remove(quitRoom);
                        }
                    }
                    RefreshUserList();
                    RefreshRoomList();
                    break;
                case "ExitRoom":
                    Player exitUser = new Player(ChessPieceType.None);
                    Room exitRoom = new Room();
                    if (SearchUserByIP(delPkg.SenderIP, delPkg.SenderName, out exitUser))
                    {
                        if (SearchRoomByIP(delPkg.SenderIP, delPkg.SenderName, out exitRoom))
                        {
                            if (exitRoom.RoomMemberNum == 2)
                            {
                                if (exitUser.NickName == exitRoom.RoomMaster.NickName && exitUser.IP == exitRoom.RoomMaster.IP)
                                {
                                    SendExitRoomResponse(exitRoom, true);
                                }
                                else if (exitUser.NickName == exitRoom.RoomMember.NickName && exitUser.IP == exitRoom.RoomMember.IP)
                                {
                                    SendExitRoomResponse(exitRoom, false);
                                }
                            }
                            RoomList.Remove(exitRoom);
                        }
                    }
                    RefreshRoomList();
                    break;
                case "LuoZi":
                    Room room = new Room();
                    if (SearchRoomByIP(delPkg.SenderIP, delPkg.SenderName, out room))
                    {
                        SendLuoZiResponse(room, delPkg.Data);
                    }
                    break;
                case "CreatRoom":                  
                    Player roomMaster = new Player (ChessPieceType.None);
                    if (SearchUserByIP(delPkg.SenderIP,delPkg.SenderName,out roomMaster))
                    {
                        roomMaster.Color = ChessPieceType.Black;
                        Room tmpRoom = new Room(roomMaster, this.RoomList.Count);
                        this.RoomList.Add(tmpRoom);
                        RefreshRoomList();
                        SendCreatRoomResponse(roomMaster.UserSocket);
                    }        
                    break;
                case "GetInRoom":
                    Player roomMemeber = new Player(ChessPieceType.None);
                    Room resultRoom = new Room();
                    if (SearchUserByIP(delPkg.SenderIP,delPkg.SenderName,out roomMemeber))
                    {
                        roomMemeber.Color = ChessPieceType.White;
                        for (int i = 0; i < RoomList.Count; i++)
                        {
                            string roomID = RoomList[i].RoomName.Substring(0, 3);
                            if (roomID == delPkg.Data)
                            {
                                resultRoom = RoomList[i];
                                resultRoom.AddMember(roomMemeber);
                            }
                        }
                        RefreshRoomList();
                        SendGetInRoomResponse(resultRoom);
                    }
                    break;
                case "RequestRoomList":
                    Player responseUserRoomList =new Player (ChessPieceType.None);
                    if (SearchUserByIP(delPkg.SenderIP,delPkg.SenderName,out responseUserRoomList))
                    {
                        SendRoomList(responseUserRoomList.UserSocket);
                    }
                    break;
                case "RequestUserList":
                    Player responseUserUserList =new Player (ChessPieceType.None);
                    if (SearchUserByIP(delPkg.SenderIP, delPkg.SenderName, out responseUserUserList))
                    {
                        SendUserList(responseUserUserList.UserSocket);
                    }
                    break;
                case "RequestRevertGame":
                    Player RevertUser = new Player(ChessPieceType.None);
                    Room RevertRoom = new Room();
                    if (SearchUserByIP(delPkg.SenderIP, delPkg.SenderName, out RevertUser))
                    {
                        if (SearchRoomByIP(delPkg.SenderIP, delPkg.SenderName, out RevertRoom))
                        {
                            if (RevertRoom.RoomMemberNum == 2)
                            {
                                if (RevertUser.NickName == RevertRoom.RoomMaster.NickName && RevertUser.IP == RevertRoom.RoomMaster.IP)
                                {
                                    SendRevertGameRequest(RevertRoom, true);
                                }
                                else if (RevertUser.NickName == RevertRoom.RoomMember.NickName && RevertUser.IP == RevertRoom.RoomMember.IP)
                                {
                                    SendRevertGameRequest(RevertRoom, false);
                                }
                            }
                        }
                    }
                    break;
                case "RevertGameResponse":
                    Player RevertResponseUser = new Player(ChessPieceType.None);
                    Room RevertResponseRoom = new Room();
                    if (SearchUserByIP(delPkg.SenderIP, delPkg.SenderName, out RevertResponseUser))
                    {
                        if (SearchRoomByIP(delPkg.SenderIP, delPkg.SenderName, out RevertResponseRoom))
                        {
                            if (RevertResponseRoom.RoomMemberNum == 2)
                            {
                                if (delPkg.Data == "Yes")
                                {
                                    SendRevertGameResult(RevertResponseRoom, true);
                                }
                                else if (delPkg.Data == "No")
                                {
                                    SendRevertGameResult(RevertResponseRoom, false);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        //窗体关闭事件
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < UserList.Count; i++)
            {
                SendServerQuit(UserList[i].UserSocket);
            }
            mrowlTCPListener.Close();
        }


        //连接请求的响应
        private void SendLianJieResponse(Socket clientSocket,bool isSuccess)
        {
            if (isSuccess)
            {
                MessagePackage lianjieresponsePkg = new MessagePackage("LianJieResponse", "Success",
                       this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                this.mrowlTCPListener.SendToClient(clientSocket, lianjieresponsePkg.MsgPkgToString());
            }
            else 
            {
                MessagePackage lianjieresponsePkg = new MessagePackage("LianJieResponse", "False@",
                       this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                this.mrowlTCPListener.SendToClient(clientSocket, lianjieresponsePkg.MsgPkgToString());
            }
            
        }

        //发送创建房间的响应
        private void SendCreatRoomResponse(Socket clientSocket)
        {
            MessagePackage responsePkg = new MessagePackage("CreatRoomResponse", "Success",
                        this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            this.mrowlTCPListener.SendToClient(clientSocket, responsePkg.MsgPkgToString());
        }

        //发送进入房间的响应
        private void SendGetInRoomResponse(Room room)
        {
            MessagePackage responsePkg = new MessagePackage("GetInRoomResponse", MessagePackage.RoomInfoToString(room),
                            this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
            this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
        }

        //发送退出房间的响应
        private void SendExitRoomResponse(Room room,bool isMaster)
        {
            MessagePackage responsePkg = new MessagePackage("ExitRoomResponse", "",
                            this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            if (isMaster)
            {
                this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
            }
            else
            {
                this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
            }
        }

        //发送服务器关闭消息
        private void SendServerQuit(Socket socket)
        {
            MessagePackage responsePkg = new MessagePackage("ServerQuit", "",
                            this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));           
            this.mrowlTCPListener.SendToClient(socket, responsePkg.MsgPkgToString());
        }

        //发送落子消息的响应
        private void SendLuoZiResponse(Room room,string pieceInfoStr)
        {
            MessagePackage responsePkg = new MessagePackage("LuoZiResponse", pieceInfoStr,
                          this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
            this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
        }


        //发送重新游戏的请求
        private void SendRevertGameRequest(Room room, bool isMaster)
        {
            MessagePackage responsePkg = new MessagePackage("RequestRevertGame", MessagePackage.RoomInfoToString(room),
                            this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            if (isMaster)
            {
                this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
            }
            else
            {
                this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
            }
        }

        //发送重新游戏的判定结果
        private void SendRevertGameResult(Room room,bool isOK)
        {
            if (isOK)
            {
                MessagePackage responsePkg = new MessagePackage("RevertGameResult", "Yes",
                           this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
                this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
            }
            else
            {
                MessagePackage responsePkg = new MessagePackage("RevertGameResult", "No",
                          this.mrowlTCPListener.localIP.ToString(), "Server", DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                this.mrowlTCPListener.SendToClient(room.RoomMaster.UserSocket, responsePkg.MsgPkgToString());
                this.mrowlTCPListener.SendToClient(room.RoomMember.UserSocket, responsePkg.MsgPkgToString());
            }
        }
    }
}
