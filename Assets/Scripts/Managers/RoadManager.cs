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
    private Queue<GameObject> tilePool;

    private void Start()
    {
        InitObjects();
        SpawnTiles();
    }

    private void InitObjects()
    {
        activeTiles = new List<GameObject>();
        tilePool = new Queue<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Bus").transform;

        // Instantiate and populate the tile pool
        for (int i = 0; i < tilePrefabs.Length; i++)
        {
            GameObject tile = Instantiate(tilePrefabs[i]);
            tile.SetActive(false);
            tilePool.Enqueue(tile);
        }
    }

    private void SpawnTiles()
    {
        // Spawns tiles in front of the bus.
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
        // Spawns and deletes tiles as the bus is moving.
        if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject tile;
        if (tilePool.Count > 0)
        {
            tile = tilePool.Dequeue();
        }
        else
        {
            // If the tile pool is empty, instantiate a new tile
            tile = Instantiate(tilePrefabs[0]);
        }

        tile.transform.SetParent(transform);
        tile.transform.position = new Vector3(spawnX, tile.transform.position.y, spawnZ += tileLength);
        tile.SetActive(true);
        activeTiles.Add(tile);
    }

    private void DeleteTile()
    {
        if (activeTiles.Count > 0)
        {
            GameObject tile = activeTiles[0];
            tile.SetActive(false);
            activeTiles.RemoveAt(0);
            tilePool.Enqueue(tile);
        }
    }

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
