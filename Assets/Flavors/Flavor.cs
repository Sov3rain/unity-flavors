using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Flavor", menuName = "Flavors/Flavor")]
public sealed class Flavor : ScriptableObject
{
    [Header("Build Settings")]
    public string ProductName;
    public string BundleVersion;
    public string BundleIdentifier;
    public Texture2D Icon;

    [Header("Runtime Settings")]
    [SerializeField]
    private List<FlavorProperty> Properties;

    public string GetString(string key, string defaultValue = "")
    {
        return GetProperty<string>(key, defaultValue);
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        return GetProperty<int>(key, defaultValue);
    }

    public float GetFloat(string key, float defaultValue = 0f)
    {
        return GetProperty<float>(key, defaultValue);
    }

    private T GetProperty<T>(string key, T defaultValue = default)
    {
        if (Properties == null)
            return defaultValue;

        var prop = Properties.Find(kvp => kvp.Key == key);

        if (prop == null)
            return defaultValue;

        if (typeof(T) == typeof(string))
            return (T)(object)prop.Value;
        else if (typeof(T) == typeof(int))
            return (T)(object)int.Parse(prop.Value);
        else if (typeof(T) == typeof(float))
            return (T)(object)float.Parse(prop.Value);

        return defaultValue;
    }

    [Serializable]
    public class FlavorProperty
    {
        public string Key;
        public string Value;
    }
}

