using UnityEngine;

/*
Permite al zombie seguir con la mirada el jugador, solo afectado su rotación y dandole
un pequeño delay en este seguimiento.
*/

public class ZombieWatching : MonoBehaviour
{
    // Variable que almacena los atributos del zombie.
    private ZombieAttributes _zombie;

    // Variable que almacena la posición del zombie.
    private Vector3 _characterPosition;

    private Animator _animator;

    // Inicialización de la variable que almacena los atributos del zombie al comienzo del juego.
    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetBool("isWatching", true);
    }

    // Si un objeto con la etiqueta "Player" se queda en este radio de activación, el zombie
    // rotará de acuerdo a la posición del personaje y con un leve delay, definido por el usuario.
    // Para realizar esta acción, la vida del zombie debe ser superior a 0.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            _characterPosition = other.transform.position;
            WatchingPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("isWatching", false);
    }

    // Método que permite al enemigo observar al jugador desde su posición. Se obtiene a partir
    // de la posición actual del zombie, la del jugador y se utilizan Quaternions para dar un
    // pequeño delay al giro del zombie, mirando del punto A al punto B en un tiempo establecido.
    private void WatchingPlayer()
    {
        var vectorToPlayer = _characterPosition - _zombie.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(vectorToPlayer);
        _zombie.transform.rotation = Quaternion.Lerp(_zombie.transform.rotation, newRotation, Time.deltaTime * _zombie.rotationSpeed);
    }
}
