using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using HJZBYSJ.Model;
using System.Windows.Forms;
using System.Data.Common;
using MrOwlLibrary.DataBase;
using System.Data;
using System.Net;

namespace HJZBYSJ.DataBase
{
    public enum GameModel { SingleAgainsComputer, Online, DoubleOffLine }
    public class Game
    {
        //当前游戏的ID
        public int gameID = 0;
        //当前游戏的存档名
        public string gameName = "";
        //当前游戏的回合数
        public int step = 0;
        //当前游戏是否有人胜利
        public bool isWin = false;
        //当前游戏的当前落子方
        public ChessPieceType currentColor = ChessPieceType.None;
        //当前游戏的棋盘
        public Chessboard gameBoard = new Chessboard();
        //当前游戏的棋盘实体的XML字符串
        public string gameBoardXmlStr = ""; //Chessboard.NoneEnityXMLStr();
        //当前游戏的胜利者
        public Player Winer = new Player(ChessPieceType.None); 
        //游戏模式（默认为双人离线模式）
        public GameModel thisGameModel = GameModel.DoubleOffLine;
        //玩家对象
        public Player playerWhite = new Player(ChessPieceType.White);
        public Player playerBlack = new Player(ChessPieceType.Black);
        //当前的玩家
        public Player CurrentPlayer;

        public Game()
        {

        }

        public Game(int step, bool isWin, ChessPieceType currentColor,GameModel thisGameModel)
        {
            IPAddress ipadd;
            string localip = "";
            if(MrOwlLibrary.NetWork.MrOwlNetWork.GetLocalIP(out ipadd))
            {
                localip = ipadd.ToString();
            }
            this.step = step;
            this.isWin = isWin;
            this.currentColor = currentColor;
            this.thisGameModel = thisGameModel;
            this.gameBoard = new Chessboard();
            switch (this.thisGameModel)
            {
                case GameModel.DoubleOffLine:
                    this.playerWhite = new Player(localip,"白方选手",ChessPieceType.White);
                    this.playerBlack = new Player(localip,"黑方选手",ChessPieceType.Black);
                    break;
                case GameModel.Online:                    
                    break;
                case GameModel.SingleAgainsComputer:
                    this.playerBlack = new Player(localip, "电脑", ChessPieceType.Black);
                    this.playerWhite = new Player(localip, "Me", ChessPieceType.White);
                    break;
            }
            if (currentColor == ChessPieceType.Black)
            {
                this.CurrentPlayer = this.playerBlack;
            }
            else if (currentColor == ChessPieceType.None)
            {
                this.CurrentPlayer = this.playerWhite;
            }
        }
    }

    public  class GameUtil
    {

