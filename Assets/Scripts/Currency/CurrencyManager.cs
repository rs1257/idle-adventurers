using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<Currency, int> amounts = new();

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

        LoadAllCurrencies();
    }

    private void LoadAllCurrencies()
    {
        Addressables.LoadAssetsAsync<Currency>("Currencies", currency =>
        {
            // Called once per asset
            amounts[currency] = 0;
            Debug.Log("Loaded currency: " + currency.name);
        })
        .Completed += handle =>
        {
            Debug.Log("All currencies loaded");
            IsLoaded = true;
        };
    }

    public void Add(Currency type, int amount)
    {
        amounts[type] += amount;
    }

    public bool Spend(Currency type, int amount)
    {
        if (amounts[type] >= amount)
        {
            amounts[type] -= amount;
            return true;
        }
        return false;
    }

    public int GetAmount(Currency type)
    {
        return amounts[type];
    }
}