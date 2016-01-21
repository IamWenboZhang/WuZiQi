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
using System.Threading;

namespace HJZBYSJ.View
{
   
    public partial class FormGame : Form
    {
        public Game ThisGame = new Game();

        public bool CanLuoZi = false;

        private bool ToFormRoom = false;

        //画布
        Graphics g = null;
        Bitmap bmap = null;

        //本回合落子方的颜色 
        private ChessPieceType CurrentPlayerColor
        {
            get
            {
                return this.ThisGame.CurrentColor;
            }
            set
            {
                if (value == ChessPieceType.White)
                {
                    this.ThisGame.CurrentPlayer = this.ThisGame.playerWhite;
                    this.ThisGame.CurrentColor = ChessPieceType.White;
                    this.labelCurrentColor.Text = "白色";
                }
                else if (value == ChessPieceType.Black)
                {
                    this.ThisGame.CurrentPlayer = this.ThisGame.playerBlack;
                    this.ThisGame.CurrentColor = ChessPieceType.Black;
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
            SetGame(game);
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
                case GameModel.Online:
                    OnlineModelGame(e);
                    break;
                case GameModel.SingleAgainsComputer:
                    SingleAgainstComputerModelGame(e);
                    break;
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
            //点击在中心点的右下角
            if (((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength < 15)&&((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength < 15))
            {             
                //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength;
                boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength;
                isOk = true;
            }
            //点击在中心点的左上角
            else if (((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 15)&&((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 15))
            {
               
                //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength + 1;
                boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength + 1;
                isOk = true;
                
            }
            //点击在中心点的左下角
            else if (((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 15)&&((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength < 15))
            {
                //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength + 1;
                boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength;
                isOk = true;
            }
            //点击在中心点的右上角
            else if (((locationX - this.ThisGame.gameBoard.ZuoShangPt.X) % this.ThisGame.gameBoard.JianJuLength < 15)&&((locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) % this.ThisGame.gameBoard.JianJuLength > this.ThisGame.gameBoard.JianJuLength - 15))
            {                
                //boardXY分别为空间坐标的XY减去起始点坐标的XY除以间距的商
                boardX = (locationX - this.ThisGame.gameBoard.ZuoShangPt.X) / this.ThisGame.gameBoard.JianJuLength;
                boardY = (locationY - this.ThisGame.gameBoard.ZuoShangPt.Y) / this.ThisGame.gameBoard.JianJuLength + 1;
                isOk = true;                
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
            else if(this.CanLuoZi)
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
                        SendChessPieceToServer(tmpPiece);
                    }
                }
            }
            else if(this.ThisGame.playerWhite.IP.Length <=0 && this.ThisGame.playerWhite.NickName.Length <= 0)
            {
                MessageBox.Show("请等待白方玩家的加入！");
            }
            else if (this.ThisGame.CurrentColor != Program.ThisGamePlayer.Color)
            {
                MessageBox.Show("请等待对方落子！");
            }
        }

        private void SingleAgainstComputerModelGame(EventArgs e)
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
                        //如果人没有胜利电脑开始下子
                        if (!this.ThisGame.isWin)
                        {
                            //电脑下棋（电脑一定为黑色）
                            ChessPiece computerPiece = new ChessPiece();
                            //判断出白方的最佳点
                            string computerCount = this.ThisGame.playerWhite.GetBestPointEffectLevel(this.ThisGame.gameBoard);
                            //判断出黑方的最佳点
                            string humanCount = this.ThisGame.playerBlack.GetBestPointEffectLevel(this.ThisGame.gameBoard);
                            //比较两个点的影响力
                            int result = Player.ComparePointEffectLevel(computerCount, humanCount);
                            switch (result)
                            {
                                //如果人脑的最佳点的影响力等于电脑的最佳点的影响力则下到人脑的最佳点
                                //Computer == Human
                                case 0:
                                    computerPiece = new ChessPiece(this.ThisGame.playerWhite.BestPoint.BoardX, this.ThisGame.playerWhite.BestPoint.BoardY, this.ThisGame.CurrentPlayer.Color);
                                    break;
                                //如果人脑的最佳点的影响力小于电脑最佳点的影响力则下到电脑的最佳点
                                //Computer > Human
                                case 1:
                                    computerPiece = new ChessPiece(this.ThisGame.playerWhite.BestPoint.BoardX, this.ThisGame.playerWhite.BestPoint.BoardY, this.ThisGame.CurrentPlayer.Color);
                                    break;
                                //如果人脑的最佳点的影响力大于电脑最佳点的影响力则下到人脑的最佳点
                                //Computer < Human
                                case 2:
                                    computerPiece = new ChessPiece(this.ThisGame.playerBlack.BestPoint.BoardX, this.ThisGame.playerBlack.BestPoint.BoardY, this.ThisGame.CurrentPlayer.Color);
                                    break;
                            }
                            //落子
                            computerPiece.Color = this.ThisGame.CurrentPlayer.Color;
                            DrawPiece(computerPiece);
                            //向棋盘添加该棋子
                            this.ThisGame.gameBoard.Entity[computerPiece.BoardX, computerPiece.BoardY] = new ChessPiece(computerPiece.BoardX, computerPiece.BoardY, this.ThisGame.CurrentPlayer.Color);
                            //判断胜利
                            JudgeWin(computerPiece);
                        }
                    }
                }
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
                    if (!ToFormRoom)
                    {
                        this.DialogResult = DialogResult.Yes;
                    }
                    break;
                case GameModel.SingleAgainsComputer:
                    break;
            }            
            this.Dispose();
            this.Close();
        }

        private void SendChessPieceToServer(ChessPiece piece)
        {
            MessagePackage sendPkg = new MessagePackage("LuoZi", MessagePackage.LuoZiMsgToStirng(piece)
                               , Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
        }

        public void DealMsg(string msg)
        {
            MessagePackage dealMsgPkg = new MessagePackage(msg);
            switch (dealMsgPkg.Command)
            {
                case "LuoZiResponse":
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        
                        ChessPiece piece = MessagePackage.LuoZiMsgToChessPiece(dealMsgPkg.Data);
                        if (piece.Color == ChessPieceType.White)
                        {
                            listBoxMsg.Items.Add("白方落子于" + piece.BoardX.ToString() + "," + piece.BoardY.ToString());
                        }
                        else if (piece.Color == ChessPieceType.Black)
                        {
                            listBoxMsg.Items.Add("黑方落子于" + piece.BoardX.ToString() + "," + piece.BoardY.ToString());
                        }
                        this.DrawPiece(piece);
                        this.ThisGame.gameBoard.Entity[piece.BoardX, piece.BoardY] = piece;
                        JudgeWin(piece);
                        if (Program.ThisGamePlayer.Color == this.ThisGame.CurrentColor)
                        {
                            CanLuoZi = true;
                        }
                        else
                        {
                            CanLuoZi = false;
                        }
                    }));
                   
                    break;
                case "GetInRoomResponse":
                    //MessagePackage.RoomInfoStringToPlayerBlackAndWhite(dealMsgPkg.Data, out this.ThisGame.playerBlack, out this.ThisGame.playerWhite);
                    //this.labelUserIPBlack.Text = this.ThisGame.playerBlack.IP;
                    //this.labelUserIPWhite.Text = this.ThisGame.playerWhite.IP;
                    //this.labelUserNameBlack.Text = this.ThisGame.playerBlack.NickName;
                    //this.labelUserNameWhite.Text = this.ThisGame.playerWhite.NickName;
                    //if (Program.ThisGamePlayer.Color == ChessPieceType.Black)
                    //{
                    //    this.CanLuoZi = true;
                    //}
                    //else
                    //{
                    //    this.CanLuoZi = false;
                    //}
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        Game onlineGame2 = new Game(1, false, ChessPieceType.Black, GameModel.Online);
                        MessagePackage.RoomInfoStringToPlayerBlackAndWhite(dealMsgPkg.Data, out onlineGame2.playerBlack, out onlineGame2.playerWhite);
                        SetGame(onlineGame2);
                        listBoxMsg.Items.Add(onlineGame2.playerWhite.NickName + "进入了房间");
                        if (Program.ThisGamePlayer.Color == ChessPieceType.Black)
                        {
                            this.CanLuoZi = true;
                        }
                        else
                        {
                            this.CanLuoZi = false;
                        }
                    }));
                    break;
                case "ExitRoomResponse":
                    MessageBox.Show("你的对手退出了游戏！");
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        this.DialogResult = DialogResult.Abort;
                        ToFormRoom = true;
                        this.Close();
                    }));
                    break;
                case "RequestRevertGame":
                    if (MessageBox.Show("你的对手请求重新开始！是否同意？", "重新游戏", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SendCommandRevertResponse(true);
                    }
                    else
                    {
                        SendCommandRevertResponse(false);
                    }
                    break;
                case "RevertGameResult":
                    if (dealMsgPkg.Data == "Yes")
                    {
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            this.ThisGame.gameBoard.SetSize(this.pictureBoxGameSence);
                            this.ThisGame.gameBoard.InitEnitity();
                            this.CurrentPlayerColor = ChessPieceType.Black;
                            this.Step = 1;
                            this.ThisGame.isWin = false;
                            DrawChessBoard(out g, out bmap);
                        }));
                        if (Program.ThisGamePlayer.Color == ChessPieceType.Black)
                        {
                            this.CanLuoZi = true;
                        }
                        else
                        {
                            this.CanLuoZi = false;
                        }
                        isSendRevertCommand = false;
                    }
                    else if (dealMsgPkg.Data == "No")
                    {
                        MessageBox.Show("重新游戏的请求被拒绝！");
                        isSendRevertCommand = false;
                    }
                    break;
                case "ServerQuit":
                    this.Invoke(new MethodInvoker(delegate()
                        {
                            MessageBox.Show("服务器已经关闭！");                            
                            this.Close();
                        }));
                    break;
            }
        }

        //将一个Game对象设置到游戏中来
        private void SetGame(Game game)
        {
            this.ThisGame = game;
            this.labelUserIPBlack.Text = this.ThisGame.playerBlack.IP;
            this.labelUserIPWhite.Text = this.ThisGame.playerWhite.IP;
            this.labelUserNameBlack.Text = this.ThisGame.playerBlack.NickName;
            this.labelUserNameWhite.Text = this.ThisGame.playerWhite.NickName;
            this.ThisGame.gameBoard.SetSize(this.pictureBoxGameSence);
            DrawChessBoard(out g, out bmap);
            this.CurrentPlayerColor = this.ThisGame.CurrentColor;
            this.Step = this.ThisGame.step;
            switch (this.ThisGame.thisGameModel)
            {
                case GameModel.Online:
                    this.btnReturn.Enabled = true;
                    this.btnSave.Enabled = false;
                    Thread listenThread = new Thread(new ThreadStart(Program.ThisGameMrowlTcpClient.GetMessage));
                    listenThread.IsBackground = true;
                    listenThread.Start();
                    Program.ThisGameMrowlTcpClient.FuncChuLiMessage = DealMsg;
                    break;
            }
            for (int i = 0; i < Chessboard.Hight; i++)
            {
                for (int j = 0; j < Chessboard.Width; j++)
                {
                    DrawPiece(this.ThisGame.gameBoard.Entity[i, j]);
                }
            }
        }

        bool isSendRevertCommand = false;

        private void btnRevert_Click(object sender, EventArgs e)
        {
            switch (this.ThisGame.thisGameModel)
            {
                case GameModel.Online:
                    if (this.ThisGame.playerWhite.NickName.Length <= 0 && this.ThisGame.playerWhite.IP.Length <= 0)
                    {
                        MessageBox.Show("请等待白方加入游戏！");
                    }
                    else if (isSendRevertCommand)
                    {
                        MessageBox.Show("已接发送重新游戏请求。请等待对方回应！");
                    }
                    else
                    {
                        SendCommandReverRequest();
                    }
                    break;
                default:
                    this.ThisGame.gameBoard.SetSize(this.pictureBoxGameSence);
                    this.ThisGame.gameBoard.InitEnitity();
                    this.CurrentPlayerColor = ChessPieceType.Black;
                    this.Step = 1;
                    this.ThisGame.isWin = false;
                    DrawChessBoard(out g, out bmap);
                    break;
            }
        }

        //发送重新开始游戏的请求
        private void SendCommandReverRequest()
        {
            MessagePackage sendPkg = new MessagePackage("RequestRevertGame", Program.ThisGamePlayer.NickName + "请求重新开始游戏", Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
            isSendRevertCommand = true;
        }

        //发送退出房间的请求
        private void SendCommandExitRoom()
        {
            MessagePackage sendMsgPkg = new MessagePackage("ExitRoom", Program.ThisGamePlayer.NickName,
                   Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            Program.ThisGameMrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
        }

        //发出对于重新开始游戏的处理意见
        private void SendCommandRevertResponse(bool isAgree)
        {
            if (isAgree)
            {
                MessagePackage sendPkg = new MessagePackage("RevertGameResponse", "Yes", Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
                Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
            }
            else
            {
                MessagePackage sendPkg = new MessagePackage("RevertGameResponse", "No", Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd hh:mm:ss"));
                Program.ThisGameMrowlTcpClient.SendMessage(sendPkg.MsgPkgToString());
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
                    this.ThisGame.gameBoardXmlStr = GameUtil.ErWeiArrayToXMLStr(this.ThisGame.gameBoard.GameBoardEnityToStringArray(this.ThisGame.gameBoard.Entity));
                    if (GameUtil.Add(this.ThisGame))
                    {
                        MessageBox.Show("保存成功！");
                    }          
                }
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            if (Program.ThisGamePlayer != null)
            {
                if (Program.ThisGamePlayer.Color == ChessPieceType.White)
                {
                    this.Text = "本地玩家：" + Program.ThisGamePlayer.NickName + "(白方)";
                }
                else if (Program.ThisGamePlayer.Color == ChessPieceType.Black)
                {
                    this.Text = "本地玩家：" + Program.ThisGamePlayer.NickName + "(黑方)";
                }
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            ToFormRoom = true;
            SendCommandExitRoom();
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

    }
}
