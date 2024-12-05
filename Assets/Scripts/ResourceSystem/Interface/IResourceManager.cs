namespace TicTacToe.AssetManager.Interface
{
    public interface IResourceManager
    {
        bool TryLoad<T>(string name, out T resource) where T : UnityEngine.Object;
    }
}