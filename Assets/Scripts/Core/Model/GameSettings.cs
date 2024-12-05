using TicTacToe.Enum;

namespace TicTacToe.Model
{
    public class GameSettings
    {
        public EGameType GameType { get; private set; } = EGameType.PlayerVsAI;
        public EDifficulty Difficulty { get; private set; } = EDifficulty.Easy;

        public GameSettings()
        {
            
        }
        
        public void Initialize(EGameType gameType, EDifficulty difficulty)
        {
            GameType = gameType;
            Difficulty = difficulty;
        }
    }
}