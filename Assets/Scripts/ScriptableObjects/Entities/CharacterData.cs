using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Entity/CharacterData")]
public class CharacterData : ObjectEntityData
{
    // Tiempo en que demora el personaje en realizar una rotación de 180°
    public float timeCompleteRotation;

    // Vida actual del personaje;
    public float currentHealth;

    // Velocidad con la que el personaje avanza hacia delante.
    public float speedRunningUp;

    // Velocidad con la que el personaje avanza hacia atrás.
    public float speedRunningDown;

    // Capacidad máxima del inventario del personaje.
    public int maxInventoryCapacity;

    // Arreglo que simula el inventario del personaje.
    // Solo tiene espacio para llevar 8 objetos diferentes. A partir de ahí,
    // la cantidad máxima de cada uno se obtiene de la biblioteca de objeto.
    public List<InventoryCharacter> inventoryCharacter;
}
