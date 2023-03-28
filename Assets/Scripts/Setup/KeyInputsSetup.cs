using UnityEngine;

public class KeyInputsSetup : MonoBehaviour
{
    [SerializeField] private KeyInputs keyInputs;

    // Obtiene botón de mouse para disparar.
    public int GetActionButton()
    {
        return keyInputs.MouseButton(keyInputs.actionButton);
    }

    // Obtiene botón de mouse para apuntar.
    public int GetAimButton()
    {
        return keyInputs.MouseButton(keyInputs.aimButton);;
    }

    // Obtiene tecla para caminar hacia atrás.
    public KeyCode GetGoDownKey()
    {
        return keyInputs.goDownKey;
    }

    // Obtiene tecla para caminar hacia delante.
    public KeyCode GetGoUpKey()
    {
        return keyInputs.goUpKey;
    }

    // Obtiene tecla para interactuar con objetos del escenario.
    public KeyCode GetInteractionKey()
    {
        return keyInputs.interactionKey;
    }

    // Obtiene tecla para correr.
    public KeyCode GetRunKey()
    {
        return keyInputs.runKey;
    }

    // Obtiene tecla para rotar hacia la izquierda.
    public KeyCode GetTurnLeftKey()
    {
        return keyInputs.turnLeftKey;
    }

    // Obtiene tecla para rotar hacia la derecha.
    public KeyCode GetTurnRightKey()
    {
        return keyInputs.turnRightKey;
    }
}
