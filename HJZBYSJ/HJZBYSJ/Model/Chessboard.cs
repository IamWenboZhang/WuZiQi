using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using HJZBYSJ.DataBase;

namespace HJZBYSJ.Model
{
    //棋盘类
    public class Chessboard
    {
        public const int Width = 15;          //棋盘宽有多少个点
        public const int Hight = 15;        //棋盘高有多少个点
        public ChessPiece[,] Entity = new ChessPiece[Width,Hight];   //棋盘实体
        public int SideLength;                //棋盘边长
        public int JianJuLength;              //棋盘间距
        public Point ZuoShangPt;              //左上角的点
        public Point YouXiaPt;                //右下角的点

        //初始化函数
        public Chessboard()
        {
            InitEnitity();
        }

        //初始化棋盘实体
        public void InitEnitity()
        {
            //将棋盘实体内的棋子颜色全部赋值为None
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    this.Entity[i, j] = new ChessPiece(i, j, ChessPieceType.None);
                }
            }
        }

        //设置棋盘大小
        public void SetSize(PictureBox picbox)
        {
            //判断控件是高大于宽还是宽大于高，取短边的十分之九来作为棋盘的边长    
            if (picbox.Width > picbox.Height)
            {
                this.SideLength = picbox.Height - 30;
            }
            else
            {
                this.SideLength = picbox.Width - 30;
            }

            //得出起始点的位置
            int starLocationX = (picbox.Width - this.SideLength) / 2;
            int starLocationY = (picbox.Height - this.SideLength) / 2;
            int endLocationX = picbox.Width - starLocationX;
            int endLocationY = picbox.Height - starLocationY;
            this.ZuoShangPt = new Point(starLocationX, starLocationY);
            this.YouXiaPt = new Point(endLocationX, endLocationY);
            //计算出每一格之间的距离
            this.JianJuLength = this.SideLength / (Chessboard.Width - 1);
        }

        //public static string NoneEnityXMLStr()
        //{
        //    ChessPiece[,] NoneEntity = new ChessPiece[Width,Hight];
        //    //将棋盘实体内的棋子颜色全部赋值为None
        //    for (int i = 0; i < Width; i++)
        //    {
        //        for (int j = 0; j < Hight; j++)
        //        {
        //            NoneEntity[i, j] = new ChessPiece();
        //        }
        //    }
        //    string[][] tmp = GameBoardEnityToStringArray(NoneEntity);
        //    string result = GameUtil.ErWeiArrayToXMLStr(tmp);
        //    return result;
        //}

        //将棋子对象二维数组转化为字符串二维数组
        public string[][] GameBoardEnityToStringArray(ChessPiece[,] Entity)
        {
            string[][] result = new string[Hight][];
            for (int i = 0 ; i < Hight; i++)
            {
                result[i] = new string[Width];
            }
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result[i][j] = Entity[i, j].BoardX.ToString() + "@" 
                        + Entity[i, j].BoardY.ToString() + "@" + Entity[i, j].Color;
                    //转化结果："0@0@Black"
                }
            }
            return result;
        }

        //字符串二维数组反序列化为棋子对象二维数组
        public ChessPiece[,] StringArrayToGameBoardEnity(string[][] str)
        {
            ChessPiece[,] result = new ChessPiece[Width, Hight];
            //将棋盘实体内的棋子颜色全部赋值为None
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    result[i, j] = new ChessPiece();
                }
            }
            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    string[] tmp = new string[3];
                    tmp = str[i][j].Split(new char[]{ '@' });
                    result[i, j].BoardX = int.Parse(tmp[0]);
                    result[i, j].BoardY = int.Parse(tmp[1]);
                    switch (tmp[2])
                    {
                        case "White":
                            result[i, j].Color = ChessPieceType.White;
                            break;
                        case "Black":
                            result[i, j].Color = ChessPieceType.Black;
                            break;
                        case "None":
                            result[i, j].Color = ChessPieceType.None;
                            break;
                    }
                }
            }
            return result;
        }
    }
}
