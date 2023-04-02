using UnityEngine;

public class SnowMovement : MonoBehaviour
{
    // Posición que seguirá la nieve.
    [SerializeField] private Transform followObjectPosition;

    // Cuan alto se encuentra este sistema con respecto al punto que sigue.
    [SerializeField] float addYAxis;

    // Permite añadir un vector adicional al sistema.
    private Vector3 _addPosition;

    // Posición correspondiente al sistema de particula, sumando la altura que corresponde.
    private void Awake()
    {
        _addPosition = Vector3.zero;
        _addPosition.y += addYAxis;
    }

    // En cada frame llama al método que permite seguir una posición.
    private void Update()
    {
        FollowPosition();
    }

    // Método que que asigna la posición objetivo al sistema de partículas, además de una adicional
    // definida por un vector cero m+ás la altura que define el usuario.
    private void FollowPosition()
    {
        transform.position = followObjectPosition.position + _addPosition;
    }
}
