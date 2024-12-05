using UnityEngine;

namespace TicTacToe.UI.Interface
{
    public interface IUIImage
    {
        void SetActive(bool isActive);
        void Fade(float alpha, float duration);
        void PunchScale(Vector3 scale, float duration);
        void SetParent(Transform parent);
        void SetLocalPosition(Vector3 position);
        Transform GetTransform();
    }
}