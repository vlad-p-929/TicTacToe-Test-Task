using DG.Tweening;
using TicTacToe.UI.Interface;
using UnityEngine.UI;

namespace TicTacToe.UI.Adapter
{
    public class UIButtonAdapter : IUIButton
    {
        private readonly Button button;

        public UIButtonAdapter(Button button)
        {
            this.button = button;
        }

        public void AddListener(System.Action callback)
        {
            button.onClick.AddListener(() => callback());
        }

        public void SetActive(bool isActive)
        {
            button.gameObject.SetActive(isActive);
        }

        public void ShakePosition(float duration, float strength)
        {
            button.transform.DOShakePosition(duration, strength);
        }

        public void ShakeScale(float duration, float strength)
        {
            button.transform.DOShakeScale(duration, strength);
        }
    }
}