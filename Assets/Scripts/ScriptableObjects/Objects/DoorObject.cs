using UnityEngine;

[CreateAssetMenu(menuName = "Objects/DoorObject")]

public class DoorObject : ScriptableObject
{
    public int id;
    public string doorName;
    public string sceneName;
    public bool isOpen;
    public int keyId;
}
