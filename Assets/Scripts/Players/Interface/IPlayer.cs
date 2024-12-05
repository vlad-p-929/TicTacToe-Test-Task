using Cysharp.Threading.Tasks;

namespace TicTacToe.Player
{
    public interface IPlayer
    {
        bool IsAI { get; }

        void OnUserInput(int x, int y);

        UniTask MakeMoveAsync();
    }
}