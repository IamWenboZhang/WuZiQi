using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace HJZBYSJ_Server.Model
{
    public class Player
    {
        //玩家的颜色
        public ChessPieceType Color = ChessPieceType.None;
        //玩家的IP地址
        public string IP = "";
        //玩家的昵称
        public string NickName = "";
        //玩家的端口号
        public const string Port = "4566";
        //玩家的端口号
        public Socket UserSocket;

        public enum Direction{Left,Up,Right,Bottom,ZuoShang,YouShang,ZuoXia,YouXia,LeftAndRight,UpAndDown,ZuoShangAndYouXia,YoushangAndZuoXia}
        
        public Player(ChessPieceType color)
        {
            this.Color = color;
        }

        public Player(string ip, string nickname, ChessPieceType color)
        {
            this.IP = ip;
            this.NickName = nickname;
            this.Color = color;
        }

        //胜利的棋子列表
        public List<ChessPiece> WinList = new List<ChessPiece>();
               
        //检测是否胜利
        public bool CheckWin(ChessPiece piece,Chessboard gameBoard)
        {
            bool isWin = false;
            //左右查找
            if (CheckCount(piece, gameBoard, Direction.LeftAndRight) == 5)
            {
                isWin = true;
            }
            //上下查找
            else if (CheckCount(piece, gameBoard, Direction.UpAndDown) == 5)
            {
                isWin = true;
            }
            //左上到右下
            else if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) == 5)
            {
                isWin = true;
            }            
            //右上到左下
            else if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) == 5)
            {
                isWin = true;
            }
            return isWin;
        }

        //单方向遍历
        private int CheckSingleCount(ChessPiece piece,Chessboard gameBoard,int changeX,int changeY)
        {
            int count = 0;
            int tmpX = piece.BoardX;
            int tmpY = piece.BoardY;           
            while(gameBoard.Entity[tmpX,tmpY].Color == piece.Color)
            {
                count++;
                tmpX = piece.BoardX + changeX * count;
                tmpY = piece.BoardY + changeY * count;

                //判断到棋盘边缘棋子后停止
                if (tmpX < 0 || tmpX >= Chessboard.Width - 1 || tmpY < 0 || tmpY >= Chessboard.Hight - 1)
                {
                    break;
                }
            }
            return count;
        }

        //双方向查找
        private int CheckCount(ChessPiece piece, Chessboard gameBoard, Direction direction)
        {
            int count = 0;
            int changeX1 = 0;
            int changeY1 = 0;
            int changeX2 = 0;
            int changeY2 = 0;

            switch (direction)
            {
                case Direction.LeftAndRight: changeX1 = -1; changeY1 = 0; changeX2 = 1; changeY2 = 0; break;
                case Direction.UpAndDown: changeX1 = 0; changeY1 = -1; changeX2 = 0; changeY2 = 1; break;
                case Direction.ZuoShangAndYouXia:changeX1 = -1; changeY1 = -1; changeX2 = 1; changeY2 = 1; break;
                case Direction.YoushangAndZuoXia: changeX1 = 1; changeY1 = -1; changeX2 = -1; changeY2 = 1; break;
            }

            //判断是否为边缘棋子；如果为真不进行任何操作；如果为假进行判断
            if(piece.BoardX + changeX1 < 0 || piece.BoardX + changeX1 >= Chessboard.Width -1
                || piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1 
                ||piece.BoardY + changeY1 < 0 || piece.BoardY + changeY1 >= Chessboard.Hight - 1
                || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            {

            }
            //判断是否是左右都有
            else if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color
                && gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
            {
                //如果为真则向左右两个方向遍历判断和是否等于5
                int leftCount = CheckSingleCount(piece, gameBoard, changeX1, changeY1);
                int rightCount = CheckSingleCount(piece, gameBoard, changeX2, changeY2);
                count = leftCount + rightCount - 1 ;              
            }
            else
            {
                //如果为否判断左边有没有子
                if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color)
                {
                    //如果左边有子向左遍历判断是否等于5
                    count = CheckSingleCount(piece, gameBoard, changeX1, changeY1);              
                }
                //如果左边没有子判断右边有没有子
                else if (gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                {
                    //如果右边有子向右遍历判断是否等于5
                    count = CheckSingleCount(piece, gameBoard, changeX2 , changeY2);
                }
            }
            return count;
        }
    }
}
