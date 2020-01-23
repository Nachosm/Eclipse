using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    //Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private bool targetSeen;
    private Transform playerTransform;
    private Vector3 startingPosition;
    //private Vector3 target;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    //Animation
    private Animator anim;
    private Vector3 previousFramePosition = Vector3.zero;
    private float speed = 0f;
    private float previousFrameSpeed = 0f;
    //Rotation
    protected Vector2 diff;
    protected float angle;

    //Collision Avoidance
    protected int nearestWP;
    protected int secondNearestWP;
    protected Waypoint lastWayPointUsed;
    protected Waypoint waypointToGo;
    protected bool used = false;
    protected RaycastHit2D vision;
    protected int skipLayers;
    protected bool nothingBefore;

    //Patrol
    public bool patroling;
    public List<PatrolPoint> patrolpoints;
    private PatrolPoint lastPatrolpoint = new PatrolPoint { };
    private int nextPatrolpoint;
    private int step;
    private bool addPlusOne;
    //public PatrolPoint prefabPatrolPoint;
    private PatrolPoint newPatrolpoint;
    //Lista a Patrolpointok-ról


    
    //private Waypoint waypointToGo2;
    /*private Waypoint closestWayPoint = new Waypoint
    {
        name = "",
    };*/
    //private Vector3 tmp = new Vector3(1000, 1000, 1000);
    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player").transform;
        //playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        //hitbox = transform.GetComponent<BoxCollider2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        //Set enemy's speed
        xSpeed = 0.375f;
        ySpeed = 0.28125f;

        //Avoidance
        nearestWP = 0;
        lastWayPointUsed = new Waypoint { };
        waypointToGo = new Waypoint { };
        skipLayers = ~((1 << 0) | (1 << 12));
        nothingBefore = false;

        //Patrolpoints get in to the list
        /*for (int i = 1; i < transform.childCount; i++)
        {
            patrolpoints.Add(transform.GetChild(i).GetComponent<PatrolPoint>());
        }*/

        /*if (patrolpoints.Count != 0)
        {
            patroling = true;
            nextPatrolpoint = 0;
        }*/
        //patroling = true;
        /*if (patrolpoints.Count % 2 == 0)
            nextPatrolpoint = 1;
        else
            nextPatrolpoint = 0;*/

        if (patroling == true)
        {
            nextPatrolpoint = 0;
            addPlusOne = true;

            /*newPatrolpoint = prefabPatrolPoint;
            newPatrolpoint.setCoodrdinates(startingPosition);
            newPatrolpoint.setName(transform.name);
            Debug.Log(newPatrolpoint.getName());*/
            //Különböző aadatok legyenek a patrol pointoknak

            //newPatrolpoint.setName(prefabPatrolPoint.getName() + transform.name);

            //Debug.Log(newPatrolpoint.getName());

            //Instantiate(newPatrolpoint, new Vector3(0,0,0), Quaternion.identity);
            //Instantiate(newPatrolpoint, new Vector3(startingPosition.x,startingPosition.y-0.2f, 0f), Quaternion.identity);
            //patrolpoints.Add(newPatrolpoint);
            //patrolpoints.Insert(0, newPatrolpoint);

            //patrolpoints[0].setCoodrdinates(transform.);

        }


    }

    private void FixedUpdate()
    {

        //if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        //2. Állapot: A játékos az ellenség hatókörében helyezkedik el
        if (Vector3.Distance(playerTransform.position, transform.position) < chaseLenght)
        //if (IsInVision(vision))
        {
            
            // Raycast enemy
            vision = Physics2D.Raycast(transform.position, playerTransform.position - transform.position, Mathf.Infinity, skipLayers);
            RandomRotate(anim, chasing, patroling);
            //RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, playerTransform.position - transform.position, Mathf.Infinity, layerMask).OrderBy(h => h.distance).ToArray();
            //Raycast
            //IsInVision(vision);
            //RaycastHit2D s = Physics2D.Ray
            if (!vision.collider.name.Equals("PlayerHitBox"))
                Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.green);
            else if (vision.collider.name.Equals("PlayerHitBox") && chasing == false)
                Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.yellow);
            else
                Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.red);
                
            
            if (IsInVision(vision, triggerLenght))
            {
                chasing = true;
                //Debug.Log(vision.collider.name);
            }


            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    //Man_Zombie_0_Run start working
                    if (anim.GetInteger("station") != 2 || collidingWithPlayer == false)
                        anim.SetInteger("station", 1);
                    
                    
                    //Search for waypoints
                    //Player isn't in enemy's vision, then go to the nearest waypoint
                    if (!IsInVision(vision, chaseLenght))
                    {
                        waypointToGo = NearestWayPoint(GameManager.instance.waypointNames);
                        //waypointToGo = TheNearest(GameManager.instance.waypointNames);
                        used = true;
                        //if (lastWayPointUsed.getName() != GameManager.instance.waypointNames[wpId].getName())
                        //kisebb távolság
                        if (lastWayPointUsed.getName() != waypointToGo.getName() //&&
                            //Vector3.Distance(transform.position, waypointToGo.getCoordinates()) < 5
                            )
                        {
                            //Changed from closestWayPoint
                            UpdateMotor((waypointToGo.getCoordinates() - transform.position).normalized);
                            RotateToTarget(anim, waypointToGo.getCoordinates(), 270);
                            Debug.Log(waypointToGo.getCoordinates());
                        }
                        else
                        {
                            UpdateMotor((playerTransform.position - transform.position).normalized);
                            RotateToTarget(anim, playerTransform.position, 270);
                            Debug.Log("ToPlayer");
                            //Debug.Log(closestWayPoint);
                        }
                    }
                    //Player is in enemy's vision
                    else
                    {
                        UpdateMotor((playerTransform.position - transform.position).normalized);
                        RotateToTarget(anim, playerTransform.position, 270);
                    }
                }
            }
            //chasing == false
            else
            {
                //Reset waypoints
                //Debug.Log("NotChasing");
                waypointToGo = lastWayPointUsed;
                lastWayPointUsed = new Waypoint { };//?
                                                    //Go back where we were
                if (startingPosition != transform.position && patroling == false)
                {
                    UpdateMotor((startingPosition - transform.position).normalized);
                    //Man_Zombie_0_Run start working
                    if (anim.GetInteger("station") != 2 || collidingWithPlayer == false)
                        anim.SetInteger("station", 1);

                    //Rotate to the startig position
                    RotateToTarget(anim, startingPosition, 270);
                }
                if (patroling == true)
                {
                    if (anim.GetInteger("station") != 2 || collidingWithPlayer == false)
                        anim.SetInteger("station", 1);

                    Patroling(patrolpoints);
                }
                else
                {
                    //Man_Zombie_0_Run stop working
                    UpdateMotor((startingPosition - transform.position).normalized);

                    if (GetSpeed() < 0.0001f)
                    {
                        anim.SetInteger("station", 0);
                        startingPosition = transform.position;
                    }

                    //anim.SetInteger("station", 0);
                }
            }
            //transform.LookAt(transform.position + playerTransform.transform.rotation * Vector2.up);
        }
        //out of chaseLnegth
        //1. Állapot: A játékos az ellenfél hatókörén kívül helyezkedik el
        else
        {
            
            lastWayPointUsed = new Waypoint { };
            if (patroling == false)
            {
                if (GetSpeed() != 0.0f)
                {
                    UpdateMotor(startingPosition - transform.position);
                    RotateToTarget(anim, startingPosition, 270);
                }
                RandomRotate(anim, chasing, patroling);
                //Mérjük a sebességet, hogy időben  le tudjon állni
                //Mivel nagyon lassan állna le, ezért módosítjuk a startingPosition-t a jelenlegi helyzetünkre 0.00001f sebességkülönbségnél
                if (GetSpeed() < 0.01f)
                {
                    anim.SetInteger("station", 0);
                    startingPosition = transform.position;
                }
            }
            else
                Patroling(patrolpoints);

            chasing = false;
            //targetSeen = false;
        }
        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                //anim.SetTrigger("attack");
                continue;
            }
            if (hits[i].tag == "Fighter" && hits[i].name == "PlayerHitBox") //Change "hits[i].name == "Player"
            {
                collidingWithPlayer = true;
                //Debug.Log(hits[i].name);
                //anim.SetTrigger("attack");
                anim.SetInteger("station", 2);
            }

            if (hits[i].tag == "Waypoint")
            {
                lastWayPointUsed = GameManager.instance.waypointNames[nearestWP];
            }
            // The array is not cleaned up, so we do it ourself
            if (hits[i].tag == "Patrolpoint" && patroling
                && lastPatrolpoint.getName() != hits[i].name/*&& patrolpoints[nextPatrolpoint+1].getName() == hits[i].name*/)
            {
                PatrolPoint patrolPoint;
                bool inList = false;
                //if (lastPatrolpoint.getName() != hits[i].name)
                //Debug.Log(hits[i].name);
                for (int j = 0; j < patrolpoints.Count; j++)
                {
                    if (patrolpoints[j].getName() == hits[i].name)
                    {
                        inList = true;
                    }

                }
                lastPatrolpoint.setName(hits[i].name);
                //Debug.Log(lastPatrolpoint.getName());

                if (nextPatrolpoint == patrolpoints.Count - 1)
                    addPlusOne = false;
                else if (nextPatrolpoint == 0)
                    addPlusOne = true;

                if (addPlusOne == false)
                    nextPatrolpoint--;
                else if (addPlusOne == true)
                    nextPatrolpoint++;
                //Visszafelé is kell
            }
            hits[i] = null;
        }
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

   /* protected bool PlayerBehindCollision(RaycastHit2D vision)
    {
        if ((vision.transform.gameObject.layer == 9 || vision.transform.gameObject.layer == 10)
            && (Vector3.Distance(playerTransform.position, transform.position) < chaseLenght))
        {
            return true;
        }
        if ()
        else
            return false;
    }*/

    protected bool IsInVision(RaycastHit2D vision, float length)
    {
        /*if (vision.transform.gameObject.layer == 8)
        {
            nothingBefore = false;
            return false;
        }*/
        if (vision.transform.gameObject.layer == 11)
        {
            GameObject hit = vision.collider.transform.parent.gameObject;
            Enemy anotherEnemy;
            anotherEnemy = hit.GetComponent<Enemy>();
            //nothingBefore = true;
            //triggerLength -> length
            if (anotherEnemy.isChasing() == true //Megnézi, hogy az útban lévő ellenfél követi-e a játékost
                && (Vector3.Distance(playerTransform.position, transform.position) < length)) // Csak akkor kezdi el, ha triggerLengthen belül van
                return true;
        }
        //triggerLength -> length
        if ((vision.transform.gameObject.layer == 9 || vision.transform.gameObject.layer == 10)
            && (Vector3.Distance(playerTransform.position, transform.position) < length))
        {
            //Debug.Log("See Player");
            //nothingBefore = true;
            return true;
        }
        /*if (nothingBefore == true 
            && (vision.transform.gameObject.layer == 9 || vision.transform.gameObject.layer == 10))
            return true;*/
        return false;
    }

    protected void Patroling(List<PatrolPoint> patrols)
    {
        RotateToTarget(anim, patrols[nextPatrolpoint].getCoordinates(), 270);
        //UpdateMotor(startingPosition - patrols[0].getCoordinates().normalized);
        //UpdateMotor((patrols[nextPatrolpoint].getCoordinates()));
        UpdateMotor((patrols[nextPatrolpoint].getCoordinates() - transform.position).normalized);
        //Debug.Log(patrols[nextPatrolpoint].getCoordinates());
        //UpdateMotor((playerTransform.position - transform.position).normalized);
    }

    protected void RandomRotate(Animator character, bool chasing, bool patroling)
    {
        if (chasing == false && patroling == false)
        {
            float r = Mathf.Round(Random.Range(0, 700));
            if (r == 28)
            {
                angle = Mathf.Round(Random.Range(20, 340));
                Quaternion rotation = new Quaternion();
                rotation.eulerAngles = new Vector3(0, 0, angle);
                character.transform.rotation = rotation;
            }
        }

    }

    protected void RotateToTarget(Animator character, Vector3 target, int rotationDegree)
    {
        diff = (target - transform.position).normalized;
        angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - rotationDegree); //270
        character.transform.rotation = rotation;
    }

    protected Waypoint NearestWayPoint(List<Waypoint> waypoint) //Changed from Vector3
    {
        nearestWP = 0;
        //bool findWaypoint = false;
        //string tmp = lastWayPointUsed.getName();
        for (int i = 0; i < waypoint.Count; i++)
        {
            //Megnézi melyik van hozzá a legközelebb
            if ((Vector3.Distance(transform.position, waypoint[i].transform.position) //A jelenlegi tartózkodási hely az adott waypoint közti tartózkodási hely
                <= Vector3.Distance(transform.position, waypoint[nearestWP].getCoordinates())) //kisebb mint a jelenlegi t hely és a legközelebbi wp koordinátája távolsága
                && lastWayPointUsed.getName() != waypoint[nearestWP].getName() //Az előző waypointot nem vesszük figyelembe
                )
            {
                nearestWP = i;
            }
        }
        return waypoint[nearestWP];
    }

    protected Waypoint TheNearest(List<Waypoint> waypoints)
    {
        nearestWP = 0;
        secondNearestWP = 0;
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) <=
                Vector3.Distance(transform.position, waypoints[nearestWP].getCoordinates()))
            {
                secondNearestWP = nearestWP;
                nearestWP = i;
            }
            else if (Vector3.Distance(transform.position, waypoints[i].transform.position) <=
                Vector3.Distance(transform.position, waypoints[secondNearestWP].getCoordinates()))
            {
                secondNearestWP = i;
            }
        }
        if (waypoints[nearestWP].getCoordinates() == lastWayPointUsed.getCoordinates())
            return waypoints[secondNearestWP];
        else
            return waypoints[nearestWP];
    }

    protected Waypoint NearestWaypoint(List<Waypoint> waypoint, Waypoint exception)
    {
        for (int i = 0; i < waypoint.Count; i++)
        {
            if ((Vector3.Distance(transform.position, waypoint[i].transform.position) >
                Vector3.Distance(transform.position, exception.getCoordinates()))
                && (Vector3.Distance(transform.position, waypoint[i].transform.position) //A jelenlegi tartózkodási hely az adott waypoint közti tartózkodási hely
                <= Vector3.Distance(transform.position, waypoint[nearestWP].getCoordinates())))
            {
                nearestWP = i;
            }
        }
        return waypoint[nearestWP];
    }




    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantScore(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);

    }

    public bool isChasing()
    {
        return chasing;
    }
    public void setChasing(bool chasing)
    {
        this.chasing = chasing;
    }

    //Vector
}
