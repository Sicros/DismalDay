using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class LoadSceneController : MonoBehaviour
{
    public List<CharacterPositions> listCharacterPositions;

    private void Awake()
    {
        listCharacterPositions = new List<CharacterPositions> ();
        listCharacterPositions.Add(new CharacterPositions("", "Snowfield", new Vector3 (0f, 0.0001f, -70f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MainHall", new Vector3 (19f, 0.2f, 20f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MeetingRoom01", new Vector3 (13.5f, 0.2f, 70f), Quaternion.Euler(0f, -145f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "Office02", new Vector3 (25f, 0.2f, 73.5f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("", "MainHall02", new Vector3 (19f, 0.2f, 102f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("Snowfield", "MainHall", new Vector3 (19f, 0.2f, 20f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "Snowfield", new Vector3 (29.5f, 0.0001f, 71f), Quaternion.Euler(0f, 180f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "MeetingRoom01", new Vector3 (13.5f, 0.2f, 70f), Quaternion.Euler(0f, -145f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "Office02", new Vector3 (25f, 0.2f, 73.5f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MainHall", "MainHall02", new Vector3 (19f, 0.2f, 102f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("MeetingRoom01", "MainHall", new Vector3 (17f, 0.2f, 70f), Quaternion.Euler(0f, 0f, 0f)));
        listCharacterPositions.Add(new CharacterPositions("Office02", "MainHall", new Vector3 (21f, 0.2f, 74f), Quaternion.Euler(0f, 0f, 0f)));
        // Comienza desde la carpeta "Assets"
        // Debug.Log(Application.dataPath);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

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

public struct CharacterPositions {
    public string initialScene;
    public string nextScene;
    public Vector3 initialPosition;
    public Quaternion initialRotation;

    public CharacterPositions(string initial, string next, Vector3 position, Quaternion rotation)
    {
        initialScene = initial;
        nextScene = next;
        initialPosition = position;
        initialRotation = rotation;
    }
}