using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJZBYSJ_Server.Model
{
    public class Room
    {
        public int RoomID = 0;
        public string RoomName = "";
        public Player RoomMaster = new Player(ChessPieceType.Black);
        public Player RoomMember = new Player(ChessPieceType.White);
        private int _RoomMemberNum = 0;

        public int RoomMemberNum 
        {
            get 
            {
                if (RoomMaster.NickName != "" && RoomMaster.IP != "" && RoomMaster.UserSocket != null)
                {
                    _RoomMemberNum = 1;
                }
                if(RoomMaster.NickName != "" && RoomMaster.IP != "" && RoomMaster.UserSocket != null
                    && RoomMember.NickName != "" && RoomMember.IP != "" && RoomMember.UserSocket != null)
                {
                    _RoomMemberNum = 2;
                }
                return _RoomMemberNum;
            }
            set
            {
                _RoomMemberNum = value;
            }
        }

        public Room(Player roomMaster, int roomID)
        {
            this.RoomMaster = roomMaster;
            this.RoomID = roomID;
            this.RoomName = this.RoomID.ToString().PadLeft(3,'0') +" " + roomMaster.NickName + "的房间(" + RoomMemberNum.ToString().Trim() + "/2)";
        }

        //进入房间
        public void AddMember(Player roomMember)
        {
            this.RoomMember = roomMember;
        }

        //成员退出房间
        public void RemoveMemeber()
        {
            this.RoomMember = new Player(ChessPieceType.White);
        }
    }
}
