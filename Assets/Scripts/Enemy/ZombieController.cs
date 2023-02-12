using UnityEngine;

// Se construye una lista de valores de acuerdo a las acciones que realiza el enemigo
// Chase: Perseguir al jugador y observar al jugador, sea donde sea que vaya.
// Watch: Solo observa al jugador, quedandose en el punto que quedó.
// DoNothing: Ni persigue ni observa al jugador.
public enum ZombieAction
{
    Chase,
    Watch,
    DoNothing
}

public class ZombieController : MonoBehaviour
{
    // Variable que referencia el transform del jugador. Configurable desde el inspector.
    public Transform player;

    // Velocidad a la que persigue el enemigo al jugador. Configurable desde el inspector.
    public float chasingSpeed;

    // Distancia que se mantiene alejado el enemigo del jugador. Configurable desde el inspector.
    public float keepDistance;

    // Velocidad de rotación del enemigo. Configurable desde el inspector.
    public float rotationSpeed;

    // Acción que realizará el enemigo de acuerdo a la lista definida más arriba.
    public ZombieAction action;

    // Para decidir que acción tomar, se utiliza un "switch()", que recibe como parámetro
    // la acción definida desde el inspector (action). Cada caso, ejecuta un método llamado
    // ChangeEyesColor(), que permite cambiar el color de los ojos del enemigo de acuerdo
    // a la acción que realice (si persigue son rojos, si solo mira son amarillos y si se
    // queda quieto son verdes).
    void Update()
    {
    }
}
