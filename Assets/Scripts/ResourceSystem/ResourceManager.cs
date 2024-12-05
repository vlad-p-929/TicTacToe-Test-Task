using System.IO;
using TicTacToe.AssetManager.Interface;
using UnityEngine;

namespace TicTacToe.AssetManager
{
    public class ResourceManager : IResourceManager
    {
        public bool TryLoad<T>(string name, out T resource) where T : UnityEngine.Object
        {
            resource = null;

            AssetBundle bundle = null;
            foreach (var loadedBundle in AssetBundle.GetAllLoadedAssetBundles())
            {
                if (loadedBundle.name == CONSTS.AssetBundleName)
                {
                    bundle = loadedBundle;
                    break;
                }
            }

            if (bundle == null)
            {
                string bundlePath = Path.Combine(Application.streamingAssetsPath, CONSTS.AssetBundleName);
                bundle = AssetBundle.LoadFromFile(bundlePath);

                if (bundle == null)
                {
                    Debug.LogError($"Failed to load AssetBundle at path: {bundlePath}");
                    return false;
                }
            }

            resource = bundle.LoadAsset<T>(name);

            if (resource == null)
            {
                Debug.LogError($"Failed to load resource '{name}' from AssetBundle.");
                return false;
            }

            Debug.Log($"Successfully loaded resource '{name}' of type {typeof(T).Name} from AssetBundle.");
            return true;
        }
    }
}