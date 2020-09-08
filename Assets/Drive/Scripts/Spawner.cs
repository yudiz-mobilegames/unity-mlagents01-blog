using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

  public List<GameObject> spawnableObject;
    
   public float minSpawnedIntervalTime;
     public float maxSpawnedIntervalTime;

    private List<GameObject> SpawnedObjects = new List<GameObject>();
    private CarAgent carAgent;

    void Awake()
    {
        carAgent = GetComponentInChildren<CarAgent>();

        carAgent.onReset += DestroyAllSpawnedObjects;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var spawned = Instantiate(GetRandomSpawnableFromList(), transform);
                                                                
        SpawnedObjects.Add(spawned);

        yield return new WaitForSeconds(Random.Range(minSpawnedIntervalTime, maxSpawnedIntervalTime));
        StartCoroutine(nameof(Spawn));
    }

    private void DestroyAllSpawnedObjects()
    {
        for (int i = SpawnedObjects.Count - 1; i >= 0; i--)
        {
            Destroy(SpawnedObjects[i]);
            SpawnedObjects.RemoveAt(i);
        }
    }

    private GameObject GetRandomSpawnableFromList()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnableObject.Count);
        return spawnableObject[randomIndex];
    }
}
