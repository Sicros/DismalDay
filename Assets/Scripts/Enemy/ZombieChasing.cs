using UnityEngine;

public class ZombieChasing : MonoBehaviour
{
    private ZombieAttributes _zombie;
    private Animator _animator;
    private float _originalTriggerRadius;
    private Vector3 _characterPosition;

    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
        _originalTriggerRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            ChasingTransition(true);
            gameObject.GetComponent<SphereCollider>().radius *= 2;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _animator.GetBool("isChasing") && _zombie.currentHealth > 0)
        {
            _characterPosition = other.transform.position;
            ChasingPlayer();
        }
    }

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

    private void ChasingTransition(bool _isChasing)
    {
        _animator.SetBool("isChasing", _isChasing);
    }
}
