using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] tilePrefabs; 
    List<GameObject> activeTiles = new List<GameObject>();
    float spawnPos = 0; 
    float tileLength = 24; 

    public Transform player;
    int startTiles = 10;

    void Start()
    {
        for(int i = 0; i < startTiles; i++)
        {
            SpawnTile(5);
        }
    }

    void Update()
    {
        if(player.position.z - 40 > spawnPos - (startTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefabs[tileIndex], player.transform.forward * spawnPos, tilePrefabs[tileIndex].transform.rotation);
        activeTiles.Add(nextTile);
        spawnPos += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}



