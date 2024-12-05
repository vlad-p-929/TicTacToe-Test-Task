using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TicTacToe.Enum;
using TicTacToe.UI.Interface;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TicTacToe.View.Cell
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private Button cellButton;
        [SerializeField] private Image cellImage;
        [Inject(Id = "HintMarker")] private IUIImage hintMarker;
        
        public event Action<int, int> OnCellClicked; 

        public int x;
        public int y;
        private Sprite player1Sprite;
        private Sprite player2Sprite;
        
        void Reset()
        {
            this.cellButton = this.GetComponent<Button>();
            this.cellImage = this.GetComponent<Image>();
        }

        public void Initialize(int x, int y, Sprite player1Sprite, Sprite player2Sprite)
        {
            this.x = x;
            this.y = y;
            this.player1Sprite = player1Sprite;
            this.player2Sprite = player2Sprite;

            cellButton.onClick.AddListener(() => OnCellClicked?.Invoke(x,y));
        }
        
        public virtual void OccupyBy(EPlayerId playerId)
        {
            switch (playerId)
            {
                case EPlayerId.X:
                    cellImage.sprite = player1Sprite;
                    cellImage.DOFade(1, 0.1f);
                    cellImage.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
                    cellButton.interactable = false;
                    break;
                case EPlayerId.O:
                    cellImage.sprite = player2Sprite;
                    cellImage.DOFade(1, 0.1f);
                    cellImage.transform.DOPunchScale(Vector3.one * 0.2f, 0.1f);
                    cellButton.interactable = false;
                    break;
                default:
                    cellImage.DOFade(0, 0.1f);
                    ResetCell();
                    break;
            }
        }
        
        public async void ShowHint()
        {
            hintMarker.SetActive(true);
            hintMarker.Fade(1, 0.1f);
            hintMarker.PunchScale(Vector3.one * 0.2f, 2f);
            hintMarker.SetParent(cellImage.transform);
            hintMarker.SetLocalPosition(Vector3.zero);

            await UniTask.Delay(1000);
            
            hintMarker.Fade(0, 1f);
        }

        public virtual void ResetCell()
        {
            cellImage.DOFade(0, 0);
            cellImage.sprite = null;
            cellButton.interactable = true;
        }
    }
}