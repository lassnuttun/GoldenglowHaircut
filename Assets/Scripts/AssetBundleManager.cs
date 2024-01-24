using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager
{
    private static AssetBundle assetBundle = null;

    private static Dictionary<string, AssetBundle> DicAssetBundle = new Dictionary<string, AssetBundle>();

    public static T LoadResource<T>(string assetBundleName, string assetBundleGroupName) where T : Object
    {
        if (string.IsNullOrEmpty(assetBundleGroupName))
        {
            return default(T);
        }

        if (!DicAssetBundle.TryGetValue(assetBundleGroupName, out assetBundle)) 
        {
            assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + assetBundleGroupName);
            DicAssetBundle.Add(assetBundleGroupName, assetBundle);
        }

        return assetBundle.LoadAsset<T>(assetBundleName);
    }

    public static void UnloadResource(string assetBundleGroupName)
    {
        if (DicAssetBundle.TryGetValue(assetBundleGroupName, out assetBundle))
        {
            assetBundle.Unload(true);
            if (assetBundle != null)
            {
                assetBundle = null;
            }
            DicAssetBundle.Remove(assetBundleGroupName);
            // Resources.UnloadUnusedAssets();
        }
    }
}
