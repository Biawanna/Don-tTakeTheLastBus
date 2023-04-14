using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;

    [Header("Tile Settings")]
    [SerializeField] private float spawnZ = 0.0f;
    [SerializeField] private float spawnX = 5.0f;
    [SerializeField] private float tileLength = 10.0f;
    [SerializeField] private float safeZone = 15.0f;
    [SerializeField] private int tilesOnScreen = 7;
    
    private Transform playerTransform;
    private int lastPrefabIndex = 0;
    private List<GameObject> activeTiles;

    private void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Bus").transform;

        /// Spawns tiles in front of the bus.
        for (int i = 0; i < tilesOnScreen; i++)
        {
            if (i < 2)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile();
            }
        }
    }

    private void Update()
    {
        /// Spawns and deletes tiles as the bus is moving.
        if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    /// <summary>
    /// Instantiates a tile from the tile array.
    /// </summary>
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        }

        go.transform.SetParent(transform);
        go.transform.position = new Vector3(spawnX, go.transform.position.y,spawnZ += tileLength);
        activeTiles.Add(go);
    }

    /// <summary>
    /// Destroys a tile at the last index.
    /// </summary>
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    /// <summary>
    /// Returns a random index from the tile array.
    /// </summary>
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
        {

            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}