using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public float timeBetweenIdle;
    public float timeBetweenChasing;
    public float timeBetweenAttacking;
    public AudioSource idleAudio;
    public AudioSource chasingAudio;
    public AudioSource attackingAudio;
    private float _timeNextIdle;
    private float _timeNextChasing;
    private float _timeNextAttacking;
    private Animator _animator;
    private ZombieAttributes _attributes;
    private AudioSource _audios;

    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
        transform.TryGetComponent<ZombieAttributes>(out _attributes);
    }

    private void Update()
    {
        if (_animator.GetBool("isAttacking"))
        {
            if (Time.time >= _timeNextAttacking)
            {
                PlayIdleAudio(false);
                PlayChasingAudio(false);
                PlayAttackingAudio(true);
                _timeNextAttacking = Time.time + timeBetweenAttacking;
            }
        }
        else if (_animator.GetBool("isChasing"))
        {
            if (Time.time >= _timeNextChasing)
            {
                PlayIdleAudio(false);
                PlayAttackingAudio(false);
                PlayChasingAudio(true);
                _timeNextChasing = Time.time + timeBetweenChasing;
            }
        }
        else if (Time.time >= _timeNextIdle)
        {
            PlayChasingAudio(false);
            PlayAttackingAudio(false);
            PlayIdleAudio(true);
            _timeNextIdle = Time.time + timeBetweenIdle;
        }
    }

    private void PlayIdleAudio(bool playAudio)
    {
        if (playAudio)
        {
            idleAudio.Play();
        }
        else
        {
            idleAudio.Stop();
        }
    }

    private void PlayChasingAudio(bool playAudio)
    {
        if (playAudio)
        {
            chasingAudio.Play();
        }
        else
        {
            chasingAudio.Stop();
        }
    }

    private void PlayAttackingAudio(bool playAudio)
    {
        if (playAudio)
        {
            attackingAudio.Play();
        }
        else
        {
            attackingAudio.Stop();
        }
    }
}
