using UnityEngine;

public class OpenCodeDoor : MonoBehaviour
{
    // Referencia a la apertura de una puerta.
    [SerializeField] private OpenMainDoor openMainDoor;

    // Texto que define el código para abrir una puerta.
    [SerializeField] private string codeDoor;

    // Referencia al canvas que permite mostrar el panel para ingresar el código.
    [SerializeField] private GameObject canvasObject;
    
    // Referencia a los inputs del personaje.
    private KeyInputsSetup _inputs;

    // Se inicializa el componente de inputs además de desactivar el canvas del código al inicio.
    private void Start()
    {
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
        canvasObject.SetActive(false);
    }

    // Si el personaje está en el rango del panel e interactua con él, podrá ingresar el código.
    private void OnTriggerStay(Collider other)
    {
        if (
            other.tag == "Player"
            && Input.GetKeyDown(_inputs.GetInteractionKey())
            && !canvasObject.activeSelf
            && !openMainDoor.GetStatusDoor()
        )
        {
            canvasObject.SetActive(true);
        }
        if (
            canvasObject.activeSelf
            && Input.GetKeyDown(KeyCode.Escape)
        )
        {
            canvasObject.SetActive(false);
        }
    }

    // Método que revisa si el código ingresado es el correcto o no.
    public void OpenDoor(string inputCode)
    {
        if (codeDoor == inputCode)
        {
            openMainDoor.OpenDoor();
            openMainDoor.TextDoorOpen();
            canvasObject.SetActive(false);
        }
    }
}
