using UnityEditor;

[InitializeOnLoad]
public class FlavorEditorHelpers
{
    static FlavorEditorHelpers()
    {
        FlavorManager.SetDefineSymbols();
    }
}