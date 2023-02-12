using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    // Velocidad a la que rota el sol
    [SerializeField] private float sunSpeed;

    // Se llama al método de SunRotation() en cada frame.
    void Update()
    {
        SunRotation();
    }

    // Método que permite rotar el sol en el eje X. Esto se hace para
    // simular el ciclo de un día (día, tarde, noche). Desde el inspector
    // se puede definir la velocidad a la que rota el sol para definir
    // la duración de un día.
    private void SunRotation()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(180, 0, 0);
        transform.rotation = Quaternion.Lerp(startRotation, targetRotation, Time.deltaTime * sunSpeed);
    }
}
