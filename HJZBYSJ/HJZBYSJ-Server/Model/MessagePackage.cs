using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJZBYSJ_Server.Model
{
    class MessagePackage
    {
        public string Command = "";
        public string Data = "";
        public string SenderIP = "";
        public string SenderName = "";
        public string MsgTime = "";

        public MessagePackage()
        {

        }

        public MessagePackage(string command, string data, string senderip, string sendername, string msgtime)
        {
            this.Command = command;
            this.Data = data;
            this.SenderIP = senderip;
            this.SenderName = sendername;
            this.MsgTime = msgtime;
        }

        public MessagePackage(string msg)
        {
            string[] tmp = msg.Split(new char[] { '@' });
            this.Command = tmp[0];
            this.Data = tmp[1];
            this.SenderIP = tmp[2];
            this.SenderName = tmp[3];
            this.MsgTime = tmp[4];
        }

        public string MsgPkgToString()
        {
            string result = "";
            result = this.Command + "@" + this.Data + "@" + this.SenderIP + "@" + this.SenderName + "@" + this.MsgTime;
            return result;
        }

        public static string LuoZiMsgToStirng(ChessPiece piece)
        {
            string result = piece.BoardX.ToString() + "|" + piece.BoardY.ToString() + "|" + piece.Color.ToString();
            return result;
        }

        public static ChessPiece LuoZiMsgToChessPiece(string msg)
        {
            string[] tmp = msg.Split(new char[] { '|' });
            int boardX = int.Parse(tmp[0].Trim());
            int boardY = int.Parse(tmp[1].Trim());
            ChessPieceType color = ChessPieceType.None;
            switch (msg)
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
            ChessPiece result = new ChessPiece(boardX,boardY,color);
            return result;
        }


        public static string RoomInfoToString(Room room)
        {
            string result = "";
            result = room.RoomMaster.NickName + "_" + room.RoomMaster.IP + "_" + room.RoomMember.NickName + "_" + room.RoomMember.IP;
            return result;
        }

        public static bool RoomInfoStringToPlayerBlackAndWhite(string roominfo,out Player playerBlack,out Player playerWhite)
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
