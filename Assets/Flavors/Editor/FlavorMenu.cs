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
        var newFlavor = ScriptableObjectUtils.CreateAssetInResources<Flavor>("Flavors/New Flavor");
        Selection.activeObject = newFlavor;
    }

    [MenuItem("Flavors/Select Flavor", false, 21)]
    private static void SelectFlavor()
    {
        GetWindow<FlavorMenu>("Select Flavor");
    }

    [MenuItem("Flavors/Select Flavor", true)]
    private static bool ValidateSelectFlavor()
    {
        return FlavorManager.Instance != null;
    }

    [MenuItem("Flavors/Apply Current Flavor", false, 40)]
    private static void ApplyCurrentFlavor()
    {
        FlavorManager.Instance.ApplyCurrentFlavor();
    }

    [MenuItem("Flavors/Apply Current Flavor", true)]
    private static bool ValidateApplyCurrentFlavor()
    {
        if (!FlavorManager.Instance) return false;
        return FlavorManager.Instance.GetCurrentFlavor() != null;
    }

    private void OnGUI()
    {
        var flavors = Resources.LoadAll<Flavor>("Flavors");

        GUILayout.Label("Select a flavor to build with:");

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