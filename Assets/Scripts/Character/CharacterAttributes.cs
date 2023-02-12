using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth;
    // private bool isCurable;

    private void Start()
    {
        // isCurable = true;
    }

    public void ReceiveDamage(float damage)
    {
        if (currentHealth <= damage)
        {
            currentHealth = 0;
            // isCurable = false;
        }
        else
        {
            currentHealth -= damage;
        }
    }

    public void ReceiveHeal(float heal)
    {
        if (currentHealth + heal >= maximumHealth)
        {
            currentHealth = maximumHealth;
        }
        else
        {
            currentHealth += heal;
        }
    }
}
