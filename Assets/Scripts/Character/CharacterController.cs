using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speedWalkingUp;
    [SerializeField] private float speedWalkingDown;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float timeCompleteRotation;
    [SerializeField] private Animator animator;
    public CharacterCamera characterCamera;
    [SerializeField] private CharacterInputs _inputs;

    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        AnimationCharacter();
        RunCharacter();
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
        animator.SetBool("isWalking", _isWalking);
    }

    private void RunningTransition(bool _isRunning)
    {
        animator.SetBool("isRunning", _isRunning);
    }

    private void WalkingBackTransition(bool _isWalkingBack)
    {
        animator.SetBool("isWalkingBack", _isWalkingBack);
    }

}
