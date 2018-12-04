using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LootGenerator : Photon.MonoBehaviour
{

    public GameObject grid;
    public List<GameObject> spawnableObjects;

    public float minWait;
    public float maxWait;
    [PunRPC]
    public float currentSpawns = 0f;
    public float spawnDuration = 10f;
    public float maxSpawn = 25f;

    private bool isSpawning;

    void Awake()
    {
        isSpawning = false;
    }

    void Update()
    {
        if (!isSpawning && PhotonNetwork.connected)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnObject", timer);
            isSpawning = true;
        }
    }

    void SpawnObject()
    {
        if(currentSpawns < maxSpawn)
        {
            System.Random randomNumber = new System.Random();
            int randomIndex = randomNumber.Next(spawnableObjects.Count);

            GameObject spawnedObject = PhotonNetwork.Instantiate(
                spawnableObjects[randomIndex].name,
                GetRandomGridPosition(),
                Quaternion.identity,
                0
             );
            currentSpawns++;
            isSpawning = false;
         
            StartCoroutine(DeSpawn(spawnedObject));

        }
    }
    
    private IEnumerator DeSpawn(GameObject spawnedObject)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / spawnDuration;
            yield return null;
        }
        PhotonNetwork.Destroy(spawnedObject);
        currentSpawns--;
    }

    public Vector2 GetRandomGridPosition()
    {
        Tilemap tileMap = grid.GetComponentInChildren<Tilemap>();
        BoundsInt cellBounds = tileMap.cellBounds;
        Vector2 pos = new Vector2(
            Random.Range(cellBounds.xMin + 5, cellBounds.xMax - 1),
            Random.Range(cellBounds.yMin + 5, cellBounds.yMax - 1)
        );


        return pos;
    }
}