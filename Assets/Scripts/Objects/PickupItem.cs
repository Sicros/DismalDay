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

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickupObject.Play();
            inventoryController.AddItem(id, quantity);
            Destroy(gameObject);
        }
    }
}
