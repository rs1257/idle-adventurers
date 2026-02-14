using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdventurerManager : MonoBehaviour
{
    public static AdventurerManager Instance;

    private Dictionary<string, Adventurer> adventurers = new();

    public bool IsLoaded { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadAllAdventurers();
    }

    private void LoadAllAdventurers()
    {
        Addressables.LoadAssetsAsync<Adventurer>("Adventurers", adventurer =>
        {
            adventurers[adventurer.name] = adventurer;
            UnityEngine.Debug.Log("Loaded adventurer: " + adventurer.name);
        })
        .Completed += handle =>
        {
            UnityEngine.Debug.Log("All adventurers loaded");
            IsLoaded = true;
        };
    }

    public List<Adventurer> adventurersList
    {
        get
        {
            UnityEngine.Debug.Log(adventurers["Gatherer"]);
            return new List<Adventurer>(adventurers.Values);
        }
    }
}