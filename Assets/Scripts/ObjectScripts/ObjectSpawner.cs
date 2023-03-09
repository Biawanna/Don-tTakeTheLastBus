using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectPrefabs = new List<GameObject>();
    [SerializeField] private float spawnDelay;
    [SerializeField] private float maxPosition;
    [SerializeField] private float minPosition;

    [SerializeField] private float minRandomY;
    [SerializeField] private float maxRandomY;

    [SerializeField] private float minRandomZ;
    [SerializeField] private float maxRandomZ;

    void Start()
    {
        StartCoroutine(SpawnCrosses());
    }

    IEnumerator SpawnCrosses()
    {
        while (true)
        {
            //float randomTime = Random.Range(2f, 5f);
            float randomPosition = Random.Range(minPosition, maxPosition);
            float randomY = Random.Range(minRandomY, maxRandomY);
            float randomZ = Random.Range(minRandomZ, maxRandomZ);

            yield return new WaitForSeconds(spawnDelay);
            Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Count)], new Vector3(randomPosition, randomY, randomZ), Quaternion.Euler(0, 0, 180));
        }
    }
}