using UnityEngine;

public class ZombieChasing : MonoBehaviour
{
    [SerializeField] private ZombieController zombie;
    [SerializeField] private Animator animator;

    private float _originalTriggerRadius;

    private void Start()
    {
        _originalTriggerRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SphereCollider>().radius *= 2;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChasingPlayer();
            ChasingTransition(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
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
        var vectorToPlayer = zombie.player.position - zombie.transform.position;
        if (vectorToPlayer.magnitude > zombie.keepDistance)
        {
            zombie.transform.position += vectorToPlayer.normalized * zombie.chasingSpeed * Time.deltaTime;
        }
    }

    private void ChasingTransition(bool _isChasing)
    {
        animator.SetBool("isChasing", _isChasing);
    }
}
