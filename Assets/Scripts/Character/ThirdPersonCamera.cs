using UnityEngine;

/*
Permite al utilizar la cámara en primera persona rotarla hacia los lados y arriba y abajo
con el movimiento del mouse. Se pueden definir ciertos atributos asociados a este como
la sensibilidad vertical y horizontal, la distancia máxima y mínima que puede recorrer
de forma horizontal, así como también a que objeto debe seguir la cámara.
*/

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform camTarget;
    public float pLerp = 0.02f;
    public float rLerp = 0.01f;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);        
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rLerp);        
    }
}
