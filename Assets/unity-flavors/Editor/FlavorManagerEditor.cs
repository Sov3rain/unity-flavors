using UnityEditor;
using UnityEngine;

namespace UnityFlavors
{
    [CustomEditor(typeof(FlavorManager))]
    public class FlavorManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Flavor Manager needs to be placed in a Resources folder.", MessageType.Warning);
    
            EditorGUILayout.HelpBox("You can't edit the Flavor Manager directly, instead use the menu items 'Flavors' instead.", MessageType.Info);
    
            EditorGUILayout.Space(10);
    
            EditorGUI.BeginDisabledGroup(true);
            base.OnInspectorGUI();
            EditorGUI.EndDisabledGroup();
        }
    }
}