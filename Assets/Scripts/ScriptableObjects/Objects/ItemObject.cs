using UnityEngine;

[CreateAssetMenu(menuName = "Objects/ItemObject")]
public class ItemObject : ScriptableObject
{
    // ID del objeto.
    public int id;

    // Nombre del objeto.
    public string nameObject;

    // Descripción del objeto.
    public string description;

    // Cantidad de curación del objeto.
    public float healAmount;

    // Cantidad máxima que se puede llevar del objeto.
    public int maxQuantity;

    // Indica si el objeto es consumible o no.
    public bool consumable;

    // Indica si el objeto es importante.
    public bool keyItem;
}
