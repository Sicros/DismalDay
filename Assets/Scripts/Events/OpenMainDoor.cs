using UnityEngine;
using UnityEngine.Events;
using System;

public class OpenMainDoor : MonoBehaviour
{
    // Define un evento que hace referencia al mensaje del canvas.
    public UnityEvent<string> onInteraction;

    // Evento que define que acciones tomar al interactuar con el objeto.
    public event Action<string> onAction;

    // Booleano que revisa si se ha activado el evento.
    private bool isTriggerActive = false;

    // Referencia al control de escenas.
    private LoadSceneController _loadSceneController;

    // Referencia al controlador de inventario.
    [SerializeField] private InventoryController _inventoryController;

    // Referencia a un scriptable object de puerta.
    [SerializeField] private DoorObject _doorObject;

    // Referencia a los inputs del jugador.
    private KeyInputsSetup _inputs;

    // Al comenzar, se cargan los componentes de control de escenas e inputs. También se suscribe la carga de escena
    // al evento de action.
    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
        onAction += _loadSceneController.LoadScene;
    }

    // Permite interactuar con las puertas y revisar si estas está abiertas, en caso contrario
    // pregunta por si el personaje tiene la llave en su inventario.
    private void Update()
    {
        if
        (Input.GetKeyDown(_inputs.GetInteractionKey()) && isTriggerActive)
        {
            if (_doorObject.isOpen)
            {
                onAction?.Invoke(_doorObject.sceneName);
            }
            else if (_inventoryController.GetQuantityObject(_doorObject.keyId) > 0)
            {
                onInteraction?.Invoke("La puerta " + _doorObject.doorName + " ha sido abierta");
                _doorObject.isOpen = true;
            }
            else
            {
                onInteraction?.Invoke("La puerta " + _doorObject.doorName + " está cerrada");
            }
        }
    }

    // En caso de que el personaje esté en el rango de la puerta, el booleano de triger cambia a true.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = true;
        }
    }

    // En caso de que el personaje salga de este rango, se desactiva el booleano de trigger.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = false;
        }
    }

    // Método que permite abrir la puerta.
    public void OpenDoor()
    {
        _doorObject.isOpen = true;
    }

    // Método que llama al evento de interración con la puerta. Permite imprimir un mensaje en pantalla
    // si esta ha sido abierta.
    public void TextDoorOpen()
    {
        onInteraction?.Invoke("La puerta " + _doorObject.doorName + " ha sido abierta");
    }

    // Método que llama al envento de interacción con la puerta. Permite imprimir un mensaje en pantalla
    // si esta está cerrada.
    public void TextDoorClosed()
    {
        onInteraction?.Invoke("La puerta " + _doorObject.doorName + " está cerrada");
    }

    // Permite obtener el estado de la puerta, es decir, si esta está abierta o cerrada.
    public bool GetStatusDoor()
    {
        return _doorObject.isOpen;
    }
}
