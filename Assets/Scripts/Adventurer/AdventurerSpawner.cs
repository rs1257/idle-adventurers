using System.Diagnostics;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdventurerSpawner : MonoBehaviour
{
    public AssetReferenceGameObject healthBarPrefab;

    public void Spawn(Adventurer data, System.Action<GameObject> onSpawned)
    {
        UnityEngine.Debug.Log("Spawning adventurer: " + data.name);
        data.prefab.InstantiateAsync().Completed += handle =>
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                UnityEngine.Debug.LogError("Failed to load adventurer prefab");
                return;
            }

            GameObject adventurerGO = handle.Result;
            adventurerGO.name = data.name;

            // Attach health bar
            AttachHealthBar(adventurerGO, data);
            onSpawned?.Invoke(adventurerGO);
        };
    }

    private void AttachHealthBar(GameObject adventurerGO, Adventurer data)
    {
        healthBarPrefab.InstantiateAsync(adventurerGO.transform).Completed += barHandle =>
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
            var hp = adventurerGO.GetComponent<EntityHealth>();
            hp.healthBar = bar.GetComponent<HealthBar>();
            hp.SetMaxHealth(data.maxHealth);
        };
    }
}