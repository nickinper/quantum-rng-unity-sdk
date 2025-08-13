# Quantum RNG Unity SDK

[![Unity Version](https://img.shields.io/badge/Unity-2021.3%2B-blue.svg)](https://unity3d.com/get-unity/download)
[![License](https://img.shields.io/badge/License-Commercial-green.svg)](LICENSE)
[![Downloads](https://img.shields.io/github/downloads/nickinper/quantum-rng-unity-sdk/total.svg)](https://github.com/nickinper/quantum-rng-unity-sdk/releases)

True quantum random number generation for Unity games, powered by NIST-certified quantum sources.

## 🚀 Quick Start

### 1. Download the SDK
Download the latest `.unitypackage` from our [Releases](https://github.com/nickinper/quantum-rng-unity-sdk/releases) section.

### 2. Import into Unity
- Double-click the `.unitypackage` file
- Import all files when prompted
- The SDK will be added to `Assets/QuantumRNG/`

### 3. Get Your API Key
Visit [quantum-random-api.com](https://nickinper.github.io/quantum-random-api) to get your free API key.

### 4. Initialize and Use
```csharp
using QuantumRNG;

void Start() {
    // Initialize with your API key
    QuantumRandom.Initialize("your_api_key_here");
    
    // Generate quantum random numbers
    StartCoroutine(QuantumRandom.GetIntegers(10,
        (numbers) => {
            Debug.Log($"Generated {numbers.Count} quantum random numbers!");
            // Use in your game logic
        },
        (error) => {
            Debug.LogError($"Error: {error}");
        }
    ));
}
```

## 🎮 Perfect for Game Development

### Loot Generation
```csharp
StartCoroutine(QuantumRandom.GetIntegerInRange(1, 100,
    (roll) => {
        if (roll >= 95) SpawnLegendaryItem();      // 5% chance
        else if (roll >= 80) SpawnEpicItem();       // 15% chance
        else if (roll >= 60) SpawnRareItem();       // 20% chance
        else SpawnCommonItem();                     // 60% chance
    },
    (error) => Debug.LogError(error)
));
```

### Critical Hit System
```csharp
StartCoroutine(QuantumRandom.GetFloat(
    (chance) => {
        bool isCritical = chance > 0.9f; // 10% crit chance
        int damage = baseDamage * (isCritical ? 2 : 1);
        ApplyDamage(damage);
    },
    (error) => Debug.LogError(error)
));
```

### Procedural Generation
```csharp
StartCoroutine(QuantumRandom.GetIntegers(100,
    (quantumSeeds) => {
        GenerateLevel(quantumSeeds); // True randomness for level generation
    },
    (error) => Debug.LogError(error)
));
```

## 📖 API Reference

### Core Methods

#### `QuantumRandom.Initialize(string apiKey)`
Initialize the SDK with your API key. **Call this once at startup.**

#### `QuantumRandom.GetIntegers(int count, Action<List<int>> onSuccess, Action<string> onError)`
Generate multiple quantum random integers.
- **count**: Number of integers (1-1000)
- **onSuccess**: Callback with list of random numbers
- **onError**: Callback with error message

#### `QuantumRandom.GetIntegerInRange(int min, int max, Action<int> onSuccess, Action<string> onError)`
Generate a single quantum random integer in a specific range.

#### `QuantumRandom.GetFloat(Action<float> onSuccess, Action<string> onError)`
Generate a quantum random float between 0.0 and 1.0.

### Properties

#### `QuantumRandom.IsInitialized`
Returns `true` if the SDK has been initialized with a valid API key.

## 🌟 Why Quantum Randomness?

| Traditional RNG | Quantum RNG |
|----------------|-------------|
| ❌ Predictable algorithms | ✅ True quantum randomness |
| ❌ Can be reverse-engineered | ✅ Physically unpredictable |
| ❌ Reproducible sequences | ✅ Unique every time |
| ❌ Vulnerable to exploitation | ✅ Cryptographically secure |

Perfect for:
- **Competitive Gaming** - Provably fair randomness
- **Blockchain Games** - Verifiable random events
- **High-Stakes Systems** - Casino-grade randomness
- **Procedural Generation** - Unique, unrepeatable content

## 🔧 Requirements

- **Unity**: 2021.3 or newer
- **Platform**: All Unity-supported platforms
- **Internet**: Required for API calls
- **API Key**: Free tier available at [quantum-random-api.com](https://nickinper.github.io/quantum-random-api)

## 📝 Examples

Check out the included `Examples/QuantumRandomExample.cs` for complete working examples of:
- Basic quantum number generation
- Game mechanics integration
- Error handling best practices
- Performance optimization

## 🐛 Issues & Support

- **Bug Reports**: [GitHub Issues](https://github.com/nickinper/quantum-rng-unity-sdk/issues)
- **Feature Requests**: [GitHub Discussions](https://github.com/nickinper/quantum-rng-unity-sdk/discussions)
- **Documentation**: [API Documentation](https://nickinper.github.io/quantum-random-api/docs)
- **Email Support**: [support@quantum-random-api.com](mailto:support@quantum-random-api.com)

## 📄 License

This SDK is provided under a commercial license. See [LICENSE](LICENSE) for details.

## 🌐 Related

- **Website**: [quantum-random-api.com](https://nickinper.github.io/quantum-random-api)
- **API Documentation**: [docs.quantum-random-api.com](https://nickinper.github.io/quantum-random-api/docs)
- **Unity Asset Store**: [Coming Soon]

---

**Get started today**: [Download SDK](https://github.com/nickinper/quantum-rng-unity-sdk/releases) • [Get API Key](https://nickinper.github.io/quantum-random-api)