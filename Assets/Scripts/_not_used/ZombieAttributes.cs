using UnityEngine;

/*
Solo almacena los atributos del zombie como lo son su vida, daño de ataque, velocidad, entre otros.
Gran parte de estas variables son públicas para que puedan ser accedidas por otras clases y
elementos del juego.
También posee una variable con las animaciones.
*/

public class ZombieAttributes2 : MonoBehaviour
{
    // Vida actual del zombie.
    public float currentHealth;

    // Vida máxima del zombie.
    public float maximumHealth;

    // Daño de ataque del zombie.
    public float attackDamage;

    // Tiempo que espera entre cada ataque.
    public float timeBetweenAttacks;

    // Tiempo que toma el objeto en destruirse una vez su vida se reduce a 0.
    public float timeToDestroy;

    // Tiempo de delay para coordinar el daño provocado al jugador con la animación.
    public float coordinateAnimationWithAttack;

    // Velocidad a la que persigue el enemigo al jugador. Configurable desde el inspector.
    public float chasingSpeed;

    // Distancia que se mantiene alejado el enemigo del jugador. Configurable desde el inspector.
    public float keepDistance;

    // Distancia que se mantiene alejado el zombie con el punto en el que se genera un disparo.
    public float keepDistanceShoot;

    // Variable que indica si el zombie ya alcanzó el punto del disparo.
    public bool reachedShootSound;

    // Velocidad de rotación del enemigo. Configurable desde el inspector.
    public float rotationSpeed;

    // Variable con las animaciones del zombie.
    private Animator _animator;

    // Método convocado al inicio para almacenar las animaciones del objeto actual.
    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
    }

    // Método que calcula el daño recibido por el zombie.
    public void ReceiveDamage(float damage)
    {
        // Se revisa si la vida es superior a 0 y si esta es menor al daño recibido.
        if (currentHealth <= damage && currentHealth > 0)
        {
            // Activación de animación de muerte del zombie.
            DeathAnimation();

            // Se reasigna la vida actual a 0.
            currentHealth = 0;

            // Cambia la etiqueta del zombie.
            gameObject.tag = "EnemyDeath";

            // Destruye el objeto transcurrido el tiempo dado por el usuario.
            Destroy(gameObject, timeToDestroy);
        }
        
        // En caso de que su vida actual sea mayor al daño, este daño se le resta.
        if (currentHealth > damage)
        {
            currentHealth -= damage;
        }
    }

    // Reinicia el estado de la animación del zombie y se ejecuta la de muerte.
    public void DeathAnimation()
    {
        _animator.Rebind();
        _animator.SetTrigger("Die");
    }
}
