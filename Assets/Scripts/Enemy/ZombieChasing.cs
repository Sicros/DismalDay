using UnityEngine;

/*
Permite al zombie identificar un objetivo y perseguir dentro de un rango establecido.
Este rango aumento si el zombie detecta al jugador, para hacer más complicada la huida.
Viene acompañada con sus respectivas animaciones y atributos que pueda tener el zombie.
*/

public class ZombieChasing : MonoBehaviour
{
    // Variable que almacena los atributos del zombie.
    private ZombieAttributes _zombie;

    // Variable que almacena las animaciones del zombie.
    private Animator _animator;

    // Variable que guarda el radio original de persecución del zombie.
    private float _originalTriggerRadius;

    // Variable que guarda la posición actual del personaje.
    private Vector3 _characterPosition;

    // Inicialización de variable de animación, atributos del zombie y radio de activación.
    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
        _originalTriggerRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    // Método que se activa al ingresar en el rango un objeto con la etiqueta "Player" y si la vida
    // del zombie es mayor a 0. Activa la animación de persecución y duplica el radio de activación del mismo.
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            ChasingTransition(true);
            gameObject.GetComponent<SphereCollider>().radius *= 2;
        }
    }

    // Método que se activa al permanecer en el rango un objeto con la etiqueta de "Player" y si el zombie 
    // tiene activa su animación de persecución y su vida es superior a 0. Recalcula la posición del personaje
    // y, gracias al método de persecución, lo intenta perseguir hasta su ubicación.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _animator.GetBool("isChasing") && _zombie.currentHealth > 0)
        {
            _characterPosition = other.transform.position;
            ChasingPlayer();
        }
    }

    // Método que se activa al salir del rango y el objeto tenga una etiqueta de "Player", además de que la
    // vida del zombie sea superior a 0. Regresa el radio del collider a su tamaño original y desactiva
    // la animación de persecución.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            gameObject.GetComponent<SphereCollider>().radius = _originalTriggerRadius;
            ChasingTransition(false);
        }
    }

    // Método de persecución. Igual al que se encuentra en el script de EnemyChasingPlayer.cs,
    // con la diferencia que se elimina el método de "LookAt()", dado que ya existe otro método
    // en el script que permite esto.
    private void ChasingPlayer()
    {
        var vectorToPlayer = _characterPosition - _zombie.transform.position;
        if (vectorToPlayer.magnitude > _zombie.keepDistance)
        {
            _zombie.transform.position += vectorToPlayer.normalized * _zombie.chasingSpeed * Time.deltaTime;
        }
    }

    // Método que permite transicionar a la animación de persecución.
    private void ChasingTransition(bool _isChasing)
    {
        _animator.SetBool("isChasing", _isChasing);
    }
}
