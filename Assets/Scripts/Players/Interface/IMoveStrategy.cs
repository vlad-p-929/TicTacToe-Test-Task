using Cysharp.Threading.Tasks;
using TicTacToe.Enum;

namespace TicTacToe.Player
{
    public interface IMoveStrategy
    {
        UniTask MakeMoveAsync(EPlayerId playerId);
        void OnUserInput(int x, int y);
    }
}