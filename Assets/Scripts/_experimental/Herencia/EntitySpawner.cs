using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private List<Entity> m_spawnables;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InstantiateEntity();
        }
    }

    private void InstantiateEntity()
    {
        Pokemon masdaf = new Pokemon ();
        Entity l_entityToInstantiate = m_spawnables[Random.Range(0, m_spawnables.Count)];
        Entity l_spawnedEntity = Instantiate(l_entityToInstantiate);
        Debug.Log(l_entityToInstantiate.GetName());

        // De esta forma puedo trabajar con el objeto asumiento que es un Pokemon
        // Y en caso de que no lo sea simplemente se omite por la condición que agregamos.
        var l_pokemon = l_spawnedEntity as Pokemon;
        if (l_pokemon != default)
        {
            l_pokemon.GetWeakness();
        }

        // Otra forma de hacer lo mismo que antes, pero de otra forma.
        /*
        if (l_spawnedEntity is Pokemon l_pokemon)
        {
            l_pokemon.GetWeakness();
        }
        */
    }
}
