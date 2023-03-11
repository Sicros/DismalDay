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
    [SerializeField] private string sceneName;

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
            onAction?.Invoke(sceneName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = true;
            onInteraction?.Invoke("Press shoot key to open door.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggerActive = false;
            onInteraction?.Invoke("");
        }
    }
}
