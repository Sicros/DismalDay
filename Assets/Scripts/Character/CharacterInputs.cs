using UnityEngine;

/*
La finalidad de este scripts es unicamente almacenar los botones y teclas
que permite realizar determinadas acciones con el personaje o el mismo ambiente.
Cada una de estas variables son públicas para que puedan ser accedidas por otras
clases.
*/

// Definido de variable que almacena cada botón asociado al mouse.
public enum MouseKeys
{
    PrimaryKey,
    SecondaryKey,
    MiddleKey
}

public class CharacterInputs : MonoBehaviour
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
