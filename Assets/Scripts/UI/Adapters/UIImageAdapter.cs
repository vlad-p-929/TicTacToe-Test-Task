using DG.Tweening;
using TicTacToe.UI.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.UI.Adapter
{
    public class UIImageAdapter : IUIImage
    {
        private readonly Image image;

        public UIImageAdapter(Image image)
        {
            this.image = image;
        }

        public void SetActive(bool isActive)
        {
            image.gameObject.SetActive(isActive);
        }

        public void Fade(float alpha, float duration)
        {
            image.DOFade(alpha, duration);
        }

        public void PunchScale(Vector3 scale, float duration)
        {
            image.transform.DOPunchScale(scale, duration);
        }

        public void SetParent(Transform parent)
        {
            image.transform.SetParent(parent);
        }

        public void SetLocalPosition(Vector3 position)
        {
            image.transform.localPosition = position;
        }

        public Transform GetTransform()
        {
            return image.transform;
        }
    }
}