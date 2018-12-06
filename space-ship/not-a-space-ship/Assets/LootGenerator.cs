using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LootGenerator : Photon.MonoBehaviour
{

    public GameObject grid;
    public List<GameObject> spawnableObjects;
    public List<GameObject> spawnablePowerups;

    public float minWait;
    public float maxWait;
    public float currentSpawns = 0f;
    public float spawnDuration = 10f;
    public float maxSpawn = 25f;

    private object[] nullObj;

    private bool isSpawning;

    System.Random random;
    void Awake()
    {
        isSpawning = false;
        random = new System.Random();
    }

    void Update()
    {
        if (!isSpawning && (currentSpawns < maxSpawn) && PhotonNetwork.connected && PhotonNetwork.isMasterClient)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnObject", timer);
            isSpawning = true;
        }
    }

    void SpawnObject()
    {
        int randomIndex = random.Next(spawnableObjects.Count);
        bool shouldSpawnPowerup = random.Next(11) > 8;
        string gameObjectToSpawn =
        shouldSpawnPowerup
            ? spawnablePowerups[randomIndex].name
            : spawnableObjects[randomIndex].name;

        GameObject spawnedObject = PhotonNetwork.InstantiateSceneObject(
           gameObjectToSpawn,
           GetRandomGridPosition(),
           Quaternion.identity,
           0,
           nullObj
        );

        currentSpawns++;
        isSpawning = false;

        StartCoroutine(DeSpawn(spawnedObject));

    }

    private IEnumerator DeSpawn(GameObject spawnedObject)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / spawnDuration;
            yield return null;
        }
        if (spawnedObject != null)
        {
            PhotonNetwork.Destroy(spawnedObject);
            currentSpawns--;
        }

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