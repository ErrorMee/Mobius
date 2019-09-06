using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AssetPathUtil
{
    public static string GetWindowAssetPath(string assetName)
    {
        return string.Format("ProductAssets/UIPrefabs/Windows/{0}.prefab", assetName);
    }

}
