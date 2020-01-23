using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NPC : Pathfinding.AIDestinationSetter, Corpse  {

    //RayCast
    //protected RaycastHit2D vision;
    protected int skipRaycastLayers;
    protected int skipOverlapCircleLayers;

    //Trigger && Chasing
    [SerializeField]
    protected float triggerLength = 2;
    [SerializeField]
    protected float chaseLength = 4;

    //Patrolling
    [SerializeField]
    protected List<Transform> patrolPoints;
    protected int currentPatrolpoint = 1;
    protected Transform startingPosition;
    [SerializeField]
    protected GameObject newPatrolPoint;
    protected bool isReachedEndOfPatrolPoints = false;

    //Animation
    protected Animator anim;
    private bool seeTarget = false;

    //Speed
    private float speed = 0f;
    private float previousFrameSpeed = 0f;
    private Vector3 previousFramePosition = Vector3.zero;
    private float cooldown = .1f;

    //FightingSystem

    //Hitbox
    protected BoxCollider2D boxCollider;
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    protected bool collidingWithCharacter;

    //Attack
    private bool attack = false;

    //Receive Damage
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;
    protected int lastDamageId = 0;

    [SerializeField]
    protected GameObject[] corpses = new GameObject[] { };

    //Immunity
    protected float immuneTime = 0.2f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;

    protected bool colliding;

    //Mover
    protected RaycastHit2D hit;

    //Experience
    [SerializeField]
    protected int xpValue = 1;
    //Spawn
    [SerializeField]
    protected bool spawned = false;

    protected virtual void Start()
    {
        skipRaycastLayers = ~((1 << 0) | (1 << 10) | (1 << 11) | (1 << 12));
        skipOverlapCircleLayers = ~((1 << 11) | (1 << 8) | (1 << 12) | (1 << 10)); 
        //ANim
        anim = transform.GetChild(0).GetComponent<Animator>();

        //BoxCollider
        boxCollider = GetComponent<BoxCollider2D>();
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        if (patrolPoints.Count != 0)
        {
            newPatrolPoint = Instantiate(newPatrolPoint, transform.position, Quaternion.identity);
            patrolPoints.Insert(0, newPatrolPoint.transform);
            //patrolPoints.Add(newPatrolPoint.transform);
        }

    }
    protected override void Update()
    {
        if (!GameManager.instance.getPause())
            ai.isStopped = false;
        else
            ai.isStopped = true;
            //Searching for targets
            collidingWithCharacter = false;
            target = FindTargets(triggerLength);
            //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0);

            if (target == null)
            {
                target = Patrolling();
            }
            else if (target != null && ai != null)
            {
                //Checks for the target is close enough to attack
                attack = Attack();
                if (!attack && !collidingWithCharacter && !GameManager.instance.getPause())
                {
                    ai.destination = target.position;
                }

            }
            //Debug.Log(GetSpeed());

            if (GetSpeed() > 0.1f && !attack)
            {

                Moving(true);
            }
            else if (!attack)
                Moving(false);
        
        
        

    }

    public void makeCorpse() {
        Instantiate(corpses[int.Parse(Mathf.Floor(UnityEngine.Random.Range(0, corpses.Length)).ToString())],
            transform.position, Quaternion.Inverse(gameObject.transform.rotation));
    }

    protected float GetSpeed()
    {
        previousFrameSpeed = speed;
        //RandomRotate(anim, chasing, patroling);
        float movementPerFrame = Vector3.Distance(previousFramePosition, transform.position);
        speed = movementPerFrame / Time.deltaTime;
        previousFramePosition = transform.position;

        return Mathf.Abs(previousFrameSpeed - speed);
    }

    protected Transform FindTargets(float length)
    {

        //float distanceBetweenSeeThroughObject = 0f;
        Transform newTarget = null;
        float nearestEnemy = length + 1f;
        //Searching for targets which are in the radious
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, length, skipOverlapCircleLayers);
        if (colliders != null)
        {
            //Searching the nearest target which in radious && not behind any obstacles
            RaycastHit2D vision;
            foreach (var c in colliders)
            {
                
                //distanceBetweenSeeThroughObject = 0f;
                vision = Physics2D.Raycast(transform.position, c.transform.position - transform.position, Mathf.Infinity, skipRaycastLayers);
                /*if (!(vision.transform.gameObject.layer == 8 && vision.transform.gameObject.tag != "Fighter"))
                {
                    
                    skipRaycastLayers = ~((1 << 0) | (1 << 10) | (1 << 11) | (1 << 12));
                    Debug.Log("Cant see throgh");
                }
                else
                {
                    skipRaycastLayers = ~((1 << 0) | (1 << 8) | (1 << 10) | (1 << 11) | (1 << 12));

                }

                vision = Physics2D.Raycast(transform.position, c.transform.position - transform.position, Mathf.Infinity, skipRaycastLayers);*/
                try
                {
                    if (vision.transform.gameObject.layer == 9
                   //For the nearestTarget
                   && Vector3.Distance(transform.position, c.transform.position) <= nearestEnemy)
                    {
                        nearestEnemy = Vector3.Distance(transform.position, c.transform.position);
                        newTarget = c.transform;
                    }
                }
               catch (Exception e)
                {
                    //Debug.LogException(e, this);
                }
                //Debug.Log(c);
            }
            return newTarget;
        }
        return null;

    }

    protected Transform Patrolling()
    {
        //If the patrolPoints array isnt empty
        if (patrolPoints.Count != 0)
        {
            //AI and the patrolPoint distance less than current length
            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolpoint].transform.position) < .2f)
            {
                if (currentPatrolpoint == patrolPoints.Count - 1)
                    isReachedEndOfPatrolPoints = true;
                else if (currentPatrolpoint == 0)
                    isReachedEndOfPatrolPoints = false;

                if (isReachedEndOfPatrolPoints == false)
                    currentPatrolpoint++;
                else
                    currentPatrolpoint--;
            }
            return patrolPoints[currentPatrolpoint].transform;
        }
        return null;
    }

    protected bool Attack()
    {
        if(Vector3.Distance(transform.position, target.position) <= GetComponent<Pathfinding.AIPath>().endReachedDistance)
        {
            anim.SetInteger("station", 2);
            return true;
        }
        return false;
    }

    protected void Moving(bool isMoving)
    {
        if (isMoving)
        {
            anim.SetInteger("station", 1);
        }
            
        else
        {
            anim.SetInteger("station", 0);
        }
            
    }

    protected virtual void ReceiveDamage(Damage dmg)
    {

        if (lastDamageId != dmg.id && /*Time.time - lastImmune > immuneTime &&*/ !GameManager.instance.getPause())
        {
            lastDamageId = dmg.id;
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            if (transform.name == "Player")
                GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 35, Color.red, transform.position, Vector3.zero, 0.5f);
            else
            {
                GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 35, Color.cyan, transform.position, Vector3.zero, 0.5f);
            }
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
            
        }


    }

    protected void setTarget(Transform newTarget)
    {
        target = newTarget;
    }

    protected void isColliding(bool colliding)
    {
        this.colliding = colliding;
    }

    protected virtual void Death()
    {
        makeCorpse();

        Destroy(gameObject);
        GameManager.instance.GrantScore(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
    public int getXpValue()
    {
        return xpValue;
    }
}
