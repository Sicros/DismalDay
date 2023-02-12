using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAtacking : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ZombieAttributes _zombie;
    [SerializeField] private CharacterAttributes _character;
    [SerializeField] private float _timeNextAttack;

    private void Start()
    {
        _timeNextAttack = 0;
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out animator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.TryGetComponent<CharacterAttributes>(out _character);
            AttackingTransition(true);
            if (_timeNextAttack == 0)
            {
                _character.ReceiveDamage(_zombie.attackDamage);
                _timeNextAttack = Time.time + _zombie.timeBetweenAttacks;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Time.time >= _timeNextAttack)
        {
            _character.ReceiveDamage(_zombie.attackDamage);
            _timeNextAttack = Time.time + _zombie.timeBetweenAttacks;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AttackingTransition(false);
        }
    }

    private void AttackingTransition(bool _isAttacking)
    {
        //animator.SetBool("isAttacking", _isAttacking);
        animator.SetBool("isChasing", !_isAttacking);
    }
}
