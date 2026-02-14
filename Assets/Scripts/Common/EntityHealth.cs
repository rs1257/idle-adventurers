using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public HealthBar healthBar;

    public void SetMaxHealth(float value)
    {
        maxHealth = value;
        currentHealth = value;

        healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, maxHealth);
    }
}