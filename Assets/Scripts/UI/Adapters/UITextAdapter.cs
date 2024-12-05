using TicTacToe.UI.Interface;
using TMPro;

namespace TicTacToe.UI.Adapter
{
    public class UITextAdapter : IUIText
    {
        private readonly TMP_Text text;

        public UITextAdapter(TMP_Text text)
        {
            this.text = text;
        }

        public void SetText(string content)
        {
            text.text = content;
        }

        public void SetActive(bool isActive)
        {
            text.gameObject.SetActive(isActive);
        }
    }
}