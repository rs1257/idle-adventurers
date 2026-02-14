using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawner : MonoBehaviour
{
    public AssetReferenceGameObject healthBarPrefab;

    public void Spawn(Enemy data, System.Action<GameObject> onSpawned)
    {
        UnityEngine.Debug.Log("Spawning enemy: " + data.name);
        data.prefab.InstantiateAsync().Completed += handle =>
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                UnityEngine.Debug.LogError("Failed to load enemy prefab");
                return;
            }

            GameObject enemyGO = handle.Result;
            enemyGO.name = data.name;

            // Attach health bar
            AttachHealthBar(enemyGO, data);
            onSpawned?.Invoke(enemyGO);
        };
    }

    private void AttachHealthBar(GameObject enemyGO, Enemy data)
    {
        healthBarPrefab.InstantiateAsync(enemyGO.transform).Completed += barHandle =>
        {
            if (barHandle.Status != AsyncOperationStatus.Succeeded)
            {
                UnityEngine.Debug.LogError("Failed to load health bar prefab");
                return;
            }

            GameObject bar = barHandle.Result;

            // Position above the character
            bar.transform.localPosition = new Vector3(-0.45f, 0.65f, 0);

            // Connect to health system
            var hp = enemyGO.GetComponent<EntityHealth>();
            hp.healthBar = bar.GetComponent<HealthBar>();
            hp.SetMaxHealth(data.maxHealth);
        };
    }
}