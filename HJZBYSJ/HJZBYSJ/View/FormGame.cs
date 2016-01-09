using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MrOwlLibrary.NetWork.TCP;
using HJZBYSJ.Model;
using HJZBYSJ.DataBase;

namespace HJZBYSJ.View
{
   
    public partial class FormGame : Form
    {
        public Game ThisGame = new Game();

        public MrOwlTCPClient mrowlTCPClient = new MrOwlTCPClient();

        public Player thisPlayer = new Player(ChessPieceType.None);

        private bool CanLuoZi = false;

        //画布
        Graphics g = null;
        Bitmap bmap = null;

        //本回合落子方的颜色 
        private ChessPieceType CurrentPlayerColor
        {
            get
            {
                return this.ThisGame.currentColor;
            }
            set
            {
                if (value == ChessPieceType.White)
                {
                    this.ThisGame.CurrentPlayer = this.ThisGame.playerWhite;
                    this.ThisGame.currentColor = ChessPieceType.White;
                    this.labelCurrentColor.Text = "白色";
                }
                else if (value == ChessPieceType.Black)
                {
                    this.ThisGame.CurrentPlayer = this.ThisGame.playerBlack;
                    this.ThisGame.currentColor = ChessPieceType.Black;
                    this.labelCurrentColor.Text = "黑色";
                }
            }
        }

        //本次对局的回合数
        private int Step
        {
            get
            {
                return this.ThisGame.step;
            }
            set
            {
                this.ThisGame.step = value;
                this.labelStep.Text = value.ToString();
            }
        }

        public FormGame()
        {
            InitializeComponent();
        }

        public FormGame(Game game)
        {
            InitializeComponent();
            this.ThisGame = game;
            this.labelUserIPBlack.Text = this.ThisGame.playerBlack.IP;
            this.labelUserIPWhite.Text = this.ThisGame.playerWhite.IP;
            this.labelUserNameBlack.Text = this.ThisGame.playerBlack.NickName;
            this.labelUserNameWhite.Text = this.ThisGame.playerWhite.NickName;
            ThisGame.gameBoard.InitChessBoard(this.pictureBoxGameSence);
            DrawChessBoard(out g, out bmap);
            switch (this.ThisGame.thisGameModel)
            {
                case GameModel.DoubleOffLine:
                    this.CurrentPlayerColor = this.ThisGame.currentColor;
                    this.Step = this.ThisGame.step;
                    break;
                case GameModel.Online:
                    break;
                case GameModel.SingleAgainsComputer:
                    break;
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            //this.ThisGame.isWin = false;
            //this.ThisGame.step = 0;
            //this.ThisGame.gameBoard = new Chessboard();
            //switch (this.ThisGame.thisGameModel)
            //{
            //    case GameModel.DoubleOffLine:
            //        ThisGame.gameBoard.InitChessBoard(this.pictureBoxGameSence);
            //        this.CurrentPlayerColor = ChessPieceType.Black;
            //        this.Step = 1;
            //        this.ThisGame.isWin = false;
            //        DrawChessBoard(out g, out bmap); 
            //        break;
            //    case GameModel.Online:
            //        ThisGame.gameBoard.InitChessBoard(this.pictureBoxGameSence);
            //        this.Step = 1;
            //        this.ThisGame.isWin = false;
            //        DrawChessBoard(out g, out bmap);
            //        break;
            //    case GameModel.SingleAgainsComputer:
            //        break;
            //}        
        }

        //绘制地图
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
                Point pt1 = new Point(this.ThisGame.gameBoard.ZuoShangPt.X + this.ThisGame.gameBoard.JianJuLength * i, this.ThisGame.gameBoard.ZuoShangPt.Y);
                Point pt2 = new Point(this.ThisGame.gameBoard.ZuoShangPt.X + this.ThisGame.gameBoard.JianJuLength * i, this.ThisGame.gameBoard.YouXiaPt.Y);
                Pen p = new Pen (Color.Black,1);
                g.DrawLine(p,pt1,pt2);
            }
            //横着的十五道线（X不变Y每次加间距*i）
            for (j = 0; j < Chessboard.Hight; j++)
            {
                Point pt1 = new Point(this.ThisGame.gameBoard.ZuoShangPt.X, this.ThisGame.gameBoard.ZuoShangPt.Y + this.ThisGame.gameBoard.JianJuLength * j);
                Point pt2 = new Point(this.ThisGame.gameBoard.YouXiaPt.X, this.ThisGame.gameBoard.ZuoShangPt.Y + this.ThisGame.gameBoard.JianJuLength * j);
                Pen p = new Pen(Color.Black, 1);
                g.DrawLine(p, pt1, pt2);
            }
            this.pictureBoxGameSence.Image = bmap;
        }

