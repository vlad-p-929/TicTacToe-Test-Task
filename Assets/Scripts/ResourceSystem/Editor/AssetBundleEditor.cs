using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.AssetManager.Editor
{
    /// <summary>
    /// According to test task, they've asked to create a Custom Editor view where you can specify some objects
    /// and create Asset Bundle from them. So this specific piece of code only for demonstration.
    /// </summary>
    public class AssetBundleEditor : EditorWindow
    {
        private Object xSymbol;
        private Object oSymbol;
        private Object background;
        private string bundleName;

        private const string AssetBundleBuilder = "Asset Bundle Builder";
        private const string AssetBundleNameField = "Asset Bundle Name";
        private const string SymbolX = "X Symbol";
        private const string SymbolO = "O Symbol";
        private const string Background = "Background";

        [MenuItem("Tools/Asset Bundle Builder")]
        public static void ShowWindow()
        {
            var window = GetWindow<AssetBundleEditor>();
            window.titleContent = new GUIContent(AssetBundleBuilder);
            window.minSize = new Vector2(400, 300);
            window.Show();
        }

        #region GUI

        public void CreateGUI()
        {
            VisualElement root = rootVisualElement;

            var titleLabel = new Label(AssetBundleBuilder)
            {
                style = { unityFontStyleAndWeight = FontStyle.Bold, fontSize = 16, marginBottom = 10 }
            };
            root.Add(titleLabel);

            ObjectField xSymbolField = new ObjectField(SymbolX)
            {
                objectType = typeof(Sprite),
                value = xSymbol
            };
            xSymbolField.RegisterValueChangedCallback(evt => xSymbol = evt.newValue);
            root.Add(xSymbolField);

            ObjectField oSymbolField = new ObjectField(SymbolO)
            {
                objectType = typeof(Sprite),
                value = oSymbol
            };
            oSymbolField.RegisterValueChangedCallback(evt => oSymbol = evt.newValue);
            root.Add(oSymbolField);

            ObjectField backgroundField = new ObjectField(Background)
            {
                objectType = typeof(Texture),
                value = background
            };
            backgroundField.RegisterValueChangedCallback(evt => background = evt.newValue);
            root.Add(backgroundField);

            TextField bundleNameField = new TextField(AssetBundleNameField)
            {
                value = bundleName
            };
            bundleNameField.RegisterValueChangedCallback(evt => bundleName = evt.newValue);
            root.Add(bundleNameField);

            Button buildButton = new Button(() => BuildAssetBundle())
            {
                text = "Build Asset Bundle"
            };
            root.Add(buildButton);
        }

        #endregion

        #region Asset Bundles

        private void BuildAssetBundle()
        {
            if (string.IsNullOrEmpty(bundleName))
            {
                Debug.LogError("Asset Bundle Name cannot be empty.");
                return;
            }

            if (bundleName.EndsWith(".manifest"))
            {
                Debug.LogError("Asset Bundle Name cannot end with '.manifest'.");
                return;
            }

            string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath);
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            ResetAssetBundleNames();

            string assetBundlePath = Path.Combine(assetBundleDirectory, bundleName);

            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
            buildMap[0] = new AssetBundleBuild
            {
                assetBundleName = GetUniqueBundleName(bundleName),
                assetNames = new[]
                {
                    AssetDatabase.GetAssetPath(xSymbol),
                    AssetDatabase.GetAssetPath(oSymbol),
                    AssetDatabase.GetAssetPath(background)
                }
            };

            try
            {
                BuildPipeline.BuildAssetBundles(assetBundleDirectory, buildMap, BuildAssetBundleOptions.None,
                    BuildTarget.StandaloneWindows);
                Debug.Log($"Asset bundle {bundleName} built successfully at {assetBundlePath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to build Asset Bundle: {e.Message}");
            }
        }

        private string GetUniqueBundleName(string bundleName)
        {
            if (bundleName.EndsWith(".manifest"))
            {
                bundleName = bundleName.Replace(".manifest", "");
            }

            return bundleName;
        }

        private void ResetAssetBundleNames()
        {
            var assetImporterX = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(xSymbol));
            if (assetImporterX != null) assetImporterX.assetBundleName = null;

            var assetImporterO = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(oSymbol));
            if (assetImporterO != null) assetImporterO.assetBundleName = null;

            var assetImporterBackground = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(background));
            if (assetImporterBackground != null) assetImporterBackground.assetBundleName = null;
        }

        #endregion
    }
}