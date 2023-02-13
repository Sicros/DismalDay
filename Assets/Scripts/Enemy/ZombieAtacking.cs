using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAtacking : MonoBehaviour
{
    private Animator _animator;
    private ZombieAttributes _zombie;
    private CharacterAttributes _character;
    private float _timeNextAttack;
    

    private void Start()
    {
        transform.parent.parent.TryGetComponent<ZombieAttributes>(out _zombie);
        transform.parent.parent.TryGetComponent<Animator>(out _animator);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            other.TryGetComponent<CharacterAttributes>(out _character);
            AttackingTransition(true);
            _timeNextAttack = Time.time + _zombie.coordinateAnimationWithAttack;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Time.time >= _timeNextAttack && _zombie.currentHealth > 0)
        {
            _character.ReceiveDamage(_zombie.attackDamage);
            _timeNextAttack = Time.time + _zombie.timeBetweenAttacks;
            if (_character.currentHealth == 0)
            {
                AttackingTransition(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && _zombie.currentHealth > 0)
        {
            AttackingTransition(false);
        }
    }

    private void AttackingTransition(bool _isAttacking)
    {
        _animator.SetBool("isChasing", !_isAttacking);;
        _animator.SetBool("isAttacking", _isAttacking);
    }
}
