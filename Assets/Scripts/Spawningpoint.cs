using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawningpoint : MonoBehaviour {
    //Logic
    private float cooldown = 1.0f;
    private float lastSpawn;
    
    //Spawn an enemy
    private int enemyCount = 2;
    private SpriteRenderer usedSpawningPoint;
    private Transform playerTransform;
    private GameObject enemyPrefab;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        
        //enemyPrefab = Instantiate(Resources.Load("Man_Zombie_0_0") as GameObject, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        SpawnEnemy(enemyCount, cooldown, 1.0f, "Man_Zombie_0_0", "Sprites/tileBreaking_1_1");
        /*if (Time.time - lastSpawn > cooldown && enemyCount > 0 &&
            Vector3.Distance(transform.position, playerTransform.position) < 1)
        {
            lastSpawn = Time.time;
            enemyPrefab = Instantiate(Resources.Load("Man_Zombie_0_0") as GameObject, transform.position, Quaternion.identity);
            usedSpawningPoint = Resources.Load<SpriteRenderer>("Sprites/tileBreaking_1_1");
            enemyCount--;
            GetComponent<SpriteRenderer>().sprite = usedSpawningPoint.sprite;
        }*/
    }
    protected virtual void SpawnEnemy(int count, float cooldown, float distance, string basicPrefabSource, string usedPrefabSource)
    {
        if (Time.time - lastSpawn > cooldown && count > 0 &&
            Vector3.Distance(transform.position, playerTransform.position) < distance)
        {
            lastSpawn = Time.time;
            enemyPrefab = Instantiate(Resources.Load(basicPrefabSource) as GameObject, transform.position, Quaternion.identity);
            usedSpawningPoint = Resources.Load<SpriteRenderer>(usedPrefabSource);
            //enemyCount--;
            setEnemyCount(getEnemyCount() - 1);
            GetComponent<SpriteRenderer>().sprite = usedSpawningPoint.sprite;
        }
    }
    protected void setEnemyCount(int enemyCount)
    {
        this.enemyCount = enemyCount;
    }
    protected int getEnemyCount()
    {
        return enemyCount;
    }
}
