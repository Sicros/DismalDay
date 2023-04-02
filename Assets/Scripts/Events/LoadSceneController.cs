using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class LoadSceneController : MonoBehaviour
{
    public List<CharacterPositions> listCharacterPositions;

    private void Awake()
    {
        // Biblioteca relacionada a las posiciones y rotaciones iniciales del personaje en cada escena o en la transcición de una a otra.
        listCharacterPositions = new List<CharacterPositions> ();
        listCharacterPositions.Add(new CharacterPositions("", "Snowfield", new Vector3 (0f, 0.0001f, -70f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MainHall", new Vector3 (19f, 0.2f, 20f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MeetingRoom01", new Vector3 (13.5f, 0.2f, 70f), Quaternion.Euler(0f, -145f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "Office02", new Vector3 (25f, 0.2f, 73.5f), Quaternion.Euler(0f, 90f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MainHall02", new Vector3 (19f, 0.2f, 102f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("Snowfield", "MainHall", new Vector3 (19f, 0.2f, 20f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "Snowfield", new Vector3 (29.5f, 0.0001f, 71f), Quaternion.Euler(0f, 180f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "MeetingRoom01", new Vector3 (13.5f, 0.2f, 70f), Quaternion.Euler(0f, -145f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "Office02", new Vector3 (25f, 0.2f, 73.5f), Quaternion.Euler(0f, 90f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "MainHall02", new Vector3 (19f, 0.2f, 102f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MeetingRoom01", "MainHall", new Vector3 (17f, 0.2f, 70f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("Office02", "MainHall", new Vector3 (21f, 0.2f, 74f), Quaternion.Euler(0f, 0f, 0f)));
    }

    // Método que permite cargar escenas por su nombre.
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // Método que permite obtener la posición inicial de un personaje de una escena a otra.
    // Se considera el caso en que no exista la referencia de una a otra, dando una por defecto.
    public CharacterPositions GetInitialPosition(string initial, string next)
    {
        foreach (CharacterPositions characterPosition in listCharacterPositions)
        {
            if (characterPosition.initialScene == initial && characterPosition.nextScene == next)
            {
                return characterPosition;
            }
        }
        foreach (CharacterPositions characterPosition in listCharacterPositions)
        {
            if (characterPosition.initialScene == "" && characterPosition.nextScene == next)
            {
                return characterPosition;
            }
        }
        return new CharacterPositions("", "", Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
    }
}

// Estructrura que define la posición del personaje, guardando información de su escena actual,
// anterior y la posición y rotación inicial que debe tener en la nueva escena.
public struct CharacterPositions {
    public string initialScene;
    public string nextScene;
    public Vector3 initialPosition;
    public Quaternion initialRotation;

    // Constructor de la estructura.
    public CharacterPositions(string initial, string next, Vector3 position, Quaternion rotation)
    {
        initialScene = initial;
        nextScene = next;
        initialPosition = position;
        initialRotation = rotation;
    }
}