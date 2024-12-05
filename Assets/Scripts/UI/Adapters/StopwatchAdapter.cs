namespace TicTacToe.UI.Adapter
{
    public class StopwatchAdapter
    {
        private readonly StopwatchWidget stopwatchWidget;

        public StopwatchAdapter(StopwatchWidget stopwatchWidget)
        {
            this.stopwatchWidget = stopwatchWidget;
        }

        public void StartTimer()
        {
            stopwatchWidget.StartTimer();
        }

        public void StopTimer()
        {
            stopwatchWidget.StopTimer();
        }

        public void ResetTimer()
        {
            stopwatchWidget.ResetTimer();
        }
    }
}