using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private Dictionary<string, Enemy> enemies = new();

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

        LoadAllEnemies();
    }

    private void LoadAllEnemies()
    {
        Addressables.LoadAssetsAsync<Enemy>("Enemies", enemy =>
        {
            enemies[enemy.name] = enemy;
            UnityEngine.Debug.Log("Loaded enemy: " + enemy.name);
        })
        .Completed += handle =>
        {
            UnityEngine.Debug.Log("All enemies loaded");
            IsLoaded = true;
        };
    }

    public List<Enemy> enemiesList
    {
        get
        {
            UnityEngine.Debug.Log(enemies["Slime"]);
            return new List<Enemy>(enemies.Values);
        }
    }
}