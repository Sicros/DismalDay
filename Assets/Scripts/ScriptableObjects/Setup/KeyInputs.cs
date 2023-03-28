using UnityEngine;

[CreateAssetMenu(menuName = "Setup/KeyInputs")]

public class KeyInputs : ScriptableObject
{
    // Tecla que permite moverse hacia delante.
    public KeyCode goUpKey;

    // Tecla que permite moverse hacia atras.
    public KeyCode goDownKey;

    // Tecla que permite rotar hacia la izquierda.
    public KeyCode turnLeftKey;

    // Tecla que permite rotar hacia la derecha.
    public KeyCode turnRightKey;

    // Tecla que permite al personaje correr (duplicar velocidad).
    public KeyCode runKey;

    // Tecla que permite al personaje interactura con su entorno.
    public KeyCode interactionKey;

    // Botón de apuntado. Utilizado también para el cambio de cámara.
    public MouseKeys aimButton;

    // Botón de acción. Permite disparar cuando se presiona el aiumButton.
    public MouseKeys actionButton;

    // Método con switch que permite asignar el valor entero correspondiente
    // a cada tecla del mouse, es debido a que necesita un valor entero para poder accionarlo.
    public int MouseButton(MouseKeys buttonOption)
    {
        switch (buttonOption)
        {
            case MouseKeys.PrimaryKey:
                return 0;
            case MouseKeys.SecondaryKey:
                return 1;
            case MouseKeys.MiddleKey:
                return 2;
            default:
                return 1;
        }
    }
}
