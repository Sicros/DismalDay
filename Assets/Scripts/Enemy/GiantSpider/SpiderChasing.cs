using UnityEngine;

public class SpiderChasing : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator _animator;

    private Transform _spiderTransform;

    private void Start()
    {
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
        transform.parent.parent.TryGetComponent<Transform>(out _spiderTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spiderTransform.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}
