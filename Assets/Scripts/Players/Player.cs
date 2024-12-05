using System;
using Cysharp.Threading.Tasks;
using TicTacToe.Enum;
using TicTacToe.Player.Strategy;

namespace TicTacToe.Player
{
    public class Player : IPlayer
    {
        private readonly EPlayerId playerId;
        private readonly IMoveStrategy moveStrategy;

        public Player(EPlayerId playerId, IMoveStrategy moveStrategy)
        {
            this.playerId = playerId;
            this.moveStrategy = moveStrategy;
        }

        public void OnUserInput(int x, int y)
        {
            moveStrategy.OnUserInput(x, y);
        }

        public async UniTask MakeMoveAsync()
        {
            await moveStrategy.MakeMoveAsync(playerId);
        }

        public bool IsAI => moveStrategy is AIMoveStrategy;

    }
}