using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPr;
    public Transform[] spawnPts;
    public float timeBetweenSpawn = 1.5f;
    float spawnTime;

    // Start is called before the first frame update
    void Start() {
        spawnTime = Time.time + timeBetweenSpawn;
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > spawnTime) {
            SpawnEnemy();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void SpawnEnemy() {
        int rand = Random.Range(0, spawnPts.Length);
        var enemy = ObjectPoolManager.Instance.GetObject(enemyPr);
        enemy.transform.position = spawnPts[rand].position;
        var enemScript = enemy.GetComponent<Enemy>();
        enemScript.Init();
    }
}
