using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 2.5f;
    public float spawnOffsetX = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), 1f, spawnInterval);
    }

    void SpawnCoin()
    {
        if (!ScoreManagerScript.Instance.gameRunning) return;

        Camera cam = Camera.main;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float spawnX = cam.transform.position.x + camWidth + spawnOffsetX;
        float spawnY = Random.Range(
            cam.transform.position.y - camHeight,
            cam.transform.position.y + camHeight
        );

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}
