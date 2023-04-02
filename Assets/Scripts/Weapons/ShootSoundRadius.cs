using System.Collections.Generic;
using UnityEngine;

/*
Simula el efecto que ocasiona el sonido de una bala, que atrae a los enemigos hasta su punto central,
que corresponde al punto en el que se generó el disparo.
El efecto solo se produce al inicio del disparo, posterior a este no activará otros zombies que
hayan entrado en la zona. También este objeto se detruye en uno de los siguientes casos o la
combinación de más de uno de ellos:
    a) Los zombies llegaron al punto central de la bala (con un margen de distancia).
    b) Los zombies fueron eliminados.
    c) Los zombies están observando, persiguiendo y/o atacando al jugador.
    d) El jugador ha generado un nuevo disparo.
*/

public class ShootSoundRadius : MonoBehaviour
{
    // Tiempo de duración del efecto que genera la bala. Esto evita que zombies que hayan
    // ingresado al rango después de haber disparado, no se vean afectados.
    [SerializeField] private float timeShootRange;

    // Momento del juego en que el rango del disparo deja de tener efecto.
    private float _timeAtShootDisappear;

    // Cálculo del próximo momento en que el rango de la bala dejará de tener efecto.
    private void Awake()
    {
        _timeAtShootDisappear = Time.time + timeShootRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo se consideran aquellos objetos que tengan la etiqueta de "Zombie" y
        // si se encuentran dentro del tiempo establecido.
        if (other.gameObject.tag == "Zombie" && _timeAtShootDisappear <= Time.time)
        {
            // Se almacena en una lista los IDs de cada zombie y en un diccionario lo que son los
            // Transforms, atributos y animaciones en un diccionario con su respectivo ID como llave.
            Transform _zombieTransform = other.transform;
            Animator _zombieAnimator = other.transform.GetComponent<Animator>();
            other.transform.Find("Triggers/Chasing").TryGetComponent<ZombieChasing>(out ZombieChasing _zombieChasing);
            if (!_zombieAnimator.GetBool("isAttacking") && !_zombieAnimator.GetBool("isChasing"))
            {
                _zombieAnimator.SetBool("justWalking", true);
                _zombieChasing.UpdateChasingShootStatus(true);
                _zombieChasing.ChasingPlayer(transform.position);
            }
        }
    }
}
