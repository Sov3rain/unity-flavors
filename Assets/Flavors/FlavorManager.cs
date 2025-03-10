using UnityEngine;
using System.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class FlavorManager : ScriptableObject
{
    private static FlavorManager _instance;
    public static FlavorManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = Resources.LoadAll<FlavorManager>("/").FirstOrDefault();
            }

            return _instance;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        // Cache current flavor properties
        if (FlavorManager.Instance && Instance.Current != null)
        {
            foreach (var prop in Instance.Current.GetProperties())
            {
                Instance._props[prop.Key] = prop.Value;
            }
        }
    }

    [SerializeField]
    private Flavor _currentFlavor;

    private readonly Dictionary<string, string> _props = new();

    public Flavor Current => _currentFlavor;

    public void SetCurrentFlavor(Flavor flavor)
    {
        _currentFlavor = flavor;
        ApplyFlavor(flavor);
    }

    public void ApplyCurrentFlavor()
    {
        ApplyFlavor(_currentFlavor);
    }

    public bool IsCurrentFlavor(Flavor flavor)
    {
        return _currentFlavor == flavor;
    }

    public bool IsCurrentFlavor(string flavorName)
    {
        return _currentFlavor == null ? false : _currentFlavor.name == flavorName;
    }

    public string GetString(string key, string defaultValue = "")
    {
        return _props.TryGetValue(key, out var value) ? value : defaultValue;
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        return _props.TryGetValue(key, out var value) ? int.Parse(value) : defaultValue;
    }

    public float GetFloat(string key, float defaultValue = 0f)
    {
        return _props.TryGetValue(key, out var value) ? float.Parse(value) : defaultValue;
    }


    private void ApplyFlavor(Flavor flavor)
    {
#if UNITY_EDITOR
        BuildTargetGroup targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

        if (!string.IsNullOrEmpty(flavor.ProductName))
            PlayerSettings.productName = flavor.ProductName;

        if (!string.IsNullOrEmpty(flavor.BundleVersion))
            PlayerSettings.bundleVersion = flavor.BundleVersion;

        if (!string.IsNullOrEmpty(flavor.BundleIdentifier))
        {
            PlayerSettings.SetApplicationIdentifier(
                targetGroup,
                identifier: flavor.BundleIdentifier
            );
        }

        if (flavor.Icon)
        {
            PlayerSettings.SetIconsForTargetGroup(
                platform: BuildTargetGroup.Unknown,
                icons: new Texture2D[] { flavor.Icon }
            );
        }

        SetDefineSymbols();
#endif
    }

#if UNITY_EDITOR
    public static void SetDefineSymbols()
    {
        BuildTargetGroup targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(targetGroup);

        // Remove existing FLAVOR_ symbols
        defines = System.Text.RegularExpressions.Regex.Replace(defines, @"FLAVOR_\w+;?", "");

        if (FlavorManager.Instance && FlavorManager.Instance.Current != null)
        {
            string symbol = $"FLAVOR_{FlavorManager.Instance.Current.name.ToUpper()}";

            // Add new symbol
            if (!string.IsNullOrEmpty(defines) && !defines.EndsWith(";"))
            {
                defines += ";";
            }

            defines += symbol;
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, defines);
    }
#endif
}