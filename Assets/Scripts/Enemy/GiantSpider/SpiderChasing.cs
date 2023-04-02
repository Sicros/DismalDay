using UnityEngine;

public class SpiderChasing : MonoBehaviour
{
    // Velocidad a la que se mueve la araña.
    [SerializeField] private float speed;

    // Referencia a la animación de la misma araña.
    private Animator _animator;

    // Referencia al Transform de la araña.
    private Transform _spiderTransform;

    // Al inicio se obtienen los componentes de animación y transform de la araña.
    private void Start()
    {
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
        transform.parent.parent.TryGetComponent<Transform>(out _spiderTransform);
    }

    // El personaje, al entrar al área de la araña, activara la animación de persecución.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Walk", true);
        }
    }

    // Mientras el personaje se encuentra en su rango, esta comenzará a caminar hacia delante.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _spiderTransform.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}
