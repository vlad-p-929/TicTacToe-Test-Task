using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace TicTacToe.UI
{
    public class StopwatchWidget : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private bool isRunning;
        private float elapsedTime;

        private void Reset() => timerText = this.GetComponent<TMP_Text>();

        public void StartTimer()
        {
            if (!isRunning)
            {
                isRunning = true;
                UpdateTimer().Forget();
            }
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void ResetTimer()
        {
            elapsedTime = 0f;
            timerText.text = "0.00";
        }

        private async UniTaskVoid UpdateTimer()
        {
            while (isRunning)
            {
                elapsedTime += Time.deltaTime;
                timerText.text = elapsedTime.ToString("F2");
                await UniTask.Yield();
            }
        }
    }
}