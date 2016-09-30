using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace HJZBYSJ.Model
{
    /// <summary>
    /// 棋子类型
    /// </summary>
    public enum ChessPieceType
    {
        None, White, Black
    }


    public class ChessPiece
    {
        public int BoardX = 0;          //棋盘上的x坐标
        public int BoardY = 0;          //棋盘上的y坐标
        public ChessPieceType Color = ChessPieceType.None;   //棋子的颜色
       
        //初始化函数
        public ChessPiece()
        {
            this.BoardX = 0;
            this.BoardY = 0;
            this.Color = ChessPieceType.None;
        }

        //重载初始化函数
        public ChessPiece(int boardX, int boardY, ChessPieceType color)
        {
            this.BoardX = boardX;
            this.BoardY = boardY;
            this.Color = color;
        }
    }
}
