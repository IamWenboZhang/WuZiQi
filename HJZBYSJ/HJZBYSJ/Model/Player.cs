using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace HJZBYSJ.Model
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
        //最好的落子点
        public ChessPiece BestPoint = new ChessPiece();

        private enum Direction{Left,Up,Right,Bottom,ZuoShang,YouShang,ZuoXia,YouXia,LeftAndRight,UpAndDown,ZuoShangAndYouXia,YoushangAndZuoXia}

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

        //检测本方的最佳落子点以及该点的权值
        public string CheckBestPoint(Chessboard gameBoard)
        {
            string result = "";
            this.BestPoint.Color = this.Color;
            //遍历所有的颜色为空的点
            for (int i = 0; i < Chessboard.Width; i++)
            {
                for (int j = 0; j < Chessboard.Hight; j++)
                {
                    if (gameBoard.Entity[i, j].Color == ChessPieceType.None)
                    {
                        //将这个颜色为空的点赋值为本方颜色
                        gameBoard.Entity[i, j].Color = this.Color;
                        //检测该点的权值如果大于最佳点的权值的话将该点赋值为最佳点
                        string thisResult = CheckChessPieceCount(gameBoard.Entity[i, j], gameBoard);
                        string bestPointResult = CheckChessPieceCount(this.BestPoint, gameBoard);
                        int tmp = GetBigPieceCount(thisResult, bestPointResult);
                        switch (tmp)
                        {
                            case 0:
                                this.BestPoint = gameBoard.Entity[i,j];
                                result = thisResult;
                                break;
                            case 1:
                                this.BestPoint = gameBoard.Entity[i,j];
                                result = thisResult;
                                break;
                            case 2:
                                result = bestPointResult;
                                break;
                        }
                        //比较完毕后将该位置的颜色仍然还原为空
                        gameBoard.Entity[i, j].Color = ChessPieceType.None;
                    }
                }
            }
            return result;
        }

        //比较两个权值返回比较结果 1代表chesspiececount1大，2代表chesspiececount2大，0代表两个值相等
        public static int GetBigPieceCount(string chesspiececount1, string chesspiececount2)
        {
            int result = 0;
            int count1, count2, driection1, driection2;
            string[] tmp1 = chesspiececount1.Split(new char[] { '@' });
            count1 = int.Parse(tmp1[0]);
            driection1 = int.Parse(tmp1[1]);
            string[] tmp2 = chesspiececount2.Split(new char[] { '@' });
            count2 = int.Parse(tmp2[0]);
            driection2 = int.Parse(tmp2[1]);
            //以棋子数量count为重
            if (count1 > count2)
            {
                result = 1;
            }
            else if (count1 < count2)
            {
                result = 2;
            }
            //如果1,2的棋子数量相等的话比较影响方向的大小
            else if (count1 == count2)
            {
                if (driection1 > driection2)
                {
                    result = 1;
                }
                else if (driection1 == driection2)
                {
                    result = 0;
                }
                else if (driection1 < driection2)
                {
                    result = 2;
                }
            }
            return result;
        }

        //检测权值
        private string CheckChessPieceCount(ChessPiece piece,Chessboard gameBoard)
        {
            int pieceCount = 0;
            int driectionCount = 0;

            //左右查找
            if (CheckCount(piece, gameBoard, Direction.LeftAndRight) > pieceCount)
            {
                pieceCount = CheckCount(piece, gameBoard, Direction.LeftAndRight);
            }
            else if (CheckCount(piece, gameBoard, Direction.LeftAndRight) == pieceCount)
            {
                driectionCount += 1;
            }

            //上下查找
            if (CheckCount(piece, gameBoard, Direction.UpAndDown) > pieceCount)
            {
                pieceCount = CheckCount(piece, gameBoard, Direction.UpAndDown);
            }
            else if (CheckCount(piece, gameBoard, Direction.UpAndDown) == pieceCount)
            {
                driectionCount += 1;
            }

            //左上到右下
            if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) > pieceCount)
            {
                pieceCount = CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia);
            }
            else if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) == pieceCount)
            {
                driectionCount += 1;
            }

            //右上到左下
            if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) > pieceCount)
            {
                pieceCount = CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia);
            }
            else if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) == pieceCount)
            {
                driectionCount += 1;
            }
            string result = pieceCount.ToString().Trim() + "@" + driectionCount.ToString().Trim();
            return result;
        }

        //检测是否胜利
        public bool CheckWin(ChessPiece piece,Chessboard gameBoard)
        {
            bool isWin = false;
            //左右查找
            if (CheckCount(piece, gameBoard, Direction.LeftAndRight) >= 5)
            {
                isWin = true;
            }
            //上下查找
            else if (CheckCount(piece, gameBoard, Direction.UpAndDown) >= 5)
            {
                isWin = true;
            }
            //左上到右下
            else if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) >= 5)
            {
                isWin = true;
            }            
            //右上到左下
            else if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) >= 5)
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

            ////判断是否为边缘棋子；如果为真不进行任何操作；如果为假进行判断
            //if(piece.BoardX + changeX1 < 0 || piece.BoardX + changeX1 >= Chessboard.Width -1
            //    || piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1 
            //    ||piece.BoardY + changeY1 < 0 || piece.BoardY + changeY1 >= Chessboard.Hight - 1
            //    || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            //{

            //}
            if (piece.BoardX + changeX1 < 0 || piece.BoardX + changeX1 >= Chessboard.Width - 1 
                || piece.BoardY + changeY1 < 0 || piece.BoardY + changeY1 >= Chessboard.Hight - 1)
            {
                count = CheckSingleCount(piece, gameBoard, changeX2, changeY2);
            }
            else if (piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1
                || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            {
                count = CheckSingleCount(piece, gameBoard, changeX1, changeY1);
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
