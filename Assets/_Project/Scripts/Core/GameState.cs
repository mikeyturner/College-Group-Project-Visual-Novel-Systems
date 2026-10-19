using UnityEngine;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    private Dictionary<string, object> Flags = new Dictionary<string, object>();

    void Awake()
    {
        // Singleton Setup
         
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    // Set a value
    public void SetFlag(string Key, object Value)
    {
        Debug.Log($"GameState: {Key} = {Value}");
        Flags[Key] = Value;
    }

    // Get a value (Generic)
    public T GetFlag<T>(string key)
    {
        if (Flags.TryGetValue(key, out var value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    // Check if flag exists
    public bool HasFlag(string key)
    {
        return Flags.ContainsKey(key);
    }
}
