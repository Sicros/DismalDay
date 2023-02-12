using UnityEngine;

public class ZombieAttributes : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth;
    public float attackDamage;
    public float timeBetweenAttacks;
    public float timeToDestroy;

    public void ReceiveDamage(float damage)
    {
        if (currentHealth <= damage)
        {
            currentHealth = 0;
            Destroy(gameObject, timeToDestroy);
        }
        else
        {
            currentHealth -= damage;
        }
    }
}
