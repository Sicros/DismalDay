using UnityEngine;

public class ZombieAttributes : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth;
    public float attackDamage;

    public void ReceiveDamage(float damage)
    {
        if (currentHealth <= damage)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }
    }
}
