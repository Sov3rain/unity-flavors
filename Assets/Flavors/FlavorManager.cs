using System.Linq;
using UnityEngine;
using System;

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
            EnsureInstance();
            return _instance;
        }
    }

    [SerializeField]
    private Flavor _currentFlavor;

    public Flavor GetCurrentFlavor()
    {
        EnsureCurrentFlavor();
        return _currentFlavor;
    }

    public void SetCurrentFlavor(Flavor flavor)
    {
        _currentFlavor = flavor;
        ApplyFlavor(flavor);
    }

    public void ApplyCurrentFlavor()
    {
        EnsureCurrentFlavor();
        ApplyFlavor(_currentFlavor);
    }

    public bool IsCurrentFlavor(Flavor flavor)
    {
        EnsureCurrentFlavor();
        return _currentFlavor == flavor;
    }

    private static void EnsureInstance()
    {
        if (!_instance)
        {
            _instance = Resources.LoadAll<FlavorManager>("/").FirstOrDefault();
        }
    }

    private static void EnsureCurrentFlavor()
    {
        EnsureInstance();

        if (!Instance.GetCurrentFlavor())
        {
            var flavor = Resources.LoadAll<Flavor>("Flavors").FirstOrDefault();
            Instance.SetCurrentFlavor(flavor);
        }

        if (!Instance.GetCurrentFlavor())
        {
            throw new Exception("No Flavors found, please create at least one Flavor before using FlavorManager");
        }
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
    private static void SetDefineSymbols()
    {
        if (!Instance.GetCurrentFlavor())
            return;

        string symbol = $"FLAVOR_{Instance.GetCurrentFlavor().name.ToUpper()}";
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup);

        // Remove existing FLAVOR_ symbols
        defines = System.Text.RegularExpressions.Regex.Replace(defines, @"FLAVOR_\w+;?", "");

        // Add new symbol
        if (!string.IsNullOrEmpty(defines) && !defines.EndsWith(";"))
        {
            defines += ";";
        }

        defines += symbol;

        PlayerSettings.SetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup, defines);
    }
#endif
}