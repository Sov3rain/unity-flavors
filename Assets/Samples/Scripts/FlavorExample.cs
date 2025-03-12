using UnityEngine;
using UnityFlavors;

public class FlavorExample : MonoBehaviour
{
    void Start()
    {
        Flavor flavor = FlavorManager.Instance.Current;
        string baseUrl = FlavorManager.Instance.GetString("baseUrl");
        int logLevel = FlavorManager.Instance.GetInt("logLevel");

        Debug.Log($"Base URL: {baseUrl}");
        Debug.Log($"Log Level: {logLevel}");

        PreprocessorExample();
    }

    private void PreprocessorExample()
    {
#if FLAVOR_DEVELOPMENT
        Debug.Log("Development");
#elif FLAVOR_PRODUCTION
        Debug.Log("Production");
#endif
    }
}