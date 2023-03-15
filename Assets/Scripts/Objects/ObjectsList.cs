using UnityEngine;
using System.Collections.Generic;

/*
Script que contiene la biblioteca de objetos utilizados en el juego
*/

public class ObjectsList : MonoBehaviour
{
    public Dictionary<int, Item> itemLibrary = new Dictionary<int, Item>();

    private void Awake()
    {
        itemLibrary.Add(
            0,
            new Item {
                nameObject = "Medkit",
                description = "Heals 5 HP",
                type = "HealingObject",
                healAmount = 5f,
                maxQuantity = 3,
                consumable = true,
                keyItem = false
            }
        );
        itemLibrary.Add(
            1,
            new Item {
                nameObject = "Handguns's Bullets",
                description = "Ammunition for Handgun weapon",
                healAmount = 0f,
                maxQuantity = 60,
                consumable = true,
                keyItem = false
            }
        );
        itemLibrary.Add(
            2,
                new Item {
                nameObject = "Key door",
                description = "Key to open doors",
                healAmount = 0f,
                maxQuantity = 999,
                consumable = false,
                keyItem = true
            }
        );
    }
}

// Estructura que define a un objeto. Todos tienen un nombre, descripción, tipo y cantidad máxima.
// El resto de valores se define de acuerdo a la utilidad del objeto.
public struct Item {
    public string nameObject;
    public string description;
    public string type;
    public float healAmount;
    public int maxQuantity;
    public bool consumable;
    public bool keyItem;
}