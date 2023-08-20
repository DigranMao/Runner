using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneratorPg : MonoBehaviour
{
    public GameObject[] tilePrefabs;  
    float spawnPos = 0; //спавн платформы по координате z
    float tileLength = 24; //длина платформы

    List<GameObject> activeTiles = new List<GameObject>();
    public Transform player;
    int startTiles = 6;

    void Start()
    {
        for(int i = 0; i < startTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
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
        Instantiate(tilePrefabs[tileIndex], player.transform.forward * spawnPos, tilePrefabs[tileIndex].transform.rotation);
        spawnPos += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

}
