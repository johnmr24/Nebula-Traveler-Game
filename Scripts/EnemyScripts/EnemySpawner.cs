using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner E;

    public GameObject[] enemyPrefab;
    public float spawnRate = 10f;
    public float spawnStartDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        E = this;
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(spawnStartDelay);
        SpawnEnemy();
    }
    void SpawnEnemy()
    {
        GameObject go = GameObject.Find("robotSphere");

        GameObject enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)]);
        Vector3 pos = go.transform.position;
        pos.y += 60f;
        pos.x += 30f;
        enemy.transform.position = pos;

        Invoke("SpawnEnemy", spawnRate);
    }
}
