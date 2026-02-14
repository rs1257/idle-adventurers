using UnityEngine;
using UnityEngine.AddressableAssets;

public class Entity : ScriptableObject
{
    public string description;
    public Sprite icon;
    public AssetReferenceGameObject prefab;
    public HealthBar healthBar;

    public int level;
    public int health;
    public int maxHealth;
    public int attack;
    public int defense;
}
