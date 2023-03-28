using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Entity/CharacterData")]
public class CharacterData : ObjectEntityData
{
    // Tiempo en que demora el personaje en realizar una rotación de 180°
    public float timeCompleteRotation;

    // Vida actual del personaje;
    public float currentHealth;

    public float speedRunningUp;

    public float speedRunningDown;

    // Arreglo que simula el inventario del personaje.
    // Solo tiene espacio para llevar 10 objetos diferentes. A partir de ahí,
    // la cantidad máxima de cada uno se obtiene de la biblioteca de objeto.
    public int[,] inventoryCharacter = new int[10, 2] {
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
        {-1, 0},
    };
}
