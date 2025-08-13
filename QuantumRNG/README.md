# Quantum Random Number Generator SDK for Unity

True quantum randomness for your Unity games, powered by NIST-certified quantum sources.

## 🚀 Quick Start

### 1. Get Your API Key
Visit [https://your-quantum-api.com](https://your-quantum-api.com) to create a free account and get your API key.

### 2. Initialize the SDK
```csharp
using QuantumRNG;

void Start() {
    QuantumRandom.Initialize("your_api_key_here");
}
```

### 3. Generate Quantum Random Numbers
```csharp
StartCoroutine(QuantumRandom.GetIntegers(10,
    (numbers) => {
        Debug.Log($"Got {numbers.Count} quantum random numbers!");
        // Use numbers in your game logic
    },
    (error) => {
        Debug.LogError($"Error: {error}");
    }
));
```

## 📖 API Reference

### Core Methods

#### `QuantumRandom.Initialize(string apiKey)`
Initialize the SDK with your API key. Call this once at the start of your game.

#### `QuantumRandom.GetIntegers(int count, Action<List<int>> onSuccess, Action<string> onError)`
Generate a list of quantum random integers.
- `count`: Number of integers to generate (1-1000)
- `onSuccess`: Callback with the list of random numbers
- `onError`: Callback with error message if request fails

#### `QuantumRandom.GetIntegerInRange(int min, int max, Action<int> onSuccess, Action<string> onError)`
Generate a single quantum random integer within a specific range.
- `min`: Minimum value (inclusive)
- `max`: Maximum value (inclusive)

#### `QuantumRandom.GetFloat(Action<float> onSuccess, Action<string> onError)`
Generate a quantum random float between 0.0 and 1.0.

## 🎮 Game Use Cases

### Loot Generation
```csharp
StartCoroutine(QuantumRandom.GetIntegerInRange(1, 100,
    (roll) => {
        if (roll >= 95) SpawnLegendaryItem();
        else if (roll >= 80) SpawnEpicItem();
        // etc...
    },
    (error) => Debug.LogError(error)
));
```

### Critical Hit Calculation
```csharp
StartCoroutine(QuantumRandom.GetFloat(
    (chance) => {
        bool isCritical = chance > 0.9f; // 10% crit chance
        ApplyDamage(baseDamage * (isCritical ? 2.0f : 1.0f));
    },
    (error) => Debug.LogError(error)
));
```

### Procedural Generation
```csharp
StartCoroutine(QuantumRandom.GetIntegers(100,
    (seeds) => {
        GenerateLevel(seeds); // Use quantum seeds for true randomness
    },
    (error) => Debug.LogError(error)
));
```

## 🔧 Error Handling

The SDK provides detailed error messages for common issues:
- Invalid API key
- Network connectivity problems
- Rate limiting
- Service unavailability

Always implement the `onError` callback to handle these gracefully.

## 📝 Example Scene

Check the `Examples/` folder for `QuantumRandomExample.cs` - a complete working example showing:
- Basic quantum number generation
- Loot rarity systems
- Damage multipliers
- Error handling

## 🌐 Why Quantum Randomness?

Traditional pseudo-random number generators are predictable and can be exploited. Quantum random numbers are:
- **Truly random** - Based on quantum mechanical processes
- **Unpredictable** - Cannot be reverse-engineered or predicted
- **Certified** - Sourced from NIST quantum beacons
- **Fair** - Perfect for competitive gaming and blockchain applications

## 📞 Support

- **Documentation**: [https://docs.your-quantum-api.com](https://docs.your-quantum-api.com)
- **Support**: [support@your-quantum-api.com](mailto:support@your-quantum-api.com)
- **Discord**: [Join our developer community](https://discord.gg/quantum-rng)

## 📄 License

This SDK is provided under a commercial license. See your account dashboard for terms.

---

**Get your API key**: [https://your-quantum-api.com](https://your-quantum-api.com)