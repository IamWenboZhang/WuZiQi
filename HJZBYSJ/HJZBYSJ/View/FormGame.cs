using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HJZBYSJ.Model;

namespace HJZBYSJ.View
{
    public partial class FormGame : Form
    {

        //默认为单人游戏
        public bool GameTypeIsDouble = false;

        //画布
        Graphics g = null;
        Bitmap bmap = null;

        //棋盘对象
        Chessboard gameBoard = new Chessboard();

        //玩家对象
        Player playerWhite = new Player(ChessPieceType.White);
        Player playerBlack = new Player(ChessPieceType.Black);

        private ChessPieceType CurrentColor;

        //本回合落子方的颜色 
        private ChessPieceType CurrentPlayerColor
        {
            get
            {
                return CurrentColor;
            }
            set
            {
                if (value == ChessPieceType.White)
                {
                    this.CurrentPlayer = this.playerWhite;
                    this.CurrentColor = ChessPieceType.White;
                    this.labelCurrentColor.Text = "白色";
                }
                else if (value == ChessPieceType.Black)
                {
                    this.CurrentPlayer = this.playerBlack;
                    this.CurrentColor = ChessPieceType.Black;
                    this.labelCurrentColor.Text = "黑色";
                }
            }
        }
        Player CurrentPlayer;

        private int _Step = 0;

        //本次对局的回合数
        private int Step
        {
            get
            {
                return _Step;
            }
            set
            {
                this._Step = value;
                this.labelStep.Text = value.ToString();
            }
        }

        //本局是否处于胜利状态
        bool IsWin = false;

        public FormGame()
        {
            InitializeComponent();
        }


        private void FormGame_Load(object sender, EventArgs e)
        {
            if(this.GameTypeIsDouble)
            {

            }
            else
            {
                gameBoard.InitChessBoard(this.pictureBoxGameSence);
                this.CurrentPlayerColor = ChessPieceType.Black;
                this.Step = 1;
                this.IsWin = false;
                DrawChessBoard(out g, out bmap);
            }
        }
 
        private void DrawChessBoard(out Graphics gph, out Bitmap bmap)
        {   
            //创建画布
            bmap = new Bitmap(this.pictureBoxGameSence.Size.Width, this.pictureBoxGameSence.Size.Height);//图片大小
            gph = Graphics.FromImage(bmap);
            gph.Clear(Color.Orange);
            //循环画出棋盘位置
            int i ,j;
            //竖着的15道线（Y不变X每次加间距*i）
            for (i = 0; i < Chessboard.Width; i++)
            {
                Point pt1 = new Point(gameBoard.ZuoShangPt.X + gameBoard.JianJuLength * i, gameBoard.ZuoShangPt.Y);
                Point pt2 = new Point(gameBoard.ZuoShangPt.X + gameBoard.JianJuLength * i, gameBoard.YouXiaPt.Y);
                Pen p = new Pen (Color.Black,1);
                g.DrawLine(p,pt1,pt2);
            }
            //横着的十五道线（X不变Y每次加间距*i）
            for (j = 0; j < Chessboard.Hight; j++)
            {
                Point pt1 = new Point(gameBoard.ZuoShangPt.X, gameBoard.ZuoShangPt.Y + gameBoard.JianJuLength * j);
                Point pt2 = new Point(gameBoard.YouXiaPt.X, gameBoard.ZuoShangPt.Y + gameBoard.JianJuLength * j);
                Pen p = new Pen(Color.Black, 1);
                g.DrawLine(p, pt1, pt2);
            }
            this.pictureBoxGameSence.Image = bmap;
        }


