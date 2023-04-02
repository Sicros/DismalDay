using UnityEngine;

[CreateAssetMenu(menuName = "Objects/DoorObject")]

public class DoorObject : ScriptableObject
{
    // Identificador de la puerta.
    public int id;

    // Nombre de la puerta.
    public string doorName;

    // Nombre de la escena a la que lleva la puerta.
    public string sceneName;

    // Booleano que indica si la puerta está abierta.
    public bool isOpen;

    // Identificador de la llave que permite abrir la puerta.
    public int keyId;
}
