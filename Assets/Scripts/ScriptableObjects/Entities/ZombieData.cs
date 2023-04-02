using UnityEngine;

[CreateAssetMenu(menuName = "Entity/ZombieData")]
public class ZombieData : ObjectEntityData
{
    // Daño de ataque del zombie.
    public float attackDamage;

    // Tiempo que espera entre cada ataque.
    public float timeBetweenAttacks;

    // Tiempo que toma el objeto en destruirse una vez su vida se reduce a 0.
    public float timeToDestroy;

    // Tiempo de delay para coordinar el daño provocado al jugador con la animación.
    public float coordinateAnimationWithAttack;

    // Distancia que se mantiene alejado el enemigo del jugador. Configurable desde el inspector.
    public float keepDistancePlayer;

    // Distancia que se mantiene alejado el zombie con el punto en el que se genera un disparo.
    public float keepDistanceShoot;
}
