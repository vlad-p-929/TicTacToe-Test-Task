namespace TicTacToe.UI.Interface
{
    public interface IUIButton
    {
        void AddListener(System.Action callback);
        void SetActive(bool isActive);
        void ShakePosition(float duration, float strength);
        void ShakeScale(float duration, float strength);
    }
}