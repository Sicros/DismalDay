using UnityEngine;
using System.Collections.Generic;

/*
Script que contiene la biblioteca de objetos utilizados en el juego
*/

public class ObjectsList : MonoBehaviour
{
    public Dictionary<int, Item> itemLibrary = new Dictionary<int, Item>();
    [SerializeField] private ItemObject[] itemObjects;

    private void Awake()
    {
        foreach (ItemObject _itemObject in itemObjects)
        {
            itemLibrary.Add(
                _itemObject.id,
                new Item(
                    _itemObject.nameObject,
                    _itemObject.description,
                    _itemObject.healAmount,
                    _itemObject.maxQuantity,
                    _itemObject.consumable,
                    _itemObject.keyItem
                )
            );
        }
    }
}

// Estructura que define a un objeto. Todos tienen un nombre, descripción, tipo y cantidad máxima.
// El resto de valores se define de acuerdo a la utilidad del objeto.
public struct Item {
    public string nameObject;
    public string description;
    public float healAmount;
    public int maxQuantity;
    public bool consumable;
    public bool keyItem;

    public Item (string name, string desc, float heal, int maxq, bool isConsumable, bool isKey)
    {
        nameObject = name;
        description = desc;
        healAmount = heal;
        maxQuantity = maxq;
        consumable = isConsumable;
        keyItem = isKey;
    }
}