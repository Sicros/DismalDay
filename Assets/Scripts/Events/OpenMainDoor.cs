using UnityEngine;
using UnityEngine.Events;
using System;

public class OpenMainDoor : MonoBehaviour
{
    public UnityEvent<string> onInteraction;

    public event Action<string> onAction;

    private bool isTriggerActive = false;

    [SerializeField] private CharacterInputs _characterInputs;

    [SerializeField] private LoadSceneController _loadSceneController;

    [SerializeField] private InventoryController _inventoryController;

    [SerializeField] private string sceneName;

    [SerializeField] private int idKey;

    [SerializeField] private bool isOpen;

    [SerializeField] private string nameDoor;

    private void Awake()
    {
        onAction += _loadSceneController.LoadScene;
    }

    private void Update()
    {
        if
        (
            Input.GetMouseButtonDown(_characterInputs.MouseButton(_characterInputs.actionButton))
            && !Input.GetMouseButton(_characterInputs.MouseButton(_characterInputs.aimButton))
            && isTriggerActive
        )
        {
            if (isOpen)
            {
                onAction?.Invoke(sceneName);
            }
            else if (_inventoryController.GetQuantityObject(idKey) > 0)
            {
                onInteraction?.Invoke("La puerta " + nameDoor + " ha sido abierta");
                isOpen = true;
            }
            else
            {
                onInteraction?.Invoke("La puerta " + nameDoor + " está cerrada");
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
        isOpen = true;
    }

    public void TextDoorOpen()
    {
        onInteraction?.Invoke("La puerta " + nameDoor + " ha sido abierta");
    }

    public void TextDoorClosed()
    {
        onInteraction?.Invoke("La puerta " + nameDoor + " está cerrada");
    }

    public bool GetStatusDoor()
    {
        return isOpen;
    }
}
