using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YIUIFramework;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Object = UnityEngine.Object;

namespace GameFramework
{
    public sealed class GameMgr : MonoSingleton<GameMgr>
    {
        [SerializeField] private bool openFirstPanelOnStart = true;
        [SerializeField] private string firstPanelResName = "GameStartPanel";

        private void Awake()
        {
            SingletonMgr.Initialize();
            InitLoadDI();
        }

        private async void Start()
        {
            await MgrCenter.Inst.Register(PanelMgr.Inst);

            if (openFirstPanelOnStart && !string.IsNullOrEmpty(firstPanelResName))
            {
                PanelMgr.Inst.OpenPanel(firstPanelResName);
            }
        }

        private static void InitLoadDI()
        {

#if UNITY_EDITOR
            YIUILoadDI.LoadAssetFunc = LoadAssetEditor;
            YIUILoadDI.LoadAssetAsyncFunc = LoadAssetAsyncEditor;
            YIUILoadDI.VerifyAssetValidityFunc = VerifyAssetEditor;
#else
            Debug.LogError("YIUILoadDI not configured. Set loaders for runtime.");
#endif
        }

#if UNITY_EDITOR
        private static (Object, int) LoadAssetEditor(string pkgName, string resName, Type assetType)
        {
            var obj = TryLoadFromYIUI(pkgName, resName, assetType) ??
                      FindAsset(resName, assetType, GetSearchRoot(pkgName));
            return (obj, 0);
        }

        private static UniTask<(Object, int)> LoadAssetAsyncEditor(string pkgName, string resName, Type assetType)
        {
            return UniTask.FromResult(LoadAssetEditor(pkgName, resName, assetType));
        }

        private static bool VerifyAssetEditor(string pkgName, string resName)
        {
            var searchRoot = GetSearchRoot(pkgName);
            var guids = AssetDatabase.FindAssets(resName, new[] { searchRoot });
            if (guids != null && guids.Length > 0)
            {
                return true;
            }

            if (!string.IsNullOrEmpty(pkgName))
            {
                guids = AssetDatabase.FindAssets(resName, new[] { "Assets/GameRes" });
                return guids != null && guids.Length > 0;
            }

            return false;
        }

        private static Object TryLoadFromYIUI(string pkgName, string resName, Type assetType)
        {
            if (string.IsNullOrEmpty(pkgName) || string.IsNullOrEmpty(resName))
            {
                return null;
            }

            if (assetType == typeof(GameObject))
            {
                var prefabPath =
                    $"{UIStaticHelper.UIProjectResPath}/{pkgName}/{UIStaticHelper.UIPrefabs}/{resName}.prefab";
                var prefab = AssetDatabase.LoadAssetAtPath(prefabPath, assetType);
                if (prefab != null)
                {
                    return prefab;
                }
            }

            return FindAsset(resName, assetType, GetSearchRoot(pkgName));
        }

        private static Object FindAsset(string resName, Type assetType, string searchRoot)
        {
            if (string.IsNullOrEmpty(resName))
            {
                return null;
            }

            var filter = $"t:{assetType.Name} {resName}";
            var guids = AssetDatabase.FindAssets(filter, new[] { searchRoot });
            if (guids == null || guids.Length == 0)
            {
                return null;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath(path, assetType);
        }

        private static string GetSearchRoot(string pkgName)
        {
            if (string.IsNullOrEmpty(pkgName))
            {
                return "Assets/GameRes";
            }

            return $"{UIStaticHelper.UIProjectResPath}/{pkgName}";
        }
#endif
    }
}
