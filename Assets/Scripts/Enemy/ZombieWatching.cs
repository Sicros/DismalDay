using UnityEngine;

public class ZombieWatching : MonoBehaviour
{
    private ZombieAttributes _zombie;
    private Vector3 _characterPosition;

    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            _characterPosition = other.transform.position;
            WatchingPlayer();
        }
    }

    // Método que permite al enemigo observar al jugador desde su posición. Es exactamente el
    // mismo método que se encuentra en el script de EnemyWatchingPlayer.cs.
    private void WatchingPlayer()
    {
        var vectorToPlayer = _characterPosition - _zombie.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(vectorToPlayer);
        _zombie.transform.rotation = Quaternion.Lerp(_zombie.transform.rotation, newRotation, Time.deltaTime * _zombie.rotationSpeed);
    }
}
