using UnityEditor;

[InitializeOnLoad]
public class FlavorEditorHelpers
{
    static FlavorEditorHelpers()
    {
        if(!FlavorManager.Instance) return;

        FlavorManager.Instance.ApplyCurrentFlavor();
    }
}