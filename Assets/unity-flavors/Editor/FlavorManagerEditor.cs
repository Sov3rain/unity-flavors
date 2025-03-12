using UnityEditor;

[CustomEditor(typeof(FlavorManager))]
public class FlavorManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginDisabledGroup(true);
        base.OnInspectorGUI();
        EditorGUI.EndDisabledGroup();
    }
}