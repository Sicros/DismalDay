using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setup/SceneInfo")]

public class SceneInfo : ScriptableObject
{
    public string previousScene;
    public string currentScene;
}
