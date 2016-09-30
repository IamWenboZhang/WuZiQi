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
        //棋盘方向
        private enum Direction{Left,Up,Right,Bottom,ZuoShang,YouShang,ZuoXia,YouXia,LeftAndRight,UpAndDown,ZuoShangAndYouXia,YoushangAndZuoXia}
        //初始化函数
        public Player(ChessPieceType color)
        {
            this.Color = color;
        }
        //初始化函数（重载）
        public Player(string ip, string nickname, ChessPieceType color)
        {
            this.IP = ip;
            this.NickName = nickname;
            this.Color = color;
        }

        //点影响值的比较函数
        //返回结果 0：相等   1：str1>str2   2:str1<str2
        public static int ComparePointEffectLevel(string effectStr1, string effectStr2)
        {
            int result = 0;
            int count1, count2, isDu1, isDu2, driection1, driection2;
            string[] tmp1 = effectStr1.Split(new char[] { '@' });
            count1 = int.Parse(tmp1[0]);
            isDu1 = int.Parse(tmp1[1]);
            driection1 = int.Parse(tmp1[2]);
            string[] tmp2 = effectStr2.Split(new char[] { '@' });
            count2 = int.Parse(tmp2[0]);
            isDu2 = int.Parse(tmp2[1]);
            driection2 = int.Parse(tmp2[2]);
            //重要性： 棋子数>是否被堵>方向数
            //当棋子数量等于5时不需要进行比较两个点的影响力直接相等
            if (count1 == 5 && count2 == 5)
            {
                result = 0;
            }          
            else 
            {
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
                    if (isDu1 > isDu2)
                    {
                        result = 1;
                    }
                    else if (isDu1 == isDu2)
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
                    else if (isDu1 < isDu2)
                    {
                        result = 2;
                    }
                }
            }
            return result;
        }
       

        //方向影响值的比较函数
        //返回结果 0：相等   1：str1>str2   2:str1<str2
        public static int CompareDirectionEffectLevel(string effectStr1, string effectStr2)
        {
            int result = 0;
            int count1, count2,isDu1,isDu2, driection1, driection2;
            string[] tmp1 = effectStr1.Split(new char[] { '@' });
            count1 = int.Parse(tmp1[0]);
            driection1 = int.Parse(tmp1[1]);
            string[] tmp2 = effectStr2.Split(new char[] { '@' });
            count2 = int.Parse(tmp2[0]);
            driection2 = int.Parse(tmp2[1]);
            //重要性： 棋子数>是否被堵>方向数
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
      

        //检测该点权值
        public string CheckPointEffectLevel(ChessPiece piece, Chessboard gameBoard)
        {
            string bestResult = "0@0";
            
            int directioncount = 0;

            //左右查找
            //左右是否被堵
            bool ZuoYouisDu = false;
            //检测左右大方向同色相连的棋子数
            int ZuoYoucount = CheckCount(piece, gameBoard, Direction.LeftAndRight,out ZuoYouisDu);
            string ZuoYouEffectLevel = "";
            //判断是否被堵
            if (ZuoYouisDu)
            {
                ZuoYouEffectLevel = ZuoYoucount.ToString() + "@" + "0";
            }
            else
            {
                ZuoYouEffectLevel = ZuoYoucount.ToString() + "@" + "1";
            }
            //将最佳值与该大方向的影响值进行比较
            int tmpzuoyouresult = CompareDirectionEffectLevel(ZuoYouEffectLevel, bestResult);
            //如果为1代表第一个参数大于第二个参数
            if (tmpzuoyouresult == 1)
            {
                bestResult = ZuoYouEffectLevel;
                directioncount = 1;
            }
            //如果为零代表两个参数影响值相等
            else if (tmpzuoyouresult == 0)
            {
                directioncount += 1;
            }



            //上下查找
            bool ShangXiaIsDu = false;
            int ShangXiacount = CheckCount(piece, gameBoard, Direction.UpAndDown,out ShangXiaIsDu);
            string ShangXiaEffectLevel = "";
            if (ShangXiaIsDu)
            {
                ShangXiaEffectLevel = ShangXiacount.ToString() + "@" + "0";
            }
            else
            {
                ShangXiaEffectLevel = ShangXiacount.ToString() + "@" + "1";
            }
            int tmpshangxiaresult = CompareDirectionEffectLevel(ShangXiaEffectLevel, bestResult);
            if (tmpshangxiaresult == 1)
            {
                bestResult = ShangXiaEffectLevel;
                directioncount = 1;
            }
            else if (tmpshangxiaresult == 0)
            {
                directioncount += 1;
            }


            //左上到右下
            bool ZuoShangYouXiaIsDu = false;
            int ZuoShangYouXiacount = CheckCount(piece, gameBoard, Direction.ZuoShangAndYouXia,out ZuoShangYouXiaIsDu);
            string ZuoShangYouXiaEffectLevel = "";
            if (ZuoShangYouXiaIsDu)
            {
                ZuoShangYouXiaEffectLevel = ZuoShangYouXiacount.ToString() + "@" + "0";
            }
            else
            {
                ZuoShangYouXiaEffectLevel = ZuoShangYouXiacount.ToString() + "@" + "1";
            }
            int tmpZuoShangYouXiaresult = CompareDirectionEffectLevel(ZuoShangYouXiaEffectLevel, bestResult);
            if (tmpZuoShangYouXiaresult == 1)
            {
                bestResult = ZuoShangYouXiaEffectLevel;
                directioncount = 1;
            }
            else if (tmpZuoShangYouXiaresult == 0)
            {
                directioncount += 1;
            }


            //右上到左下
            bool YouShangZuoXiaIsDu = false;
            int YouShangZuoXiacount = CheckCount(piece, gameBoard, Direction.YoushangAndZuoXia, out YouShangZuoXiaIsDu);
            string YouShangZuoXiaEffectLevel = "";
            if (YouShangZuoXiaIsDu)
            {
                YouShangZuoXiaEffectLevel = YouShangZuoXiacount.ToString() + "@" + "0";
            }
            else
            {
                YouShangZuoXiaEffectLevel = YouShangZuoXiacount.ToString() + "@" + "1";
            }
            int tmpYouShangZuoXiaresult = CompareDirectionEffectLevel(YouShangZuoXiaEffectLevel, bestResult);
            if (tmpYouShangZuoXiaresult == 1)
            {
                bestResult = YouShangZuoXiaEffectLevel;
                directioncount = 1;
            }
            else if (tmpYouShangZuoXiaresult == 0)
            {
                directioncount += 1;
            }
            string result = bestResult + "@" + directioncount.ToString();
            return result;
        }

        public string GetBestPointEffectLevel(Chessboard gamBoard)
        {
            int maxX = 0, maxY = 0;
            string maxEffectLevel = "0@0@0";
            //遍历棋盘
            for (int i = 0; i < Chessboard.Hight; i++)
            {
                for (int j = 0; j < Chessboard.Width; j++)
                {
                    //当棋子为空时
                    if (gamBoard.Entity[i, j].Color == ChessPieceType.None)
                    {
                        //得出该棋子的权值
                        gamBoard.Entity[i, j].Color = this.Color;
                        string pointEffectLevel = CheckPointEffectLevel(gamBoard.Entity[i, j], gamBoard);
                        gamBoard.Entity[i, j].Color = ChessPieceType.None;
                        //跟最大权值比较
                        int cpvalue = ComparePointEffectLevel(pointEffectLevel, maxEffectLevel);
                        //如果该棋子的权值比最大权值大，最大权值等于该子的权值，记录下该子的索引
                        if (cpvalue == 1)
                        {
                            maxEffectLevel = pointEffectLevel;
                            maxX = i;
                            maxY = j;
                        }
                    }
                   
                }
            }

            this.BestPoint = new ChessPiece(maxX, maxY, this.Color);
            return maxEffectLevel;
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

        //单方向遍历并可以引出是否被堵
        private int CheckSingleCount(ChessPiece piece, Chessboard gameBoard, Direction direction,out bool isDu)
        {
            int changeX = 0, changeY = 0;
            isDu = false;
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
            while (gameBoard.Entity[tmpX, tmpY].Color == piece.Color)
            {
                count++;
                tmpX = piece.BoardX + changeX * count;
                tmpY = piece.BoardY + changeY * count;
                //如果传入的棋子本身就是棋盘边缘piece.BoardY = 14且方向为左右 则不判断
                if ((piece.BoardY == Chessboard.Hight - 1 && (direction != Direction.Bottom && direction != Direction.YouXia && direction != Direction.ZuoXia)))
                {
                    if (tmpX < 0 || tmpX > Chessboard.Width - 1)
                    {
                        break;
                    }
                }
                else if ((piece.BoardX == Chessboard.Width - 1 && (direction != Direction.Right && direction != Direction.YouXia && direction != Direction.YouShang)))
                {
                    if (tmpY < 0 || tmpY > Chessboard.Hight - 1)
                    {
                        break;
                    }
                }
                //判断到棋盘边缘棋子后停止
                else if (tmpX < 0 || tmpX >= Chessboard.Width - 1 || tmpY < 0 || tmpY >= Chessboard.Hight - 1)
                {
                    //如果走到棋盘边缘则也算被堵
                    isDu = true;
                    break;
                }
                //如果颜色不为空同时颜色也不为本玩家的颜色则说明该方向被堵了
                else if (gameBoard.Entity[tmpX, tmpY].Color != ChessPieceType.None && gameBoard.Entity[tmpX, tmpY].Color != this.Color)
                {
                    isDu = true;
                }
            }
            return count;
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
            //循环 条件：当该位置棋子的颜色和传入进来的棋子颜色相同为真
            //每次循环增加一次相同颜色棋子数 并改变tmpX，tmpY
            while(gameBoard.Entity[tmpX,tmpY].Color == piece.Color)
            {
                count++;
                tmpX = piece.BoardX + changeX * count;
                tmpY = piece.BoardY + changeY * count;

                //如果传入的棋子本身就是棋盘边缘piece.BoardY = 14且方向为左右 则不判断
                if ((piece.BoardY == Chessboard.Hight - 1 && (direction != Direction.Bottom && direction != Direction.YouXia && direction != Direction.ZuoXia)))
                {
                    if (tmpX < 0 || tmpX > Chessboard.Width - 1)
                    {
                        break;
                    }
                }
                else if ((piece.BoardX == Chessboard.Width - 1 && (direction != Direction.Right && direction != Direction.YouXia && direction != Direction.YouShang)))
                {
                    if (tmpY < 0 || tmpY > Chessboard.Hight - 1)
                    {
                        break;
                    }
                }
                //判断到棋盘边缘棋子后停止
                else if (tmpX < 0 || tmpX > Chessboard.Width - 1 || tmpY < 0 || tmpY > Chessboard.Hight - 1)
                {
                    break;
                }
            }
            return count;
        }

        //双方向查找
        private int CheckCount(ChessPiece piece, Chessboard gameBoard, Direction direction,out bool isDu)
        {
            isDu = false;
            int count = 0;
            int changeX1 = 0;
            int changeY1 = 0;
            int changeX2 = 0;
            int changeY2 = 0;
            Direction direction1 = Direction.Left;//左，上，左上，右上
            Direction direction2 = Direction.Right;//右，下，右下，左下          
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
                count = CheckSingleCount(piece, gameBoard, direction2,out isDu);
            }
            else if (piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1
                || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            {
                count = CheckSingleCount(piece, gameBoard, direction1,out isDu);
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
                    bool leftisDu = false;
                    int leftCount = CheckSingleCount(piece, gameBoard, direction1,out leftisDu);
                    bool rightisDu = false;
                    int rightCount = CheckSingleCount(piece, gameBoard, direction2,out rightisDu);
                    count = leftCount + rightCount - 1;
                    if (leftisDu || rightisDu)
                    {
                        isDu = true;
                    }
                }
                else
                {
                    //如果为否判断左边有没有子
                    if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color)
                    {
                        //如果左边有子向左遍历判断是否等于5
                        count = CheckSingleCount(piece, gameBoard, direction1,out isDu);
                    }
                    //如果左边没有子判断右边有没有子
                    else if (gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                    {
                        //如果右边有子向右遍历判断是否等于5
                        count = CheckSingleCount(piece, gameBoard, direction2, out isDu);
                    }
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
            Direction direction1 = Direction.Left;//direction1代表左，上，左上，右上四个方向  后边赋的Left只是一个初始化值无实际意义
            Direction direction2 = Direction.Right;//direction2代表右，下，右下，左下四个方向 后边赋的Right只是一个初始化值无实际意义
          
          //判断传进来的方向（四个大方向中的一个）
            switch (direction)
            {
                case Direction.LeftAndRight: direction1 = Direction.Left; direction2 = Direction.Right; break;
                case Direction.UpAndDown: direction1 = Direction.Up; direction2 = Direction.Bottom; break;
                case Direction.ZuoShangAndYouXia: direction1 = Direction.ZuoShang; direction2 = Direction.YouXia; break;
                case Direction.YoushangAndZuoXia: direction1 = Direction.YouShang; direction2 = Direction.ZuoXia; break;
            }
                
            //if (piece.BoardX + changeX1 < 0 || piece.BoardX + changeX1 >= Chessboard.Width - 1 
            //    || piece.BoardY + changeY1 < 0 || piece.BoardY + changeY1 >= Chessboard.Hight - 1)
            //{
            //    count = CheckSingleCount(piece, gameBoard, direction2);
            //}
            //else if (piece.BoardX + changeX2 < 0 || piece.BoardX + changeX2 >= Chessboard.Width - 1
            //    || piece.BoardY + changeY2 < 0 || piece.BoardY + changeY2 >= Chessboard.Hight - 1)
            //{
            //    count = CheckSingleCount(piece, gameBoard, direction1);
            //}
            ////判断是否是左右都有
            //else if (piece.BoardX + changeX2 >= 0 && piece.BoardX + changeX2 <= Chessboard.Width - 1
            //    && piece.BoardY + changeY2 >= 0 && piece.BoardY + changeY2 <= Chessboard.Hight - 1 && piece.BoardX + changeX1 >= 0 && piece.BoardX + changeX1 <= Chessboard.Width - 1
            //    && piece.BoardY + changeY1 >= 0 && piece.BoardY + changeY1 <= Chessboard.Hight - 1)
            //{ 
                //
                if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color
                && gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                {
                    //如果为真则向左右两个方向遍历判断和是否等于5
                    int direction1Count = CheckSingleCount(piece, gameBoard, direction1);
                    int direction2Count = CheckSingleCount(piece, gameBoard, direction2);
                    count = direction1Count + direction2Count - 1 ;
                }
                else
                {
                    //如果为否判断方向1有没有子
                    if (gameBoard.Entity[piece.BoardX + changeX1, piece.BoardY + changeY1].Color == piece.Color)
                    {
                        //如果左边有子向方向1遍历
                        count = CheckSingleCount(piece, gameBoard, direction1);
                    }
                    //如果左边没有子判断方向2有没有子
                    else if (gameBoard.Entity[piece.BoardX + changeX2, piece.BoardY + changeY2].Color == piece.Color)
                    {
                        //如果右边有子向方向2遍历
                        count = CheckSingleCount(piece, gameBoard, direction2);
                    }
                }
            //}
            return count;
        }
    }
}
