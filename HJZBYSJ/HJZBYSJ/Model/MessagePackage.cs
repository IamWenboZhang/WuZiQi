using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJZBYSJ.Model
{
    class MessagePackage
    {
        public string Command = "";             //命令类型
        public string Data = "";               //数据
        public string SenderIP = "";            //发送这段消息的客户端的IP地址
        public string SenderName = "";          //发送这段消息的客户端的昵称
        public string MsgTime = "";             //发送这段消息的时间

        public MessagePackage()
        {

        }

        //初始化函数
        public MessagePackage(string command, string data, string senderip, string sendername, string msgtime)
        {
            this.Command = command;
            this.Data = data;
            this.SenderIP = senderip;
            this.SenderName = sendername;
            this.MsgTime = msgtime;
        }

        //根据字符串反序列化为MessagePackage类的对象
        public MessagePackage(string msg)
        {
            string[] tmp = msg.Split(new char[] { '@' });
            this.Command = tmp[0];
            this.Data = tmp[1];
            this.SenderIP = tmp[2];
            this.SenderName = tmp[3];
            this.MsgTime = tmp[4];
        }

        //将MessagePackage类的对象序列化为字符串
        public string MsgPkgToString()
        {
            string result = "";
            result = this.Command + "@" + this.Data + "@" + this.SenderIP + "@" + this.SenderName + "@" + this.MsgTime;
            return result;
        }

        //棋子序列化为字符串
        public static string LuoZiMsgToStirng(ChessPiece piece)
        {
            string result = piece.BoardX.ToString() + "|" + piece.BoardY.ToString() + "|" + piece.Color.ToString();
            return result;
        }

        //将棋子字符串反序列化为棋子类的对象
        public static ChessPiece LuoZiMsgToChessPiece(string msg)
        {
            string[] tmp = msg.Split(new char[] { '|' });
            int boardX = int.Parse(tmp[0].Trim());
            int boardY = int.Parse(tmp[1].Trim());
            string colorStr = tmp[2].Trim();
            ChessPieceType color = ChessPieceType.None;
            switch (colorStr)
            {
                case "White":
                    color = ChessPieceType.White;
                    break;
                case "Black":
                    color = ChessPieceType.Black;
                    break;
                default:
                    color = ChessPieceType.None;
                    break;
            }
            ChessPiece result = new ChessPiece(boardX, boardY, color);
            return result;
        }   

        //把一个房间的信息解析出来 实例化黑方玩家和白方玩家
        public static bool RoomInfoStringToPlayerBlackAndWhite(string roominfo, out Player playerBlack, out Player playerWhite)
        {
            playerBlack = new Player(ChessPieceType.Black);
            playerWhite = new Player(ChessPieceType.White);
            bool result = true;
            try
            {
                string[] tmp = roominfo.Split(new char[] { '_' });
                playerBlack = new Player(tmp[1], tmp[0], ChessPieceType.Black);
                playerWhite = new Player(tmp[3], tmp[2], ChessPieceType.White);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
