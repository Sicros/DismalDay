using UnityEngine;

public class OpenCodeDoor : MonoBehaviour
{
    [SerializeField] private OpenMainDoor openMainDoor;

    [SerializeField] private string codeDoor;

    [SerializeField] private GameObject canvasObject;
    
    // Referencia a los inputs del personaje.
    private KeyInputsSetup _inputs;

    private void Start()
    {
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
        canvasObject.SetActive(false);
    }

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
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
