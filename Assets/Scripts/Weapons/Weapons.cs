using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : Collidable {

    //Damage struct
    public int[] damagePoint = { 2, 3 };
    public float[] pushForce = { 2.0f, 2.2f };

    //Upgrade
    public int weaponLevel = 0;
    protected SpriteRenderer spriteRenderer;



    //Logic
    [SerializeField]
    protected bool active = true;
    [SerializeField]
    protected float cooldown_0 = 0.2f;
    [SerializeField]
    protected float cooldown_1 = 0.2f;
    protected float lastAttack_0;
    protected float lastAttack_1;
    [SerializeField]
    private bool automataAttack = false;

    //Ammo
    [SerializeField]
    protected int ammoAmount1;
    [SerializeField]
    protected int repertoryCurrent1;
    [SerializeField]
    protected int repertoryCapacity1;
    //Projectile
    //public List<Rigidbody2D> projectile;

    //protected int currentProjectileSelection = 0;
    [SerializeField]
    protected Transform ProjectileStartingTransform;
    protected Vector3 mousePosition;
    //Order
    /*[SerializeField]
    private int orderInHand;*/

    public int AmmoAmount1
    {
        get { return ammoAmount1; }
        set { ammoAmount1 = value; }
    }
    public int RepertoryCurrent1
    {
        get { return repertoryCurrent1; }
        set { repertoryCurrent1 = value; }
    }
    public int RepertoryCapacity1
    {
        get { return repertoryCapacity1; }
        set { repertoryCapacity1 = value; }
    }

    public bool Active
    {
        get { return active; }
        set { active = value; }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (active && !GameManager.instance.getPause() && GameManager.instance.player.isAlive)
        {
            base.Update();
            //Automata Attack
            if (automataAttack)
            {
                if (Input.GetMouseButton(0) && Time.time - lastAttack_0 > cooldown_0)
                {
                    lastAttack_0 = Time.time;
                    Attack_0();
                }
                else if (Input.GetMouseButton(1) && Time.time - lastAttack_1 > cooldown_1)
                {
                    lastAttack_1 = Time.time;
                    Attack_1();
                }
            }
            //Not automata Attack
            else
            {
                    base.Update();

                    if (Input.GetMouseButtonDown(0) && Time.time - lastAttack_0 > cooldown_0)
                    {
                        lastAttack_0 = Time.time;
                        Attack_0();
                    }
                    else if (Input.GetMouseButtonDown(1) && Time.time - lastAttack_1 > cooldown_1)
                    {
                        lastAttack_1 = Time.time;
                        Attack_1();
                    }
            }
        }
    }

    protected virtual void Attack()
    {
        Debug.Log("Player Attacking");
    }

   
    /*
    protected void CreateProjectile(Vector3 position, Rigidbody2D projectile, float velocity)
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D clone;
        clone = Instantiate(projectile, position, rotation) as Rigidbody2D;
        clone.velocity = transform.TransformDirection(Vector2.right * velocity);
        //clone.velocity = transform.TransformDirection(new Vector3(0, position.y, position.z));
        if (clone.tag != "Fighter")
            Destroy(clone.gameObject, 0.55f);
        //currentProjectileSelection++;
        /*if (currentProjectileSelection == projectile.Count)
            currentProjectileSelection = 0;

    }*/

    protected virtual void Attack_0()
    {
        Debug.Log("Attack0");
    }

    protected virtual void Attack_1()
    {
        Debug.Log("Attack1");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        // Change stats
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }
    
    
    
}
