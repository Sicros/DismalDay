using UnityEngine;

/*
Biblioteca de audios. Se guarda un listado con todos los audios que estén asociados al zombie.
También se tienen tiempos de esperar entre que un audio en loop se detenga y se vuelva a reproducir.
Se obtienen las animaciones para conocer el estado en el que se encuentra el zombie actualmente.
*/

public class ZombieAudio : MonoBehaviour
{
    // Intervalo de tiempo con el que se reproduce el audio idle.
    public float timeBetweenIdle;

    // Intervalo de tiempo con el que se reproduce el audio de persecución.
    public float timeBetweenChasing;

    // Intervalo de tiempo con el que se reproduce el audio de ataque.
    public float timeBetweenAttacking;

    // Fuente de audio idle.
    public AudioSource idleAudio;

    // Fuente de audio de persecución.
    public AudioSource chasingAudio;

    // Fuente de audio de ataque.
    public AudioSource attackingAudio;

    // Tiempo que va almacenando el próximo momento en que debe ejecutarse el audio idle.
    private float _timeNextIdle;

    // Tiempo que va almacenando el próximo momento en que debe ejecutarse el audio de persecución.
    private float _timeNextChasing;

    // Tiempo que va almacenando el próximo momento en que debe ejecutarse el audio de ataque.
    private float _timeNextAttacking;

    // Variable que almacena las animaciones del zombie.
    private Animator _animator;

    // Variable que almacena los atributos del zombie.
    private ZombieAttributes _attributes;

    // Iniciación de las variables de animación y atributos del zombie al comenzar el juego.
    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
        transform.TryGetComponent<ZombieAttributes>(out _attributes);
    }

    private void Update()
    {
        // Si el zombie está atacando, se reproduce el audio de ataque y se detienen los demás.
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
        // Si el zombie está persiguiendo al personaje, se reproduce el audio de persecución y se detienen los demás.
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
        // Si ninguno de los casos anteriores se cumplen, se reproduce el audio idle y se detienen los demás.
        else if (Time.time >= _timeNextIdle)
        {
            PlayChasingAudio(false);
            PlayAttackingAudio(false);
            PlayIdleAudio(true);
            _timeNextIdle = Time.time + timeBetweenIdle;
        }
    }

    // Método que permite detener o reproducir el audio idle gracias a su parámetro booleano.
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

    // Método que permite detener o reproducir el audio de persecución gracias a su parámetro booleano.
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

    // Método que permite detener o reproducir el audio de ataque gracias a su parámetro booleano.
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
