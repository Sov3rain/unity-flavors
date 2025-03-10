using UnityEngine;

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
                _instance = Resources.Load<FlavorManager>("FlavorManager");
            }

            return _instance;
        }
    }

    [SerializeField]
    private Flavor _currentFlavor;

    public Flavor GetCurrentFlavor()
    {
        return _currentFlavor;
    }

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

    private static void ApplyFlavor(Flavor flavor)
    {
#if UNITY_EDITOR
        if (!string.IsNullOrEmpty(flavor.ProductName))
            PlayerSettings.productName = flavor.ProductName;

        if (!string.IsNullOrEmpty(flavor.BundleVersion))
            PlayerSettings.bundleVersion = flavor.BundleVersion;

        if (!string.IsNullOrEmpty(flavor.BundleIdentifier))
        {
            PlayerSettings.SetApplicationIdentifier(
                targetGroup: EditorUserBuildSettings.selectedBuildTargetGroup,
                identifier: flavor.BundleIdentifier
            );
        }

        SetDefineSymbols();
#endif
    }

#if UNITY_EDITOR
    public static void SetDefineSymbols()
    {
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup);

        // Remove existing FLAVOR_ symbols
        defines = System.Text.RegularExpressions.Regex.Replace(defines, @"FLAVOR_\w+;?", "");

        if (FlavorManager.Instance && FlavorManager.Instance.GetCurrentFlavor() != null)
        {
            string symbol = $"FLAVOR_{FlavorManager.Instance.GetCurrentFlavor().name.ToUpper()}";

            // Add new symbol
            if (!string.IsNullOrEmpty(defines) && !defines.EndsWith(";"))
            {
                defines += ";";
            }

            defines += symbol;
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup, defines);
    }
#endif
}