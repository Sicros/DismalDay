using UnityEngine;
using UnityEngine.Events;
using System;

public class OpenMainDoor : MonoBehaviour
{
    public UnityEvent<string> onInteraction;

    public event Action<string> onAction;

    private bool isTriggerActive = false;

    private LoadSceneController _loadSceneController;

    [SerializeField] private InventoryController _inventoryController;

    [SerializeField] private DoorObject _doorObject;

    private KeyInputsSetup _inputs;

    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
        onAction += _loadSceneController.LoadScene;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = false;
        }
    }

    public void OpenDoor()
    {
        _doorObject.isOpen = true;
    }

    public void TextDoorOpen()
    {
        onInteraction?.Invoke("La puerta " + _doorObject.doorName + " ha sido abierta");
    }

    public void TextDoorClosed()
    {
        onInteraction?.Invoke("La puerta " + _doorObject.doorName + " está cerrada");
    }

    public bool GetStatusDoor()
    {
        return _doorObject.isOpen;
    }
}
