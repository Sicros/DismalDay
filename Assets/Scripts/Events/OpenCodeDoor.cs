using UnityEngine;

public class OpenCodeDoor : MonoBehaviour
{
    // Referencia a los inputs del personaje.
    [SerializeField] private CharacterInputs characterInputs;

    [SerializeField] private OpenMainDoor openMainDoor;

    [SerializeField] private string codeDoor;

    [SerializeField] private GameObject canvasObject;

    private void Awake()
    {
        canvasObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entró: " + other.name);
    }

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
    private void OnTriggerStay(Collider other)
    {
        if (
            other.tag == "Player"
            && Input.GetMouseButtonDown(characterInputs.MouseButton(characterInputs.actionButton))
            && !Input.GetMouseButton(characterInputs.MouseButton(characterInputs.aimButton))
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

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Salió: " + other.name);
        if (other.tag == "Player")
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
