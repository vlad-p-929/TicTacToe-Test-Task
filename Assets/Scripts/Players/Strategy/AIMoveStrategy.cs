using Cysharp.Threading.Tasks;
using TicTacToe.Enum;
using TicTacToe.Model;
using UnityEngine;

namespace TicTacToe.Player.Strategy
{
    public class AIMoveStrategy : IMoveStrategy
    {
        private readonly GridModel gridModel;
        private readonly EDifficulty difficulty;

        public AIMoveStrategy(EDifficulty difficulty, GridModel gridModel)
        {
            this.difficulty = difficulty;
            this.gridModel = gridModel;
        }

        public async UniTask MakeMoveAsync(EPlayerId playerId)
        {
            await UniTask.Delay(Random.Range(200, 500));

            var move = difficulty == EDifficulty.Easy
                ? gridModel.FindRandomAvailableCell()
                : gridModel.FindBestAvailableCell(playerId);

            if (move != null)
            {
                gridModel.MakeMove(move.Value.x, move.Value.y, playerId);
            }
        }

        public void OnUserInput(int x, int y)
        {

        }
    }
}