        // 测试数据库连接是否成功
        public static bool TestConnection()
        {
            try
            {
                DbConnection con = MrOwlDB_SQLserver.GetDbConnection();
                using (con)
                {
                    con.Open();
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static DataTable GetAllGame()
        {
            try
            {
                DbConnection con = MrOwlDB_SQLserver.GetDbConnection(); ;
                using (con)
                {
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    string sql = "Select GameID,GameName From Game";
                    DataTable dt = new DataTable("Game");
                    cmd.CommandText = sql;
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GameUtil.GetAllGame函数失败:" + ex.Message);
                return null;
            }
        }


        // 获得一条记录
        public static bool GetAt(int gameId, ref Game game)
        {

            try
            {
                DbConnection con = MrOwlDB_SQLserver.GetDbConnection(); ;
                using (con)
                {
                    con.Open();
                    string sql = "Select * From Game Where GameID = @GameID";
                    DbCommand cmd = MrOwlDB_SQLserver.GetDbCommand(sql, con);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@GameID", DbType.Int32, 4, gameId);
                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            game.gameID = Convert.ToInt32(dr["GameID"]);
                            game.gameName = dr["GameName"].ToString();
                            game.step = Convert.ToInt32(dr["Step"]);
                            game.isWin = Convert.ToBoolean(dr["IsWin"]);
                            game.gameBoardXmlStr = dr["GameBoardXmlStr"].ToString();
                            string[][] tmp;
                            tmp = GameUtil.XMLStrToErWeiArray(game.gameBoardXmlStr);
                            game.gameBoard.Entity = Chessboard.StringArrayToGameBoardEnity(tmp);
                            switch(dr["CurrentColor"].ToString())
                            {
                                case "Black":
                                    game.currentColor = ChessPieceType.Black;
                                    break;
                                case "White":
                                    game.currentColor = ChessPieceType.White;
                                    break;
                                case "None":
                                    game.currentColor = ChessPieceType.None;
                                    break;
                            }
                            switch (dr["GameWiner"].ToString())
                            {
                                case "Black":
                                    game.Winer = game.playerBlack;
                                    break;
                                case "White":
                                    game.Winer = game.playerWhite;
                                    break;
                                case "None":
                                    game.Winer = new Player(ChessPieceType.None);
                                    break;
                            }
                            IPAddress ipadd;
                            string localip = "";
                            if (MrOwlLibrary.NetWork.MrOwlNetWork.GetLocalIP(out ipadd))
                            {
                                localip = ipadd.ToString();
                            }
                            switch (dr["GameModel"].ToString())
                            {
                                case "DoubleOffLine":
                                    game.thisGameModel = GameModel.DoubleOffLine;
                                    game.playerWhite = new Player(localip, "白方选手", ChessPieceType.White);
                                    game.playerBlack = new Player(localip, "黑方选手", ChessPieceType.Black);
                                    break;
                                case "Online":
                                    game.thisGameModel = GameModel.Online;
                                    break;
                                case "SingleAgainsComputer":
                                    game.thisGameModel = GameModel.SingleAgainsComputer;
                                    game.playerBlack = new Player(localip, "电脑", ChessPieceType.Black);
                                    game.playerWhite = new Player(localip, "Me", ChessPieceType.White);
                                    break;
                            }
                        }
                    }
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GameUtil.GetAt函数失败:" + ex.Message);
                return false;
            }
        }


        // 添加一条记录
        public static bool Add(Game a)
        {
            try
            {
                DbConnection con = MrOwlDB_SQLserver.GetDbConnection();
                using (con)
                {
                    con.Open();
                    DbCommand cmd = con.CreateCommand();
                    // 添加Quota表
                    string sql = "Insert Into Game ([GameName], Step, IsWin, CurrentColor, GameBoardXmlStr,GameWiner,GameModel) " +
                        "Values (@GameName,@Step, @IsWin, @CurrentColor, @GameBoardXmlStr, @GameWiner, @GameModel)";
                    cmd.CommandText = sql;
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@GameName", DbType.AnsiString, 40, a.gameName);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@Step", DbType.Int32, 4, a.step);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@IsWin", DbType.Boolean, 4, a.isWin);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@CurrentColor", DbType.AnsiString, 10, a.currentColor);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@GameBoardXmlStr", DbType.AnsiString, 8000, a.gameBoardXmlStr);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@GameWiner", DbType.AnsiString, 10, a.Winer.Color);
                    MrOwlDB_SQLserver.AddCmdParameter(cmd, "@GameModel", DbType.AnsiString, 20, a.thisGameModel);
                    int rows = cmd.ExecuteNonQuery();
                    // 获得Expense表新添加记录的ID 
                    cmd.CommandText = "Select @@Identity";
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("GameUtil.Add函数失败:" + ex.Message);
                return false;
            }
        }

        // 将二维数组序列化成XML
        public static string ErWeiArrayToXMLStr(string[][] str)
        {
            string m_strXML;
            XmlSerializer xml = new XmlSerializer(str.GetType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, Encoding.Default);
            xml.Serialize(writer, str);
            // 得到序列化后的XML字符串,可以直接保存到数据库
            m_strXML = Encoding.Default.GetString(ms.ToArray());
            return m_strXML;
        }

        // 把XML反序列化为二维数组
        public static string[][] XMLStrToErWeiArray(string m_strXML)
        {
            // 从数据库取出XML字符串，这里使用m_strXML变量
            XmlSerializer xml = new XmlSerializer(typeof(string[][]));
            StreamReader sr = new StreamReader(new MemoryStream(System.Text.Encoding.Default.GetBytes(m_strXML)), System.Text.Encoding.Default);
            string[][] str = (string[][])xml.Deserialize(sr);
            return str;
        }     
    }
}