        //绘制棋子
        private void DrawPiece(ChessPiece piece)
        {
            Image image = this.pictureBoxGameSence.Image;
            g = Graphics.FromImage(image);
            //根据在棋盘上的位置X，Y来计算出该点的详细坐标
            //公式：ZuoShangPt.X + JianJuLength * boardX    ZuoShangPt.Y + JianJuLength * boardY
            int pieceLocationX = this.ThisGame.gameBoard.ZuoShangPt.X + this.ThisGame.gameBoard.JianJuLength * piece.BoardX;
            int pieceLocationY = this.ThisGame.gameBoard.ZuoShangPt.Y + this.ThisGame.gameBoard.JianJuLength * piece.BoardY;
            //得到棋子的中心点坐标
            Point pieceCenterPt = new Point(pieceLocationX, pieceLocationY);
            
            //棋子的左上角X坐标为
            int zsPtLocationX = pieceCenterPt.X - this.ThisGame.gameBoard.JianJuLength / 2;
            //棋子的左上角Y坐标为
            int zsPtLocationY = pieceCenterPt.Y - this.ThisGame.gameBoard.JianJuLength / 2;
            //得到棋子左上角坐标
            Point zsPt = new Point(zsPtLocationX,zsPtLocationY);

            //画出棋子
            SolidBrush sb = new SolidBrush (this.pictureBoxGameSence.BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            if (piece.Color == ChessPieceType.White)
            {
                sb = new SolidBrush(Color.White);
                g.FillEllipse(sb, zsPt.X, zsPt.Y, this.ThisGame.gameBoard.JianJuLength, this.ThisGame.gameBoard.JianJuLength);
            }
            else if (piece.Color == ChessPieceType.Black)
            {
                sb = new SolidBrush(Color.Black);
                g.FillEllipse(sb, zsPt.X, zsPt.Y, this.ThisGame.gameBoard.JianJuLength, this.ThisGame.gameBoard.JianJuLength);
            }
            this.pictureBoxGameSence.Image = image;
        }

        //单击棋盘落子
        private void pictureBoxGameSence_Click(object sender, EventArgs e)
        {
            switch (this.ThisGame.thisGameModel)
            {
                case GameModel.DoubleOffLine:
                    ShuangRenDanJiModelGame(e);
                    break;
                case GameModel.Online: break;
                case GameModel.SingleAgainsComputer: break;
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
            if ((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength < 20)
            {
                if ((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength < 20)
                {
                    //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                    boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength;
                    boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength;
                    isOk = true;
                }
            }
            else if ((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 20)
            {
                if ((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 20)
                {
                    //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                    boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength + 1;
                    boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength + 1;
                    isOk = true;
                }
            }
            return isOk;
        }

        private void JudgeWin(ChessPiece piece)
        {
            //判断是否胜利
            if (this.ThisGame.CurrentPlayer.CheckWin(piece, this.ThisGame.gameBoard))
            {
                if (this.CurrentPlayerColor == ChessPieceType.White)
                {
                    this.ThisGame.Winer = this.ThisGame.CurrentPlayer;
                    MessageBox.Show("白方胜利");
                    this.ThisGame.isWin = true;
                }
                if (this.CurrentPlayerColor == ChessPieceType.Black)
                {
                    this.ThisGame.Winer = this.ThisGame.CurrentPlayer;
                    MessageBox.Show("黑方胜利");
                    this.ThisGame.isWin = true;
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

        private void OnlineModelGame(EventArgs e)
        {
            //判断是否是联网对战模式
            if (CanLuoZi)
            {
                //判断是否是胜利状态
                if (this.ThisGame.isWin)
                {
                    MessageBox.Show("已经有人胜利！");
                }
                else if (this.CanLuoZi)
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
                        if (this.ThisGame.gameBoard.Entity[boardX, boardY].Color == ChessPieceType.None)
                        {
                            ChessPiece tmpPiece = new ChessPiece(boardX, boardY, this.ThisGame.CurrentPlayer.Color);
                            this.DrawPiece(tmpPiece);
                            this.ThisGame.gameBoard.Entity[boardX, boardY] = tmpPiece;
                            MessagePackage sendPkg = new MessagePackage("LuoZi", MessagePackage.LuoZiMsgToStirng(tmpPiece)
                                , thisPlayer.IP, thisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                            mrowlTCPClient.SendMessage(sendPkg.MsgPkgToString());
                            JudgeWin(tmpPiece);
                        }
                    }
                    this.CanLuoZi = false;
                }
            }
            else
            {
                MessageBox.Show("请等待对方落子！");
            }
        }

        private void ShuangRenDanJiModelGame(EventArgs e)
        {
            //判断是否是胜利状态
            if (this.ThisGame.isWin)
            {
                if (this.ThisGame.Winer.Color == ChessPieceType.White)
                {
                    MessageBox.Show("白方已经胜利！");
                }
                else if (this.ThisGame.Winer.Color == ChessPieceType.Black)
                {
                    MessageBox.Show("黑方已经胜利！");
                }
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
                    if (this.ThisGame.gameBoard.Entity[boardX, boardY].Color == ChessPieceType.None)
                    {
                        ChessPiece tmpPiece = new ChessPiece(boardX, boardY, this.ThisGame.CurrentPlayer.Color);
                        this.DrawPiece(tmpPiece);
                        this.ThisGame.gameBoard.Entity[boardX, boardY] = tmpPiece;
                        JudgeWin(tmpPiece);                        
                    }
                }
            }      
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (this.ThisGame.thisGameModel)
            {
                case GameModel.DoubleOffLine:
                    break;
                case GameModel.Online:
                    MessagePackage sendMsgPkg = new MessagePackage("TuiChu", this.thisPlayer.NickName,
                    this.thisPlayer.IP, this.thisPlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                    this.mrowlTCPClient.SendMessage(sendMsgPkg.MsgPkgToString());
                    this.mrowlTCPClient.Close();
                    break;
                case GameModel.SingleAgainsComputer:
                    break;
            }
            this.Dispose();
        }

        //private void SetGame(ChessPieceType currentColor, int step, bool iswin, ChessPiece[,] gameboard)
        //{
        //    gameBoard.InitChessBoard(this.pictureBoxGameSence);
        //    this.CurrentPlayerColor = ChessPieceType.Black;
        //    this.Step = 1;
        //    this.IsWin = false;
        //    DrawChessBoard(out g, out bmap);
        //    for (int i = 0; i < Chessboard.Width; i++)
        //    {
        //        for (int j = 0; j < Chessboard.Hight; j++)
        //        {
        //            DrawPiece(gameboard[i, j]);
        //        }
        //    }
        //}
        private void SetGame(Game game)
        {
            this.ThisGame = game;
            this.ThisGame.gameBoard = new Chessboard();
            this.ThisGame.gameBoard.InitChessBoard(this.pictureBoxGameSence);
            this.ThisGame.isWin = game.isWin;
            this.CurrentPlayerColor = game.currentColor;
            this.Step = game.step;
            string[][] test;
            test = GameUtil.XMLStrToErWeiArray(game.gameBoardXmlStr);
            this.ThisGame.gameBoard.Entity = Chessboard.StringArrayToGameBoardEnity(test);
            DrawChessBoard(out g, out bmap);
            for (int i = 0; i < Chessboard.Width; i++)
            {
                for (int j = 0; j < Chessboard.Hight; j++)
                {
                    DrawPiece(this.ThisGame.gameBoard.Entity[i, j]);
                }
            }
        }

        private void btnRevert_Click(object sender, EventArgs e)
        {
            this.ThisGame.gameBoard.InitChessBoard(this.pictureBoxGameSence);
            this.CurrentPlayerColor = ChessPieceType.Black;
            this.Step = 1;
            this.ThisGame.isWin = false;
            DrawChessBoard(out g, out bmap);
        }

        //载入存档按钮单击事件
        private void btnLoad_Click(object sender, EventArgs e)
        {
            FormGameFile frmGameFile = new FormGameFile();
            if (frmGameFile.ShowDialog() == DialogResult.OK)
            {
                Game tmp = new Game ();
                if (GameUtil.GetAt(frmGameFile.SelectedGameID, ref tmp))
                {
                    SetGame(tmp);
                }
            }
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GameUtil.TestConnection())
            {
                FormSetGameFileName frmSetName = new FormSetGameFileName();
                DialogResult result = frmSetName.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.ThisGame.gameName = frmSetName.GameName;
                }
                this.ThisGame.gameBoardXmlStr = GameUtil.ErWeiArrayToXMLStr(Chessboard.GameBoardEnityToStringArray(this.ThisGame.gameBoard.Entity));
                GameUtil.Add(this.ThisGame);
            }
        }
    }
}
