using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace HJZBYSJ_Server.Model
{
    public class Chessboard
    {
        public const int Width = 15;          //棋盘宽有多少个点
        public const int Hight = 15;        //棋盘高有多少个点
        public ChessPiece[,] Entity = new ChessPiece[Width, Hight];   //棋盘实体
        public int SideLength;                //棋盘边长
        public int JianJuLength;              //棋盘间距
        public Point ZuoShangPt;              //左上角的点
        public Point YouXiaPt;                //右下角的点

        public void InitChessBoard(PictureBox picbox)
        {
            //将棋盘实体内的棋子颜色全部赋值为None
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Hight; j++)
                {
                    this.Entity[i, j] = new ChessPiece();
                }
            }
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
    }
}
