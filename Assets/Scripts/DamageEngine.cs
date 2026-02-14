using UnityEngine;

public class DamageEngine : MonoBehaviour
{
    public int calculateDamage(int attackersAttack, int defendersDefense)
    {
        int damage = attackersAttack - defendersDefense;
        return Mathf.Max(0, damage);
    }

    public void applyDamage(EntityHealth defender, int damage)
    {
        defender.currentHealth -= damage;
        defender.currentHealth = Mathf.Max(0, defender.currentHealth);
        defender.healthBar.SetHealth(defender.currentHealth, defender.maxHealth);
        Debug.Log(defender.name + " took " + damage + " damage, remaining health: " + defender.currentHealth);
    }
}