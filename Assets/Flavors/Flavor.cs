using UnityEngine;

[CreateAssetMenu(fileName = "Flavor", menuName = "Flavors/Flavor")]
public class Flavor : ScriptableObject
{
    public string ProductName;
    public string BundleVersion;
    public string BundleIdentifier;
}