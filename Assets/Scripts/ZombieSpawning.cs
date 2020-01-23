using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : Mover, Corpse {

    protected Transform target = null;
    protected Animator anim;

    private Spawner creator = null;
    //Corpse
    [SerializeField]
    private GameObject[] corpses = new GameObject[] { };
    protected override void Start()
    {
        base.Start();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    protected void FixedUpdate () {
        //Debug.Log(target.position);
        if (!GameManager.instance.getPause())
        {
            UpdateMotor(target.position - transform.position);
            RotateToTarget(target.position);
        }
        
	}

    public void makeCorpse()
    {
        Instantiate(corpses[int.Parse(Mathf.Floor(Random.Range(0, corpses.Length)).ToString())], transform.position, Quaternion.identity);
    }

    protected void RotateToTarget(Vector3 target)
    {
        Vector3 diff = (target - transform.position).normalized;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 270); //270
        anim.transform.rotation = rotation;
    }

    protected override void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //moveDelta += pushDirection;
        //pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Human"));
        if (hit.collider == null)
        {
            //Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Human"));
        if (hit.collider == null)
        {
            //Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }


    protected override void Death()
    {
        makeCorpse();
        Destroy(gameObject);
        int xpValue = transform.GetChild(0).GetComponent<SpawningEnemyHitbox>().getZombiePrefab().getXpValue();
        GameManager.instance.GrantScore(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        GameManager.instance.KilledZombiesCount++;
    }

    // Use it SpawningEnemyHitbox
    public Spawner getCreator()
    {
        return creator;
    }
    // Use it in Spawner
    public void setCreator(Spawner newCreator)
    {
        creator = newCreator;
    }
}
