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
    private CharacterInputs _inputs;

    private void Start()
    {
        GameManager.instance.TryGetComponent<CharacterInputs>(out _inputs);
    }

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(_inputs.interactionKey))
        {
            pickupObject.Play();
            inventoryController.AddItem(id, quantity);
            uiText.UpdateInteractionObject("Obtuviste " + _objectList.itemLibrary[id].nameObject + " (" + quantity + ")");
            Destroy(gameObject);
        }
    }
}
