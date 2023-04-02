using UnityEngine;
using UnityEngine.AI;

/*
Permite al zombie identificar un objetivo y perseguir dentro de un rango establecido.
Este rango aumento si el zombie detecta al jugador, para hacer más complicada la huida.
Viene acompañada con sus respectivas animaciones y atributos que pueda tener el zombie.
*/

public class ZombieChasing : MonoBehaviour
{
    // Variable que almacena los atributos del zombie.
    private ZombieEntity _zombie;

    // Variable que almacena las animaciones del zombie.
    private Animator _animator;

    // Variable que almacena el transform del zombie.
    private Transform _zombieTransform;

    // Variable que guarda el radio original de persecución del zombie.
    private float _originalTriggerRadius;

    // Variable que guarda la posición actual del personaje.
    private Vector3 _characterPosition;

    // Booleano que indica si está siguiendo el sonido de un disparo.
    private bool _chasingShoot;

    // Referencia a componente de NavMesh
    private NavMeshAgent zombieNavMeshAgent;

    // Inicialización de variable de animación, atributos del zombie y radio de activación.
    private void Start()
    {
        _chasingShoot = false;
        transform.parent.parent.TryGetComponent<Transform>(out _zombieTransform);
        transform.parent.parent.TryGetComponent<ZombieEntity>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
        _originalTriggerRadius = gameObject.GetComponent<SphereCollider>().radius;
        transform.parent.parent.TryGetComponent<NavMeshAgent>(out zombieNavMeshAgent);
        zombieNavMeshAgent.speed = _zombie.GetSpeedWalkingUp();
    }

    private void Update()
    {
        if (_zombie.GetCurrentHealth() == 0)
        {
            StopChasingPlayer();
        }
        if (_chasingShoot && (_characterPosition - transform.position).magnitude <= _zombie.GetKeepDistanceShoot())
        {
            StopChasingPlayer();
            WalkingTransition(false);
            _chasingShoot = false;
        }
    }

    // Método que se activa al ingresar en el rango un objeto con la etiqueta "Player" y si la vida
    // del zombie es mayor a 0. Activa la animación de persecución y duplica el radio de activación del mismo.
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player" && _zombie.GetCurrentHealth() > 0)
        {
            ChasingTransition(true);
            gameObject.GetComponent<SphereCollider>().radius *= 1.6f;
            _chasingShoot = false;
        }
    }

    // Método que se activa al permanecer en el rango un objeto con la etiqueta de "Player" y si el zombie 
    // tiene activa su animación de persecución y su vida es superior a 0. Recalcula la posición del personaje
    // y, gracias al método de persecución, lo intenta perseguir hasta su ubicación.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _animator.GetBool("isChasing") && _zombie.GetCurrentHealth() > 0)
        {
            _characterPosition = other.transform.position;
            ChasingPlayer(_characterPosition);
        }
        if (other.gameObject.tag == "PlayerDeath")
        {
            _animator.Rebind();
        }
    }

    // Método que se activa al salir del rango y el objeto tenga una etiqueta de "Player", además de que la
    // vida del zombie sea superior a 0. Regresa el radio del collider a su tamaño original y desactiva
    // la animación de persecución.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.GetCurrentHealth() > 0)
        {
            gameObject.GetComponent<SphereCollider>().radius = _originalTriggerRadius;
            ChasingTransition(false);
            WalkingTransition(false);
        }
    }

    // Método de persecución. Igual al que se encuentra en el script de EnemyChasingPlayer.cs,
    // con la diferencia que se elimina el método de "LookAt()", dado que ya existe otro método
    // en el script que permite esto.
    // Update: Ahora los zombies se acercan al personaje usando NavMesh, para evitar que estos
    // colisionen con objetos intentando alcanzar al jugador.
    public void ChasingPlayer(Vector3 _chasingPosition)
    {
        _characterPosition = _chasingPosition;
        zombieNavMeshAgent.SetDestination(_chasingPosition);
    }

    // Método que detiene la persecución del zombie
    private void StopChasingPlayer()
    {
        zombieNavMeshAgent.SetDestination(transform.position);
    }

    // Método que permite transicionar a la animación de persecución.
    private void ChasingTransition(bool _isChasing)
    {
        _animator.SetBool("isChasing", _isChasing);
    }

    // Método que permite transicionar a la animación de persecución.
    private void WalkingTransition(bool _isWalking)
    {
        _animator.SetBool("justWalking", _isWalking);
    }

    // Método para actualizar estado se seguimiento de disparo.
    public void UpdateChasingShootStatus(bool _newStatus)
    {
        _chasingShoot = _newStatus;
    }
}
