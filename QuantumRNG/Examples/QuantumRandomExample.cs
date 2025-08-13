// Quantum RNG SDK - Example Implementation
// This script demonstrates how to use the Quantum Random Number Generator
// in your Unity projects for truly random game mechanics.

using System.Collections.Generic;
using UnityEngine;
using QuantumRNG;

/// <summary>
/// Example script showing how to use the Quantum RNG SDK
/// Attach this to a GameObject to see quantum random numbers in action
/// </summary>
public class QuantumRandomExample : MonoBehaviour
{
    [Header("API Configuration")]
    [Tooltip("Your API key from https://your-quantum-api.com")]
    public string apiKey = "your_api_key_here";
    
    [Header("Example Settings")]
    [Tooltip("How many random numbers to generate")]
    public int numberCount = 10;
    
    [Tooltip("Test quantum randomness on Start")]
    public bool testOnStart = true;

    [Header("Game Examples")]
    [Tooltip("Generate random loot rarity (1-100)")]
    public bool testLootRarity = true;
    
    [Tooltip("Generate random damage multiplier (0.5x - 2.0x)")]
    public bool testDamageMultiplier = true;

    void Start()
    {
        // Initialize the Quantum RNG SDK
        QuantumRandom.Initialize(apiKey);
        
        if (testOnStart)
        {
            StartCoroutine(RunExamples());
        }
    }

    void Update()
    {
        // Press Space to run examples manually
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RunExamples());
        }
    }

    /// <summary>
    /// Demonstrates various uses of quantum random numbers in games
    /// </summary>
    System.Collections.IEnumerator RunExamples()
    {
        Debug.Log("=== Quantum Random Number Examples ===");

        // Example 1: Basic quantum random integers
        yield return StartCoroutine(BasicQuantumNumbers());

        if (testLootRarity)
        {
            // Example 2: Loot rarity system
            yield return StartCoroutine(LootRarityExample());
        }

        if (testDamageMultiplier)
        {
            // Example 3: Damage multiplier
            yield return StartCoroutine(DamageMultiplierExample());
        }

        Debug.Log("=== Examples Complete ===");
    }

    /// <summary>
    /// Example 1: Generate basic quantum random numbers
    /// </summary>
    System.Collections.IEnumerator BasicQuantumNumbers()
    {
        Debug.Log($"🎲 Generating {numberCount} quantum random numbers...");

        yield return StartCoroutine(QuantumRandom.GetIntegers(numberCount,
            // Success: Display the numbers
            (numbers) => {
                Debug.Log($"✅ Received {numbers.Count} quantum random numbers:");
                for (int i = 0; i < numbers.Count; i++)
                {
                    Debug.Log($"   [{i}]: {numbers[i]:N0}");
                }
            },
            // Error: Show what went wrong
            (error) => {
                Debug.LogError($"❌ Failed to get quantum numbers: {error}");
            }
        ));
    }

    /// <summary>
    /// Example 2: Use quantum randomness for loot rarity
    /// Perfect for RPGs, loot boxes, and reward systems
    /// </summary>
    System.Collections.IEnumerator LootRarityExample()
    {
        Debug.Log("🎁 Quantum Loot Rarity System Example...");

        yield return StartCoroutine(QuantumRandom.GetIntegerInRange(1, 100,
            (rarityRoll) => {
                string rarity = GetLootRarity(rarityRoll);
                Debug.Log($"🎁 Loot drop: {rarityRoll}/100 = {rarity}");
            },
            (error) => {
                Debug.LogError($"❌ Loot generation failed: {error}");
            }
        ));
    }

    /// <summary>
    /// Example 3: Use quantum randomness for damage multipliers
    /// Perfect for critical hits, spell effects, and combat variety
    /// </summary>
    System.Collections.IEnumerator DamageMultiplierExample()
    {
        Debug.Log("⚔️ Quantum Damage Multiplier Example...");

        yield return StartCoroutine(QuantumRandom.GetFloat(
            (randomFloat) => {
                // Convert 0.0-1.0 to 0.5x-2.0x multiplier
                float multiplier = 0.5f + (randomFloat * 1.5f);
                int baseDamage = 100;
                int finalDamage = Mathf.RoundToInt(baseDamage * multiplier);
                
                Debug.Log($"⚔️ Attack: {baseDamage} base damage × {multiplier:F2} = {finalDamage} final damage");
            },
            (error) => {
                Debug.LogError($"❌ Damage calculation failed: {error}");
            }
        ));
    }

    /// <summary>
    /// Convert quantum random number to loot rarity
    /// This shows how to use true randomness for game mechanics
    /// </summary>
    string GetLootRarity(int roll)
    {
        if (roll >= 95) return "🌟 LEGENDARY (5%)";
        if (roll >= 80) return "💜 EPIC (15%)";
        if (roll >= 60) return "💙 RARE (20%)";
        if (roll >= 30) return "💚 UNCOMMON (30%)";
        return "⚪ COMMON (30%)";
    }

    void OnGUI()
    {
        // Simple debug UI
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));
        GUILayout.Label("Quantum RNG SDK Example", GUI.skin.box);
        
        if (!QuantumRandom.IsInitialized)
        {
            GUILayout.Label("❌ Not initialized - check API key", GUI.skin.box);
        }
        else
        {
            GUILayout.Label("✅ SDK initialized", GUI.skin.box);
        }
        
        if (GUILayout.Button("Generate Quantum Numbers"))
        {
            StartCoroutine(RunExamples());
        }
        
        GUILayout.Label("Press Space to test manually");
        GUILayout.EndArea();
    }
}