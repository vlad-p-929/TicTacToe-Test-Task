using System;
using TicTacToe.Player;
using Zenject;

namespace TicTacToe.Model
{
    public class GameModel
    {
        public event Action OnPlayerChanged;
        
        public IPlayer CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsPlayerVsAI { get; }
        private IPlayer Player1 { get; }
        private IPlayer Player2 { get; }

        [Inject]
        public GameModel(
            [Inject(Id = "Player1")] IPlayer player1, 
            [Inject(Id = "Player2")] IPlayer player2)
        {
            Player1 = player1;
            Player2 = player2;

            CurrentPlayer = player1;
        
            IsPlayerVsAI = !player1.IsAI && player2.IsAI;
        }
        
        public string GetCurrentPlayerName() => 
            CurrentPlayer == Player1 ? CONSTS.PlayerX : CONSTS.PlayerO;

        public void ResetGame()
        {
            IsGameOver = false;
            CurrentPlayer = Player1;
        }

        public void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
            
            OnPlayerChanged?.Invoke();
        }

        public void GameOver() => IsGameOver = true;
    }
}