        private void DrawPiece(ChessPiece piece)
        {
            Image image = this.pictureBoxGameSence.Image;
            g = Graphics.FromImage(image);
            //根据在棋盘上的位置X，Y来计算出该点的详细坐标
            //公式：ZuoShangPt.X + JianJuLength * boardX    ZuoShangPt.Y + JianJuLength * boardY
            int pieceLocationX = gameBoard.ZuoShangPt.X + gameBoard.JianJuLength * piece.BoardX;
            int pieceLocationY = gameBoard.ZuoShangPt.Y + gameBoard.JianJuLength * piece.BoardY;
            //得到棋子的中心点坐标
            Point pieceCenterPt = new Point(pieceLocationX, pieceLocationY);
            
            //棋子的左上角X坐标为
            int zsPtLocationX = pieceCenterPt.X - gameBoard.JianJuLength / 2;
            //棋子的左上角Y坐标为
            int zsPtLocationY = pieceCenterPt.Y - gameBoard.JianJuLength / 2;
            //得到棋子左上角坐标
            Point zsPt = new Point(zsPtLocationX,zsPtLocationY);

            //画出棋子
            SolidBrush sb = new SolidBrush (this.pictureBoxGameSence.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (piece.Color == ChessPieceType.White)
            {
                sb = new SolidBrush(Color.White);
            }
            else if (piece.Color == ChessPieceType.Black)
            {
                sb = new SolidBrush(Color.Black);
            }
            g.FillEllipse(sb, zsPt.X, zsPt.Y, gameBoard.JianJuLength, gameBoard.JianJuLength);
            this.pictureBoxGameSence.Image = image;
        }

        private void pictureBoxGameSence_Click(object sender, EventArgs e)
        {
            //判断是否是联网对战模式

            //判断是否是胜利状态
            if (this.IsWin)
            {
                MessageBox.Show("已经有人胜利！");
            }
            else
            {
                //判断点击的位置是否可以落子
                MouseEventArgs g = e as MouseEventArgs;
                int x = g.X;
                int y = g.Y;
                int boardX, boardY;
                if (LocationXYConvertToChessBoardXY(x, y, out boardX, out boardY))
                {
                    //判断该位置是否有子               
                    //如果没有子则绘制棋子
                    if (gameBoard.Entity[boardX, boardY].Color == ChessPieceType.None)
                    {
                        ChessPiece tmpPiece = new ChessPiece(boardX, boardY, this.CurrentPlayer.Color);
                        this.DrawPiece(tmpPiece);
                        gameBoard.Entity[boardX, boardY] = tmpPiece;
                        //判断是否胜利
                        if (this.CurrentPlayer.CheckWin(tmpPiece, gameBoard))
                        {
                            if (this.CurrentPlayerColor == ChessPieceType.White)
                            {
                                MessageBox.Show("白方胜利");
                                this.IsWin = true;
                            }
                            if (this.CurrentPlayerColor == ChessPieceType.Black)
                            {
                                MessageBox.Show("黑方胜利");
                                this.IsWin = true;
                            }
                        }
                        else
                        {
                            this.Step = this.Step + 1;
                            if (this.CurrentPlayerColor == ChessPieceType.White)
                            {
                                this.CurrentPlayerColor = ChessPieceType.Black;
                            }
                            else if (this.CurrentPlayerColor == ChessPieceType.Black)
                            {
                                this.CurrentPlayerColor = ChessPieceType.White;
                            }
                        }
                    }
                }
            }          
        }

        /// <summary>
        /// 将空间坐标转化为棋盘对应的下标(用来判断是否可以落子)
        /// </summary>
        /// <param name="locationX">空间坐标X</param>
        /// <param name="locationY">空间坐标Y</param>
        private bool LocationXYConvertToChessBoardXY(int locationX,int locationY,out int boardX,out int boardY)
        {
            bool isOk = false;
            //如果空间坐标的XY减去起始点坐标的XY可以被间距整除的话说明在棋盘可以下子的位置上
            boardX = 0;
            boardY = 0;
            if ((locationX - gameBoard.ZuoShangPt.X) % gameBoard.JianJuLength < 20)
            {
                if ((locationY - gameBoard.ZuoShangPt.Y) % gameBoard.JianJuLength < 20)
                {
                    //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                    boardX = (locationX - gameBoard.ZuoShangPt.X) / gameBoard.JianJuLength;
                    boardY = (locationY - gameBoard.ZuoShangPt.Y) / gameBoard.JianJuLength;
                    isOk = true;
                }
            }
            else if ((locationX - gameBoard.ZuoShangPt.X) % gameBoard.JianJuLength > gameBoard.JianJuLength - 20)
            {
                if ((locationY - gameBoard.ZuoShangPt.Y) % gameBoard.JianJuLength > gameBoard.JianJuLength - 20                                                                                                                                                                                                                                                                                                                         )
                {
                    //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                    boardX = (locationX - gameBoard.ZuoShangPt.X) / gameBoard.JianJuLength + 1;
                    boardY = (locationY - gameBoard.ZuoShangPt.Y) / gameBoard.JianJuLength + 1;
                    isOk = true;
                }
            }
            return isOk;
        }   
     
    }
}
