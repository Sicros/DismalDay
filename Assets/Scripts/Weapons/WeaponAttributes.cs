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
}
