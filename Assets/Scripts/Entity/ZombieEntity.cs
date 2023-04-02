using UnityEngine;

public class ZombieEntity : ObjectEntity
{
    // Referencia a scriptable object con los datos del zombie.
    [SerializeField] private ZombieData zombieData;

    // Referencia al componente de animación del zombie.
    private Animator _animator;

    // Vida actual del zombie.
    private float _currentHealth;

    // Se asigna la vida máxima del zombie como su vida actual.
    private void Awake()
    {
        _currentHealth = zombieData.maximumHealth;
    }

    // Método convocado al inicio para almacenar las animaciones del objeto actual.
    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
    }

    // Reinicia el estado de la animación del zombie y se ejecuta la de muerte.
    public void DeathAnimation()
    {
        _animator.Rebind();
        _animator.SetTrigger("Die");
    }

    // Método que calcula el daño recibido por el zombie.
    public void ReceiveDamage(float damage)
    {
        // Se revisa si la vida es superior a 0 y si esta es menor al daño recibido.
        if (_currentHealth <= damage && _currentHealth > 0)
        {
            // Activación de animación de muerte del zombie.
            DeathAnimation();

            // Se reasigna la vida actual a 0.
            _currentHealth = 0;

            // Cambia la etiqueta del zombie.
            gameObject.tag = "EnemyDeath";

            // Agrega zombie muerto a la lista.
            SaveAndLoad.instance.AddObjectToDestroy(gameObject);

            // Destruye el objeto transcurrido el tiempo dado por el usuario.
            Destroy(gameObject, zombieData.timeToDestroy);
        }
        
        // En caso de que su vida actual sea mayor al daño, este daño se le resta.
        if (_currentHealth > damage)
        {
            _currentHealth -= damage;
        }
    }

    // Retorna la vida actual del zombie.
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    // Retorna la velocidad de caminar hacia delante del zombie.
    public float GetSpeedWalkingUp()
    {
        return zombieData.speedWalkingUp;
    }

    // Retorna la velocidad de rotación del zombie.
    public float GetRotationSpeed()
    {
        return zombieData.rotationSpeed;
    }

    // Retorna el tiempo de espera antes de iniciar la animación de ataque.
    public float GetCoordinateAnimationWithAttack()
    {
        return zombieData.coordinateAnimationWithAttack;
    }

    // Retorna el daño que provoca el zombie por ataque.
    public float GetAttackDamage()
    {
        return zombieData.attackDamage;
    }

    // Retorna el tiempo de espera entre cada ataque.
    public float GetTimeBetweenAttacks()
    {
        return zombieData.timeBetweenAttacks;
    }

    // Retorna la distancia que mantiene con el punto de disparo.
    public float GetKeepDistanceShoot()
    {
        return zombieData.keepDistanceShoot;
    }

    // Retorna la distancia que mantiene con el jugador.
    public float GetKeepDistancePlayer()
    {
        return zombieData.keepDistancePlayer;
    }
}
