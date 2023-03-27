using UnityEngine;

public class GameManager : MonoBehaviour
{
    // public indica que se puede acceder globalmente.
    // static define que solo existe uno para todos.
    // GameManager es el tipo de dato que corresponde en este caso.
    // Por regla se utiliza el nombre "instance".
    public static GameManager instance;

    // Lo primero que se debe hacer es preguntar si ya existe un GameManager
    private void Awake()
    {
        if (instance != null)
        {
            // Ya existe un GameManager, por lo que se destruye.
            Destroy(gameObject);
        }
        else
        {
            // "this" es una palabra clave que indica que se apunta a si mismo.
            // Es decir, hacia el mismo GameManager definido en este script.
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
}
