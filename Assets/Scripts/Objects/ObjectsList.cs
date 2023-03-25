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
                description = "Cura 5 puntos de vida",
                healAmount = 5f,
                maxQuantity = 3,
                consumable = true,
                keyItem = false
            }
        );
        itemLibrary.Add(
            1,
            new Item {
                nameObject = "Balas pistola",
                description = "Munición para la pistola",
                healAmount = 0f,
                maxQuantity = 60,
                consumable = true,
                keyItem = false
            }
        );
        itemLibrary.Add(
            2,
                new Item {
                nameObject = "LLave complejo de oficinas",
                description = "Llave que permite abrir la puerta principal del complejo de oficinas",
                healAmount = 0f,
                maxQuantity = 1,
                consumable = true,
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
    public float healAmount;
    public int maxQuantity;
    public bool consumable;
    public bool keyItem;
}