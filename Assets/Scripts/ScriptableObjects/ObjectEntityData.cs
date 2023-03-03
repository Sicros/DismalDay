using UnityEngine;

[CreateAssetMenu(menuName = "Entity/EntityData")]

public class ObjectEntityData : ScriptableObject
{
    // Vida máxima de la entidad.
    public float maximumHealth;

    // Velocidad de caminar de la entidad hacia delante.
    public float speedWalkingUp;

    // Velocidad de caminar de la entidad hacia atrás.
    public float speedWalkingDown;

    // Velocidad de rotación de la entidad.
    public float rotationSpeed;
}
