using UnityEngine;
using System;

public class PickupItem : MonoBehaviour
{
    // ID del objeto recogio.
    [SerializeField] private int id;

    // Cantidad del objeto recogido.
    [SerializeField] private int quantity;

    // Audio que se reproduce al recoger el objeto.
    [SerializeField] private AudioSource pickupObject;

    // Referencia al inventario del personaje.
    [SerializeField] private InventoryController inventoryController;

    // Referencia a UIText
    [SerializeField] private UIText uiText;

    // Referencia a biblioteca de objetos.
    [SerializeField] private ObjectsList _objectList;

    // Referencia a los inputs del personaje.
    private KeyInputsSetup _inputs;

    // Carga del componente relacionada los inputs del jugador.
    private void Start()
    {
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
    }

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(_inputs.GetInteractionKey()))
        {
            int result = inventoryController.AddItem(id, quantity);
            if (result == 0)
            {
                uiText.UpdateInteractionObject("Ya tienes el máximo de este objeto o el inventario está lleno");
            }
            else
            {
                pickupObject.Play();
                uiText.UpdateInteractionObject("Obtuviste " + _objectList.itemLibrary[id].nameObject + " (" + result + ")");
                SaveAndLoad.instance.AddObjectToDestroy(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
