using TicTacToe.Enum;
using TicTacToe.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown gameTypeDropdown;
        [SerializeField] private TMP_Dropdown difficultyDropdown;
        [SerializeField] private Button playButton;

        private EGameType selectedEGameType;
        private EDifficulty selectedDifficulty;

        private void Start()
        {
            gameTypeDropdown.onValueChanged.AddListener(OnGameTypeChanged);
            difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);

            playButton.onClick.AddListener(OnPlayClicked);

            OnGameTypeChanged(0);
            OnDifficultyChanged(0);
        }

        private void OnGameTypeChanged(int index)
        {
            selectedEGameType = (EGameType)index;

            difficultyDropdown.gameObject.SetActive(selectedEGameType != EGameType.PlayerVsPlayer);
        }

        private void OnDifficultyChanged(int index) => selectedDifficulty = (EDifficulty)index;

        private void OnPlayClicked() => StartGame(selectedEGameType, selectedDifficulty);

        private void StartGame(EGameType gameType, EDifficulty difficulty)
        {
            var gameSettings = ServiceLocator.GetOrCreateService<GameSettings>();
            gameSettings.Initialize(gameType, difficulty);
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}