using UnityEngine;
using UnityEditor;
using System.Linq;

public class FlavorSelectionWindow : EditorWindow
{
    public static void ShowWindow()
    {
        var menu = GetWindow<FlavorSelectionWindow>(utility: true, title: "Choose a Flavor");
        menu.ShowModalUtility();
    }

    private void OnGUI()
    {
        string[] guids = AssetDatabase.FindAssets("t:Flavor");
        Flavor[] flavors = guids.Select(guid =>
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<Flavor>(path);
        }).ToArray();

        var labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.richText = true;

        GUILayout.Space(10);
        GUILayout.Label("<size=14><b>Choose a flavor to apply</b></size>", labelStyle);
        GUILayout.Label("", GUI.skin.horizontalSlider);
        GUILayout.Space(20);

        if (flavors.Length == 0)
        {
            GUILayout.Label("No flavors found, create one using the menu item 'Flavors/Create Flavor'", labelStyle);
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