using UnityEngine;

public enum MouseKeys
{
    PrimaryKey,
    SecondaryKey,
    MiddleKey
}

public class CharacterInputs : MonoBehaviour
{
    public KeyCode goUpKey;
    public KeyCode goDownKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    public KeyCode runKey;
    public MouseKeys aimButton;
    public MouseKeys actionButton;

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
