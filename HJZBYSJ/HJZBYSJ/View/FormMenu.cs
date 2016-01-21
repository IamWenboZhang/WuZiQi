using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using HJZBYSJ.DataBase;
using HJZBYSJ.Model;

namespace HJZBYSJ.View
{
    
    public partial class FormMenu : Form
    {
        public Game LoadGame = new Game();

        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            //this.Close();
            Game tmpGame = new Game(1, false, ChessPieceType.Black, GameModel.DoubleOffLine);
            this.Hide();
            Program.frmGame = new FormGame(tmpGame);
            DoinFormGame();
        }

        private void btnOnline_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Ignore;
            //this.Close();
            DoinFormLoad();
        }

        private void DoinFormLoad()
        {
            this.Hide();
            Program.frmLoad = new FormLoad();
            DialogResult result = Program.frmLoad.ShowDialog();
            if (result != DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                DoinFormRoom();
            }
        }

        private void DoinFormRoom()
        {
            this.Hide();
            Program.frmRoom = new FormRoom();
            DialogResult result = Program.frmRoom.ShowDialog();
            //创建房间进入游戏界面
            if (result == DialogResult.OK)
            {
                Game onlineGame = new Game(1, false, ChessPieceType.Black, GameModel.Online);
                Program.ThisGamePlayer.Color = ChessPieceType.Black;
                onlineGame.playerBlack = Program.ThisGamePlayer;
                Program.frmGame = new FormGame(onlineGame);
                //Program.frmGame.ShowDialog();
                DoinFormGame();
            }
             //进入房间进入游戏界面
            else if (result == DialogResult.Yes)
            {                
                //Program.frmGame.ShowDialog();
                DoinFormGame();
            }
            else
            {
                FormRoom.SendCommandTuiChu();
                this.Show();
            }
        }

        private void DoinFormGame()
        {
            this.Hide();
            DialogResult result = Program.frmGame.ShowDialog();
            if (result == DialogResult.Abort)
            {
                DoinFormRoom();
            }
            else if(result == DialogResult.Yes)
            {
                MessagePackage sendMsgPkg = new MessagePackage("TuiChu", Program.ThisGamePlayer.NickName,
                    Program.ThisGamePlayer.IP, Program.ThisGamePlayer.NickName, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                Program.ThisGameMrowlTcpClient.SendMessage(sendMsgPkg.MsgPkgToString());
                Program.ThisGameMrowlTcpClient.Close();
                this.Show();
            }
            else
            {
                this.Show();
            }
        }
      
        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            //this.Close();
            Application.ExitThread();
            Application.Exit();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            FormGameFile frmGameFile = new FormGameFile();

            if (frmGameFile.ShowDialog() == DialogResult.OK)
            {
                this.Hide();
                if (GameUtil.GetAt(frmGameFile.SelectedGameID, ref LoadGame))
                {
                    Program.frmGame = new FormGame(this.LoadGame);
                    DoinFormGame();
                }                
            }
            
        }

        private void btnAgainstComputer_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.No;
            //this.Close();
            Game tmpGame = new Game(1, false, ChessPieceType.Black, GameModel.SingleAgainsComputer);
            this.Hide();
            Program.frmGame = new FormGame(tmpGame);
            //frmGame.ShowDialog();
            DoinFormGame();
        }

    }
}
