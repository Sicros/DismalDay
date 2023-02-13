using UnityEngine;

/*
Información relacionada al ataque que realiza el zombie. Contiene su animación respectiva,
los atributos del zombie, los atributos del personaje al que esté atacando, así como una variable
que permite definir un intervalo de tiempo entre cada ataque.
Este script debe ser asignado a un objeto que tenga algún collider que le permite ser activado.
*/

public class ZombieAtacking : MonoBehaviour
{
    // Variable que almacena las animaciones del ataque zombie.
    private Animator _animator;

    // Atributos del zombie asociado.
    private ZombieAttributes _zombie;

    // Atributos el personaje al que se ataca.
    private CharacterAttributes _character;

    // Tiempo de espera entre cada ataque.
    private float _timeNextAttack;
    
    // Al inicar el juego se almacenan los componentes de las animaciones y atributos del zombie.
    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
    }

    // Activador de ingreso al radio del collider.
    private void OnTriggerEnter(Collider other)
    {
        // Solo se trabajan con aquellos objetos con la etiqueta de "Player" y si el zombie tiene una vida superior a 0.
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            // Se almacenan los atributos del personaje que entró en el radio.
            other.TryGetComponent<CharacterAttributes>(out _character);

            // Activación de animación de ataque del zombie.
            AttackingTransition(true);

            // Calculo del próximo momento en que se realizará el ataque.
            _timeNextAttack = Time.time + _zombie.coordinateAnimationWithAttack;
        }
    }

    // Activador mientras el personaje se encuentre en el radio del zombie.
    private void OnTriggerStay(Collider other)
    {
        // Solo se consideran los objetos con la etiqueta de "Player", solo si el zombie tiene una vida superior a 0
        // y si la hora actual supera a la del momento en que se debe realizar el ataque.
        if (other.gameObject.tag == "Player" && Time.time >= _timeNextAttack && _zombie.currentHealth > 0)
        {
            // Llamada al método de dañar al personaje. Se ingresar el daño de ataque del zombie.
            _character.ReceiveDamage(_zombie.attackDamage);

            // Recalculo del momento en que se deba realizar el próximo ataque.
            _timeNextAttack = Time.time + _zombie.timeBetweenAttacks;

            // En caso de que la vida del personaje se reduzca a 0, se detienen la animación.
            if (_character.currentHealth == 0)
            {
                AttackingTransition(false);
            }
        }
    }

    // Activador de la salida del personaje del radio. Si la vida del zombie es superior a 0,
    // se desactiva la animación de ataque.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            AttackingTransition(false);
        }
    }

    // Transición de animación a ataque. Desactiva la animación de persecución
    // y activa la de ataque o viceversa. Esto se hace gracias al parámetro booleano
    // que se utiliza en el método.
    private void AttackingTransition(bool _isAttacking)
    {
        _animator.SetBool("isChasing", !_isAttacking);;
        _animator.SetBool("isAttacking", _isAttacking);
    }
}
