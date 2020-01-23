using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable, RandomID {

    //Damage
    public int damage;
    public float pushForce = 5.0f;

    private Transform playerTransform;
    private Vector3 startingPosition;

    //Animation
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    //Attack
    private float lastAttack = 0f;
    private float attackTime = 0.2f;

    //Crawling
    [SerializeField]
    protected bool crawling = false;

    protected override void Start()
    {
        base.Start();
        //anim = transform.parent.gameObject.GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public int GenerateRandomID(int maxNumber)
    {
        return int.Parse(Mathf.Round(Random.Range(0, maxNumber)).ToString());
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "PlayerHitBox" && coll.tag == "Fighter" && !GameManager.instance.getPause() && Time.time - lastAttack > attackTime) //Change coll.name == "Player"
        {
            lastAttack = Time.time;
            //Attack Player
            //Create a new damage object, before sending it to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce,
                id = GenerateRandomID(5000)
            };
            
            coll.SendMessageUpwards("ReceiveDamage", dmg);
        }
       /* if (coll.name == "SpawningWaypoint")
        {
            crawling = false;
            coll.SendMessageUpwards("setCrawling", false);
            anim.SetInteger("station", 0);
        }*/
    }
    /*
    protected void collideWithSpawningWaypoint(Collider2D coll)
    {
        if (coll.name == "SpawningWaypoint")
        {
            crawling = false;
            coll.SendMessageUpwards("setCrawling", false);
            //anim.SetInteger("station", 0);
        }
    }*/



}
