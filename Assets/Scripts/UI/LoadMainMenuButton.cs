using UnityEngine;

namespace TicTacToe.UI
{
    public class LoadMainMenuButton : MonoBehaviour
    {
        public void LoadMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}