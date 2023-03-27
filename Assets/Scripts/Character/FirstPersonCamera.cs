using UnityEngine;

/*
Permite al utilizar la cámara en primera persona rotarla hacia los lados y arriba y abajo
con el movimiento del mouse. Se pueden definir ciertos atributos asociados a este como
la sensibilidad vertical y horizontal, la distancia máxima y mínima que puede recorrer
de forma horizontal, así como también a que objeto debe seguir la cámara.
*/

public class FirstPersonCamera : MonoBehaviour
{
    // Transform del personaje que sigue la camara.
    [SerializeField] private Transform character;

    // Rotación y posición que imita la camara.
    [SerializeField] private Transform pointOfView;

    // Distancia mínima que puede recorrer la cámara de forma vertical.
    [SerializeField] private float minimumVertical;

    // Distancia máxima que puede recorrer la cámara de forma vertical.
    [SerializeField] private float maximumVertical;

    // Sensibilidad vertical del mouse.
    [SerializeField] private float sensitivityVertical;

    // Sensibilidad horizontal del mouse.
    [SerializeField] private float sensitivityHorizontal;

    // Variable que almacena la rotación que debe realizar la camara.
    private float _rotationX;

    // Variable que almacena la rotación del personaje.
    private Quaternion _characterInitialRotation;

    // Método que se ejecuta al habilitarse el objeto.
    private void OnEnable()
    {
        // Iniciación de variable _rotationX
        _rotationX = 0f;

        // Almacenamiento de la rotación inicial del personaje.
        _characterInitialRotation = character.transform.rotation;

        // Aplicación rotación inicial personaje a componente.
        transform.rotation = character.transform.rotation;
    }

    private void Update()
    {
        // La cámara actualiza su posición para que imite la del objeto asignado.
        transform.position = pointOfView.transform.position;

        // Se almacena el movimiento realizado en el eje Y de acuerdo a su sensibilidad.
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVertical;

        // Recalcula la rotación de acuerdo al mínimo y máximo movimiento vertical.
        _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);

        // Se almacena el movimiento realizado en el eje X de acuerdo a su sensibilidad.
        float delta = Input.GetAxis("Mouse X") * sensitivityHorizontal;

        // Obtiene la rotación a realizar en el Y sumando la diferencia obtenido del movimiento del mouse.
        float rotationY = transform.eulerAngles.y + delta;

        // Asigna la nueva rotación calculada al transform de la camara.
        transform.eulerAngles = new Vector3(_rotationX, rotationY, 0);

        // Asigna la nueva rotación calculada al transform del personaje.
        character.transform.eulerAngles = new Vector3(_rotationX, rotationY, 0);
    }

    // Método que se ejecuta al inhabilitarse el objeto.
    private void OnDisable()
    {
        // Se mantiene la rotación del personaje en sus (Y, Z), pero se devuelve a su estado original
        // en el eje X.
        character.transform.rotation = Quaternion.Euler(
            _characterInitialRotation.eulerAngles.x,
            character.transform.rotation.eulerAngles.y,
            character.transform.rotation.eulerAngles.z
        );
    }
}
