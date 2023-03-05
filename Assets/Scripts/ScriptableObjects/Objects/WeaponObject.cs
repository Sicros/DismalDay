using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Weapon")]

public class WeaponObject : ScriptableObject
{
    // Daño provocado por el arma.
    public float damage;

    // Tiempo de espera antes de realizar otro disparo.
    public float timeBetweenShoots;

    // Cantidad máxima de balas que puede llevar por carga.
    public int maxBullets;

    // Time to reload wepon
    public float reloadTime;
}
