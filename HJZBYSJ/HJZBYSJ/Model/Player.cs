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

        ////检测本方的最佳落子点以及该点的权值
        //public string CheckBestPoint(Chessboard gameBoard)
        //{
        //    string result = "";
        //    this.BestPoint.Color = this.Color;
        //    //遍历所有的颜色为空的点
        //    for (int i = 0; i < Chessboard.Width; i++)
        //    {
        //        for (int j = 0; j < Chessboard.Hight; j++)
        //        {
        //            if (gameBoard.Entity[i, j].Color == ChessPieceType.None)
        //            {
        //                //将这个颜色为空的点赋值为本方颜色
        //                ChessPiece tmpChessPiece = new ChessPiece(i, j, this.Color);
        //                //检测该点的权值如果大于最佳点的权值的话将该点赋值为最佳点
        //                string thisResult = CheckChessPieceCount(tmpChessPiece, gameBoard);
        //                string bestPointResult = CheckChessPieceCount(this.BestPoint, gameBoard);
        //                int tmp = GetBigPieceCount(thisResult, bestPointResult);
        //                switch (tmp)
        //                {
        //                    case 0:
        //                        this.BestPoint = tmpChessPiece;
        //                        result = thisResult;
        //                        break;
        //                    case 1:
        //                        this.BestPoint = tmpChessPiece;
        //                        result = thisResult;
        //                        break;
        //                    case 2:
        //                        result = bestPointResult;
        //                        break;
        //                }
        //                ////比较完毕后将该位置的颜色仍然还原为空
        //                //gameBoard.Entity[i, j].Color = ChessPieceType.None;
        //            }
        //        }
        //    }
        //    return result;
        //}

        ////比较两个权值返回比较结果 1代表chesspiececount1大，2代表chesspiececount2大，0代表两个值相等
        //public static int GetBigPieceCount(string chesspiececount1, string chesspiececount2)
        //{
        //    int result = 0;
        //    int count1, count2, driection1, driection2;
        //    string[] tmp1 = chesspiececount1.Split(new char[] { '@' });
        //    count1 = int.Parse(tmp1[0]);
        //    driection1 = int.Parse(tmp1[1]);
        //    string[] tmp2 = chesspiececount2.Split(new char[] { '@' });
        //    count2 = int.Parse(tmp2[0]);
        //    driection2 = int.Parse(tmp2[1]);
        //    //以棋子数量count为重
        //    if (count1 > count2)
        //    {
        //        result = 1;
        //    }
        //    else if (count1 < count2)
        //    {
        //        result = 2;
        //    }
        //    //如果1,2的棋子数量相等的话比较影响方向的大小
        //    else if (count1 == count2)
        //    {
        //        if (driection1 > driection2)
        //        {
        //            result = 1;
        //        }
        //        else if (driection1 == driection2)
        //        {
        //            result = 0;
        //        }
        //        else if (driection1 < driection2)
        //        {
        //            result = 2;
        //        }
        //    }
        //    return result;
        //}

        ////检测权值
        //private string CheckChessPieceCount(ChessPiece piece, Chessboard gameBoard)
        //{
        //    int pieceCount = 0;
        //    int driectionCount = 0;

        //    //左右查找
        //    if (CheckCount(piece, gameBoard, Direction.LeftAndRight) > pieceCount)
        //    {
        //        pieceCount = CheckCount(piece, gameBoard, Direction.LeftAndRight);
        //    }
        //    else if (CheckCount(piece, gameBoard, Direction.LeftAndRight) == pieceCount)
        //    {
        //        driectionCount += 1;
        //    }

        //    //上下查找
        //    if (CheckCount(piece, gameBoard, Direction.UpAndDown) > pieceCount)
        //    {
        //        pieceCount = CheckCount(piece, gameBoard, Direction.UpAndDown);
        //    }
        //    else if (CheckCount(piece, gameBoard, Direction.UpAndDown) == pieceCount)
        //    {
        //        driectionCount += 1;
        //    }

        //    //左上到右下
        //    if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) > pieceCount)
        //    {
        //        pieceCount = CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia);
        //    }
        //    else if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) == pieceCount)
        //    {
        //        driectionCount += 1;
        //    }

        //    //右上到左下
        //    if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) > pieceCount)
        //    {
        //        pieceCount = CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia);
        //    }
        //    else if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) == pieceCount)
        //    {
        //        driectionCount += 1;
        //    }
        //    string result = pieceCount.ToString().Trim() + "@" + driectionCount.ToString().Trim();
        //    return result;
        //}

        //求出最佳点并返回该点的权值
        public string GetBestPointEffectLevel(Chessboard gameBoard)
        {
            string result = "";
            //遍历所有颜色为空的位置
            for (int i = 0; i < Chessboard.Hight; i++)
            {
                for (int j = 0; j < Chessboard.Width; j++)
                {
                    if (gameBoard.Entity[i, j].Color == ChessPieceType.None)
                    {
                        //ChessPiece thisPointPiece = new ChessPiece();
                        //thisPointPiece.BoardX = gameBoard.Entity[i,j].BoardX;
                        //thisPointPiece.BoardY = gameBoard.Entity[i, j].BoardY;
                        //thisPointPiece.Color = gameBoard.Entity[i, j].Color;
                        //求出该位置的影响值
                        string thisPointEffectLevel = CheckEffectiveLevel(gameBoard.Entity[i,j], gameBoard);
                        //求出最佳点的影响值
                        string bestPointEffectLevel = CheckEffectiveLevel(gameBoard.Entity[this.BestPoint.BoardX,this.BestPoint.BoardY], gameBoard);
                        //比较两个影响值
                        int compareResult = CompareEffectLevel(thisPointEffectLevel, bestPointEffectLevel);
                      
                        switch (compareResult)
                        {
                            //如果两个点影响值相等则将该点的值赋值给最佳点
                            case 0:
                                this.BestPoint = gameBoard.Entity[i, j];
                                result = thisPointEffectLevel;
                                break;
                            //如果该点影响值大将该点的值赋值给最佳点返回该点的影响值
                            case 1:
                                this.BestPoint = gameBoard.Entity[i, j]; 
                                result = thisPointEffectLevel;
                                break;
                            //如果该点影响值小则不变
                            case 2:
                                this.BestPoint = gameBoard.Entity[this.BestPoint.BoardX, this.BestPoint.BoardY];
                                result = bestPointEffectLevel;
                                break;
                        }
                    }
                }
            }
           
            return result;
        }

        //影响值的比较函数
        public static int CompareEffectLevel(string effectStr1, string effectStr2)
        {
            int result = 0;
            int count1, count2, driection1, driection2;
            string[] tmp1 = effectStr1.Split(new char[] { '@' });
            count1 = int.Parse(tmp1[0]);
            driection1 = int.Parse(tmp1[1]);
            string[] tmp2 = effectStr2.Split(new char[] { '@' });
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

        //比较值的大小的函数
        private void CompareValue(int value,ref int finalCount , ref int directionCount)
        {
            if (value > finalCount)
            {
                finalCount = value;
                directionCount = 1;
            }
            else if (value == finalCount)
            {
                directionCount = directionCount + 1;
            }
        }
        //检查该点的权值
        public string CheckEffectiveLevel(ChessPiece piece, Chessboard gameBoard)
        {
            int finalCount = 0;//连在一起的棋子数
            int directionCount = 0;//影响的方向数
            piece.Color = this.Color;

            //当棋子在最左上角时向右方，下方以及右下方三个方向遍历查找
            if (piece.BoardX - 1 < 0 && piece.BoardY - 1 < 0)
            {
                int RightCount = CheckSingleCount(piece, gameBoard,Direction.Right);
                CompareValue(RightCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
                int YouXiaCount = CheckSingleCount(piece, gameBoard, Direction.YouXia);
                CompareValue(YouXiaCount, ref finalCount, ref directionCount);

            }
            //当棋子在右上角时向左，左下，下三个方向查找
            else if (piece.BoardX + 1 > Chessboard.Width - 1 && piece.BoardY - 1 < 0)
            {
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int ZuoXiaCount = CheckSingleCount(piece, gameBoard, Direction.ZuoXia);
                CompareValue(ZuoXiaCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
            }
            //当棋子在左下角时向右，右上，上三个方向查找
            else if (piece.BoardX - 1 < 0 && piece.BoardY + 1 > Chessboard.Hight-1)
            {
                int YouCount = CheckSingleCount(piece, gameBoard, Direction.Right);
                CompareValue(YouCount, ref finalCount, ref directionCount);
                int YouShangCount = CheckSingleCount(piece, gameBoard, Direction.YouShang);
                CompareValue(YouShangCount, ref finalCount, ref directionCount);
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
            }
            //当棋子在右下角时向左，左上，上三个方向查找
            else if (piece.BoardX + 1 > Chessboard.Width - 1 && piece.BoardY + 1 > Chessboard.Hight - 1)
            {
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int ZuoShangCount = CheckSingleCount(piece, gameBoard, Direction.ZuoShang);
                CompareValue(ZuoShangCount, ref finalCount, ref directionCount);
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
            }
            //当棋子在正上方时向左，右，下，左下，右下五个方向查找
            else if (piece.BoardY - 1 < 0 && piece.BoardX - 1 >= 0 && piece.BoardX + 1 <= Chessboard.Width - 1)
            {
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int YouCount = CheckSingleCount(piece, gameBoard, Direction.Right);
                CompareValue(YouCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
                int ZuoXiaCount = CheckSingleCount(piece, gameBoard, Direction.ZuoXia);
                CompareValue(ZuoXiaCount, ref finalCount, ref directionCount);
                int YouXiaCount = CheckSingleCount(piece, gameBoard,Direction.YouXia);
                CompareValue(YouXiaCount, ref finalCount, ref directionCount);
            }
            //当棋子在正左边的时候向上，下，右，右上，右下五个方向查找
            else if (piece.BoardX - 1 < 0 && piece.BoardY + 1 <= Chessboard.Hight - 1 && piece.BoardY - 1 >= 0)
            {
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
                int YouCount = CheckSingleCount(piece, gameBoard, Direction.Right);
                CompareValue(YouCount, ref finalCount, ref directionCount);
                int YouShangCount = CheckSingleCount(piece, gameBoard, Direction.YouShang);
                CompareValue(YouShangCount, ref finalCount, ref directionCount);
                int YouXiaCount = CheckSingleCount(piece, gameBoard, Direction.YouXia);
                CompareValue(YouXiaCount, ref finalCount, ref directionCount);
            }
            //当棋子在正右边的时候向上，下，左，左上，左下五个方向查找
            else if (piece.BoardX + 1 > Chessboard.Width - 1 && piece.BoardY - 1 >= 0 && piece.BoardY + 1 <= Chessboard.Hight - 1)
            {
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int ZuoShangCount = CheckSingleCount(piece, gameBoard, Direction.ZuoShang);
                CompareValue(ZuoShangCount, ref finalCount, ref directionCount);
                int ZuoXiaCount = CheckSingleCount(piece, gameBoard, Direction.ZuoXia);
                CompareValue(ZuoXiaCount, ref finalCount, ref directionCount);
            }
            //当棋子在正下边的时候向左，右，上，左上，右上五个方向查找
            else if (piece.BoardY + 1 > Chessboard.Width - 1 && piece.BoardX - 1 >= 0 && piece.BoardX + 1 <= Chessboard.Width - 1)
            {
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int YouCount = CheckSingleCount(piece, gameBoard, Direction.Right);
                CompareValue(YouCount, ref finalCount, ref directionCount);
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
                int ZuoShangCount = CheckSingleCount(piece, gameBoard, Direction.ZuoShang);
                CompareValue(ZuoShangCount, ref finalCount, ref directionCount);
                int YouShangCount = CheckSingleCount(piece, gameBoard, Direction.YouShang);
                CompareValue(YouShangCount, ref finalCount, ref directionCount);
            }
            //当棋子在中间时向上下左右左上右下右上左下八个方向查找
            else if (piece.BoardY - 1 >= 0 && piece.BoardY + 1 <= Chessboard.Hight - 1 && piece.BoardX - 1 >= 0 && piece.BoardX + 1 <= Chessboard.Width - 1)
            {
                int ZuoCount = CheckSingleCount(piece, gameBoard, Direction.Left);
                CompareValue(ZuoCount, ref finalCount, ref directionCount);
                int YouCount = CheckSingleCount(piece, gameBoard, Direction.Right);
                CompareValue(YouCount, ref finalCount, ref directionCount);
                int ShangCount = CheckSingleCount(piece, gameBoard, Direction.Up);
                CompareValue(ShangCount, ref finalCount, ref directionCount);
                int XiaCount = CheckSingleCount(piece, gameBoard, Direction.Bottom);
                CompareValue(XiaCount, ref finalCount, ref directionCount);
                int ZuoShangCount = CheckSingleCount(piece, gameBoard, Direction.ZuoShang);
                CompareValue(ZuoShangCount, ref finalCount, ref directionCount);
                int ZuoXiaCount = CheckSingleCount(piece, gameBoard, Direction.ZuoXia);
                CompareValue(ZuoXiaCount, ref finalCount, ref directionCount);
                int YouShangCount = CheckSingleCount(piece, gameBoard, Direction.YouShang);
                CompareValue(YouShangCount, ref finalCount, ref directionCount);
                int YouXiaCount = CheckSingleCount(piece, gameBoard, Direction.YouXia);
                CompareValue(YouXiaCount, ref finalCount, ref directionCount);
            }

            piece.Color = ChessPieceType.None;
            string result = finalCount.ToString() + "@" + directionCount.ToString();
            return result;
        }


        //检测是否胜利
        public bool CheckWin(ChessPiece piece,Chessboard gameBoard)
        {
            bool isWin = false;
            int count = 0;
            ////左右查找
            //if (CheckCount(piece, gameBoard, Direction.LeftAndRight) >= 5)
            //{
            //    isWin = true;
            //}
            ////上下查找
            //else if (CheckCount(piece, gameBoard, Direction.UpAndDown) >= 5)
            //{
            //    isWin = true;
            //}
            ////左上到右下
            //else if (CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia) >= 5)
            //{
            //    isWin = true;
            //}            
            ////右上到左下
            //else if (CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia) >= 5)
            //{
            //    isWin = true;
            //}


            //左右查找
            count = CheckCount(piece, gameBoard, Direction.LeftAndRight);
            if (count >= 5)
            {
                isWin = true;
            }
            //上下查找
            else
            {
                count = CheckCount(piece, gameBoard, Direction.UpAndDown);
                if (count >= 5)
                {
                    isWin = true;
                }
                //左上到右下
                else
                {
                    count = CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia);
                    if (count >= 5)
                    {
                        isWin = true;
                    }
                    //右上到左下
                    else
                    {
                        count = CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia);
                        if (count >= 5)
                        {
                            isWin = true;
                        }
                    }
                }
            }
            return isWin;
        }

        //单方向遍历
        private int CheckSingleCount(ChessPiece piece,Chessboard gameBoard,Direction direction)
        {
            int changeX = 0, changeY = 0;
            switch (direction)
            {
                case Direction.Up: changeX = 0; changeY = -1; break;
                case Direction.Bottom: changeX = 0; changeY = 1; break;
                case Direction.Left: changeX = -1; changeY = 0; break;
                case Direction.Right: changeX = 1; changeY = 0; break;
                case Direction.YouShang: changeX = 1; changeY = -1; break;
                case Direction.YouXia: changeX = 1; changeY = 1; break;
                case Direction.ZuoShang: changeX = -1; changeY = -1; break;
                case Direction.ZuoXia: changeX = -1; changeY = 1; break;
            }
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
            Direction direction1 = Direction.Left;//左，上，左上，右上
            Direction direction2 = Direction.Right;//右，下，右下，左下
            //switch (direction)
            //{                
            //    case Direction.LeftAndRight: changeX1 = -1; changeY1 = 0; changeX2 = 1; changeY2 = 0; break;
            //    case Direction.UpAndDown: changeX1 = 0; changeY1 = -1; changeX2 = 0; changeY2 = 1; break;
            //    case Direction.ZuoShangAndYouXia: changeX1 = -1; changeY1 = -1; changeX2 = 1; changeY2 = 1; break;
            //    case Direction.YoushangAndZuoXia: changeX1 = 1; changeY1 = -1; changeX2 = -1; changeY2 = 1; break;
            //}
            switch (direction)
            {
                case Direction.LeftAndRight: direction1 = Direction.Left; direction2 = Direction.Right; break;
                case Direction.UpAndDown: direction1 = Direction.Up; direction2 = Direction.Bottom; break;
                case Direction.ZuoShangAndYouXia: direction1 = Direction.ZuoShang; direction2 = Direction.YouXia; break;
                case Direction.YoushangAndZuoXia: direction1 = Direction.YouShang; direction2 = Direction.ZuoXia; break;
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
                count = CheckSingleCount(piece, gameBoard, direction2);
            }
            else if (piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1
                || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            {
                count = CheckSingleCount(piece, gameBoard, direction1);
            }
            //判断是否是左右都有
            else if (piece.BoardX + changeX2 >= 0 && piece.BoardX + changeX2 <= Chessboard.Width - 1
                && piece.BoardY + changeY2 >= 0 && piece.BoardY + changeY2 <= Chessboard.Hight - 1 && piece.BoardX + changeX1 >= 0 && piece.BoardX + changeX1 <= Chessboard.Width - 1
                && piece.BoardY + changeY1 >= 0 && piece.BoardY + changeY1 <= Chessboard.Hight - 1)
            { 
                if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color
                && gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                {
                    //如果为真则向左右两个方向遍历判断和是否等于5
                    int leftCount = CheckSingleCount(piece, gameBoard, direction1);
                    int rightCount = CheckSingleCount(piece, gameBoard, direction2);
                    count = leftCount + rightCount - 1 ;
                }
                else
                {
                    //如果为否判断左边有没有子
                    if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color)
                    {
                        //如果左边有子向左遍历判断是否等于5
                        count = CheckSingleCount(piece, gameBoard, direction1);
                    }
                    //如果左边没有子判断右边有没有子
                    else if (gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                    {
                        //如果右边有子向右遍历判断是否等于5
                        count = CheckSingleCount(piece, gameBoard, direction2);
                    }
                }
            }
            return count;
        }
    }
}
