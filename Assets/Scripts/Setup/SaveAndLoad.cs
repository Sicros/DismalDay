using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// Valores que son almacenados en los archivos de guardado.
[Serializable]
public struct SaveData
{
    public float currentHealthCharacter;
    public int currentHandgunBullets;
    public List<OpenDoor> openDoors;
    public List<ObjectsPerScene> objectsToDestroy;
    public List<InventoryCharacter> inventoryCharacter;
}

// Estructura que guarda información de las puertas abiertas.
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

// Estructura que guarda información del inventario del personaje.
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

// Estructura que guarda objetos que deben ser destruidos al cargar la escena.
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
    // Referencia a los datos del personaje.
    [SerializeField] private CharacterData characterData;

    // Referencia a los datos del arma.
    [SerializeField] private WeaponObject weaponData;

    // Lista de las puertas y su estado.
    [SerializeField] private DoorObject[] doorObjects;

    // Variable que almacena información de la partida actual.
    private SaveData _tempSave;

    // Instanciación del componente actual.
    public static SaveAndLoad instance;

    // Evita que el objeto sea destruido al cargar una nueva escena.
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

    // Método que permite cargar los datos guardados desde un archivo tipo JSON. Estos valores
    // son asignados a una variable temporal y aplicados a los datos de cada objeto que corresponda.
    public void LoadDataJSON(string saveDataName)
    {
        #if UNITY_EDITOR
            if (System.IO.File.Exists(Application.dataPath + saveDataName))
            {
                var _saveDataJson = File.ReadAllText(Application.dataPath + saveDataName);
                _tempSave = JsonUtility.FromJson<SaveData>(_saveDataJson);
            }
        #else
            if (System.IO.File.Exists(Application.persistentDataPath + saveDataName))
            {
                var _saveDataJson = File.ReadAllText(Application.persistentDataPath + saveDataName);
                _tempSave = JsonUtility.FromJson<SaveData>(_saveDataJson);
            }
        #endif
        characterData.currentHealth = _tempSave.currentHealthCharacter;
        characterData.inventoryCharacter = _tempSave.inventoryCharacter;
        weaponData.currentBullets = _tempSave.currentHandgunBullets;
        ReplaceDoorsStatus();
        DestroyObjectsFromList();
        Debug.Log("Data loaded");
    }

    // Método que permite guardar el estado actual del juego en una archivo tipo JSON.
    // Se reescriben los últimos valores obtenidos que sean de interés en la variable temporal,
    // la que es guardada en el archivo.
    public void SaveDataJSON(string saveDataName)
    {
        _tempSave.currentHealthCharacter = characterData.currentHealth;
        _tempSave.inventoryCharacter = characterData.inventoryCharacter;
        _tempSave.currentHandgunBullets = weaponData.currentBullets;
        SaveDoorsStatus();
        var stringjson = JsonUtility.ToJson(_tempSave);
        #if UNITY_EDITOR
            File.WriteAllText(Application.dataPath + saveDataName, stringjson);
        #else
            File.WriteAllText(Application.persistentDataPath + saveDataName, stringjson);
        #endif
        print("Saving");
    }

    // Crea un variable temporal con valores iniciales.
    public void NewGame()
    {
        _tempSave.currentHealthCharacter = 10;
        _tempSave.currentHandgunBullets = 15;
        _tempSave.openDoors = new List<OpenDoor>();
        _tempSave.objectsToDestroy = new List<ObjectsPerScene>();
        _tempSave.inventoryCharacter = new List<InventoryCharacter> ();
        _tempSave.objectsToDestroy = new List<ObjectsPerScene> ();
        DeafultDoorsStatus();
        DefaultInventoryCharacter();
    }

    // Método que permite agregar a la lista los objetos que deben ser destruidos por escena, si es que
    // estos ya fueron eliminados (en caso de enemigos) o recogidos (en caso de objetos).
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

    // Método que permite destruir todos los objetos que se encuentren en la lista, relacionados
    // a la escena que haya sido cargada.
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

    // Método que permite actualizar el estado de las puertas, marcando como abiertas aquellas que
    // ya hayan sido abiertas o no.
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

    // Añade el estado de la puerta a una lista, que es cargada en cada escena.
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

    // Define el estado inicial de todas las puertas.
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

    // Define el estado inicial del inventario del personaje.
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

    // Obtiene la ruta dentro del directorio de un objeto.
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

    // Reemplazar valores guardados por default
    public void ApplyValues()
    {
        characterData.currentHealth = _tempSave.currentHealthCharacter;
        characterData.inventoryCharacter = _tempSave.inventoryCharacter;
        weaponData.currentBullets = _tempSave.currentHandgunBullets;
        ReplaceDoorsStatus();
    }
}
