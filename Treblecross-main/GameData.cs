namespace Treblecross
{
    public class GameData
    {
        public GameInfo Game { get; set; }
        public List<PlayerInfo> Players { get; set; }
        public BoardInfo Board { get; set; }

        public static Board ParseBoard(GameData gameData, Player playerTurn)
        {

            GameState state = new GameState(playerTurn, gameData.Board.ParseState());
            Board board = new Board(gameData.Board.Size[0], gameData.Board.Size[1], state);
              
            return board;
        }

        public static Player[] ParsePlayer(GameData gameData)
        {
            List<Player> players = new List<Player> { };
            foreach (var p in gameData.Players)
            {
                Piece piece = new Piece(p.Piece.Id, p.Piece.Mark, p.Piece.Colour);
                Player player = new Player(p.Id, p.Name, p.Type, piece);
                players.Add(player);
            }

            return players.ToArray();
        }
    }

    public class GameInfo
    {
        public string Name { get; set; }
        public GameMode Mode { get; set; }
    }

    public class PlayerInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerType Type { get; set; }
        public PieceInfo Piece { get; set; }
    }

    public class PieceInfo
    {
        public int Id { get; set; }
        public char Mark { get; set; }
        public ConsoleColor Colour { get; set; }
    }

    public class BoardInfo
    {
        public int[] Size { get; set; }
        public int Player { get; set; }
        public List<List<int>> State { get; set; }

        public int[,] ParseState()
        {
            int[][] aryOfArys = State.Select(x => x.ToArray()).ToArray();
            int[,] ary2d = new int[aryOfArys.Length, aryOfArys[0].Length];

            for (int i = 0; i < aryOfArys.Length; i++) {
                for (int j = 0; j < aryOfArys[0].Length; j++)
                {
                    ary2d[i, j] = aryOfArys[i][j];
                }
            }

            return ary2d;
        }

    }


}