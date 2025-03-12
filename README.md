# Unity Flavors

A flexible package for managing different configurations (flavors) of your Unity application.

## Overview

Unity Flavors provides an easy way to maintain multiple variants of your application from a single codebase. This is particularly useful for:

- Managing different build configurations (development, staging, production)
- White-labeling your application with different branding
- Creating free/premium versions with different features
- Handling region-specific customizations

## Features

- Simple flavor creation and switching
- Automatic define symbols for conditional compilation
- Easy access to flavor-specific properties at runtime
- Editor integration for seamless workflow
- Configurable app identifiers, version numbers, and icons per flavor

## Installation

1. Import the Unity Flavors package into your project
2. Navigate to `Flavors > Create Flavor Manager` in the Unity menu
3. Start creating and using flavors!

## Getting Started

### Creating a Flavor Manager

Before creating any flavors, you need to initialize the Flavor Manager:

1. In the Unity menu, select `Flavors > Create Flavor Manager`
2. This will create a FlavorManager asset in your Resources folder

### Creating Flavors

1. In the Unity menu, select `Flavors > Create Flavor`
2. Enter a name for your flavor (e.g., "Development", "Production", "FreeVersion")
3. Click either "Create" or "Create and set as current"

### Switching Between Flavors

1. In the Unity menu, select `Flavors > Select Flavor`
2. Click on the flavor you want to apply

### Configuring a Flavor

Select a flavor asset in your Project window and configure its properties in the Inspector:

#### Build Settings
- **Product Name**: The name of your application
- **Bundle Version**: The version string of your application
- **Bundle Identifier**: The application identifier (e.g., "com.company.app")
- **Icon**: The application icon

#### Runtime Settings
- **Properties**: Key-value pairs for flavor-specific runtime configuration

## Usage Examples

### Using Preprocessor Directives

Flavors automatically create preprocessor directives based on the flavor name in the format `FLAVOR_FLAVORNAME`. You can use these to conditionally compile code:

```csharp
// This code will only compile when the "Development" flavor is active
#if FLAVOR_DEVELOPMENT
    Debug.Log("Running in development flavor");
    // Enable development-only features
#endif

// Different code paths for different flavors
#if FLAVOR_PREMIUM
    // Premium version features
    EnablePremiumFeatures();
#elif FLAVOR_FREE
    // Free version features
    ShowAds();
#endif

// Combining with other preprocessor directives
#if UNITY_ANDROID && FLAVOR_CHINA
    // China-specific Android code
    InitializeChineseServices();
#endif
```

### Accessing Flavor Properties at Runtime

Define properties in your Flavor asset's inspector, then access them at runtime:

```csharp
using UnityEngine;
using UnityFlavors;

public class ExampleComponent : MonoBehaviour
{
    void Start()
    {
        // Get string properties
        string apiUrl = FlavorManager.Instance.GetString("ApiUrl", "https://default-api.com");
        string welcomeMessage = FlavorManager.Instance.GetString("WelcomeMessage", "Hello!");
        
        // Get numeric properties
        int maxUsers = FlavorManager.Instance.GetInt("MaxUsers", 10);
        float cooldownTime = FlavorManager.Instance.GetFloat("CooldownTime", 5.0f);
        
        // Use the properties
        Debug.Log($"Welcome message: {welcomeMessage}");
        Debug.Log($"API URL: {apiUrl}");
        Debug.Log($"Max users: {maxUsers}");
        Debug.Log($"Cooldown: {cooldownTime}s");
    }
}
```

### Checking Current Flavor

You can check which flavor is currently active:

```csharp
using UnityEngine;
using UnityFlavors;

public class FlavorChecker : MonoBehaviour
{
    void Start()
    {
        if (FlavorManager.Instance.IsCurrentFlavor("Development"))
        {
            Debug.Log("Running Development flavor");
        }
        
        // Display current flavor name
        Debug.Log($"Current flavor: {FlavorManager.Instance.Current.name}");
    }
}
```

## Best Practices

1. **Create a default flavor** - Always have a default flavor with sensible defaults
2. **Use common property keys** - Maintain consistency in property keys across flavors
3. **Organize flavor assets** - Keep flavor assets in a dedicated folder
4. **Document your flavors** - Keep notes on what each flavor is for
5. **Test all flavors** - Regularly test builds with different flavors

## Troubleshooting

### Define Symbols Not Working

If preprocessor directives aren't working:

1. Select `Flavors > Refresh Define Symbols` from the menu
2. Ensure your flavor name doesn't contain special characters
3. Check if the flavor is properly set as current

### Properties Not Found

If your properties aren't accessible at runtime:

1. Make sure the FlavorManager asset is in the Resources folder
2. Verify that the property keys match exactly (case-sensitive)
3. Check that you've properly set a current flavor
