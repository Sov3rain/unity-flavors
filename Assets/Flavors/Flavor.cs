using UnityEngine;

[CreateAssetMenu(fileName = "Flavor", menuName = "Flavors/Flavor")]
public sealed class Flavor : ScriptableObject
{
    public string ProductName;
    public string BundleVersion;
    public string BundleIdentifier;
    public Texture2D Icon;
}