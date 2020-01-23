using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Fighter {
    //Logic
    private float cooldown = 2.0f;
    private float lastSpawn;
    private List<float> lastSpawns = new List<float>();
    private int skipOverlapCircleLayersForSearchingPlayer = ~((1 << 0) | (1 << 11) | (1 << 8) | (1 << 12) | (1 << 10) | (1 << 13));
    private int skipRaycastLayersForSearchingPlayer = ~((1 << 0) | (1 << 10) | (1 << 11) | (1 << 12) | (1 << 13));
    private Transform target = null;
    //Death
    private bool alive = true;
    [SerializeField]
    private Sprite deathSprite;
    //Spawn an enemy
    [SerializeField]
    private int enemyCount = 2;
    [SerializeField]
    protected ZombieSpawning[] enemyPrefabs = new ZombieSpawning[] { };
    
    private Transform playerTransform;
    [SerializeField]
    private Transform[] wakeupPoint;

    
    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        for (int i = 0; i < wakeupPoint.Length; i++)
        {
            wakeupPoint[i] = transform.GetChild(i).GetComponent<BoxCollider2D>().transform;
            lastSpawns.Add(cooldown);
        }
    }

    protected void Update()
    {
        //Debug.Log(wakeupPoint.name);
        if (alive == true)
        {
            target = FindTargets(1.0f);
            //Debug.Log(target);
            if (target != null)
                SpawnEnemyToFreeAwakeingPoint(cooldown, 1.0f);
        }
        
    }

    protected Transform FindTargets(float length)
    {
        Transform newTarget = null;
        float nearestEnemy = length + 1f;
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, length, skipOverlapCircleLayersForSearchingPlayer);
        if(colliders != null)
        {
            RaycastHit2D vision;
            foreach(var c in colliders)
            {
                vision = Physics2D.Raycast(transform.position, c.transform.position - transform.position, Mathf.Infinity, skipRaycastLayersForSearchingPlayer);
                if (vision.transform.gameObject.layer == 9 &&
                    Vector3.Distance(transform.position, c.transform.position) < nearestEnemy)
                {
                    nearestEnemy = Vector3.Distance(transform.position, c.transform.position);
                    newTarget = c.transform;
                }
            }
            return newTarget;
        }
        return null;
    }

    protected void SpawnEnemy(float cooldown, float distance)
    {
        ZombieSpawning newEnemyPrefab;
        if (Time.time - lastSpawn > cooldown && enemyCount > 0 &&
            Vector3.Distance(transform.position, playerTransform.position) < distance)
        {
            lastSpawn = Time.time;
            newEnemyPrefab = Instantiate(enemyPrefabs[0], transform.position, Quaternion.identity);
            newEnemyPrefab.setTarget(wakeupPoint[0]);
            enemyCount--;
        }
    }

    protected void SpawnEnemyToFreeAwakeingPoint(float cooldown, float distance)
    {
        ZombieSpawning newEnemyPrefab;
        int skipRaycastLayers = ~((1 << 0) | (1 << 8) | (1 << 10) | (1 << 9) | (1 << 13));
        RaycastHit2D vision;

        if (enemyCount > 0 && !GameManager.instance.getPause())
        {
            for (int i = 0; i < wakeupPoint.Length; i++)
            {

                vision = Physics2D.Raycast(transform.position, wakeupPoint[i].position - transform.position, Mathf.Infinity, skipRaycastLayers);
                if (Time.time - lastSpawns[i] >= cooldown &&
                Vector3.Distance(transform.position, target.position) < distance &&
                vision.transform.gameObject.layer != 11)
                {
                    //lastSpawn = Time.time;
                    lastSpawns[i] = Time.time;
                    newEnemyPrefab = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform.position, Quaternion.identity);
                   
                    newEnemyPrefab.setTarget(wakeupPoint[i]);
                    //ZombieSpawning
                    newEnemyPrefab.setCreator(GetComponent<Spawner>());
                    //Debug.Log(transform.GetComponent<Spawner>().name);
                    enemyCount--;
                }


                //Debug.Log(cooldowns[i]);
            }
        }

        
    }

    public Transform getTarget()
    {
        return target;
    }

    protected override void Death()
    {
        alive = false;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }
}
