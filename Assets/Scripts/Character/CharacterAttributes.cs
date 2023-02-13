using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth;
    private Animator _animator;

    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
    }

    public void ReceiveDamage(float damage)
    {
        if (currentHealth <= damage && currentHealth != 0)
        {
            DeathAnimation();
            currentHealth = 0;
            gameObject.tag = "PlayerDeath";
        }
        if (currentHealth > damage)
        {
            GetHitAnimation();
            currentHealth -= damage;
        }
    }

    public void ReceiveHeal(float heal)
    {
        if (currentHealth + heal >= maximumHealth && currentHealth > 0)
        {
            currentHealth = maximumHealth;
        }
        if (currentHealth + heal < maximumHealth && currentHealth > 0)
        {
            currentHealth += heal;
        }
    }

    public void DeathAnimation()
    {
        _animator.SetTrigger("Die");
    }

    public void GetHitAnimation()
    {
        _animator.SetTrigger("getHit");
    }
}
