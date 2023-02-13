using UnityEngine;
using Cinemachine;

/*
Permite intercambiar las camaras que siguen al personaje. Actualmente existen dos:
    1. Primera persona: Utilizada para apuntar y disparar a los enemigos.
    2. Tercera persona: Utilizado para moverse por el escenario.

Por defecto, las camaras se intercambian con el segundo clic del mouse.
*/

public class CharacterCamera : MonoBehaviour
{
    // Variable que identifica a la cámara en primera persona.
    public Camera firstPerson;

    // Variable que identifica a la cámara en tercera persona.
    public CinemachineVirtualCamera thirdPerson;

    // Identifica si la cámara en tercera persona está activada (true) o desactivada (false).
    [SerializeField] private bool activeThirdPerson;

    // Variable que almacena los inputs para mover o rotar al personaje.
    [SerializeField] private CharacterInputs _inputs;

    // Se fija la cámara inicial dependiendo del valor que se le asigne a activeThirdPerson.
    // Si está activo, se usará la cámara en tercera persona, de lo contrario será en
    // primera persona.
    void Awake()
    {
        activeThirdPerson = ChangeCamera(activeThirdPerson, firstPerson, thirdPerson);
    }

    // Similar a la acción realizada en el método de Start(), solo que esta vez es el usuario
    // quien debe presionar una tecla para hacer el cambio entre cámaras. La tecla se define
    // desde el inspector de Unity, y debe ser un botón del mouse.
    void Update()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.aimButton)))
        {
            activeThirdPerson = ChangeCamera(activeThirdPerson, firstPerson, thirdPerson);

        }
        if (Input.GetMouseButtonUp(_inputs.MouseButton(_inputs.aimButton)))
        {
            activeThirdPerson = ChangeCamera(activeThirdPerson, firstPerson, thirdPerson);
        }
    }

    // Método que permite alternar entre dos cámaras. El primer parámetro indica si la cámara
    // a utilizar es en tercera persona (thirdPersonCam), si es true se activa esta y se
    // desactiva la otra (firstPersonCam), si es false ocurre lo contrario.
    // El método retorna el valor contrario al booleano ingresado.
    private bool ChangeCamera(bool isThirdPerson, Camera firstPersonCam, CinemachineVirtualCamera thirdPersonCam)
    {
        thirdPersonCam.gameObject.SetActive(isThirdPerson);
        firstPersonCam.gameObject.SetActive(!isThirdPerson);
        return !isThirdPerson;
    }
}
