using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct SaveData
{
    public float currentHealthCharacter;
    public int currentHandgunBullets;
    public List<OpenDoor> openDoors;
    public List<ObjectsPerScene> objectsToDestroy;
    public Dictionary<string, List<string>> itemsToDestroy;
    public List<InventoryCharacter> inventoryCharacter;
}

[Serializable]
public struct OpenDoor
{
    public int id;
    public bool isOpen;

    public OpenDoor(int idDoor, bool doorIsOpen)
    {
        id = idDoor;
        isOpen = doorIsOpen;
    }
}

[Serializable]
public struct InventoryCharacter
{
    public int id;
    public int quantity;

    public InventoryCharacter(int idItem, int quantityItem)
    {
        id = idItem;
        quantity = quantityItem;
    }
}

[Serializable]
public struct ObjectsPerScene
{
    public string sceneName;
    public List<string> gameObjectNames;

    public ObjectsPerScene(string scene, List<string> zombies)
    {
        sceneName = scene;
        gameObjectNames = zombies;
    }
}

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private WeaponObject weaponData;
    [SerializeField] private DoorObject[] doorObjects;
    private SaveData _tempSave;
    public static SaveAndLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        _tempSave = new SaveData();
        NewGame();
    }

    public void LoadDataJSON(string saveDataName)
    {
        if (System.IO.File.Exists(Application.dataPath + saveDataName))
        {
            var _saveDataJson = File.ReadAllText(Application.dataPath + saveDataName);
            var _tempSave = JsonUtility.FromJson<SaveData>(_saveDataJson);
        }
        characterData.currentHealth = _tempSave.currentHealthCharacter;
        characterData.inventoryCharacter = _tempSave.inventoryCharacter;
        weaponData.currentBullets = _tempSave.currentHandgunBullets;
        ReplaceDoorsStatus();
        DestroyObjectsFromList();
        Debug.Log("Data loaded");
    }

    public void SaveDataJSON(string saveDataName)
    {
        _tempSave.currentHealthCharacter = characterData.currentHealth;
        _tempSave.inventoryCharacter = characterData.inventoryCharacter;
        _tempSave.currentHandgunBullets = weaponData.currentBullets;
        SaveDoorsStatus();
        var stringjson = JsonUtility.ToJson(_tempSave);
        File.WriteAllText(Application.dataPath + saveDataName, stringjson);
        print("Saving");
    }

    public void NewGame()
    {
        _tempSave.currentHealthCharacter = 10;
        _tempSave.currentHandgunBullets = 15;
        _tempSave.openDoors = new List<OpenDoor>();
        _tempSave.objectsToDestroy = new List<ObjectsPerScene>();
        _tempSave.inventoryCharacter = new List<InventoryCharacter> ();
        DeafultDoorsStatus();
        DefaultInventoryCharacter();
    }

    public void AddObjectToDestroy(GameObject objectToDestroy)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        bool _zombieAdded = false;
        string _objectName = GetGameObjectPath(objectToDestroy);
        foreach (ObjectsPerScene _objectsInScene in _tempSave.objectsToDestroy)
        {
            if(_objectsInScene.sceneName == sceneName)
            {
                _objectsInScene.gameObjectNames.Add(_objectName);
                _zombieAdded = true;
                break;
            }
        }
        if (!_zombieAdded)
        {
            List<string> _tempZombieList = new List<string>();
            _tempZombieList.Add(_objectName);
            _tempSave.objectsToDestroy.Add(new ObjectsPerScene(sceneName, _tempZombieList));

        }
    }

    public void DestroyObjectsFromList()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        foreach (ObjectsPerScene _zombiesInScene in _tempSave.objectsToDestroy)
        {
            if (_zombiesInScene.sceneName == sceneName)
            {
                foreach (string _zombieObjectPath in _zombiesInScene.gameObjectNames)
                {
                    Destroy(GameObject.Find(_zombieObjectPath));
                }
            }
        }
    }

    public void AddItemToDestroy(string sceneName, string zombieName)
    {
        if (!_tempSave.itemsToDestroy.ContainsKey(sceneName))
        {
            _tempSave.itemsToDestroy.Add(sceneName, new List<string>());
        }
        _tempSave.itemsToDestroy[sceneName].Add(zombieName);
    }

    public void ReplaceDoorsStatus()
    {
        foreach (DoorObject doorObject in doorObjects)
        {
            foreach (OpenDoor openDoor in _tempSave.openDoors)
            {
                if (openDoor.id == doorObject.id)
                {
                    doorObject.isOpen = openDoor.isOpen;
                    break;
                }
            }
        }
    }

    public void SaveDoorsStatus()
    {
        for (int i = 0; i < _tempSave.openDoors.Count; i++)
        {
            foreach (DoorObject doorObject in doorObjects)
            {
                if (_tempSave.openDoors[i].id == doorObject.id)
                {
                    _tempSave.openDoors[i] = new OpenDoor(doorObject.id, doorObject.isOpen);
                    break;
                }
            }
        }
    }

    public void DeafultDoorsStatus()
    {
        _tempSave.openDoors.Add(new OpenDoor(1, false));
        _tempSave.openDoors.Add(new OpenDoor(2, true));
        _tempSave.openDoors.Add(new OpenDoor(3, false));
        _tempSave.openDoors.Add(new OpenDoor(4, false));
        _tempSave.openDoors.Add(new OpenDoor(5, false));
        _tempSave.openDoors.Add(new OpenDoor(6, false));
        _tempSave.openDoors.Add(new OpenDoor(7, true));
        _tempSave.openDoors.Add(new OpenDoor(8, true));
        _tempSave.openDoors.Add(new OpenDoor(9, true));
    }

    public void DefaultInventoryCharacter()
    {
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
        _tempSave.inventoryCharacter.Add(new InventoryCharacter(-1, 0));
    }

    private static string GetGameObjectPath(GameObject gameObject)
    {
        string pathGameObject = gameObject.transform.name;
        while (gameObject.transform.parent != null)
        {
            gameObject = gameObject.transform.parent.gameObject;
            pathGameObject = gameObject.transform.name + "/" + pathGameObject;
        }
        Debug.Log(pathGameObject);
        return pathGameObject;
    }
}
