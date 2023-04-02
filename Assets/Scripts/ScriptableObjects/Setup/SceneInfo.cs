using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/SceneInfo")]

public class SceneInfo : ScriptableObject
{
    // Nombre de la última escena.
    public string previousScene;

    // Nombre de la escena actual.
    public string currentScene;
}
