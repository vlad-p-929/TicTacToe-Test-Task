using Cysharp.Threading.Tasks;
using TicTacToe.Enum;
using TicTacToe.Model;

namespace TicTacToe.Player.Strategy
{
    public class HumanMoveStrategy : IMoveStrategy
    {
        private readonly GridModel gridModel;

        private bool isMoveMade;
        private (int x, int y) move;

        public HumanMoveStrategy(GridModel gridModel) => this.gridModel = gridModel;

        public async UniTask MakeMoveAsync(EPlayerId playerId)
        {
            isMoveMade = false;

            await UniTask.WaitUntil(() => isMoveMade);
            
            if (!gridModel.MakeMove(move.x, move.y, playerId))
            {
                await MakeMoveAsync(playerId);
            }
        }

        public void OnUserInput(int x, int y)
        {
            move = (x, y);
            isMoveMade = true;
        }
    }
}