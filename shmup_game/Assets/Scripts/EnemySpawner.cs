using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnOffsetX = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
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
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
