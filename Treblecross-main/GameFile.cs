using System.Text.Json;

namespace Treblecross 
{

    public class GameFile : IDisposable
    {
        public void Dispose()
        {
            // Dispose(true);
            GC.SuppressFinalize(this);
        }


        public void Save(GameData gameData) {
            //DateTime now = DateTime.Now;
            File.WriteAllText("Gamedata.json", JsonSerializer.Serialize(gameData));
            Log.Info("File Saved.");
        }


        public (GameMode?, Player[], Board) Load() {

            try {
                GameMode mode;

                string filePath;
                do {
                    Log.Info("Menu", "Enter your file path: ");
                    filePath = Console.ReadLine();
                } while (!File.Exists(filePath));

                string jsonData = File.ReadAllText(filePath);
                GameData gameData = JsonSerializer.Deserialize<GameData>(jsonData);
                if (gameData == null) throw new Exception("Unable to read GameData (=null) object.");

                mode = gameData.Game.Mode;
                Player[] players = GameData.ParsePlayer(gameData);
                Player playerTurn = players.Where(x => x.Id == gameData.Board.Player).FirstOrDefault();
                Board board = GameData.ParseBoard(gameData, playerTurn);

                return (mode, players, board);

            } catch (Exception ex) {
                Log.Error("Failed at loading file.", ex);
            }

            return (null, null, null);
        }

        
    }

}