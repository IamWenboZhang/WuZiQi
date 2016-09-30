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
using HJZBYSJ.DataBase;

namespace HJZBYSJ.View
{
    public partial class FormRoom : Form
    {
        //public MrOwlTCPClient mrowlTcpClient = new MrOwlTCPClient();
        //public Player ThisPlayer;
        public Thread listenThread;
        //public string ServerIP = "";
        //public string NickName = "";

        public FormRoom()
        {
            InitializeComponent();
        }

       //进入房间按钮单击事件
        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (listBoxRooms.SelectedItem != null)
            {
                //获取选择的房间名
                string roomName = listBoxRooms.SelectedItem.ToString();
                //获取当前房间人数
                string roomNum = roomName.Substring(roomName.Length - 4, 1);
                if (roomNum == "2")
                {
                    MessageBox.Show("该房间人数已满，无法进入！");
                }
                else
                {
                    SendCommandGetinRoom();
                }
            }
        }

        //创建房间单击事件
        private void btnCreat_Click(object sender, EventArgs e)
        {
            SendCommandCreatRoom();
        }

        //返回按钮单击事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRoom_Load(object sender, EventArgs e)
        {
            this.Text = "游戏大厅：(" + Program.ThisGamePlayer.NickName + ")";
            this.listenThread = new Thread(new ThreadStart(Program.ThisGameMrowlTcpClient.GetMessage));
            listenThread.IsBackground = true;
            listenThread.Start();
            Program.ThisGameMrowlTcpClient.FuncChuLiMessage = DealMsg;
            SendCommandRequestRoomList();
            SendCommandRequestUserList();
        }

        //消息处理函数
        public void DealMsg(byte[] bytemsg)
        {
            string msg = Encoding.Default.GetString(bytemsg);
            MessagePackage dealMsgPkg = new MessagePackage(msg);
            switch (dealMsgPkg.Command)
            {
                case "UserList":
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        this.listBoxUsers.Items.Clear();
                        string[] tmpUserlist = dealMsgPkg.Data.Split(new char[]{'|'});
                        for (int i = 0; i < tmpUserlist.Length - 1; i++)
                        {
                            this.listBoxUsers.Items.Add(tmpUserlist[i]);
                        }
                    }));                    
                    break;
                case "LuoZi":
                    ChessPiece piece = MessagePackage.LuoZiMsgToChessPiece(dealMsgPkg.Data);
                    break;
                case "CreatRoomResponse":                   
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case "GetInRoomResponse":
                    if (Program.ThisGamePlayer.Color != ChessPieceType.Black)
                    {
                        Game onlineGame2 = new Game(1, false, ChessPieceType.Black, GameModel.Online);
                        Program.ThisGamePlayer.Color = ChessPieceType.White;
                        onlineGame2.playerWhite = Program.ThisGamePlayer;
                        MessagePackage.RoomInfoStringToPlayerBlackAndWhite(dealMsgPkg.Data, out onlineGame2.playerBlack, out onlineGame2.playerWhite);
                        Program.frmGame = new FormGame(onlineGame2);
                        if (Program.ThisGamePlayer.Color == ChessPieceType.Black)
                        {
                            Program.frmGame.CanLuoZi = true;
                        }
                        else
                        {
                            Program.frmGame.CanLuoZi = false;
                        }
                    }
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                    break;
                case "RoomList":
                    this.Invoke(new MethodInvoker(delegate()
                        {
                            this.listBoxRooms.Items.Clear();
                            string[] tmpRoomlist = dealMsgPkg.Data.Split(new char[] { '|' });
                            for (int i = 0; i < tmpRoomlist.Length - 1; i++)
                            {
                                this.listBoxRooms.Items.Add(tmpRoomlist[i]);
                            }
                        }));
                    break;
                case "ServerQuit":
                    this.Invoke(new MethodInvoker(delegate()
                        {
                            MessageBox.Show("服务器已经关闭！");
                            this.Close();
                        }));
                    break;
            }
        }

       


        //向服务器发送退出请求
        public static void SendCommandTuiChu()
        {
            MessagePackage sendMsgPkg = new MessagePackage("TuiChu", Program.ThisGamePlayer.NickName,
                Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
        }


        //向服务器发送请求用户列表的请求
        private void SendCommandRequestUserList()
        {
            MessagePackage sendPkg = new MessagePackage("RequestUserList", "请求获得用户列表" + Program.ThisGamePlayer.NickName, Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
        }

        //向服务器端发送请求房间列表的请求
        private void SendCommandRequestRoomList()
        {
            MessagePackage sendPkg = new MessagePackage("RequestRoomList", "请求获得房间列表" + Program.ThisGamePlayer.NickName, Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
        }

        //向服务器端发送创建房间的请求
        private void SendCommandCreatRoom()
        {
            MessagePackage sendMsgPkg = new MessagePackage("CreatRoom", Program.ThisGamePlayer.NickName + "的房间"
               , Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
        }

        //向服务器端发送进入房间的请求
        private void SendCommandGetinRoom()
        {
            //获取房间名
            string roomName = listBoxRooms.SelectedItem.ToString();
            //获取房间的编号
            string roomID = roomName.Substring(0, 3);
            //请求消息：房间名
            MessagePackage sendPkg = new MessagePackage("GetInRoom", roomID, Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
        }

        ////向服务器端发送落子的请求(本窗体不需要使用)
        //private void SendCommandLuoZi()
        //{

        //}
    }
}
