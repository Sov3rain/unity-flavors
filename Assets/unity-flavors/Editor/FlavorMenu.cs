using UnityEngine;
using UnityEditor;
using System.Linq;

namespace UnityFlavors
{
    public class FlavorMenu : Editor
    {
        [MenuItem("Flavors/Create Flavor Manager", false, 0)]
        private static void CreateFlavorManager()
        {
            var newFlavorManager = ScriptableObject.CreateInstance<FlavorManager>();
            AssetDatabase.CreateAsset(newFlavorManager, "Assets/Resources/FlavorManager.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    
        [MenuItem("Flavors/Create Flavor Manager", true)]
        private static bool ValidateCreateFlavorManager()
        {
            return !AssetDatabase.FindAssets("t:FlavorManager").Any();
        }
    
        [MenuItem("Flavors/Create Flavor", false, 20)]
        private static void CreateFlavor()
        {
            FlavorCreationWindow.ShowWindow();
        }
    
        [MenuItem("Flavors/Select Flavor", false, 21)]
        private static void SelectFlavor()
        {
            FlavorSelectionWindow.ShowWindow();
        }
    
        [MenuItem("Flavors/Select Flavor", true)]
        private static bool ValidateSelectFlavor()
        {
            return FlavorManager.Instance != null;
        }
    
        [MenuItem("Flavors/Apply Current Flavor", false, 100)]
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
    
        [MenuItem("Flavors/Refresh Define Symbols", false, 101)]
        private static void RefreshDefineSymbols()
        {
            FlavorManager.SetDefineSymbols();
        }
    }
}