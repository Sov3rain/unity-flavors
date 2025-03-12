using System.IO;
using UnityEditor;
using UnityEngine;

static class ScriptableObjectUtils
{
    public static T CreateAssetInResources<T>(string fileName)
        where T : ScriptableObject
    {
        // Ensure Resources folder exists
        if (!Directory.Exists("Assets/Resources"))
        {
            Directory.CreateDirectory("Assets/Resources");
        }

        // Create the asset
        T asset = ScriptableObject.CreateInstance<T>();

        // Generate the full path
        string fullPath = $"Assets/Resources/{fileName}.asset";

        // Create and save the asset file
        AssetDatabase.CreateAsset(asset, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return asset;
    }
}