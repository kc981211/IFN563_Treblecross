namespace Treblecross
{
    public class Board
    {
        private readonly int dimension_x;
        private readonly int dimension_y;

        public int[] Size => [dimension_y, dimension_x];

        public GameState CurrentState { get; set; }
        private string[,] displayedAry {get; set;}

        public Board(int x, int y) {
            dimension_x = x;
            dimension_y = y;
            GameState gameState = new GameState(new int[x,y]);
            displayedAry = new string[gameState.State.GetLength(0), gameState.State.GetLength(1)];
            CurrentState = gameState;
        }
        public Board(int x, int y, GameState state) {
            dimension_x = x;
            dimension_y = y;
            displayedAry = new string[state.State.GetLength(0), state.State.GetLength(1)];
            CurrentState = state;
        }


        public void Draw () {
            if (CurrentState.State.GetLength(0) == 1) {
                displayedAry = new string[CurrentState.State.GetLength(0), CurrentState.State.GetLength(1)];
                if (CurrentState.Player == null) {
                    // init
                    Console.WriteLine("[{0}]", string.Join(", ", Common<string>.Convert1dArray(displayedAry)));
                    return;
                }
                for (int i=0; i < CurrentState.State.GetLength(1); i++) {
                    if (CurrentState.State[0, i] != 0) {
                        displayedAry[0, i] = CurrentState.Player.Piece.Print();
                    }
                }
                Console.WriteLine("[{0}]", string.Join(", ", Common<string>.Convert1dArray(displayedAry)));
            } else {
                // 2D board ..
            }
        }

        public void Update(GameState state) {
            CurrentState = state;
            Draw();
        }

        public void Load(GameState state) {
            Update(state);
        }

    }


}