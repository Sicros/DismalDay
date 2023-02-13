using UnityEngine;

public class ZombieAttributes : MonoBehaviour
{
    public float currentHealth;
    public float maximumHealth;
    public float attackDamage;
    public float timeBetweenAttacks;
    public float timeToDestroy;
    public float coordinateAnimationWithAttack;

    // Velocidad a la que persigue el enemigo al jugador. Configurable desde el inspector.
    public float chasingSpeed;

    // Distancia que se mantiene alejado el enemigo del jugador. Configurable desde el inspector.
    public float keepDistance;
    public float keepDistanceShoot;

    public bool reachedShootSound;

    // Velocidad de rotación del enemigo. Configurable desde el inspector.
    public float rotationSpeed;
    private Animator _animator;
    private AudioSource _audios;
    private ZombieAudio _zombieAudios;

    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
    }

    public void ReceiveDamage(float damage)
    {
        if (currentHealth <= damage && currentHealth > 0)
        {
            DeathAnimation();
            currentHealth = 0;
            gameObject.tag = "EnemyDeath";
            Destroy(gameObject, timeToDestroy);
        }
        if (currentHealth > damage)
        {
            currentHealth -= damage;
        }
    }

    public void DeathAnimation()
    {
        _animator.Rebind();
        _animator.SetTrigger("Die");
    }
}
