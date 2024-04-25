namespace Treblecross
{
    public class Piece
    {

        public readonly char Mark = 'X'; // default: X;
        public readonly ConsoleColor Colour = ConsoleColor.White; // default: white

        public readonly int Id;

        public string Label => this.Id + "-" + Mark.ToString() + "-" + ((int)Colour).ToString();        

        public Piece(char mark)
        {
            Id = this.GetHashCode();
            this.Mark = mark;
        }

        public Piece(char mark, ConsoleColor colour)
        {
            Id = this.GetHashCode();
            Mark = mark;
            Colour = colour;
        }

        public Piece(int id, char mark, ConsoleColor colour)
        {
            Id = id;
            Mark = mark;
            Colour = colour;
        }

        public string Print()
        {
            // ANSI code
            // TODO: char.TryParse()
            return $"\u001b[38;5;{(int)Colour}m{Mark}\u001b[0m";
        }
    }

    public enum PlayerType
    {
        Cpu = 0,
        Human = 1,
    }

    public class Player
    {
        public readonly int Id;
        public string Name { get; }
        public PlayerType PlayerType { get; }
        public Piece Piece { get; }
        public string Label => this.Id + "-" + Name + "-" + PlayerType.ToString();        

        public Player(string name, PlayerType playerType, Piece piece)
        {
            Id = this.GetHashCode();
            Name = name;
            PlayerType = playerType;
            if (PlayerType == PlayerType.Cpu)
            {
                Name += " (cpu)";
            }

            Piece = piece;
        }

        public Player(int id, string name, PlayerType playerType, Piece piece)
        {
            Id = id;
            Name = name;
            PlayerType = playerType;
            if (PlayerType == PlayerType.Cpu)
            {
                Name += " (cpu)";
            }

            Piece = piece;
        }

        /// <summary>
        /// Create a computer player
        /// </summary>
        /// <returns>A Player object</returns>
        public static Player CreateComputerPlayer() {
            Piece piece = new Piece('X', ConsoleColor.Red);
            return new Player("CPU", PlayerType.Cpu, piece);
        }

        /// <summary>
        /// Create a human player
        /// </summary>
        /// <returns>A Player object</returns>
        public static Player CreateHumanPlayer() {
            Piece piece = createPeice(false);
            return createHumanPlayer(piece);
        }

        public static Player CreateHumanPlayerWithCustomPeice() {
            Piece piece = createPeice(true);
            return createHumanPlayer(piece);
        }

        private static Player createHumanPlayer (Piece piece) {
            string name;
            do {
                Log.Info("Game", "Enter player name? (empty, '-', or ',' is not allowed)");
                name = Console.ReadLine();
            } while (name == null || name == "" || name.Contains('-') || name.Contains(','));
            
            return new Player(name, PlayerType.Human, piece);
        }

        private static Piece createPeice(bool customPeice) {
            // default settings
            char mark  = 'X'; 
            int colour = 15; 

            if (customPeice) {
                do {
                    Log.Info("Game" ,"Enter a mark representing player-1? (example: X or O) ");
                } while (!char.TryParse(Console.ReadLine(), out mark));

                do {
                    Log.Info("Game","Enter a color representing player-1? ");
                    Console.WriteLine("Options: \r\n" +
                        "0: black\r\n" + "9: blue\r\n" + "10: green\r\n" + "12: red\r\n" + "14: yellow\r\n" + "15: white"
                        );
                } while (!int.TryParse(Console.ReadLine(), out colour));
            } 

            return new Piece(mark, (ConsoleColor)colour);
        }

        // public override string ToString()
        // {
        //     return "[Player] Name: " + Name + ", PlayerType: " + PlayerType + ", Peice: " + Piece.Print();
        // }
    }
    
}