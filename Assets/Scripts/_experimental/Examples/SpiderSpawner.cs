using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpiderType
{
    Small,
    Medium,
    Chonk
}

public class SpiderSpawner : MonoBehaviour
{
    [SerializeField] private SkulltullaController[] m_spiderPrefabs;
    [SerializeField] private Transform[] m_spiderWaypoints;
    [SerializeField] private int m_amountToInstantiate = 3;

    [SerializeField] private SkulltullaController m_smallSpider, m_mediumSpider, m_chonkSpider;
    private Queue<SkulltullaController> m_spawnedSkultullas;
    private Stack<SkulltullaController> m_spawnedSkultullasStack;

    private List<string> nombres;
    [SerializeField] private SpiderType m_spiderToSpawn;

    private Dictionary<SpiderType, SkulltullaController> m_spiderDictionary =
        new Dictionary<SpiderType, SkulltullaController>();

    private void PopulateDictionary()
    {
        m_spiderDictionary.Add(SpiderType.Small, m_smallSpider);
        m_spiderDictionary.Add(SpiderType.Medium, m_mediumSpider);
        m_spiderDictionary.Add(SpiderType.Chonk, m_chonkSpider);
    }

    private void Awake()
    {
        foreach (var l_nombre in nombres)
        {
            Debug.Log(l_nombre);
        }
        /*
        for (int i = 0; i < m_spiderPrefabs.Length; i++)
        {
            // SpawnSpider(i);
            var l_chosenSpider = m_spiderPrefabs[i];
            SpawnSpider(l_chosenSpider);
        }
        foreach (var l_skultullaController in m_spiderPrefabs)
        {
            SpawnSpider(l_skultullaController);
        }*/

        if (m_spiderDictionary.TryGetValue(m_spiderToSpawn, out var l_spiderToSpawn))
        {
            SpawnSpider(l_spiderToSpawn);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            for (int i = 0; i < m_spawnedSkultullas.Count; i++)
            {
                var l_curr = m_spawnedSkultullas.Dequeue();
                var l_stackCurr = m_spawnedSkultullasStack.Pop();
                Debug.Log($"My name is {l_curr.gameObject.name}");
                Debug.Log($"My name is {l_stackCurr.gameObject.name}");
            }
        }
    }

    private void SpawnSpider(SkulltullaController p_spiderToSpawn)
    {
        var l_spawnPosition = GetRandomWaypoint().position;
        // var l_chosenSpider = ChooseSpider();
        var l_currSpider = Instantiate(p_spiderToSpawn, l_spawnPosition, Quaternion.identity);
        l_currSpider.ReceiveWaypoints(m_spiderWaypoints);
        l_currSpider.Init();
        m_spawnedSkultullas.Enqueue(l_currSpider);
        m_spawnedSkultullasStack.Push(l_currSpider);
    }

    private void SpawnSpider(int p_index)
    {
        var l_spawnPosition = GetRandomWaypoint().position;
        // var l_chosenSpider = ChooseSpider();
        var l_chosenSpider = m_spiderPrefabs[p_index];
        var l_currSpider = Instantiate(l_chosenSpider, l_spawnPosition, Quaternion.identity);
        l_currSpider.ReceiveWaypoints(m_spiderWaypoints);
        l_currSpider.Init();
    }

    private SkulltullaController ChooseSpider()
    {
        var l_chosenSpider = Random.Range(0, m_spiderPrefabs.Length);
        return m_spiderPrefabs[l_chosenSpider];
    }

    private Transform GetRandomWaypoint()
    {
        var l_chosenWaypoint = Random.Range(0, m_spiderWaypoints.Length);
        return m_spiderWaypoints[l_chosenWaypoint];
    }
}