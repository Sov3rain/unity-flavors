using UnityEngine;
using UnityEditor;
using System.IO;

namespace UnityFlavors
{
    public class FlavorCreationWindow : EditorWindow
    {
        private string _name = "";

        public static void ShowWindow()
        {
            var window = GetWindow<FlavorCreationWindow>(utility: true, title: "Create a new flavor");
            window.ShowModalUtility();
        }

        private void OnGUI()
        {
            var labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.richText = true;

            GUILayout.Space(10);
            GUILayout.Label("<size=14><b>Enter a name for your new flavor:</b></size>", labelStyle);
            GUILayout.Space(5);

            _name = EditorGUILayout.TextField(_name);
            GUILayout.Label("", GUI.skin.horizontalSlider);

            GUILayout.Space(20);

            GUI.enabled = !string.IsNullOrEmpty(_name);

            if (GUILayout.Button("Create", GUILayout.Height(30)))
            {
                var newFlavorName = _name.Trim();
                var newFlavor = CreateNewFlavorAsset(newFlavorName);

                Selection.activeObject = newFlavor;
                Close();
            }

            if (GUILayout.Button("Create and set as current", GUILayout.Height(30)))
            {
                var newFlavorName = _name.Trim();
                var newFlavor = CreateNewFlavorAsset(newFlavorName);

                FlavorManager.Instance.SetCurrentFlavor(newFlavor);
                EditorUtility.SetDirty(FlavorManager.Instance);
                Selection.activeObject = newFlavor;
                Close();
            }

            GUI.enabled = true;
            GUILayout.Label("", GUI.skin.horizontalSlider);
            GUILayout.Space(20);

            var defineSymbol = _name.Replace(" ", "_").ToUpper();
            GUILayout.Label($"<i>Your new define symbol will be 'FLAVOR_{defineSymbol}'</i>", labelStyle);
        }

        private static Flavor CreateNewFlavorAsset(string newFlavorName)
        {
            string dirPath = $"Assets/Flavors";

            if (!AssetDatabase.IsValidFolder(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                AssetDatabase.Refresh();
            }

            Flavor newFlavor = ScriptableObject.CreateInstance<Flavor>();

            AssetDatabase.CreateAsset(newFlavor, $"{dirPath}/{newFlavorName}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return newFlavor;
        }
    }
}