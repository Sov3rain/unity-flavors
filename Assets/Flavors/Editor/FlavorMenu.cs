using UnityEngine;
using UnityEditor;
using System.Linq;

public class FlavorMenu : EditorWindow
{
    [MenuItem("Flavors/Create Flavor Manager", false, 0)]
    private static void CreateFlavorManager()
    {
        ScriptableObjectUtils.CreateAssetInResources<FlavorManager>("Flavors/FlavorManager");
    }

    [MenuItem("Flavors/Create Flavor Manager", true)]
    private static bool ValidateCreateFlavorManager()
    {
        return !Resources.LoadAll<FlavorManager>("/")?.Any() ?? false;
    }

    [MenuItem("Flavors/Create Flavor", false, 20)]
    private static void CreateFlavor()
    {
        var newFlavor = CreateFlavorInternal();
        Selection.activeObject = newFlavor;
    }

    [MenuItem("Flavors/Select Flavor", false, 21)]
    private static void SelectFlavor()
    {
        var menu = GetWindow<FlavorMenu>(utility: true, title: "Select Flavor");
        menu.ShowModalUtility();
    }

    [MenuItem("Flavors/Select Flavor", true)]
    private static bool ValidateSelectFlavor()
    {
        return FlavorManager.Instance != null;
    }

    [MenuItem("Flavors/Apply Current Flavor", false, 22)]
    private static void ApplyCurrentFlavor()
    {
        FlavorManager.Instance.ApplyCurrentFlavor();
    }

    [MenuItem("Flavors/Apply Current Flavor", true)]
    private static bool ValidateApplyCurrentFlavor()
    {
        if (!FlavorManager.Instance) return false;
        return FlavorManager.Instance.Current != null;
    }

    [MenuItem("Flavors/Refresh Define Symbols", false, 100)]
    private static void RefreshDefineSymbols()
    {
        FlavorManager.SetDefineSymbols();
    }

    private static Flavor CreateFlavorInternal()
    {
        var newFlavor = ScriptableObjectUtils.CreateAssetInResources<Flavor>("Flavors/New Flavor");
        return newFlavor;
    }

    private void OnGUI()
    {
        var flavors = Resources.LoadAll<Flavor>("Flavors");

        var labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.richText = true;

        GUILayout.Space(10);
        GUILayout.Label("<size=14><b>Select a flavor to apply</b></size>", labelStyle);
        GUILayout.Label("", GUI.skin.horizontalSlider);
        GUILayout.Space(20);

        if (flavors.Length == 0)
        {
            GUILayout.Label("No flavors found in Resources/Flavors folder");
            GUILayout.Space(20);

            if (GUILayout.Button("Create Flavor", GUILayout.Height(30)))
            {
                var newFlavor = CreateFlavorInternal();
                FlavorManager.Instance.SetCurrentFlavor(newFlavor);
                EditorUtility.SetDirty(FlavorManager.Instance);
                Selection.activeObject = newFlavor;
                Close();
            }

            return;
        }

        foreach (var flavor in flavors)
        {
            var isCurrent = FlavorManager.Instance.IsCurrentFlavor(flavor);
            string buttonLabel = isCurrent
                ? flavor.name + " (current)"
                : flavor.name;

            EditorGUI.BeginDisabledGroup(isCurrent);

            if (GUILayout.Button(buttonLabel, GUILayout.Height(30)))
            {
                FlavorManager.Instance.SetCurrentFlavor(flavor);
                EditorUtility.SetDirty(FlavorManager.Instance);
                Close();
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}