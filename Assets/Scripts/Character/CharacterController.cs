using System.Collections;
using UnityEngine;

/*
Método que permiten mover al personaje hacia delante y atras, rotar hacia los lados,
incluso un rotación de 180° en el eje Y. También se consideran otros atributos
como la velocidad de cada uno de estos movimientos, una método que permite correr
(duplicar la velocidad) y las animaciones que acompañan a cada una de estas acciones.
*/

public class CharacterController : MonoBehaviour
{
    // Velocidad con la que el personaje se desplaza hacia delante.
    [SerializeField] private float speedWalkingUp;

    // Velocidad con la que el personaje se desplaza hacía atras.
    [SerializeField] private float speedWalkingDown;

    // Velocidad con la que rota el personaje hacia los lados.
    [SerializeField] private float rotateSpeed;

    // Tiempo que le toma al personaje realizar un giro de 180° en eje Y.
    [SerializeField] private float timeCompleteRotation;

    // // Variable que almacena los inputs para mover o rotar al personaje.
    [SerializeField] private CharacterInputs _inputs;

    // Referencia a componente de animación del personaje.
    private Animator _animator;

    // Referencia a componente con los atributos del personaje.
    private CharacterAttributes _attributes;
    public CharacterCamera characterCamera;

    private void Start()
    {
        transform.TryGetComponent<Animator>(out _animator);
        transform.TryGetComponent<CharacterAttributes>(out _attributes);
    }

    void Update()
    {
        if (_attributes.currentHealth > 0)
        {
            MoveCharacter();
            RotateCharacter();
            AnimationCharacter();
            RunCharacter();
        }
    }

    private void MoveCharacter()
    {
        // Comando para mover el personaje hacía delante.
        if (Input.GetKey(_inputs.goUpKey) && !Input.GetKey(_inputs.goDownKey))
        {
            transform.Translate(0, 0, speedWalkingUp * Time.deltaTime);
        }

        // Comando para mover el personaje hacía atrás.
        if (!Input.GetKey(_inputs.goUpKey) && Input.GetKey(_inputs.goDownKey))
        {
            transform.Translate(0, 0, -speedWalkingDown * Time.deltaTime);
        }
    }

    // Método para rotar a personaje hacía izquierda y derecha.
    private void RotateCharacter()
    {
        // Rotar personaje hacia la izquierda.
        if (Input.GetKey(_inputs.turnLeftKey) && !Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
            characterCamera.firstPerson.transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        }

        // Rotar personaje hacia la derecha.
        if (Input.GetKey(_inputs.turnRightKey) && !Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            characterCamera.firstPerson.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        // Smoothly 180° rotation
        if (Input.GetKey(_inputs.goDownKey) && Input.GetKeyDown(_inputs.runKey) && !Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            StartCoroutine(SmoothlyRotate(180));
        }
    }

    private IEnumerator SmoothlyRotate(float turnDegrees)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0, turnDegrees, 0);

        float t = 0f;
        while (t < timeCompleteRotation)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t / timeCompleteRotation);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = targetRotation;
    }

    private void RunCharacter()
    {
        if (!Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            // Duplicar velocidad de movimiento.
            if (Input.GetKeyDown(_inputs.runKey))
            {
                speedWalkingUp *= 2;
            }

            // Volver a velocidad original.
            if (Input.GetKeyUp(_inputs.runKey))
            {
                speedWalkingUp /= 2;
            }
        }
    }

    private void AnimationCharacter()
    {
        // Animación para que personaje corra hacía delante.
        if (Input.GetKeyDown(_inputs.runKey) && !Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            RunningTransition(true);
        }

        // Desactivar animación de correr.
        if (!Input.GetKey(_inputs.runKey))
        {
            RunningTransition(false);
        }

        // Animación para que personaje camine hacía delante.
        if (Input.GetKey(_inputs.goUpKey))
        {
            WalkingTransition(true);
        }

        // Desactivar animación de caminar hacía delante.
        if (!Input.GetKey(_inputs.goUpKey) || Input.GetKeyUp(_inputs.goUpKey))
        {
            WalkingTransition(false);
        }

        // Animación para que personaje camine hacía atras.
        if (Input.GetKey(_inputs.goDownKey))
        {
            WalkingBackTransition(true);
        }

        // Desactivar animación de caminar hacía atras.
        if (!Input.GetKey(_inputs.goDownKey) || Input.GetKeyUp(_inputs.goDownKey))
        {
            WalkingBackTransition(false);
        }
    }

    private void WalkingTransition(bool _isWalking)
    {
        _animator.SetBool("isWalking", _isWalking);
    }

    private void RunningTransition(bool _isRunning)
    {
        _animator.SetBool("isRunning", _isRunning);
    }

    private void WalkingBackTransition(bool _isWalkingBack)
    {
        _animator.SetBool("isWalkingBack", _isWalkingBack);
    }

}
