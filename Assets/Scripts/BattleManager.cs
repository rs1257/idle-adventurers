using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public DamageEngine damageEngine;
    private Dictionary<string, GameObject> adventurers = new();
    private Dictionary<string, GameObject> enemies = new();

    public float tickInterval = 5f;     // Seconds between ticks

    private void Start()
    {
        StartCoroutine(Damage());
    }

    private System.Collections.IEnumerator Damage()
    {

        while (AdventurerManager.Instance == null || !AdventurerManager.Instance.IsLoaded)
            yield return null;

        // Loop forever
        while (true)
        {
            if (adventurers.Count == 0)
            {
                AdventurerSpawner spawner = FindFirstObjectByType<AdventurerSpawner>();
                Adventurer defenderData = AdventurerManager.Instance.adventurersList[0];
                spawner.Spawn(defenderData, go =>
                    {
                        Debug.Log("Spawned: " + go.name);
                        adventurers[go.name] = go;
                    });
            }
            else
            {
                damageEngine.applyDamage(adventurers["Gatherer"].GetComponent<EntityHealth>(), 10);
            }

            if (enemies.Count == 0)
            {
                EnemySpawner spawner = FindFirstObjectByType<EnemySpawner>();
                Enemy attackerData = EnemyManager.Instance.enemiesList[0];
                spawner.Spawn(attackerData, go =>
                    {
                        Debug.Log("Spawned: " + go.name);
                        enemies[go.name] = go;
                    });
            }
            else
            {
                damageEngine.applyDamage(enemies["Slime"].GetComponent<EntityHealth>(), 5);
            }

            yield return new WaitForSeconds(tickInterval);
        }
    }
}