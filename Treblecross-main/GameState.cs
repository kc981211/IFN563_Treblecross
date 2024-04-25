namespace Treblecross
{
    public class GameState {

        public int[,] State;
        public Player Player;
        GameStateHistory gameStateHistory = GameStateHistory.Instance;


        public GameState(int[,] state)
        {
            State = state;
        }
        
        public GameState(Player player, int[,] state)
        {
            Player = player;
            State = state;
        }

        public GameState Redo()
        {
            GameState latestState = gameStateHistory.GetLatestState();
            if (latestState.State != State)
            {
                GameState state = gameStateHistory.GetNext();
                GameStateHistory.Instance.PointTo(state);
                return state;
            }
            else
            {
                Log.Info("Game", "You are on the most updated move!");
            }
            return latestState;
        }

        public GameState Undo()
        {
            GameState state = gameStateHistory.GetPrevious();
            if (state == null) {
                Log.Info("Game", "There no previous state found in history!");
                return this;
            }

            GameStateHistory.Instance.PointTo(state);
            return state;
        }
    }

    //singleton class
    public class GameStateHistory
    {
        private static GameStateHistory _instance;
        private List<GameState> StateHistory;
        private GameState pointer;

        private GameStateHistory()
        {
            StateHistory = new List<GameState>();
        }

        public static GameStateHistory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameStateHistory();

                return _instance;
            }
        }

        /// <summary>
        /// For initial calling.
        /// </summary>
        /// <param name="state"></param>
        public void Init(GameState state) {
            StateHistory.Add(state);
            pointer = state;
        }

        public void PointTo(GameState state) {
            pointer = state;
        }

        public GameState GetLatestState()
        {
            GameState currentState = StateHistory[StateHistory.Count -1];
            return currentState;
        }

        public void AddHistory(GameState state)
        {
            int idx = StateHistory.IndexOf(pointer);
            if (idx == -1) {
                Log.Error("Fail at adding state to history", new Exception("pointer is not found in the history"));
                return;
            }

            StateHistory.RemoveRange(idx + 1, StateHistory.Count - 1 - idx);
            pointer = state;
            StateHistory.Add(state);
        }
        
        public GameState GetPrevious()
        {
            try { 
                GameState previousState = StateHistory[StateHistory.IndexOf(pointer) - 2]; 
                pointer = previousState;
                return previousState;
            } catch (Exception) {
                return null;
            }
        }

        public GameState GetNext()
        {
            GameState nextState = StateHistory[StateHistory.IndexOf(pointer) + 2];
            pointer = nextState; 
            return nextState;
        }

        public bool Contains(GameState state) { 
            return StateHistory.Contains(state);
        }

    }
}