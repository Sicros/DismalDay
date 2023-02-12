using UnityEngine;

public class ZombieWatching : MonoBehaviour
{
    [SerializeField] private ZombieController zombie;
    [SerializeField] private Animator animator;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            WatchingPlayer();
        }
    }

    // Método que permite al enemigo observar al jugador desde su posición. Es exactamente el
    // mismo método que se encuentra en el script de EnemyWatchingPlayer.cs.
    private void WatchingPlayer()
    {
        var vectorToPlayer = zombie.player.position - zombie.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(vectorToPlayer);
        zombie.transform.rotation = Quaternion.Lerp(zombie.transform.rotation, newRotation, Time.deltaTime * zombie.rotationSpeed);
    }
}
