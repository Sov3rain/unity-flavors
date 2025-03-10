using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Flavor : ScriptableObject
{
    [Header("Build Settings")]
    public string ProductName;
    public string BundleVersion;
    public string BundleIdentifier;
    public Texture2D Icon;

    [Header("Runtime Settings")]
    [SerializeField]
    private FlavorProperty[] Properties;

    public IEnumerable<FlavorProperty> GetProperties() => Properties;

    [Serializable]
    public class FlavorProperty
    {
        public string Key;
        public string Value;
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (FlavorManager.Instance && FlavorManager.Instance.IsCurrentFlavor(this))
        {
            FlavorManager.Instance.ApplyCurrentFlavor();
        }
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy Flavor");
    }
#endif
}

