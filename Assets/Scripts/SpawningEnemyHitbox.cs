using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemyHitbox : Collidable {

    [SerializeField]
    private bool reachedAwaekingPoint = false;
    [SerializeField]
    private Zombie zombiePrefab;



    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        // If it arrives to the awekingPoint 
        if (reachedAwaekingPoint)
        {
            Zombie newEnemyPrefab;

            newEnemyPrefab = Instantiate(zombiePrefab, transform.position, transform.rotation);
            newEnemyPrefab.setHitpoint(transform.parent.GetComponent<ZombieSpawning>().getHitpoint());
            newEnemyPrefab.setTarget(transform.parent.GetComponent<ZombieSpawning>().getCreator().getTarget());
            //Debug.Log(transform.parent.gameObject.GetComponent<ZombieSpawning>().getCreator().getTarget());
            Destroy(transform.parent.gameObject);
        }
            
    }



    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "AwakingPoint")
        {
            GetComponent<Animator>().SetInteger("station", 1);
        }
    }

    public Zombie getZombiePrefab()
    {
        return zombiePrefab;
    }
}