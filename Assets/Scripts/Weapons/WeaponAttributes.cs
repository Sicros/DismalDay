using UnityEngine;

/*
Información relacionada a los atributos del arma. Actualmente solo se almacenan
el daño que provoca el arma al objetivo y el tiempo de espera entre disparos.
*/

public class WeaponAttributes : MonoBehaviour
{
    // Daño provocado por el arma.
    public float damage;

    // Tiempo de espera antes de realizar otro disparo.
    public float timeBetweenShoots;

    // Cantidad máxima de balas que puede llevar por carga.
    public int maxBullets;

    // Cantidad máxima de balas que puede llevar por carga.
    public int currentBullets;

    // Time to reload wepon
    public float reloadTime;
}
